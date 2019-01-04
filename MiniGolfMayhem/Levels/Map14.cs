using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map14 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Right;
        public Vector2 Start { get; set; } = new Vector2(1, 2);
        public int MapWidth { get; set; } = 4;
        public int MapHeight { get; set; } = 5;

        public Vector2 End { get; set; } = new Vector2(3, 1);
        public Side EndSide { get; set; } = Side.Bottom;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                1,75,20,3,
                37,47,3,12,
                19,59,38,12,
                1,21,77,12,
                19,73,46,21
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