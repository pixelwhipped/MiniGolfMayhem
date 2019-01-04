using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniGolfMayhem.GameElements;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Arena
{
    public class BounceTest : BaseArena
    {
        public static Side StartType = Side.Top;
        public static Vector2 StartTile = new Vector2(2, 1);
        public const int MapWidth = 14;
        public const int MapHeight = 12;
        #region Map Layers
        // 27 74
        public static MapLayer Layer1 = new MapLayer(new[]
        {
            0,33,0,0,0,0,0,0,0,0,0,0,0,0,
            0,54,0,1,2,3,0,0,0,0,0,0,0,0,
            0,19,20,21,0,10,0,0,1,22,23,3,0,0,
            0,1,75,3,0,4,0,0,77,0,43,59,42,51,
            0,74,0,37,39,20,47,22,12,23,73,46,47,38,
            0,19,76,21,0,13,70,0,78,8,50,9,12,52,
            0,0,0,0,0,19,68,60,46,56,29,55,21,0,
            0,0,0,0,0,57,64,58,0,17,49,18,0,0,
            0,0,0,0,0,5,6,7,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,14,15,16,0,0,0,0,0,0,
            0,0,0,0,0,66,49,67,0,0,0,0,0,0
        },MapWidth, MapHeight, 1);
        public static MapLayer Layer2 = new MapLayer(new[]
        {
                        0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                        0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                        0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                        0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                        0,0,0,0,0,82,0,0,91,0,0,0,0,0,
                        0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                        0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                        0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                        0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                        0,0,0,0,0,83,84,85,0,0,0,0,0,0,
                        0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                        0,0,0,0,0,0,0,0,0,0,0,0,0,0
        }, MapWidth, MapHeight, 2);
        #endregion

        /*public static MapLayer Layer1 = new MapLayer(new[]
        {
            0, 33, 0,
            0, 27, 0,
            57, 64, 58,
            28, 29, 30,
            28, 29, 30,
            17, 65, 18,
            0, 4, 0,
            0, 0, 0,
            0, 13, 0,
            0, 52, 0
        }, 3, 10, 1);
        public static MapLayer Layer2 = new MapLayer(new[]
        {
            0,0,0,
            0,0,0,
            0,0,0,
            0,0,0,
            0,0,0,
            0,0,0,
            0,0,0,
            0,82,0,
            0,0,0,
            0,0,0
        }, 3, 10, 2);*/
        public BounceTest(Golf game,BaseArena previousArena) : base(game, previousArena)
        {
            Initialise();
        }

        public sealed override void Initialise()
        {
            Map = new Map(Game, Game.TileSet, Layer1, Layer2, MapWidth, MapHeight, new Vector2(1,0),Side.Top,1, new Vector2(1, 2), Side.Right, 1, new List<Player> {new Player(Game,0,"Ben")} ,0,m=> Game.Arena = Game.MenuArena);
            base.Initialise();
        }

        public Map Map { get; set; }

        public override void Update(GameTime gameTime)
        {
            if (Game.KeyboardInput.TypedKey(Keys.Escape))
            {
                Game.Arena = PreviousArena;
            }
            else
            {
                Map.Update(gameTime);
            }
            if (Game.KeyboardInput.Pressed(Keys.Up))
            {
                Map.Players[Map.CurrentPlayer].Acceleration += new Vector2(0, -0.05f);
            }
            if (Game.KeyboardInput.Pressed(Keys.Down))
            {
                Map.Players[Map.CurrentPlayer].Acceleration += new Vector2(0, 0.05f);
            }
            if (Game.KeyboardInput.Pressed(Keys.Left))
            {
                Map.Players[Map.CurrentPlayer].Acceleration += new Vector2(-0.05f, 0);
            }
            if (Game.KeyboardInput.Pressed(Keys.Right))
            {
                Map.Players[Map.CurrentPlayer].Acceleration += new Vector2(0.05f, 0);
            }
            
           base.Update(gameTime);
        }

        public override void Draw(SpriteBatch batch)
        {
            Map?.DrawMap(batch,Vector2.One);
            base.Draw(batch);
        }
    }
}
