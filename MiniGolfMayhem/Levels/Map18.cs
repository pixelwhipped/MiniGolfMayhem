using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map18 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Left;
        public Vector2 Start { get; set; } = new Vector2(1, 2);
        public int MapWidth { get; set; } = 7;
        public int MapHeight { get; set; } = 6;

        public Vector2 End { get; set; } = new Vector2(6,3);
        public Side EndSide { get; set; } = Side.Right;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                0,1,47,3,0,0,0,
                0,12,53,19,47,3,0,
                1,46,68,62,21,53,0,
                37,63,68,20,20,68,42,
                19,3,54,0,0,54,0,
                0,19,46,20,20,21,0
            };

        public int[] Layer2 { get; set; } =
            {
                0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,
                0,0,0,0,0,0,0
            };

        #endregion
    }
}