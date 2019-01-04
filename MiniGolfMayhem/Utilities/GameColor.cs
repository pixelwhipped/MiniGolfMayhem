using Microsoft.Xna.Framework;

namespace MiniGolfMayhem.Utilities
{
    public struct GameColor
    {
        public Color Color;
        public string Name;
    }
    public static class GameColors
    {

        public static GameColor[] Colors =
        {
            new GameColor{Color = Color.Blue, Name = Strings.ColorBlue},
            new GameColor{Color = Color.Red, Name = Strings.ColorRed},
            new GameColor{Color = Color.DarkGreen, Name = Strings.ColorGreen},
            new GameColor{Color = Color.Purple, Name = Strings.ColorPurple},
        };
    }
}
