using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map02 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Left;
        public Vector2 Start { get; set; } = new Vector2(0, 0);
        public int MapWidth { get; set; } = 3;
        public int MapHeight { get; set; } = 1;

        public Vector2 End { get; set; } = new Vector2(2, 0);
        public Side EndSide { get; set; } = Side.Top;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                25,75,42
            };

        public int[] Layer2 { get; set; } =
            {
                0, 0, 0
            };

        #endregion
    }
}