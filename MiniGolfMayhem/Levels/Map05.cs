using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map05 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Top;
        public Vector2 Start { get; set; } = new Vector2(2, 1);
        public int MapWidth { get; set; } = 5;
        public int MapHeight { get; set; } = 5;

        public Vector2 End { get; set; } = new Vector2(2, 0);
        public Side EndSide { get; set; } = Side.Bottom;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                1,2,20,20,3,
                37,3,33,1,38,
                37,68,68,68,38,
                37,46,68,46,38,
                19,20,46,75,21
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