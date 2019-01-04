using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.UI;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Arena
{
    public class SettingsArena : BaseArena
    {
        public MenuMapScroller MenuMap => Game.MenuMap;
        private readonly Menu _menu;

        private const string MaxPercent = " 100%";

        public SettingsArena(Golf game, BaseArena previousArena) : base(game, previousArena)
        {
            var effectVolume = new ScaleMenuItem(game,
                Fonts.GameFont.MeasureString(new Vector2(10, 100), Strings.EffectVolume + MaxPercent), () => Game.Sounds.EffectVolume,
                p => { Game.Sounds.SetEffectVolume(p); }, Strings.EffectVolume);
            var musicVolume = new ScaleMenuItem(game,
                Fonts.GameFont.MeasureString(new Vector2(10, effectVolume.Bounds.Y + effectVolume.Bounds.Height), Strings.MusicVolume + MaxPercent), () => Game.Sounds.MusicVolume,
                p => { Game.Sounds.SetMusicVolume(p); }, Strings.MusicVolume);

            var tileSets = new TileSetMenuItem(game,
                Fonts.GameFont.MeasureString(new Vector2(10, musicVolume.Bounds.Y + musicVolume.Bounds.Height), Strings.RoadBlock), () => Array.IndexOf(Game.TileSets,Game.TileSet),
                p => { Game.TileSet = Game.TileSets[p]; }, Strings.TileSet,new List<int> {0,1,2,3});

            var backItem = new MenuItem(game,
                Fonts.GameFont.MeasureString(new Vector2(10, tileSets.Bounds.Y + tileSets.Bounds.Height), Strings.Back),
                Strings.Back);
            _menu = new Menu(Game, HandleMenuSelect,
                            effectVolume,
                            musicVolume,
                            tileSets,
                            backItem
                );
        }

        public override void Update(GameTime gameTime)
        {
            Game.SideBar.DesignWidth = Game.Width - Textures.SideBar.Width;//-(Game.Center.X/2);
            _menu.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Initialise()
        {

        }

        public void HandleMenuSelect(MenuItem item)
        {
            switch (item.Name)
            {
                case Strings.Back:
                {
                        Game.GameSettings.TileSet = Array.IndexOf(Game.TileSets, Game.TileSet);
                    Game.CustomGameStorage.SaveSetting(Game.GameSettings);
                    Game.Arena = PreviousArena;
                    break;
                }

            }
        }

        public override void Draw(SpriteBatch batch)
        {
            MenuMap.Draw(batch);
            batch.Begin();
            batch.Draw(Game.SideBar);
            batch.End();
            _menu.Draw();
        }
    }
}
