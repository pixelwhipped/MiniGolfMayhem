using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.Input;

namespace MiniGolfMayhem.UI
{
    public class MenuItem : IDisposable
    {
        public bool Selected;
        public Rectangle _bounds;
        public Rectangle Bounds => new Rectangle(_bounds.X+((int)_offset().X), _bounds.Y + ((int)_offset().Y), _bounds.Width, _bounds.Height);
        public Golf Game;
        public SpriteBatch SpriteBatch;
        public string Name;
        private readonly Func<Vector2> _offset;

        public UnifiedInput UnifiedInput => Game.UnifiedInput;

        public MenuItem(Golf game, Rectangle bounds, string name, Func<Vector2> offset = null)
        {
            Game = game;
            _bounds = bounds;
            Name = name;
            _offset = offset ?? (() => Vector2.Zero);
            
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            UnifiedInput.TapListeners.Add(Tap);
        }

        public Vector2 TapLoaction = Vector2.Zero;
        public virtual void Tap(Vector2 value) => TapLoaction = value;


        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw()
        {
            SpriteBatch.Begin();
            SpriteBatch.DrawString(Fonts.GameFont, Name, new Vector2(Bounds.X + 2, Bounds.Y + 3), Color.Black * 0.25f);
            SpriteBatch.DrawString(Fonts.GameFont, Name, new Vector2(Bounds.X, Bounds.Y), (Selected) ? Color.White * Game.Arena.Fade.Fade : Color.Gray);                
            SpriteBatch.End();
        }

        public void Dispose()
        {
            SpriteBatch.Dispose();
        }
    }
}
