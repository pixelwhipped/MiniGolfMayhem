using System;
using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public class TestMap : IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Top;
        public Vector2 Start { get; set; } = new Vector2(0, 0);
        public int MapWidth { get; set; } = 1;
        public int MapHeight { get; set; } = 2;

        public Vector2 End { get; set; } = new Vector2(0, 1);
        public Side EndSide { get; set; } = Side.Bottom;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } =
        {
            33,
            52
        };

        public int[] Layer2 { get; set; } =
        {
            0, 0
        };

        public SMap ToSMap() => new SMap
        {
            End = End,
            EndLayer = EndLayer,
            EndSide = EndSide,
            LevelGUID = "EC837E35-0F98-4F0C-AF14-689C8CF95D6F",
            Layer1 = Layer1,
            Layer2 = Layer2,
            MapHeight = MapHeight,
            MapWidth = MapWidth,
            Start = Start,
            StartLayer = StartLayer,
            StartSide = StartSide
        };

        #endregion
    }

    public class Map01: IMap
    {
        public int StartLayer { get; set; } = 1;
        public Side StartSide { get; set; } = Side.Top;
        public Vector2 Start { get; set; } = new Vector2(0, 0);
        public int MapWidth { get; set; } = 1;
        public int MapHeight { get; set; } = 3;

        public Vector2 End { get; set; } = new Vector2(0, 2);
        public Side EndSide { get; set; } = Side.Bottom;
        public int EndLayer { get; set; } = 1;

        #region Map Layers
        // 27 74
        public int[] Layer1 { get; set; } = 
        {
            33,
            12,
            52
        };

        public int[] Layer2 { get; set; } =
        {
            0, 0, 0
        };

        #endregion
    }
}
