using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.Utilities;
using KeyboardInput = MiniGolfMayhem.Input.KeyboardInput;

namespace MiniGolfMayhem.UI
{
    public class NameMenuItem : MenuItem
    {

        private readonly Getter<int> _getColor;
        private readonly Getter<string> _get;
        private readonly Setter<string> _set;
        private const string UnderScore = "_";
        public new Rectangle Bounds
        {
            get
            {
                return Fonts.GameFontGrey.MeasureString(new Vector2(base.Bounds.X, base.Bounds.Y),_get() + UnderScore);
            }
        }

        public NameMenuItem(Golf game, Rectangle bounds, Getter<string> get, Setter<string> set, Getter<int> getColor, string name)
            : base(game, bounds, name)
        {            
            _getColor = getColor;            
            _get = get;
            _set = set;
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);
            game.KeyboardInput.AddKeyboardListener(KeyboardEvent);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var k in Game.KeyboardInput.Typed)
            {
                _set(_get()+k.ToUpperInvariant());    
            }
            if (Game.KeyboardInput.TypedKey(Keys.Back) && _get()!=string.Empty)
            {
                _set(_get().Substring(0, _get().Length - 1));
            }
            
        }


        public void KeyboardEvent(KeyboardInput keyboardInput)
        {
            
        }

        public override void Draw()
        {
            SpriteBatch.Begin();
            SpriteBatch.DrawString(Fonts.GameFontGrey, _get() + UnderScore, new Vector2(Bounds.X + 2, Bounds.Y + 3), Color.Black * 0.25f);
            SpriteBatch.DrawString(Fonts.GameFontGrey, _get() + UnderScore, new Vector2(Bounds.X, Bounds.Y), (Selected) ? GameColors.Colors[_getColor()].Color * Game.FadeX2 : GameColors.Colors[_getColor()].Color);
            
            //SpriteBatch.DrawString(Grid.Font, UnderScore, new Vector2(Bounds.X + Bounds.Width - dash.Width, Bounds.Y), (Selected) ? GameColors.Colors[_getColor()].Color * Grid.FadeX2 : GameColors.Colors[_getColor()].Color);
            SpriteBatch.End();
        }

    }
}
