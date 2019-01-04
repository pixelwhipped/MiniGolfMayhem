using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class Map12 : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Left;
        public Vector2 Start { get; set; } = new Vector2(0, 3);
        public int MapWidth { get; set; } = 20;
        public int MapHeight { get; set; } = 7;

        public Vector2 End { get; set; } = new Vector2(19, 3);
        public Side EndSide { get; set; } = Side.Right;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
            {
                8,50,50,9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                28,29,29,55,20,76,47,20,20,20,20,20,26,20,20,47,20,20,60,3,
                28,29,29,55,20,20,68,20,20,20,60,20,20,47,20,68,20,20,20,38,
                28,29,29,55,47,20,46,20,73,47,20,20,20,68,20,46,20,20,20,38,
                28,29,29,55,68,20,20,20,20,68,20,20,20,46,20,75,20,20,20,38,
                28,29,29,55,46,20,20,60,20,46,20,20,60,61,20,20,20,20,61,21,
                17,49,49,18,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
            };

        public int[] Layer2 { get; set; } =
            {
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
            };

        #endregion
    }
}