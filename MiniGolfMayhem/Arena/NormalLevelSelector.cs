using System.Collections.Generic;
using MiniGolfMayhem.Levels;

namespace MiniGolfMayhem.Arena
{
    public class NormalLevelSelector : ILevelSelector
    {
        private List<IMap> _maps;

        public int Count => _maps.Count;
        private int _index;
        public NormalLevelSelector()
        {
            _index = 0;
            _maps = new List<IMap> { new Map01(), new Map02(), new Map03(), new Map04(), new Map05(), new Map06(), new Map07(), new Map08(), new Map09(), new Map10(), new Map11(), new Map12(), new Map13(), new Map14(), new Map15(), new Map16(), new Map17(), new Map18() };
        }
        public void Next()
        {
            _index++;
            if (_index >= _maps.Count)
            {
                _index = 0;
            }            
        }

        public void Previous()
        {
            _index--;
            if (_index < 0)
            {
                _index = _maps.Count - 1;
            }            
        }
        public IMap CurrentMap => _maps[_index];
        public List<IMap> Maps 
        {
            get
            {
                var ret = new List<IMap>();
                var add = false;
                foreach (var map in _maps)
                {
                    if (map == CurrentMap) add = true;
                    if (add) ret.Add(map);
                }
                return ret;
            }
        }
        public SMap EditMap => null;
    }
}