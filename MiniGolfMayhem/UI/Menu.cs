using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MiniGolfMayhem.Input;

namespace MiniGolfMayhem.UI
{
    public class Menu
    {
        private readonly MenuItem[] _items;

        private int _index;
        public Golf Game { get; private set; }
        private readonly Procedure<MenuItem> _selectEvent;
        private UnifiedInput UnifiedInput => Game.UnifiedInput;

        private readonly List<Vector2> _taps;
        public Menu(Golf game, Procedure<MenuItem> selectEvent, MenuItem item, params MenuItem[] items)
        {

            item.Selected = true;
            foreach (var menuItem in items)
            {
                menuItem.Selected = false;
            }
            var i = new List<MenuItem> { item };
            i.AddRange(items);
            _items = i.ToArray();
            Game = game;
            _selectEvent = selectEvent;
            _taps = new List<Vector2>();
            UnifiedInput.TapListeners.Add(Tap);
        }

        public void Tap(Vector2 value)
        {
            _taps.Add(value);
        }


        public void Update(GameTime gameTime)
        {
            if (Game.Transitioning) return;
            #region Keyboard Control
            if (Game.KeyboardInput.TypedKey(Keys.Down))
            {
                Game.Sounds.Menu.Play();
                _items[_index].Selected = false;
                if (_index + 1 >= _items.Length)
                {
                    _index = 0;
                }
                else
                {
                    _index++;
                }
                _items[_index].Selected = true;
            }
            else if (Game.KeyboardInput.TypedKey(Keys.Up))
            {
                Game.Sounds.Menu.Play();
                _items[_index].Selected = false;
                if (_index - 1 < 0)
                {
                    _index = _items.Length - 1;
                }
                else
                {
                    _index--;
                }
                _items[_index].Selected = true;
            }
            else if (Game.KeyboardInput.TypedKey(Keys.Enter) || Game.KeyboardInput.TypedKey(Keys.Space))
            {
                Game.Sounds.Menu.Play(); //Fail
                _selectEvent(_items[_index]);
            }
            #endregion

            #region Mouse Touch        
            var mindex = _index;
            var clicked = false;
            for (var index = 0; index < _items.Length; index++)
            {
                if (_items[index].Bounds.Contains(UnifiedInput.Location) && UnifiedInput.Action)
                {
                    mindex = index;
                    clicked = true;
                    break;
                }
                if (_items[index].Bounds.Contains(UnifiedInput.Location) && !UnifiedInput.Hidden)
                {
                    mindex = index;
                    break;
                }

            }
            if(mindex != _index)
            {
                foreach (var menuItem in _items)
                {
                    menuItem.Selected = false;
                }
                _items[mindex].Selected = true;
                _index = mindex;
                Game.Sounds.Menu.Play();
            }

            if (clicked)
            {
                Game.Sounds.Menu.Play(); //Fail
                _selectEvent(_items[_index]);
            }
            

            #endregion 
            foreach (var menuItem in _items)
            {
                menuItem.Update(gameTime);
                if (menuItem.Selected && menuItem.GetType() == typeof (NameMenuItem))
                {
                    if (!Game.KeyboardInput.IsOskVisable)
                    {
                        Game.KeyboardInput.IsOskVisable = true;
                    }

                }
                else if (menuItem.Selected && menuItem.GetType() != typeof(NameMenuItem))
                {
                    Game.KeyboardInput.IsOskVisable = false;
                }
                
            }
        }
        public void Draw()
        {
            foreach (var menuItem in _items)
            {
                menuItem.Draw();
            }
        }
    }
}
