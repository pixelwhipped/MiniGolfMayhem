using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map16 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Top;
        public Vector2 Start { get; set; } = new Vector2(0, 0);
        public int MapWidth { get; set; } = 1;
        public int MapHeight { get; set; } = 16;

        public Vector2 End { get; set; } = new Vector2(0, 15);
        public Side EndSide { get; set; } = Side.Bottom;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                33,
                70,
                12,
                69,
                101,
                93,
                10,
                101,
                48,
                4,
                13,
                101,
                93,
                12,
                12,
                34
            };

        public int[] Layer2 { get; set; } =
            {
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            };

        #endregion
    }
}