using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Arena
{
    public class BaseArena
    {
        public Fader Fade;

        public static float FadeRotator;
        public static bool FadeRotatorIn;

        public Golf Game;
        public BaseArena PreviousArena;
        public BaseArena(Golf game, BaseArena previousArena)
        {
            Game = game;
            Fade = new Fader(true, true);
            PreviousArena = previousArena;
            Game.KeyboardInput.IsOskVisable = false;
        }

        public virtual void Draw(SpriteBatch batch)
        {
        }

        public virtual void Initialise()
        {
        }

        public virtual void OnTap(Vector2 a)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
            Fade.Update();
            FadeRotator = FadeRotatorIn
                ? MathHelper.Clamp(FadeRotator += 0.01f, .2f, .8f)
                : MathHelper.Clamp(FadeRotator -= 0.01f, .2f, .8f);
            if (FadeRotator >= .8f) FadeRotatorIn = false;
            if (FadeRotator <= .2f) FadeRotatorIn = true;
        }
    }
}