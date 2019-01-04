using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.UI;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Arena
{
    public class EndArena : BaseArena
    {
        public MenuMapScroller MenuMap => Game.MenuMap;
        public EndArena(Golf game, BaseArena previousArena, List<Player> players) : base(game, previousArena)
        {
            Players = players;
            Players.Sort(new ScoreComparitor());
            Game.UnifiedInput.TapListeners.Add(OnTap);
        }

        public List<Player> Players { get; set; }

        public override void Update(GameTime gameTime)
        {
            Game.SideBar.DesignWidth = 0;
            if (Game.KeyboardInput.TypedKey(Keys.Escape) || Game.KeyboardInput.TypedKey(Keys.Enter))
            {
                Game.Sounds.Menu.Play();
                Game.Arena = PreviousArena;
            }
        }

        public override void OnTap(Vector2 a)
        {
            if (Game.Transitioning) return;
            var exitStr = Fonts.GameFont.MeasureString(Strings.Exit);
            if (
                new Rectangle((int)(Game.Width - (exitStr.X + 10)), (int)(Game.Height - (exitStr.Y + 10)),
                    (int)exitStr.X, (int)exitStr.Y).Contains(a))
            {
                Game.Sounds.Menu.Play();
                Game.Arena = PreviousArena;
            }

        }

        public override void Draw(SpriteBatch batch)
        {
            MenuMap.Draw(batch);

            var textOffset = new Vector2(10, 10);

            var i = 1;
            
            batch.Begin();
            {
                batch.Draw(Game.SideBar);
                foreach (var p in Players)
                {
                    var str = Fonts.GameFont.MeasureString($"{Ordinal(i)} {p.Name}");
                    textOffset = new Vector2(Game.Center.X-(str.X/2f), textOffset.Y);
                    batch.DrawString(Fonts.GameFont, $"{Ordinal(i)} {p.Name}", textOffset + new Vector2(2f,3f), Color.Black * 0.25f);
                    batch.DrawString(Fonts.GameFont, $"{Ordinal(i)} {p.Name}", textOffset, Color.White);
                    str = Fonts.GameFontGrey.MeasureString($"{p.Total}");
                    textOffset = new Vector2(Game.Center.X - (str.X / 2f), textOffset.Y + str.Y + 15);
                    batch.DrawString(Fonts.GameFontGrey, $"{p.Total}", textOffset + new Vector2(2f, 3f), Color.Black * 0.25f);
                    batch.DrawString(Fonts.GameFontGrey, $"{p.Total}", textOffset, p.Color);
                    textOffset = new Vector2(0, textOffset.Y + Fonts.GameFontGrey.MeasureString($"{p.Total}").Y + 30);
                    i++;
                }

                var exitStr = Fonts.GameFont.MeasureString(Strings.Exit);
                batch.DrawString(Fonts.GameFont, Strings.Exit,
                    new Vector2(Game.Width - (exitStr.X + 10)+2f, Game.Height - (exitStr.Y + 10)+3f), Color.Black * 0.25f);
                batch.DrawString(Fonts.GameFont, Strings.Exit,
                    new Vector2(Game.Width - (exitStr.X + 10), Game.Height - (exitStr.Y + 10)), Color.White);
            }
            batch.End();
            base.Draw(batch);
        }

        public string Ordinal(int number)
        {
            const string TH = "th";
            string s = number.ToString();

            // Negative and zero have no ordinal representation
            if (number < 1)
            {
                return s;
            }

            number %= 100;
            if ((number >= 11) && (number <= 13))
            {
                return s + TH;
            }

            switch (number % 10)
            {
                case 1: return s + "st";
                case 2: return s + "nd";
                case 3: return s + "rd";
                default: return s + TH;
            }
        }
    }

    public class ScoreComparitor : IComparer<Player>
    {
        public int Compare(Player x, Player y)
        {
            if (x.Total > y.Total)
                return 1;
            if (x.Total < y.Total)
                return -1;
            else
                return 0;
        }
    }
}
