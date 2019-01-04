using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniGolfMayhem.Arena
{
    public class PauseArena : BaseArena
    {
        public List<string> Credits;
        public TimeSpan LastTouch;
        public TimeSpan Show;

        public PauseArena(Golf game, BaseArena arena)
            : base(game, arena)
        {

        }
    }
}