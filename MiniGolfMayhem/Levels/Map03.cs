using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map03 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Left;
        public Vector2 Start { get; set; } = new Vector2(0, 1);
        public int MapWidth { get; set; } = 5;
        public int MapHeight { get; set; } = 3;

        public Vector2 End { get; set; } = new Vector2(2, 0);
        public Side EndSide { get; set; } = Side.Top;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                0,0,33,0,0,
                25,22,48,23,3,
                0,0,19,20,21
            };

        public int[] Layer2 { get; set; } =
            {
                0,0,0,0,0,
                0,0,91,0,0,
                0,0,0,0,0
            };

        #endregion
    }
}