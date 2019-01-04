using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map17 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Left;
        public Vector2 Start { get; set; } = new Vector2(5, 0);
        public int MapWidth { get; set; } = 9;
        public int MapHeight { get; set; } = 9;

        public Vector2 End { get; set; } = new Vector2(4, 2);
        public Side EndSide { get; set; } = Side.Bottom;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                1,20,20,20,3,25,20,20,3,
                12,0,1,20,46,20,3,0,101,
                101,0,93,57,50,58,12,0,12,
                19,22,12,66,65,67,12,23,21,
                0,0,12,0,4,0,93,0,0,
                8,50,64,50,50,50,64,50,9,
                17,65,49,49,49,49,49,65,18,
                0,101,0,0,13,0,0,101,0,
                0,19,22,23,46,22,23,21,0
            };

        public int[] Layer2 { get; set; } =
            {
                0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,
                0,0,91,91,91,91,91,0,0,
                0,0,0,0,0,0,0,0,0,
                0,0,0,0,82,0,0,0,0,
                0,0,0,0,82,0,0,0,0,
                0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0
            };

        #endregion
    }
}