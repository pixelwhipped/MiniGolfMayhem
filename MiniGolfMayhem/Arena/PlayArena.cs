using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Media.Playback;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.Levels;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Arena
{
    public class PlayArena : BaseArena
    {
        public List<IMap> Maps;
        public int CurrentMapIndex;
        public Map CurrentMap;
        public Map PreviousMap;
        public List<Player> Players;
        

        public PlayArena(Golf game, BaseArena previousArena, List<IMap> maps, List<Player> players) : base(game, previousArena)
        {
            Game.KeyboardInput.IsOskVisable = false;
            Maps = maps ?? new List<IMap> { new Map01(), new Map02(), new Map03(), new Map04(), new Map05(), new Map06(), new Map07(), new Map08(), new Map09(), new Map10() };
            Players = players??new List <Player> { new Player(Game, 0, Strings.Player1) };//, new Player(Game, Color.Red, "Jansen"), new Player(Game, Color.DarkGreen, "Aurora"), new Player(Game, Color.Purple, "Chloe") };
            Game.UnifiedInput.TapListeners.Add(OnTap);
            Initialise();
        }

        public PlayArena(Golf game, BaseArena previousArena, SaveState lastState) : base(game, previousArena)
        {
            Game.KeyboardInput.IsOskVisable = false;
            Maps = new List<IMap>(lastState.Maps);
            Players = new List<Player>();
            
            foreach (var p in lastState.Players)
            {
             Players.Add(new Player(game,p));   
            }
            Initialise();
            Game.UnifiedInput.TapListeners.Add(OnTap);
            
        //    foreach (var p in Players)
        //    {
        //        p.State = Graphics.PlayerState.Finished;
        //    }
            //Players[lastState.CurrentPlayer].State = Graphics.PlayerState.Ready;
            CurrentMap.CurrentPlayer = lastState.CurrentPlayer;
            for (int index = 0; index < CurrentMap.Players.Count; index++)
            {
                var p = CurrentMap.Players[index];
                p.Par = lastState.Players[index].Par;
                p.Total = lastState.Players[index].Total;
                p.Layer = lastState.Players[index].Layer;
                p.CurrentTile = lastState.Players[index].CurrentTile;
                p.State = lastState.Players[index].State;
                p.LastTile = lastState.Players[index].LastTile;
                p.Node.WorldPosition = lastState.Players[index].Position;
            }
            Players[lastState.CurrentPlayer].State = Graphics.PlayerState.Ready;
        }

        public sealed override void Initialise()
        {
            
            CurrentMapIndex = 0;
            CurrentMap = GenerateMap();
            PreviousMap = CurrentMap;
            SaveState();
            base.Initialise();
        }

        public override void OnTap(Vector2 a)
        {
            if (Game.Transitioning) return;
            var exitStr = Fonts.GameFont.MeasureString(Strings.Exit);
            var skipStr = Fonts.GameFont.MeasureString(Strings.Skip);
            if (Players.All(p => p.Par > 6 || p.State == Graphics.PlayerState.Done) &&
               new Rectangle((int)(Game.Width - (skipStr.X+10)), (int)(Game.Height - (exitStr.Y + 20 + skipStr.Y)), (int)skipStr.X,(int)skipStr.Y).Contains(a))
            {
                Game.Sounds.Menu.Play();
                NextMap(CurrentMap);
            }
            if (
                new Rectangle((int) (Game.Width - (exitStr.X + 10)), (int) (Game.Height - (exitStr.Y + 10)),
                    (int)exitStr.X, (int)exitStr.Y).Contains(a))
            {
                SaveState();
                PreviousMap = CurrentMap;
                Game.Sounds.Menu.Play();
                Game.Arena = PreviousArena;
            }            

        }

        public void SaveState()
        {
            List<SMap> maps = new List<SMap>();
            for (var i = CurrentMapIndex; i < Maps.Count;i++)
            {
                maps.Add(ToSMap(Maps[i]));
            }
            Game.CustomGameStorage.SaveState(new SaveState {Players = Players.Select(p=>new PlayerSave
            {
                ColorIndex =  p.ColorIndex,Name=p.Name,Par=p.Par,Total=p.Total,
                LastTile = p.LastTile, Layer = p.Layer,State=p.State,CurrentTile = p.CurrentTile,Position = p.Position
            }).ToArray(),
                Maps = maps.ToArray(),
                CurrentPlayer = CurrentMap.CurrentPlayer});
        }

        public SMap ToSMap(IMap map)
        {
            return new SMap
            {
                End = map.End,
                EndLayer = map.EndLayer,
                EndSide = map.EndSide,
                LevelGUID = new Guid().ToString(),
                Layer1 = map.Layer1,
                Layer2 = map.Layer2,
                MapHeight = map.MapHeight,
                MapWidth = map.MapWidth,
                Start = map.Start,
                StartLayer = map.StartLayer,
                StartSide = map.StartSide
            };
        }
        private Map GenerateMap() => new Map(Game,Game.TileSet,
            new MapLayer(Maps[CurrentMapIndex].Layer1, Maps[CurrentMapIndex].MapWidth, Maps[CurrentMapIndex].MapHeight,1),
            new MapLayer(Maps[CurrentMapIndex].Layer2, Maps[CurrentMapIndex].MapWidth, Maps[CurrentMapIndex].MapHeight, 2),
            Maps[CurrentMapIndex].MapWidth, Maps[CurrentMapIndex].MapHeight, 
            Maps[CurrentMapIndex].Start,
            Maps[CurrentMapIndex].StartSide,
            Maps[CurrentMapIndex].StartLayer,
            Maps[CurrentMapIndex].End,
            Maps[CurrentMapIndex].EndSide,
            Maps[CurrentMapIndex].EndLayer,Players,0,NextMap);

        public void NextMap(Map map)
        {
            CurrentMapIndex++;
            if (CurrentMapIndex >=Maps.Count)
            {
                Game.Arena = new EndArena(Game,PreviousArena,Players);
                return;
            }
            PreviousMap = CurrentMap;
            CurrentMap = GenerateMap();
            SaveState();
            Game.Arena = this;
        }

        private bool oskHide = false;

        public override void Update(GameTime gameTime)
        {
            if (!oskHide)
            {
                oskHide = true;
                Game.KeyboardInput.IsOskVisable = false;
            }
            Game.SideBar.DesignWidth = 0;
            if (Game.KeyboardInput.TypedKey(Keys.Escape))
            {
                Game.Sounds.Menu.Play();
                SaveState();
                PreviousMap = CurrentMap;
                Game.Arena = PreviousArena;    
                            
            }
            /*else if (Game.KeyboardInput.TypedKey(Keys.S))
            {
                Game.Sounds.Menu.Play();
                NextMap(CurrentMap);
            }*/
            else if (Players.All(p=> p.Par > 5 || p.State == Graphics.PlayerState.Done) && Game.KeyboardInput.TypedKey(Keys.S))
            {
                Game.Sounds.Menu.Play();
                NextMap(CurrentMap);
            }
            else
            {
                if(!Game.Transitioning)CurrentMap.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone);
            {
                if (Game.Transitioning)
                {
                    PreviousMap?.DrawMap(batch, Vector2.One);
                }
                else
                {
                    CurrentMap?.DrawMap(batch, Vector2.One);
                }
            }
            batch.End();
            batch.Begin();
            {
                
                batch.Draw(Game.SideBar);
                
                CurrentMap?.DrawMapPlayers(batch);
                var exitStr = Fonts.GameFont.MeasureString(Strings.Exit);
                var skipStr = Fonts.GameFont.MeasureString(Strings.Skip);
                if (Players.All(p => p.Par > 5 || p.State == Graphics.PlayerState.Done))
                {
                    batch.DrawString(Fonts.GameFont, Strings.Skip,
                        new Vector2(Game.Width - (skipStr.X + 10)+2f, Game.Height - (exitStr.Y + 20 + skipStr.Y)+3f),
                        Color.Black * 0.25f);

                    batch.DrawString(Fonts.GameFont, Strings.Skip,
                        new Vector2(Game.Width - (skipStr.X + 10), Game.Height - (exitStr.Y + 20 + skipStr.Y)),
                        Color.White);
                }
                batch.DrawString(Fonts.GameFont, Strings.Exit,
                    new Vector2(Game.Width - (exitStr.X + 10)+2f, Game.Height - (exitStr.Y + 10)+3f), Color.Black * 0.25f);

                batch.DrawString(Fonts.GameFont, Strings.Exit,
                    new Vector2(Game.Width - (exitStr.X + 10), Game.Height - (exitStr.Y + 10)), Color.White);
            }
            batch.End();
            base.Draw(batch);
        }
    }

    public class SaveState
    {
        public PlayerSave[] Players { get; set; }
        public SMap[] Maps { get; set; }
        public int CurrentPlayer { get; set; }
    }
}