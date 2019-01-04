using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map06 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Top;
        public Vector2 Start { get; set; } = new Vector2(0, 0);
        public int MapWidth { get; set; } = 5;
        public int MapHeight { get; set; } = 5;

        public Vector2 End { get; set; } = new Vector2(4, 0);
        public Side EndSide { get; set; } = Side.Top;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                33,1,47,3,51,
                48,12,69,12,12,
                12,37,68,38,12,
                12,12,70,12,12,
                19,46,21,19,21
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