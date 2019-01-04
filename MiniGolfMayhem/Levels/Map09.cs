using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map09 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Right;
        public Vector2 Start { get; set; } = new Vector2(2, 0);
        public int MapWidth { get; set; } = 5;
        public int MapHeight { get; set; } = 5;

        public Vector2 End { get; set; } = new Vector2(0, 3);
        public Side EndSide { get; set; } = Side.Top;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                1,20,20,26,3,
                93,0,1,3,12,
                19,75,68,46,21,
                33,0,48,0,0,
                19,39,21,0,0
            };

        public int[] Layer2 { get; set; } =
            {
                0,0,0,0,0,
                0,0,0,0,0,
                0,0,0,0,0,
                0,0,0,0,0,
                0,0,0,0,0
            };

        #endregion
    }
}