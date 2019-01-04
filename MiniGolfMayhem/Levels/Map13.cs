using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map13 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Right;
        public Vector2 Start { get; set; } = new Vector2(4, 0);
        public int MapWidth { get; set; } = 5;
        public int MapHeight { get; set; } = 5;

        public Vector2 End { get; set; } = new Vector2(2, 2);
        public Side EndSide { get; set; } = Side.Left;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                1,20,20,20,24,
                12,1,20,20,3,
                12,12,43,3,10,
                10,19,2,21,12,
                19,20,20,20,21
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