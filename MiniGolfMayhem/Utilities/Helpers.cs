using System;
using Microsoft.Xna.Framework;

namespace MiniGolfMayhem.Utilities
{
    public class Helpers
    {
        public static float VectorToAngle(Vector2 vector) { return (float)Math.Atan2(vector.X, vector.Y); }
        public static Vector2 AngleToVector(float angle) { return new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle)); }

        public static float ToRadians(float angle) { return angle * (float)(Math.PI / 180); }
        public static float ToRadians(Vector2 vector) { return ToRadians(VectorToAngle(vector)); }
        public static float ToAngle(float radians) { return radians * (float)(180 / Math.PI); }
        public static float ToAngle(Vector2 vector) { return ToAngle(ToRadians(vector)); }
    }
}
