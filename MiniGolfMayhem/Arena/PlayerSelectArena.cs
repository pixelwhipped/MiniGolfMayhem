using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.NetworkOperators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.Levels;
using MiniGolfMayhem.UI;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Arena
{
    public class PlayerSelectArena : BaseArena
    {
        public List<Player> Players { get; set; }
        public MenuMapScroller MenuMap => Game.MenuMap;
        private readonly Menu _menu;
        public MapChooser MapChooser;

        public ILevelSelector CustomLevelSelector { get; set; }

        public ILevelSelector NormalLevelSelector { get; set; }
        private GameTypeItem gameType;
        private SaveState lastState;

        public PlayerSelectArena(Golf game, BaseArena previousArena, List<Player> players) : base(game, previousArena)
        {
            Players = players;

            var nextItem = new MenuItem(game,Fonts.GameFont.MeasureString(
                                        new Vector2(10, 100),
                                        Strings.Next), Strings.Next);
            var playerItem = new PlayersMenuItem(game,
                                    Fonts.GameFont.MeasureString(
                                        new Vector2(10, nextItem.Bounds.Y + nextItem.Bounds.Height),
                                        Strings.PlayersMStr), () => Game.GameSettings.Players, p => { Game.GameSettings.Players = p; }, Strings.Players);
            gameType = new GameTypeItem(game, Fonts.GameFont.MeasureString(new Vector2(10, playerItem.Bounds.Y + playerItem.Bounds.Height), Strings.GameType), Strings.GameType);
            var backItem = new MenuItem(game,
                Fonts.GameFont.MeasureString(new Vector2(10 , gameType.Bounds.Y + gameType.Bounds.Height), Strings.Back), Strings.Back);
            Maps = new List<SMap>(game.CustomGameStorage.LoadMaps().Maps);
            if (!Maps.Any())
            {
                Game.CustomGameStorage.AddMap(new TestMap().ToSMap());
                Maps = new List<SMap>(game.CustomGameStorage.LoadMaps().Maps);
            }
            CustomLevelSelector = new CustomLevelSelector(Maps);
            NormalLevelSelector = new NormalLevelSelector();
            MapChooser = new MapChooser(game,new Rectangle((int)Game.Center.X-((Textures.MapBorder.Width+(Textures.Back.Width*2))/2),100, Textures.MapBorder.Width, Textures.MapBorder.Height), string.Empty, gameType.GameType==GameType.Normal?NormalLevelSelector:CustomLevelSelector, this);
            _menu = Maps.Any()? new Menu(Game, HandleMenuSelect,
                nextItem,
                playerItem,
                gameType,
                            backItem
                ): new Menu(Game, HandleMenuSelect,
                nextItem,
                playerItem,
                            backItem
                );
            
        }

        public List<SMap> Maps { get; set; }

        public override void Update(GameTime gameTime)
        {
            Game.SideBar.DesignWidth = Game.Width - Textures.SideBar.Width;// - (Game.Center.X / 2);
            _menu.Update(gameTime);
            MapChooser.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Initialise()
        {

        }

        public void HandleMenuSelect(MenuItem item)
        {

            switch (item.Name)
            {
                case Strings.Next:
                {
                    Game.GameSettings.EffectVolume = Game.Sounds.EffectVolume;
                    Game.GameSettings.MusicVolume = Game.Sounds.MusicVolume;
                    Game.GameSettings.TileSet = Array.IndexOf(Game.TileSets, Game.TileSet);                    
                    Game.Arena = new PlayerState(Game,this,Players,new List<int> { 0, 1, 2, 3 });
                    break;
                }
                case Strings.GameType:
                {
                    MapChooser.LevelSelector = gameType.GameType == GameType.Normal
                        ? CustomLevelSelector
                        : NormalLevelSelector;
                        break;
                    }
                case Strings.Back:
                {                    
                    Game.Arena = PreviousArena;
                    break;
                }
            }
            Game.CustomGameStorage.SaveSetting(Game.GameSettings);
        }

        public override void Draw(SpriteBatch batch)
        {
            MenuMap.Draw(batch);
            batch.Begin();
            batch.Draw(Game.SideBar);
            batch.End();
            _menu.Draw();
            MapChooser.Draw();
        }
    }
}
