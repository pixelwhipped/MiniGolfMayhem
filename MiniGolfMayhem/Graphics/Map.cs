using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniGolfMayhem.GameElements;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Graphics
{
    public class Map
    {
        public Procedure<Map> EndLevel { get; set; }
        public readonly MapLayer Layer1;
        public readonly MapLayer Layer2;
        public int Width => Columns * TileSet.TileWidth;
        public int Height => Rows * TileSet.TileHeight;
        public Vector2 StartWorldCenter { get; set; }
        public Vector2 Start;
        public Side StartSide;
        public int StartLayer;

        public Vector2 EndWorldCenter { get; set; }
        public Vector2 End;
        public Side EndSide;
        public int EndLayer;

        public readonly List<Player> Players;
        public int CurrentPlayer;

        public readonly int Columns;
        public readonly int Rows;

        public TileSet TileSet => Game.TileSet;

        public Golf Game;
        public Camera2D Camera;

        public Tween ColorTween;
        public Map(Golf game, TileSet tileSet, MapLayer layer1, MapLayer layer2, int columns, int rows, Vector2 start, Side startStartSide, int startLayer, Vector2 end, Side endEndSide, int endLayer, List<Player> players, int currentPlayer, Procedure<Map> endLevel )
        {
            Game = game;
            EndLevel = endLevel;
            ColorTween = new Tween(new TimeSpan(0,0,0,1),0.0f,0.8f,true );
            //TileSet = tileSet;
            Layer1 = layer1;
            Layer2 = layer2;
            Columns = columns;
            Start = start;
            StartSide = startStartSide;
            StartLayer = startLayer;
            StartWorldCenter =
                new Vector2(Math.Max(Start.X, 0)*TileSet.TileWidth,
                    Math.Max(Start.Y, 0)*TileSet.TileHeight) + WorldHelpers.GetStartOffset(StartSide);
            End = end;
            EndSide = endEndSide;
            EndLayer = endLayer;
            EndWorldCenter =
                new Vector2(Math.Max(End.X, 0) * TileSet.TileWidth,
                    Math.Max(End.Y, 0) * TileSet.TileHeight) + WorldHelpers.GetEndOffset(EndSide);

            Players = players;
            CurrentPlayer = currentPlayer;            
            Rows = rows;
            
            Camera = new Camera2D();
            if (!Players.Any()) return;
            foreach (var p in Players)
            {
                p.Node = new Node(new Vector2(Math.Max(Start.X, 0) * tileSet.TileWidth, Math.Max(Start.Y, 0) * tileSet.TileHeight) + WorldHelpers.GetStartOffset(StartSide), 2.5f, 1, Vector2.Zero, Vector2.Zero, startLayer);
                p.LastTile = GetPlayerTile(p);
                p.PreviousTile = p.LastTile;
                p.Layer = startLayer;
                p.Node.OnCollision += PlayBounce;
                p.State = PlayerState.Finished;
                p.Par = -1;
            }
            Camera.Focus = Game.Center - Players[CurrentPlayer].Position;
            Players[CurrentPlayer].Start();
        }

        private static DateTime _lastTimeBounce = DateTime.Now;
        private void PlayBounce(object a, object b, Vector2 c)
        {
            if((DateTime.Now-_lastTimeBounce).Ticks > new TimeSpan(0,0,0,0,500).Ticks)
                Game.Sounds.Bounce.Play();
            _lastTimeBounce = DateTime.Now;
        }

        private Vector2 _overScroll = Vector2.Zero;
        public void Update(GameTime gameTime)
        {
            
            Camera.Update(gameTime);
            ColorTween.Update(gameTime.ElapsedGameTime);

                if (!Players.Any()) return;
                if (Players.All(p => p.State == PlayerState.Done))
                {
                    EndLevel(this);
                    return;
                }
                if (Players.All(p => p.State == PlayerState.Finished || p.State == PlayerState.Done))
                {
                    CurrentPlayer++;
                    if (CurrentPlayer >= Players.Count) CurrentPlayer = 0;
                    while (Players[CurrentPlayer].State == PlayerState.Done)
                    {
                        CurrentPlayer++;
                        if (CurrentPlayer >= Players.Count) CurrentPlayer = 0;
                    }

                    Players[CurrentPlayer].Start();
                }
                foreach (var player in Players.Where(p => p.State != PlayerState.Finished))
                {
                    player.Update(gameTime, this);
                }
           
            if (new Rectangle(50, 0, (int) Game.Width-100, 50).Contains(Game.UnifiedInput.Location))
            {
                _overScroll += new Vector2(0, 1);
            }
            else if (new Rectangle(0, 0, 50, 50).Contains(Game.UnifiedInput.Location))
            {
                _overScroll += new Vector2(1, 1);
            }
            else if (new Rectangle(50, (int)Game.Height-50, (int)Game.Width - 100, 50).Contains(Game.UnifiedInput.Location))
            {
                _overScroll += new Vector2(0, -1);
            }
            else if (new Rectangle((int)Game.Width - 50, 0, 50, 50).Contains(Game.UnifiedInput.Location))
            {
                _overScroll += new Vector2(-1, 1);
            }
            else if (new Rectangle(0, 50, 50, (int)Game.Height-100).Contains(Game.UnifiedInput.Location))
            {
                _overScroll += new Vector2(1, 0);
            }
            else if (new Rectangle((int)Game.Width - 50, (int)Game.Height - 50, 50, 50).Contains(Game.UnifiedInput.Location))
            {
                _overScroll += new Vector2(-1, -1);
            }
            else if (new Rectangle(0, (int)Game.Height - 50, 50, 50).Contains(Game.UnifiedInput.Location))
            {
                _overScroll += new Vector2(1, -1);
            }
            else if (new Rectangle((int)Game.Width-50, 50, 50, (int)Game.Height - 100).Contains(Game.UnifiedInput.Location))
            {
                _overScroll += new Vector2(-1, 0);
            }

            else
            {
                _overScroll = Vector2.Zero;                
            }
            Camera.Focus = (Game.Center - Players[CurrentPlayer].Position) + _overScroll;
            /* */
        }

        public void DrawMap(SpriteBatch batch, Vector2 posistion)
        {            
            posistion += Camera.Position;
            var yOffset = posistion.Y;
            for (var y = 0; y < Rows; y++)
            {
                var xOffset = posistion.X;
                for (var x = 0; x < Columns; x++)
                {
                    var offset = new Vector2(xOffset, yOffset);
                    TileSet.DrawTile(batch, Layer1.GetTile(x, y), offset, posistion,null, 0.4f);
                    if (x == (int) Start.X && y == (int) Start.Y)
                        TileSet.DrawTile(batch, WorldHelpers.GetStartTile(StartSide), offset, posistion,null, 0.45f);
                    if (x == (int) End.X && y == (int) End.Y)
                    {
                        TileSet.DrawTile(batch, WorldHelpers.GetEndTile(EndSide), offset, posistion, null, 0.45f);                       
                    }
                    foreach (var p in Players)
                        p.Draw(batch, posistion);

                   TileSet.DrawTileBridges(batch, Layer1.GetTile(x, y), offset, posistion, null, 0.6f);
                    

                    TileSet.DrawTile(batch, Layer2.GetTile(x, y), offset, posistion, Players,0.7f);
                    TileSet.DrawTileBridges(batch, Layer2.GetTile(x, y), offset, posistion, null,0.9f);
                    xOffset += TileSet.TileWidth;
                    if (xOffset > Game.Width) break;
                }
                yOffset += TileSet.TileHeight;
                if (yOffset > Game.Height) break;
            }                               
        }

        public void DrawMapPlayers(SpriteBatch batch)
        {
            if (!Players.Any()) return;
            var textOffset = new Vector2(10, 10);
            foreach (var p in Players)
            {
                var c = p == Players[CurrentPlayer] ? Color.Lerp(p.Color, Color.White, ColorTween) : p.Color;
                batch.DrawString(Fonts.GameFontGrey, $"{p.Name}", textOffset + new Vector2(2,3), Color.Black * 0.25f);
                batch.DrawString(Fonts.GameFontGrey, $"{p.Name}", textOffset, c);
                textOffset += new Vector2(0, Fonts.GameFontGrey.MeasureString(p.Name).Y + 5);//batch.DrawString(Fonts.GameFontGrey, $"{p.Name}", textOffset + new Vector2(2,3), Color.Black * 0.25f);
                batch.DrawString(Fonts.GameFontGrey, $"Par {p.Par + 1}", textOffset + new Vector2(2, 3), Color.Black * 0.25f);
                batch.DrawString(Fonts.GameFontGrey, $"Par {p.Par + 1}", textOffset, c);

                textOffset += new Vector2(0, Fonts.GameFontGrey.MeasureString($"Par {p.Par + 1}").Y + 5);
                batch.DrawString(Fonts.GameFontGrey, $"Total {p.Total}", textOffset + new Vector2(2, 3), Color.Black * 0.25f);
                batch.DrawString(Fonts.GameFontGrey, $"Total {p.Total}", textOffset, c);
                textOffset += new Vector2(0, Fonts.GameFontGrey.MeasureString($"Total {c}").Y + 20);
            }
        }

        public Point GetPlayerTile(Player p) => new Point((int) Math.Floor(p.Position.X/TileSet.TileWidth), (int) Math.Floor(p.Position.Y/TileSet.TileHeight));
        public int GetTile(Player p, int layer)
        {
            var px = GetPlayerTile(p);
            return layer == 1 ? Layer1.GetTile(px.X, px.Y) : Layer2.GetTile(px.X, px.Y);
        }

        public List<Node> GetBouncers(Player p)
        {
            var px = GetPlayerTile(p);
            var b =  p.Layer == 1 ? Layer1.GetBouncers(px.X, px.Y) : Layer2.GetBouncers(px.X, px.Y);            
            return b;
        }

        public List<Rail> GetWalls(Player p)
        {
            var px = GetPlayerTile(p);
            return p.Layer == 1 ? Layer1.GetWalls(px.X, px.Y) : Layer2.GetWalls(px.X, px.Y);
        }

        public List<Rectangle> GetWater(Player p)
        {
            var px = GetPlayerTile(p);
            return p.Layer == 1 ? Layer1.GetWater(px.X, px.Y) : Layer2.GetWater(px.X, px.Y);
        }
        public Elevation GetElevation(Player p) => GetElevation(GetPlayerTile(p));

        public Elevation GetElevation(Point px) => Layer1.GetElevation(px.X, px.Y);

        public List<KeyValuePair<Rectangle, List<Rectangle>>> GetTeleports(Player p)
        {
            var px = GetPlayerTile(p);
            return p.Layer == 1 ? Layer1.GetTeleports(px.X, px.Y) : Layer2.GetTeleports(px.X, px.Y);
        }

        #region  MenuLayers

        public static MapLayer MenuLayer1 = new MapLayer(new[]
        {
            0, 37, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 0, 34, 0, 12, 0, 0, 12, 0, 4, 57, 50, 58, 0, 14, 15, 16, 0, 1, 20, 38, 0, 0, 0, 12, 0, 20, 68, 20, 20, 56, 29, 30, 0, 17, 65, 18, 0, 4, 0, 12, 0, 0, 0, 19, 20, 0, 77, 0, 13, 28, 29, 55, 60, 61, 46, 73, 20, 20, 20, 38, 0, 0, 0, 1, 3, 3, 19, 47, 21, 66, 65, 67, 0, 0, 0, 0, 0, 13, 0, 12, 0, 51, 0, 10, 19, 10, 0, 48, 0, 0, 74, 0, 0, 0, 0, 0, 0, 37, 2, 59, 39, 59, 20, 21, 0, 19, 26, 46, 47, 42, 12, 0, 0, 0, 0, 1, 20, 21, 0, 27, 0, 52, 0, 0, 0, 0, 0, 1, 21, 0, 19, 47, 20, 20, 20, 21, 0, 0, 0, 37, 20, 20, 20, 3, 0, 1, 22, 12, 0, 23, 75, 38, 0, 0, 0, 0, 0, 1, 2, 21, 0, 0, 0, 4, 0, 21, 0, 19, 76, 3, 0, 12, 1, 3, 0, 0, 0, 12, 0, 0, 1, 26, 20, 20, 75, 0, 0, 0, 0, 12, 0, 37, 21, 4, 1, 3, 0, 10, 0, 0, 19, 3, 0, 13, 0, 20, 20, 73, 20, 38, 0, 70, 0, 13, 4, 74, 1, 59, 20, 3, 0, 12, 0, 19, 20, 1, 20, 20, 3, 12, 0, 69, 0, 4, 13, 27, 12, 12, 57, 64, 50, 64, 58, 0, 0, 12, 1, 3, 37, 21, 0, 12, 0, 13, 12, 19, 21, 12, 66, 65, 49, 65, 67, 0, 0, 12, 12, 4, 12, 1, 20, 46, 3, 19, 21, 0, 0, 37, 20, 21, 0, 19, 2, 3, 0, 12, 19, 20, 21, 4, 0, 0, 4, 0, 0, 0, 1, 21, 0, 0, 0, 0, 0, 12, 0, 19, 20, 20, 20, 20, 20, 20, 20, 20, 26, 22, 10, 23, 26, 3, 0, 0, 0, 4, 0, 3, 0, 13, 0, 13, 0, 0, 13, 0, 1, 2, 21, 0, 25, 59, 24, 1, 20, 20, 20, 19, 47, 21, 0, 19, 76, 75, 21, 8, 64, 9, 0, 0, 0, 48, 0, 12, 0, 13, 0, 0, 12, 0, 0, 0, 0, 0, 0, 5, 6, 7, 0, 0, 0, 48, 0, 12, 0, 12, 0
        }, 20, 20, 1);

        public static MapLayer MenuLayer2 = new MapLayer(new[]
        {
            0, 0, 0, 0, 0, 0, 0, 0, 83, 84, 85, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 82, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 82, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 91, 91, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 82, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 82, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 82, 0, 82, 0, 0, 82, 0, 0, 0, 91, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 82, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        }, 20, 20, 2);

        #endregion
    }
}