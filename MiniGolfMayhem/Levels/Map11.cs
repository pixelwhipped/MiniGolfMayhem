using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map11 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Top;
        public Vector2 Start { get; set; } = new Vector2(0, 1);
        public int MapWidth { get; set; } = 5;
        public int MapHeight { get; set; } = 6;

        public Vector2 End { get; set; } = new Vector2(3,5);
        public Side EndSide { get; set; } = Side.Top;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                0,0,1,3,0,
                33,0,101,4,0,
                19,22,101,23,3,
                1,22,101,23,21,
                101,0,101,13,0,
                19,102,21,52,0
            };

        public int[] Layer2 { get; set; } =
            {
                0,0,0,0,0,
                0,0,0,0,0,
                0,0,91,82,0,
                0,0,91,82,0,
                0,0,0,0,0,
                0,0,0,0,0
            };

        #endregion
    }
}