using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniGolfMayhem.GameElements;
using MiniGolfMayhem.Graphics;

namespace MiniGolfMayhem.UI
{
    public class MenuMapScroller
    {
        public Map MenuMap;
        private Vector2 _offset = Vector2.Zero;
        public Golf Game;

        public MenuMapScroller(Golf game)
        {
            Game = game;
        }
        public void Initialize() => MenuMap = new Map(Game, Game.TileSet, Map.MenuLayer1, Map.MenuLayer2, 20, 20, new Vector2(0, 2), Side.Left, 1, new Vector2(15, 12), Side.Bottom, 1, new List<Player>(), 0,
            m => { });

        public void Update(GameTime gameTime)
        {
            _offset = new Vector2(_offset.X - 1f, _offset.Y - 1f);
            if (Math.Abs(_offset.X) > MenuMap.Width) _offset = new Vector2(0, _offset.Y);
            if (Math.Abs(_offset.Y) > MenuMap.Height) _offset = new Vector2(_offset.X, 0);

            MenuMap.Update(gameTime);
        }
        public void Draw(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone);
            MenuMap.DrawMap(batch, _offset);
            MenuMap.DrawMap(batch, _offset + new Vector2(MenuMap.Width, 0));
            MenuMap.DrawMap(batch, _offset + new Vector2(0, MenuMap.Height));
            MenuMap.DrawMap(batch, _offset + new Vector2(MenuMap.Width, MenuMap.Height));
            batch.End();
        }
    }
}
