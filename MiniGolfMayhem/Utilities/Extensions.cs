using Microsoft.Xna.Framework;

namespace MiniGolfMayhem.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// Determines if the vector has a NaN value
        /// </summary>
        /// <param name="vec">Source vector</param>
        /// <returns>Returns true if either x or y is a NaN</returns>
        public static bool IsNan(this Vector2 vec) => (float.IsNaN(vec.X) || float.IsNaN(vec.Y));
    }
}
