using System.Collections.Generic;
using MiniGolfMayhem.Levels;

namespace MiniGolfMayhem.Arena
{
    public interface ILevelSelector
    {
        void Next();
        void Previous();
        int Count { get; }
        List<IMap> Maps { get; }
        IMap CurrentMap { get; }
        SMap EditMap { get; }
    }
}