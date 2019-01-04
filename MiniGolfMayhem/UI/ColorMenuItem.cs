using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.UI
{
    public class ColorMenuItem : MenuItem
    {

        private Rectangle RightRegion { get; }
        private Rectangle LeftRegion { get; }
        private readonly Getter<int> _get;
        private readonly Setter<int> _set;
        private readonly List<int> _colors;
        private int _index;

        public ColorMenuItem(Golf game, Rectangle bounds, Getter<int> get, Setter<int> set, string name, List<int> colors)
            : base(game, bounds, name)
        {
            _colors = colors;            
            var mx = Textures.Back.Width;
            var my = Textures.Back.Height;
            var xs = (float)Math.Min(my, Bounds.Height) / Math.Max(my, Bounds.Height);
            _bounds = new Rectangle(bounds.X, bounds.Y, (int)(bounds.Width + ((mx * xs) * 2)), bounds.Height);
            RightRegion = new Rectangle((int)(bounds.X + bounds.Width + (mx * xs)), bounds.Y, (int)(mx * xs), bounds.Height);
            LeftRegion = new Rectangle(bounds.X, bounds.Y, (int)(mx * xs), bounds.Height);
            _get = get;
            _set = set;

        }
        public override void Update(GameTime gameTime)
        {
            if (Selected && Game.KeyboardInput.TypedKey(Keys.Left))
            {
                Game.Sounds.Menu.Play();
                Left();
            }
            if (Selected && Game.KeyboardInput.TypedKey(Keys.Right))
            {
                Game.Sounds.Menu.Play();
                Right();
            }

            if (RightRegion.Contains(TapLoaction))
            {
                Game.Sounds.Menu.Play();
                Right();
                TapLoaction = Vector2.Zero;
            }
            if (!LeftRegion.Contains(TapLoaction)) return;
            Game.Sounds.Menu.Play();
            Left();
            TapLoaction = Vector2.Zero;
        }

        private void Left()
        {
            _index = _index == 0 ? _colors.Count- 1 : _index - 1;
            _set(_colors[_index]);
        }

        private void Right()
        {
            _index = ((_index + 1) > _colors.Count - 1) ? 0 : _index + 1;
            _set(_colors[_index]);
        }
        public override void Draw()
        {
            var x = Fonts.GameFont.MeasureString(Vector2.Zero, GameColors.Colors[_get()].Name.ToUpperInvariant()).Width;
            x = (Bounds.Width - (LeftRegion.Width * 2) - x) / 2;
            SpriteBatch.Begin();
            SpriteBatch.DrawString(Fonts.GameFontGrey, GameColors.Colors[_get()].Name.ToUpperInvariant(), new Vector2(x + Bounds.X + LeftRegion.Width+2, Bounds.Y+3), Color.Black * 0.25f);
            SpriteBatch.DrawString(Fonts.GameFontGrey, GameColors.Colors[_get()].Name.ToUpperInvariant(), new Vector2(x + Bounds.X + LeftRegion.Width, Bounds.Y), (Selected) ? GameColors.Colors[_get()].Color * Game.FadeX2 : GameColors.Colors[_get()].Color);
            SpriteBatch.Draw(Textures.Forward, RightRegion,
                             (Selected) ? Color.White * Game.FadeX2 : Color.Gray);
            SpriteBatch.Draw(Textures.Back, LeftRegion,
                 (Selected) ? Color.White * Game.FadeX2 : Color.Gray);
            SpriteBatch.End();
        }
    }
}
