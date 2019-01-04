using System.Collections.Generic;
using Windows.UI.Xaml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.UI;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Arena
{
    public class MenuArena : BaseArena
    {   
        private readonly Menu _menu;

        public int Spacing = 10;

        public MenuMapScroller MenuMap => Game.MenuMap;
        public MenuArena(Golf game) : base(game,null)
        {
            PreviousArena = this;
            LastState = Game.CustomGameStorage.LoadState();
            MenuItem playItem;
            var resumeItem = new MenuItem(game,
                    Fonts.GameFont.MeasureString(new Vector2(0, Sprites.Title.Height), Strings.Resume), Strings.Resume,
                    () => new Vector2(Game.Center.X - (Fonts.GameFont.MeasureString(Strings.Resume).X / 2f), 0));
            // var measureString = Fonts.GameFont.MeasureString(Strings.Play);

            if (LastState != null)
            {
                playItem = new MenuItem(game,
                    Fonts.GameFont.MeasureString(
                    new Vector2(0, resumeItem.Bounds.Y + resumeItem.Bounds.Height),
                    Strings.Play), Strings.Play,
                    () => new Vector2(Game.Center.X - (Fonts.GameFont.MeasureString(Strings.Play).X/2f), 0));
            }
            else
            {
                playItem = new MenuItem(game,
                    Fonts.GameFont.MeasureString(new Vector2(0, Sprites.Title.Height), Strings.Play), Strings.Play,
                    () => new Vector2(Game.Center.X - (Fonts.GameFont.MeasureString(Strings.Play).X / 2f), 0));
            }
           // measureString = Fonts.GameFont.MeasureString(Strings.Edit);
            var editItem = new MenuItem(game,
                Fonts.GameFont.MeasureString(
                    new Vector2(0, playItem.Bounds.Y + playItem.Bounds.Height),
                    Strings.Edit), Strings.Edit, () => new Vector2(Game.Center.X - (Fonts.GameFont.MeasureString(Strings.Edit).X / 2f), 0));
         //   measureString = Fonts.GameFont.MeasureString(Strings.Settings);
            var settingsItem = new MenuItem(game,
                Fonts.GameFont.MeasureString(
                    new Vector2(0, editItem.Bounds.Y + editItem.Bounds.Height),
                    Strings.Settings), Strings.Settings,() => new Vector2(Game.Center.X - (Fonts.GameFont.MeasureString(Strings.Settings).X / 2f), 0));
          //  measureString = Fonts.GameFont.MeasureString(Strings.Exit);
            var exitItem = new MenuItem(game,
                Fonts.GameFont.MeasureString(
                    new Vector2(0, settingsItem.Bounds.Y + settingsItem.Bounds.Height),
                    Strings.Exit), Strings.Exit, () => new Vector2(Game.Center.X - (Fonts.GameFont.MeasureString(Strings.Exit).X / 2f), 0));
           
            if (LastState == null)
            {
                _menu = new Menu(game, HandleMenuSelect,
                    playItem,
                    editItem,
                    settingsItem,
                    exitItem
                    );
            }
            else
            {
                _menu = new Menu(game, HandleMenuSelect,
                    resumeItem,
                    playItem,
                    editItem,
                    settingsItem,
                    exitItem
                    );
            }

            Game.Sounds.ShortIntro.Play();
        }

        public SaveState LastState { get; set; }

        public void HandleMenuSelect(MenuItem item)
        {
            switch (item.Name)
            {
                case Strings.Play:
                    {                        
                        Game.Arena = new PlayerSelectArena(Game, this, new List<Player>()); //new PlayArena(Game,this,null,null);//
                        break;
                    }
                case Strings.Resume:
                    {
                        Game.Arena = new PlayArena(Game, this, LastState); //new PlayArena(Game,this,null,null);//
                        break;
                    }
                case Strings.Edit:
                {
                    Game.Arena = Game.Arena = new EditArena(Game, this);
                        break;
                    }
                case Strings.Settings:
                {                        
                        Game.Arena = new SettingsArena(Game, this);
                        break;
                    }
                case Strings.Exit:
                    {
                        Application.Current.Exit();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        public override void Update(GameTime gameTime)
        {
            Game.SideBar.DesignWidth = 0;
            _menu.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch batch)
        {
            
            MenuMap.Draw(batch);
            
         //   Game.Clouds.Draw(batch);

            batch.Begin();
            batch.Draw(Game.Logo);
            batch.Draw(Game.SideBar);
            _menu.Draw();
            batch.End();
        }
    }
}