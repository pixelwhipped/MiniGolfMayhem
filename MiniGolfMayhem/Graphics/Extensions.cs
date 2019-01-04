using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniGolfMayhem.Graphics
{
    public static class Extensions
    {
        public static void Draw(this SpriteBatch batch, Sprite sprite, Vector2 location,float z, Color? color = null,
            float scale = 1f, bool flip = false)
        {
            if (sprite.Animation != AnimationState.Stop)
                batch.Draw(sprite.Map,
                    new Rectangle((int) location.X, (int) location.Y, (int) (sprite.Width*scale),
                        (int) (sprite.Height*scale)), sprite.GetSource(), color ?? Color.White, 0,
                    Vector2.Zero, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, z);
        }

        public static void Draw(this SpriteBatch batch, Sprite sprite, Rectangle destination, float z, Color? color = null,
            float scale = 1f, bool flip = false)
        {
            if (sprite.Animation != AnimationState.Stop)
                batch.Draw(sprite.Map, destination, sprite.GetSource(), color ?? Color.White, 0, Vector2.Zero,
                    flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, z);
        }

        public static void Draw(this SpriteBatch batch, IDrawable drawable)
        {
            drawable.Draw(batch);
        }
    }
}