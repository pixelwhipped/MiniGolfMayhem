using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;

namespace MiniGolfMayhem.Levels
{
    public interface IMap
    {
        int StartLayer { get; set; }
        Side StartSide { get; set; }
        Vector2 Start { get; set; }
        int MapWidth { get; set; }
        int MapHeight { get; set; }

        Vector2 End { get; set; }
        Side EndSide { get; set; }
        int EndLayer { get; set; }

        int[] Layer1 { get; set; }

        int[] Layer2 { get; set; }
    }
}