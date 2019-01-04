using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniGolfMayhem.GameElements;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.Levels;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Arena
{
    public class EditArena : BaseArena, IMap
    {
        public TileSet TileSet => Game.TileSet;
        public List<TileData> Tiles = new List<TileData>();
        public Vector2 TileSelectionOffset => new Vector2(Game.Width - ((TileSet.TileWidth*0.5f)*8), 0f);
        public int[] TileSections => Layer == 1 ? WorldHelpers.BottomTiles : WorldHelpers.TopTiles;

        public int Layer;

        public TileData CurrentTile
        {
            get { return _currentTile; }
            set
            {
                PreviousTile = _currentTile ?? value;
                _currentTile = value;
            }
        }

        public TileData PreviousTile;
        private TileData _currentTile;

        public int StartLayer { get; set; }
        public Side StartSide { get; set; }
        public Vector2 Start { get; set; }

        public int MapWidth
        {
            get { return CurrentTile.Columns; }
            set { }
        }

        public int MapHeight
        {
            get { return CurrentTile.Rows; }
            set { }
        }

        public Vector2 End { get; set; }
        public Side EndSide { get; set; }
        public int EndLayer { get; set; }

        public int[] Layer1
        {
            get
            {
                var layer = new int[CurrentTile.Rows*CurrentTile.Columns];
                var i = 0;
                for (var y = 0; y < CurrentTile.Rows; y++)
                {
                    for (var x = 0; x < CurrentTile.Columns; x++)
                    {
                        var t = Tiles.FirstOrDefault(p => p.MapLocation.X == x && p.MapLocation.Y == y);
                        layer[i] = t?.BottomTileNumber ?? 0;
                        i++;
                    }
                }
                return layer;
            }
            set { }
        }

        public int[] Layer2
        {
            get
            {
                var layer = new int[CurrentTile.Rows*CurrentTile.Columns];
                var i = 0;
                for (var y = 0; y < CurrentTile.Rows; y++)
                {
                    for (var x = 0; x < CurrentTile.Columns; x++)
                    {
                        var t = Tiles.FirstOrDefault(p => p.MapLocation.X == x && p.MapLocation.Y == y);
                        layer[i] = t?.TopTileNumber ?? 0;
                        i++;
                    }
                }
                return layer;
            }
            set { }
        }

        public EditArena(Golf game, BaseArena previousArena, SMap map) : base(game, previousArena)
        {
            FromSMap(map);
            Initialise();
        }

        public EditArena(Golf game, BaseArena previousArena) : base(game, previousArena)
        {
            LevelGUID = Guid.NewGuid().ToString();
            Initialise();
        }

        public sealed override void Initialise()
        {            
            Game.UnifiedInput.TapListeners.Add(Tap);
            Layer = 1;
            if (_currentTile == null)
            {
                CurrentTile = new TileData(Point.Zero, 33, 0, Tiles);
                Tiles.Add(CurrentTile);
            }
            Camera = new Camera2D();

            base.Initialise();
        }

        public string LevelGUID { get; set; }

        private void Tap(Vector2 value)
        {
            if (ChangeTileSelection(value))
            {
                Game.Sounds.Menu.Play();
            }
            else if (ChangeLayerSelection(value))
            {
                Game.Sounds.Menu.Play();
            }
            else if (LayerDeleteSelector(value))
            {
                Game.Sounds.Menu.Play();
            }
            else if (StartSelector(value))
            {
                Game.Sounds.Menu.Play();
            }
            else if (EndSelector(value))
            {
                Game.Sounds.Menu.Play();
            }
            else if (PlaySelector(value))
            {
                Game.Sounds.Menu.Play();
            }
            else if (SaveSelector(value))
            {
                Game.Sounds.Menu.Play();
            }
            else if (SelectNextClosestTile(value))
            {
                Game.Sounds.Menu.Play();
            }
            else if (OnTapExit(value))
            {
                Game.Sounds.Menu.Play();
                Game.Arena = PreviousArena;
            }
        }



        public Camera2D Camera { get; set; }

        public override void Update(GameTime gameTime)
        {
            Game.SideBar.DesignWidth = 0;
            if (Game.KeyboardInput.TypedKey(Keys.Escape))
            {
                Game.Arena = Game.MenuArena;
            }

            Camera.Focus = Game.Center -
                           new Vector2(CurrentTile.MapLocation.X*TileSet.TileWidth,
                               CurrentTile.MapLocation.Y*TileSet.TileHeight);
            Camera.Update(gameTime);
            base.Update(gameTime);
        }

        public bool SelectNextClosestTile(Vector2 point)
        {
            foreach (var t in Tiles)
            {
                var p = new Vector2(t.MapLocation.X*TileSet.TileWidth, t.MapLocation.Y*TileSet.TileHeight) +
                        Camera.Position;
                var r = new Rectangle((int) p.X, (int) p.Y, TileSet.TileWidth, TileSet.TileHeight);
                if (r.Contains(point))
                {
                    CurrentTile = t;
                    return true;
                }
            }

            foreach (var t in Tiles)
            {
                var os = t.OpenSides();
                foreach (var s in os[Layer == 1 ? 0 : 1])
                {
                    var p = new Vector2(t.MapLocation.X*TileSet.TileWidth, t.MapLocation.Y*TileSet.TileHeight) +
                            Camera.Position;
                    switch (s)
                    {
                        case Side.Top:
                            p += new Vector2(0, -TileSet.TileHeight);
                            break;
                        case Side.Bottom:
                            p += new Vector2(0, TileSet.TileHeight);
                            break;
                        case Side.Left:
                            p += new Vector2(-TileSet.TileWidth, 0);
                            break;
                        case Side.Right:
                            p += new Vector2(TileSet.TileWidth, 0);
                            break;
                    }
                    var r = new Rectangle((int) p.X, (int) p.Y, TileSet.TileWidth, TileSet.TileHeight);
                    if (r.Contains(point))
                    {
                        switch (s)
                        {
                            case Side.Top:
                                CurrentTile.TileAbove =
                                    new TileData(new Point(CurrentTile.Location.X, CurrentTile.Location.Y - 1),
                                        Layer == 1 ? CurrentTile.BottomTileNumber : 0,
                                        Layer == 1 ? 0 : CurrentTile.TopTileNumber, Tiles);
                                CurrentTile = CurrentTile.TileAbove;
                                break;
                            case Side.Bottom:
                                CurrentTile.TileBelow =
                                    new TileData(new Point(CurrentTile.Location.X, CurrentTile.Location.Y + 1),
                                        Layer == 1 ? CurrentTile.BottomTileNumber : 0,
                                        Layer == 1 ? 0 : CurrentTile.TopTileNumber, Tiles);
                                CurrentTile = CurrentTile.TileBelow;
                                break;
                            case Side.Left:
                                CurrentTile.TileToLeft =
                                    new TileData(new Point(CurrentTile.Location.X - 1, CurrentTile.Location.Y),
                                        Layer == 1 ? CurrentTile.BottomTileNumber : 0,
                                        Layer == 1 ? 0 : CurrentTile.TopTileNumber, Tiles);
                                CurrentTile = CurrentTile.TileToLeft;
                                break;
                            case Side.Right:
                                CurrentTile.TileToRight =
                                    new TileData(new Point(CurrentTile.Location.X + 1, CurrentTile.Location.Y),
                                        Layer == 1 ? CurrentTile.BottomTileNumber : 0,
                                        Layer == 1 ? 0 : CurrentTile.TopTileNumber, Tiles);
                                CurrentTile = CurrentTile.TileToRight;
                                break;
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public override void Draw(SpriteBatch batch)
        {
            var posistion = Camera.Position;
            batch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone);
            {
                batch.Draw(Game.SideBar);
                foreach (var t in Tiles)
                {
                    var c = t == CurrentTile ? Color.White : Color.Gray;
                    var offset = new Vector2(t.MapLocation.X*TileSet.TileWidth, t.MapLocation.Y*TileSet.TileHeight) +
                                 Camera.Position;
                    TileSet.DrawTile(batch, t.BottomTileNumber, offset, posistion, null, 0.4f, 1f, c);
                    TileSet.DrawTileBridges(batch, t.BottomTileNumber, offset, posistion, null, 0.6f, 1f, c);
                    if (StartLayer == 1 && t.MapLocation.X == (int) Start.X && t.MapLocation.Y == (int) Start.Y)
                        TileSet.DrawTile(batch, WorldHelpers.GetStartTile(StartSide), offset, posistion, null, 0.45f);
                    if (EndLayer == 1 && t.MapLocation.X == (int) End.X && t.MapLocation.Y == (int) End.Y)
                    {
                        TileSet.DrawTile(batch, WorldHelpers.GetEndTile(EndSide), offset, posistion, null, 0.45f);
                    }
                    TileSet.DrawTile(batch, t.TopTileNumber, offset, posistion, null, 0.7f, 1, c*0.5f);
                    TileSet.DrawTileBridges(batch, t.TopTileNumber, offset, posistion, null, 0.8f, 1, c*0.5f);
                    var os = t.OpenSides();
                    foreach (var s in os[0])
                    {
                        c = Layer == 1 ? Color.Red*0.5f : Color.Red*0.25f;
                        switch (s)
                        {
                            case Side.Top:
                                batch.Draw(Golf.Pixel,
                                    new Rectangle((int) offset.X, (int) offset.Y - TileSet.TileHeight, TileSet.TileWidth,
                                        TileSet.TileHeight), null,c,0,Vector2.Zero, SpriteEffects.None, 1f);
                                break;
                            case Side.Bottom:
                                batch.Draw(Golf.Pixel,
                                    new Rectangle((int) offset.X, (int) offset.Y + TileSet.TileHeight, TileSet.TileWidth,
                                        TileSet.TileHeight), null, c, 0, Vector2.Zero, SpriteEffects.None, 1f);
                                break;
                            case Side.Left:
                                batch.Draw(Golf.Pixel,
                                    new Rectangle((int) offset.X - TileSet.TileWidth, (int) offset.Y, TileSet.TileWidth,
                                        TileSet.TileHeight), null, c, 0, Vector2.Zero, SpriteEffects.None, 1f);
                                break;
                            case Side.Right:
                                batch.Draw(Golf.Pixel,
                                    new Rectangle((int) offset.X + TileSet.TileWidth, (int) offset.Y, TileSet.TileWidth,
                                        TileSet.TileHeight), null, c, 0, Vector2.Zero, SpriteEffects.None, 1f);
                                break;
                        }
                    }
                    foreach (var s in os[1])
                    {
                        c = Layer == 2 ? Color.Blue*0.5f : Color.Blue*0.25f;
                        switch (s)
                        {
                            case Side.Top:
                                batch.Draw(Golf.Pixel,
                                    new Rectangle((int) offset.X, (int) offset.Y - TileSet.TileHeight, TileSet.TileWidth,
                                        TileSet.TileHeight), null, c, 0, Vector2.Zero, SpriteEffects.None, 1f);
                                break;
                            case Side.Bottom:
                                batch.Draw(Golf.Pixel,
                                    new Rectangle((int) offset.X, (int) offset.Y + TileSet.TileHeight, TileSet.TileWidth,
                                        TileSet.TileHeight), null, c, 0, Vector2.Zero, SpriteEffects.None, 1f);
                                break;
                            case Side.Left:
                                batch.Draw(Golf.Pixel,
                                    new Rectangle((int) offset.X - TileSet.TileWidth, (int) offset.Y, TileSet.TileWidth,
                                        TileSet.TileHeight), null, c, 0, Vector2.Zero, SpriteEffects.None, 1f);
                                break;
                            case Side.Right:
                                batch.Draw(Golf.Pixel,
                                    new Rectangle((int) offset.X + TileSet.TileWidth, (int) offset.Y, TileSet.TileWidth,
                                        TileSet.TileHeight), null, c, 0, Vector2.Zero, SpriteEffects.None, 1f);
                                break;
                        }
                    }
                }
            }
            batch.End();
            batch.Begin();
            {
                var str = Fonts.GameFont.MeasureString(Strings.Exit);
                batch.DrawString(Fonts.GameFont, Strings.Exit,
                    new Vector2(Game.Width - (str.X + 10)+2, Game.Height - (str.Y + 10)+3),
                    Color.Black * 0.25f);
                batch.DrawString(Fonts.GameFont, Strings.Exit,
                    new Vector2(Game.Width - (str.X + 10), Game.Height - (str.Y + 10)),
                    Color.White);

                DrawTileSelector(batch, Layer == 1 ? CurrentTile.BottomTileNumber : CurrentTile.TopTileNumber);
                DrawLayerSelector(batch);
                DrawLayerDeleteSelector(batch);
                DrawStartSelector(batch);
                DrawEndSelector(batch);
                DrawPlaySelector(batch);
                DrawSaveSelector(batch);
                batch.End();
            }
            base.Draw(batch);
        }

        private void DrawLayerSelector(SpriteBatch batch) => batch.Draw(Layer == 1 ? Textures.BottomLayer : Textures.TopLayer, Vector2.Zero, Color.White);

        private bool LayerDeleteSelector(Vector2 value)
        {
            if (
                new Rectangle(0, Textures.TopLayer.Height, Textures.TopLayerDelete.Width, Textures.TopLayerDelete.Height)
                    .Contains(value))
            {
                if (Tiles.Count <= 1)
                {
                    if (CurrentTile.BottomTileNumber == 0 || CurrentTile.TopTileNumber == 0) return false;
                    if (Layer == 1)
                    {
                        if (Start == new Vector2(CurrentTile.MapLocation.X, CurrentTile.MapLocation.Y)) StartLayer = 0;
                        if (End == new Vector2(CurrentTile.MapLocation.X, CurrentTile.MapLocation.Y)) EndLayer = 0;
                        CurrentTile.BottomTileNumber = 0;
                    }
                    else CurrentTile.TopTileNumber = 0;
                    return true;
                }
                if (Layer == 1)
                {
                    if (Start == new Vector2(CurrentTile.MapLocation.X, CurrentTile.MapLocation.Y)) StartLayer = 0;
                    if (End == new Vector2(CurrentTile.MapLocation.X, CurrentTile.MapLocation.Y)) EndLayer = 0;
                    CurrentTile.BottomTileNumber = 0;
                }
                else CurrentTile.TopTileNumber = 0;
                if (CurrentTile.BottomTileNumber == 0 && CurrentTile.TopTileNumber == 0)
                {

                    Tiles.Remove(CurrentTile);
                    CurrentTile = Tiles.Last();
                    return true;
                }
            }
            return false;
        }

        private void DrawLayerDeleteSelector(SpriteBatch batch)
        {
            var show = false;
            if (Tiles.Count <= 1)
            {
                if (!(CurrentTile.BottomTileNumber == 0 || CurrentTile.TopTileNumber == 0))
                    if (Layer == 1 && CurrentTile.BottomTileNumber != 0) show = true;
                    else if (Layer == 2 && CurrentTile.TopTileNumber != 0) show = true;
            }
            else
            {
                if (Layer == 1 && CurrentTile.BottomTileNumber != 0) show = true;
                else if (Layer == 2 && CurrentTile.TopTileNumber != 0) show = true;
            }
            batch.Draw(Layer == 1 ? Textures.BottomLayerDelete : Textures.TopLayerDelete,
                new Vector2(0, Textures.TopLayer.Height), !show ? Color.Gray : Color.White);
        }

        private void DrawStartSelector(SpriteBatch batch)
        {
            var show = Layer == 1 && End != new Vector2(CurrentTile.MapLocation.X, CurrentTile.MapLocation.Y);
            batch.Draw(
                StartSide == Side.Bottom
                    ? Textures.StartBottom
                    : StartSide == Side.Left
                        ? Textures.StartLeft
                        : StartSide == Side.Top ? Textures.StartTop : Textures.StartRight,
                new Vector2(0, Textures.TopLayer.Height*2), !show ? Color.Gray : Color.White);
        }

        private bool OnTapExit(Vector2 point)
        {
            var str = Fonts.GameFont.MeasureString(Strings.Exit);
            return new Rectangle((int)(Game.Width - (str.X+10)), (int)(Game.Height - (str.Y+10)), (int)str.X, (int)str.Y).Contains(point);
        }
        private void DrawEndSelector(SpriteBatch batch)
        {
            var show = Layer == 1 && Start != new Vector2(CurrentTile.MapLocation.X, CurrentTile.MapLocation.Y);
            batch.Draw(
                EndSide == Side.Bottom
                    ? Textures.EndBottom
                    : EndSide == Side.Left
                        ? Textures.EndLeft
                        : EndSide == Side.Top ? Textures.EndTop : Textures.EndRight,
                new Vector2(0, Textures.TopLayer.Height*3), !show ? Color.Gray : Color.White);
        }

        private bool StartSelector(Vector2 value)
        {
            if (Layer == 1 && End != new Vector2(CurrentTile.MapLocation.X, CurrentTile.MapLocation.Y) &&
                new Rectangle(0, Textures.TopLayer.Height*2, Textures.StartBottom.Width, Textures.StartBottom.Height)
                    .Contains(value))
            {
                if (StartSide == Side.Bottom) StartSide = Side.Left;
                else if (StartSide == Side.Left) StartSide = Side.Top;
                else if (StartSide == Side.Top) StartSide = Side.Right;
                else if (StartSide == Side.Right) StartSide = Side.Bottom;
                StartLayer = 1;
                Start = new Vector2(CurrentTile.MapLocation.X, CurrentTile.MapLocation.Y);
                return true;
            }
            return false;
        }

        private bool EndSelector(Vector2 value)
        {
            if (Layer == 1 && Start != new Vector2(CurrentTile.MapLocation.X, CurrentTile.MapLocation.Y) &&
                new Rectangle(0, Textures.TopLayer.Height*3, Textures.StartBottom.Width, Textures.StartBottom.Height)
                    .Contains(value))
            {
                if (EndSide == Side.Bottom) EndSide = Side.Left;
                else if (EndSide == Side.Left) EndSide = Side.Top;
                else if (EndSide == Side.Top) EndSide = Side.Right;
                else if (EndSide == Side.Right) EndSide = Side.Bottom;
                EndLayer = 1;
                End = new Vector2(CurrentTile.MapLocation.X, CurrentTile.MapLocation.Y);
                return true;
            }
            return false;
        }

        private bool PlaySelector(Vector2 value)
        {
            if (IsValidMap() &&
                new Rectangle(0, Textures.TopLayer.Height*4, Textures.StartBottom.Width, Textures.StartBottom.Height)
                    .Contains(value))
            {
                Game.Arena = new PlayArena(Game, this, new List<IMap> {this}, null);
                return true;
            }
            return false;
        }

        private bool SaveSelector(Vector2 value)
        {
            if (IsValidMap() &&
                new Rectangle(0, Textures.TopLayer.Height * 5, Textures.StartBottom.Width, Textures.StartBottom.Height)
                    .Contains(value))
            {
                return Game.CustomGameStorage.AddMap(ToSMap());
            }
            return false;
        }

        private void DrawPlaySelector(SpriteBatch batch)
        {
            var show = IsValidMap();
            batch.Draw(Textures.Play, new Vector2(0, Textures.TopLayer.Height*4), !show ? Color.Gray : Color.White);
        }

        private void DrawSaveSelector(SpriteBatch batch)
        {
            var show = IsValidMap();
            batch.Draw(Textures.Save, new Vector2(0, Textures.TopLayer.Height * 5), !show ? Color.Gray : Color.White);
        }

        private bool IsValidMap()
        {
            if (StartLayer != 1) return false;
            if (EndLayer != 1) return false;
            if (Tiles.All(t => new Vector2(t.MapLocation.X, t.MapLocation.Y) != Start)) return false;
            if (Tiles.All(t => new Vector2(t.MapLocation.X, t.MapLocation.Y) != End)) return false;
            foreach (var t in Tiles)
            {
                var os = t.OpenSides();
                if (os[0].Any()) return false;
                if (os[1].Any()) return false;
            }
            return true;
        }

        private bool ChangeLayerSelection(Vector2 point)
        {
            if (!new Rectangle(0, 0, Textures.TopLayer.Width, Textures.TopLayer.Height).Contains(point)) return false;
            Layer = Layer == 1 ? 2 : 1;
            return true;
        }

        public void DrawTileSelector(SpriteBatch batch, int tile)
        {
            var location = TileSelectionOffset;           
            foreach (var t in TileSections)
            {
             //   var x = (Layer==1?CurrentTile.BottomTiles.Contains(t): CurrentTile.TopTiles.Contains(t)) ? Color.White : Color.DarkBlue;
                Game.TileSet.DrawTile(batch, t, location, Vector2.Zero, null, .9f, .5f,
                    Color.White*(t == tile ? 1f : .5f));
                Game.TileSet.DrawTileBridges(batch, t, location, Vector2.Zero, null, .8f, .5f,
                    Color.White * (t == tile ? 1f : .5f));
                if (t == tile)
                {
                    Game.TileSet.DrawTile(batch, 118, location, Vector2.Zero, null, 1f, .5f);
                }
                location = new Vector2(location.X + (TileSet.TileWidth*0.5f), location.Y);
                if (location.X + (TileSet.TileWidth*0.5f) > Game.Width)
                {
                    location = new Vector2(TileSelectionOffset.X, location.Y + (TileSet.TileHeight*0.5f));
                }
            }
        }

        public bool ChangeTileSelection(Vector2 point)
        {
            var location = TileSelectionOffset;
            foreach (var t in TileSections)
            {
                if (
                    new Rectangle((int) location.X, (int) location.Y, (int) (TileSet.TileWidth*0.5f),
                        (int) (TileSet.TileHeight*0.5f)).Contains(
                            point))
                {
                    if (Layer == 1)
                    {
                        CurrentTile.BottomTileNumber = t;
                        return true;
                    }
                    else
                    {
                        CurrentTile.TopTileNumber = t;
                        return true;
                    }
                }
                location = new Vector2(location.X + (TileSet.TileWidth*0.5f), location.Y);
                if (location.X + (TileSet.TileWidth*0.5f) > Game.Width)
                {
                    location = new Vector2(TileSelectionOffset.X, location.Y + (TileSet.TileHeight*0.5f));
                }
            }
            return false;
        }

        public void FromSMap(SMap map)
        {
            End = map.End;
            EndLayer = map.EndLayer;
            EndSide = map.EndSide;
            LevelGUID = map.LevelGUID;                
          //  MapHeight = map.MapHeight;
         //   MapWidth = map.MapWidth;
            Start = map.Start;
            StartLayer = map.StartLayer;
            StartSide = map.StartSide;

            var i = 0;
            for (int r = 0; r < map.MapHeight; r++)
            {
                for (var c = 0; c < map.MapWidth; c++)
                {
                    if (map.Layer1[i] != 0 || map.Layer1[i] != 0)
                    {
                        var t = new TileData(new Point(c, r), map.Layer1[i], map.Layer2[i], Tiles);
                        Tiles.Add(t);
                        if ((int)Start.X == c && (int)Start.Y == r) CurrentTile = t;
                    }
                    i++;
                }
            }

        }

        public SMap ToSMap()
        {
            if (!IsValidMap()) return null;
            return new SMap
            {
                End = End,
                EndLayer = EndLayer,
                EndSide = EndSide,
                LevelGUID = LevelGUID,
                Layer1 = Layer1,
                Layer2 = Layer2,
                MapHeight = MapHeight,
                MapWidth = MapWidth,
                Start = Start,
                StartLayer = StartLayer,
                StartSide = StartSide
            };
        }
    }
}