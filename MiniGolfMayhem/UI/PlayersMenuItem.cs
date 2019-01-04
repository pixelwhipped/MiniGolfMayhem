using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.UI
{
    public class PlayersMenuItem : MenuItem
    {
        private readonly Rectangle _rightRegion;
        private readonly Rectangle _leftRegion;
        private readonly Getter<int> _get;
        private readonly Setter<int> _set;

        public float Value
        {
            get { return _get(); }
        }

        public PlayersMenuItem(Golf game, Rectangle bounds, Getter<int> get, Setter<int> set, string name)
            : base(game, bounds, name)
        {            
            var mx = Textures.Back.Width;
            var my = Textures.Back.Height;
            var xs = (float) Math.Min(my, Bounds.Height)/Math.Max(my, Bounds.Height);
            _bounds = new Rectangle(bounds.X, bounds.Y, (int) (bounds.Width + ((mx*xs)*2)), bounds.Height);
            _rightRegion = new Rectangle((int) (bounds.X + bounds.Width + (mx*xs)), bounds.Y, (int) (mx*xs),
                bounds.Height);
            _leftRegion = new Rectangle(bounds.X, bounds.Y, (int) (mx*xs), bounds.Height);
            _get = get;
            _set = set;
           // UnifiedInput.TapListeners.Add(Tap);
        }

        public override void Update(GameTime gameTime)
        {
            if (TapLoaction == Vector2.Zero && !Game.KeyboardInput.Any()) return;
            if (_rightRegion.Contains(TapLoaction) || Game.KeyboardInput.TypedKey(Keys.Right))
            {
                Game.Sounds.Menu.Play();
                _set((int)MathHelper.Clamp(_get() + 1f, 1f, 4f));
            }
            else if (_leftRegion.Contains(TapLoaction) || Game.KeyboardInput.TypedKey(Keys.Left))
            {
                Game.Sounds.Menu.Play();
                _set((int)MathHelper.Clamp(_get() - 1f, 1f, 4f));
            }
            TapLoaction = Vector2.Zero;


        }

        public override void Draw()
        {
            SpriteBatch.Begin();
            SpriteBatch.DrawString(Fonts.GameFont, Name + Strings.Space + _get(),
                new Vector2(Bounds.X + _leftRegion.Width+2f, Bounds.Y+3f),Color.Black * 0.25f);
            SpriteBatch.DrawString(Fonts.GameFont, Name + Strings.Space + _get(),
                new Vector2(Bounds.X + _leftRegion.Width, Bounds.Y), (Selected) ? Color.White*Game.FadeX2 : Color.Gray);

            SpriteBatch.Draw(Textures.Forward, _rightRegion,
                (Selected) ? Color.White*Game.FadeX2 : Color.Gray);
            SpriteBatch.Draw(Textures.Back, _leftRegion,
                (Selected) ? Color.White*Game.FadeX2 : Color.Gray);
            SpriteBatch.End();
        }

    }
}
