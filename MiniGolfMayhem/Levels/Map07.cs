using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map07 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Right;
        public Vector2 Start { get; set; } = new Vector2(7, 3);
        public int MapWidth { get; set; } = 8;
        public int MapHeight { get; set; } = 7;

        public Vector2 End { get; set; } = new Vector2(6, 2);
        public Side EndSide { get; set; } = Side.Right;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                1,20,3,0,0,0,0,0,
                69,8,64,9,0,0,0,0,
                70,5,6,7,0,1,42,0,
                19,75,73,76,22,12,23,24,
                0,14,15,16,0,19,3,0,
                0,17,65,18,0,1,21,0,
                0,0,19,20,20,21,0,0
            };

        public int[] Layer2 { get; set; } =
            {
                0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,
                0,83,84,85,0,91,0,0,
                0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0
            };

        #endregion
    }
}