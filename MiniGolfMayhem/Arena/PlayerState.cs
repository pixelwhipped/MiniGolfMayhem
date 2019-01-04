using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.UI;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Arena
{
    public class PlayerState: BaseArena
    {
        private readonly List<Player> _players;
        private List<int> _colors;

        private readonly Menu _menu;
        private readonly MenuItem _startNextItem;
        private readonly NameMenuItem _nameItem;
        private readonly ColorMenuItem _colorItem;
        private readonly MenuItem _backItem;

        private Player _player
        {
            get { return _players[_players.Count-1]; }
            set
            {
                _players[_players.Count-1] = value;
            }
        }

        private readonly Tween _invalidName;
        public PlayerSelectArena PlayerSelectArena { get; set; }

        public MenuMapScroller MenuMap => Game.MenuMap;

        public PlayerState(Golf game, PlayerSelectArena startState, List<Player> players, List<int> colors)
            : base(game, startState)
        {
            //game.KeyboardInput.IsOSKVisable = true;
            PlayerSelectArena = startState;
            _players = players;
            _colors = colors;
            if (_players.Count == 0)
            {
                AddNewPlayer();
            }
                        
            _startNextItem = new MenuItem(game,
                                     Fonts.GameFont.MeasureString(new Vector2(10, 100),
                                     ((players.Count == Game.GameSettings.Players)
                                     ? Strings.Start : Strings.Next)),
                                     ((players.Count == Game.GameSettings.Players)
                                     ? Strings.Start : Strings.Next));
            _nameItem = new NameMenuItem(game, Fonts.GameFont.MeasureString(
                                                   new Vector2(10, 10+_startNextItem.Bounds.Y + _startNextItem.Bounds.Height),
                                                   _player.Name),
                () => _player.Name,
                p => { _player.Name = p; }, () => _player.ColorIndex, Strings.Name);
            int max = 0;
            int mindex = 0;
            Rectangle m = new Rectangle();
            for (var index = 0; index < colors.Count; index++)
            {
                var color = colors[index];
                m = Fonts.GameFont.MeasureString(
                    new Vector2(10, _nameItem.Bounds.Y + _nameItem.Bounds.Height),
                    GameColors.Colors[color].Name.ToUpperInvariant());
                if (m.Width > max)
                {
                    max = m.Width;
                    mindex = index;
                }
            }
            _colorItem = new ColorMenuItem(game,
                                               new Rectangle(10, _nameItem.Bounds.Y + _nameItem.Bounds.Height + 10,
                                                   m.Width,m.Height), () => _player.ColorIndex,
                                           delegate (int p) { _player.ColorIndex = p; }, Strings.Color, colors);
            _backItem = new MenuItem(game,
                        Fonts.GameFont.MeasureString(
                            new Vector2(10, 10+ _colorItem.Bounds.Y + _colorItem.Bounds.Height),
                            Strings.Back), Strings.Back);
            _menu = new Menu(game, HandleMenuSelect,
                            _startNextItem,
                            _nameItem,
                            _colorItem,
                            _backItem
                );
            _invalidName = new Tween(new TimeSpan(0, 0, 0, 1), 1, 0);
            _invalidName.Finish();
        }

        public void HandleMenuSelect(MenuItem item)
        {            
            switch (item.Name)
            {
                case Strings.Next:
                    {
                        if (_player.Name == string.Empty)
                        {
                            _invalidName.Reset();
                        }
                        else
                        {
                            SetPlayerName(_players.Count, _player.Name);
                            _colors.Remove(_player.ColorIndex);                            
                            AddNewPlayer();
                            Game.Arena = new PlayerState(Game, PlayerSelectArena, _players, _colors);
                        }
                        break;
                    }
                case Strings.Start:
                    {
                        if (_player.Name == string.Empty)
                        {
                            _invalidName.Reset();
                        }
                        else
                        {
                            SetPlayerName(_players.Count, _player.Name);
                            Game.Arena = new PlayArena(Game, Game.MenuArena, PlayerSelectArena.MapChooser.LevelSelector.Maps, _players);
                        }
                        break;
                    }
                case Strings.Back:
                    {
                        Back();
                        break;
                    }
            }
            Game.CustomGameStorage.SaveSetting(Game.GameSettings);
        }

        public void AddNewPlayer()
        {
            var c = GetPlayerColor(_players.Count + 1);
            if (!_colors.Contains(c)) c = _colors[0];
            _players.Add(new Player(Game, c, GetPlayerName(_players.Count + 1)));
        }
        private string GetPlayerName(int count)
        {
            if (count == 1) return Game.GameSettings.Player1;
            if (count == 2) return Game.GameSettings.Player2;
            if (count == 3) return Game.GameSettings.Player3;
            if (count == 4) return Game.GameSettings.Player4;
            return Strings.Player1;
        }
        private int GetPlayerColor(int count)
        {
            if (count == 1) return Game.GameSettings.Player1Color;
            if (count == 2) return Game.GameSettings.Player2Color;
            if (count == 3) return Game.GameSettings.Player3Color;
            if (count == 4) return Game.GameSettings.Player4Color;
            return 0;
        }
        private void SetPlayerName(int count, string name)
        {
            if (count == 1) Game.GameSettings.Player1 = name;
            if (count == 2) Game.GameSettings.Player2 = name;
            if (count == 3) Game.GameSettings.Player3 = name;
            if (count == 4) Game.GameSettings.Player4 = name;
            Game.CustomGameStorage.SaveSetting(Game.GameSettings);
        }

        public override void Update(GameTime gameTime)
        {
            Game.SideBar.DesignWidth = Game.Width - Textures.SideBar.Width;
            _menu.Update(gameTime);
            _invalidName.Update(gameTime.ElapsedGameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch batch)
        {
            MenuMap.Draw(batch);
            batch.Begin();
            batch.Draw(Game.SideBar);
            batch.End();
            _menu.Draw();
            var x = Fonts.GameFont.MeasureString(Strings.InvalidName);
            batch.Begin();
            {
                batch.DrawString(Fonts.GameFont, Strings.InvalidName,
                    new Vector2(Game.Center.X - (x.X/2), Game.Center.Y - (x.Y/2)), Color.White*_invalidName);
            }
            batch.End();
        }

        public void Back()
        {            
            if (_players.Count == 1)
            {                
                Game.Arena = PreviousArena;
            }
            else
            {

                if (_players.Count >= 1)
                {
                    _colors.Add(_players[_players.Count - 1].ColorIndex);
                    _players.Remove(_players[_players.Count - 1]);
                    Game.Arena = new PlayerState(Game, PlayerSelectArena, _players, _colors);
                }
                else
                {                    
                    Game.Arena = PreviousArena;
                }
            }
        }
    }
}
