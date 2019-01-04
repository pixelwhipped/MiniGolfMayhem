using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map15 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Bottom;
        public Vector2 Start { get; set; } = new Vector2(3, 4);
        public int MapWidth { get; set; } = 4;
        public int MapHeight { get; set; } = 5;

        public Vector2 End { get; set; } = new Vector2(0, 0);
        public Side EndSide { get; set; } = Side.Left;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                25,26,20,3,
                0,0,0,12,
                0,0,0,27,
                0,0,0,12,
                0,0,0,34
            };

        public int[] Layer2 { get; set; } =
            {
                0,0,0,0,
                0,0,0,0,
                0,0,0,0,
                0,0,0,0,
                0,0,0,0
            };

        #endregion
    }
}