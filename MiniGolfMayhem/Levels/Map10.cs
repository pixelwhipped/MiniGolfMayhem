using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map10 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Top;
        public Vector2 Start { get; set; } = new Vector2(1,1);
        public int MapWidth { get; set; } = 2;
        public int MapHeight { get; set; } = 5;

        public Vector2 End { get; set; } = new Vector2(0, 2);
        public Side EndSide { get; set; } = Side.Bottom;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                1,3,
                54,12,
                101,12,
                54,12,
                19,21
            };

        public int[] Layer2 { get; set; } =
            {
                0,0,
                0,0,
                0,0,
                0,0,
                0,0
            };

        #endregion
    }
}