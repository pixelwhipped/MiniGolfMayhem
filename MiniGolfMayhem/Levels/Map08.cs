using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map08 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Bottom;
        public Vector2 Start { get; set; } = new Vector2(1, 0);
        public int MapWidth { get; set; } = 3;
        public int MapHeight { get; set; } = 5;

        public Vector2 End { get; set; } = new Vector2(1, 3);
        public Side EndSide { get; set; } = Side.Top;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                1,47,3,
                12,54,12,
                12,12,12,
                10,54,93,
                19,46,21
            };

        public int[] Layer2 { get; set; } =
            {
                0,0,0,
                0,0,0,
                0,0,0,
                0,0,0,
                0,0,0
            };

        #endregion
    }
}