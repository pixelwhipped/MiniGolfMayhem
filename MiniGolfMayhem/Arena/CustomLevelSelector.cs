using System.Collections.Generic;
using MiniGolfMayhem.Levels;

namespace MiniGolfMayhem.Arena
{
    public class CustomLevelSelector : ILevelSelector
    {
        private List<SMap> _maps;
        private int _index;
        public int Count => _maps.Count;
        public CustomLevelSelector(List<SMap> maps)
        {
            _maps = maps;
            _index = 0;
            EditMap = _maps[_index];
        }

        public void Next()
        {
            _index++;
            if (_index >= _maps.Count)
            {
                _index = 0;                                
            }
            EditMap = _maps[_index];
        }

        public void Previous()
        {
            _index--;
            if (_index < 0 )
            {
                _index = _maps.Count-1;
            }
            EditMap = _maps[_index];
        }

        public List<IMap> Maps
        {
            get
            {
                var ret = new List<IMap>();
                var add = false;
                foreach (var map in _maps)
                {
                    if (map == EditMap) add = true;
                    if (add) ret.Add(map);
                }
                return ret;
            }
        }

        public IMap CurrentMap => _maps[_index];

        public SMap EditMap { get; set; }
    }
}