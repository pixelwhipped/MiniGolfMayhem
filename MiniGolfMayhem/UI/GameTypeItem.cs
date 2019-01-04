using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.UI
{
    public enum GameType
    {
        Normal,Custom
    }
    public class GameTypeItem : MenuItem
    {

        private Rectangle RightRegion { get; }
        private Rectangle LeftRegion { get; }
   
        public GameType GameType { get; set; }

        public GameTypeItem(Golf game, Rectangle bounds, string name)
            : base(game, bounds, name)
        {
            var mx = Textures.Back.Width;
            var my = Textures.Back.Height;
            var xs = (float)Math.Min(my, Bounds.Height) / Math.Max(my, Bounds.Height);
            _bounds = new Rectangle(bounds.X, bounds.Y, (int)(bounds.Width + ((mx * xs) * 2)), bounds.Height);
            RightRegion = new Rectangle((int)(bounds.X + bounds.Width + (mx * xs)), bounds.Y, (int)(mx * xs), bounds.Height);
            LeftRegion = new Rectangle(bounds.X, bounds.Y, (int)(mx * xs), bounds.Height);

        }
        public override void Update(GameTime gameTime)
        {
            if (Selected && Game.KeyboardInput.TypedKey(Keys.Left))
            {
                Game.Sounds.Menu.Play();
                ChangeType();
            }
            if (Selected && Game.KeyboardInput.TypedKey(Keys.Right))
            {
                Game.Sounds.Menu.Play();
                ChangeType();
            }

            if (RightRegion.Contains(TapLoaction) || LeftRegion.Contains(TapLoaction))
            {
                Game.Sounds.Menu.Play();
                ChangeType();
                TapLoaction = Vector2.Zero;
            }
            TapLoaction = Vector2.Zero;
        }

        private void ChangeType() => GameType = GameType == GameType.Normal ? GameType.Custom: GameType.Normal;

        public override void Draw()
        {
            var x = Fonts.GameFont.MeasureString(Vector2.Zero, GameType==GameType.Custom?Strings.Custom:Strings.Normal).Width;
            x = (Bounds.Width - (LeftRegion.Width * 2) - x) / 2;
            SpriteBatch.Begin();
            {
                SpriteBatch.DrawString(Fonts.GameFont, GameType == GameType.Custom ? Strings.Custom : Strings.Normal,
                    new Vector2(x + Bounds.X + LeftRegion.Width+2f, Bounds.Y+3f),
                    Color.Black * 0.25f);
                SpriteBatch.DrawString(Fonts.GameFont, GameType == GameType.Custom ? Strings.Custom : Strings.Normal,
                    new Vector2(x + Bounds.X + LeftRegion.Width, Bounds.Y),
                    (Selected) ? Color.White*Game.FadeX2 : Color.White);
                SpriteBatch.Draw(Textures.Forward, RightRegion,
                    (Selected) ? Color.White*Game.FadeX2 : Color.Gray);
                SpriteBatch.Draw(Textures.Back, LeftRegion,
                    (Selected) ? Color.White*Game.FadeX2 : Color.Gray);
            }
            SpriteBatch.End();
        }
    }
}
