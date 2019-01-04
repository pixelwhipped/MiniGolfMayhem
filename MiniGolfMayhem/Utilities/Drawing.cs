using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Utilities
{
    public static class Drawing
    {
        public static void DrawNode(this SpriteBatch spritbatch, Node node, Vector2 posistion)
        {
            node.Draw(spritbatch, posistion);
        }
        public static void DrawCircle(this SpriteBatch spritbatch, Vector2 center, float radius, Color color, int lineWidth, int segments = 16)
        {
            var vertex = new Vector2[segments];
            var increment = Math.PI * 2.0 / segments;
            var theta = 0.0;
            for (var i = 0; i < segments; i++)
            {
                vertex[i] = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                theta += increment;
            }
            DrawPolygon(spritbatch, vertex, segments, color, lineWidth);
        }

        public static void DrawLine(this SpriteBatch batch, Vector2 point1, Vector2 point2, Color color,
            float width = 1f)
        {
            var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            var length = Vector2.Distance(point1, point2);

            batch.Draw(Golf.Pixel, point1, null, color,
                angle, Vector2.Zero, new Vector2(length, width),
                SpriteEffects.None, 1);
        }

        public static void DrawPolygon(this SpriteBatch spriteBatch, Vector2[] vertex, int count, Color color, int lineWidth)
        {
            if (count <= 0) return;
            var c = count - 1;
            for (var i = 0; i < c; i++)
            {
                DrawLine(spriteBatch, vertex[i], vertex[i + 1], color, lineWidth);
            }
            DrawLine(spriteBatch, vertex[c], vertex[0], color, lineWidth);
        }

        public static void DrawRectangle(this SpriteBatch batch, Rectangle rect, Color color, float width = 1f)
        {
            batch.DrawLine(new Vector2(rect.X, rect.Y), new Vector2(rect.X + rect.Width, rect.Y), color, width);
            batch.DrawLine(new Vector2(rect.X + rect.Width, rect.Y),
                new Vector2(rect.X + rect.Width, rect.Y + rect.Height), color, width);
            batch.DrawLine(new Vector2(rect.X + rect.Width, rect.Y + rect.Height),
                new Vector2(rect.X, rect.Y + rect.Height), color, width);
            batch.DrawLine(new Vector2(rect.X, rect.Y + rect.Height), new Vector2(rect.X, rect.Y), color, width);
        }

        public static void FillRectangle(this SpriteBatch batch, Rectangle rect, Color color)
        {
            batch.Draw(Golf.Pixel, rect, color);
        }
    }
}
