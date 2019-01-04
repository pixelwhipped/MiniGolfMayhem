using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Graphics
{
    public class TileSet
    {
        private readonly Texture2D _tileMap;
        private readonly Texture2D _shadows;
        private readonly Texture2D _bridges;

        public string Name { get; set; }
        public Color Color { get; set; }
        public int TileWidth { get; }

        public int TileHeight { get; }

        private readonly Rectangle[] _tiles;
        public TileSet(string name, Color color, Texture2D tileMap, Texture2D shadows, Texture2D bridges, int tileWidth = 96, int tileHeight = 96, int margin = 3, int spacing = 3,
            int rows = 16, int columns = 9)
        {
            _tileMap = tileMap;
            _shadows = shadows;
            _bridges = bridges;
            Name = name;
            Color = color;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            _tiles = new Rectangle[rows*columns];
            var i = 0;
            var yoffset = margin;
            for (var r = 0; r < rows; r++)
            {
                var xoffset = margin;
                for (var c = 0; c < columns; c++)
                {
                    _tiles[i]=new Rectangle(xoffset,yoffset,TileWidth,TileHeight);
                    xoffset += spacing + tileWidth;
                    i++;
                }
                yoffset += spacing + tileHeight;
            }
        }

        public int Count => _tiles.Length;
        public Rectangle this[int i]
        {
            get
            {
                // This indexer is very simple, and just returns or sets
                // the corresponding element from the internal array.
                i = MathHelper.Clamp(i, 0, Count);
                return _tiles[i];
            }
        }

        public void DrawTile(SpriteBatch batch,int tile, Vector2 posistion,Vector2 camera,List<Player> players,float z, float scale = 1f, Color? color = null)
        {
            var alpha = 1f;
            var maxDist = TileWidth;
            if (players != null)
            {
                var center = new Vector2(TileWidth/2f,TileHeight/2f);
                var px = players.Where(x => Vector2.Distance(x.Position+ camera, posistion+ center) < maxDist);
                var enumerable = px as Player[] ?? px.ToArray();
                if (enumerable.Any(p=>p.Layer==1 && p.LastImidiateLayer != 2))//&& !WorldHelpers.IsRamp(p.OnTile)
                {
                    var d = enumerable.Min(p=>Vector2.Distance(p.Position + camera, posistion + center));
                //    var d = ;
                    alpha -= 1-d/maxDist;
                    alpha = MathHelper.Clamp(alpha, 0.25f, 1f);
                    color = color ?? Color.White;
                }
            }
            if (tile == 0) return;
            var s = new Vector2(scale, scale);
            batch.Draw(_tileMap, posistion,null, _tiles[tile-1],null,0,s,color* alpha, SpriteEffects.None, z);
            batch.Draw(_shadows, posistion, null, _tiles[tile - 1], null, 0, s, color* alpha, SpriteEffects.None, z + 0.05f);
            
        }

        public void DrawTileBridges(SpriteBatch batch, int tile, Vector2 posistion, Vector2 camera, List<Player> players,
            float z, float scale = 1f, Color? color = null)
        {
            if (tile == 0) return;
            var s = new Vector2(scale, scale);
            var alpha = 1f;
            batch.Draw(_bridges, posistion, null, _tiles[tile - 1], null, 0, s, color * alpha, SpriteEffects.None, z);
        }
    }
}
