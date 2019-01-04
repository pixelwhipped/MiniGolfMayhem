using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Graphics
{
    public class MapLayer
    {
        public int[,] Map;
        public List<Node>[,] Bouncers;
        public List<Rail>[,] Walls;
        public List<Rectangle>[,] Water;

        public List<KeyValuePair<Rectangle, List<Rectangle>>>[,] Teleports;
        public Elevation[,] Elevations;

        public int Width;
        public int Height;
        public int Layer;
        public MapLayer(int[] map, int width, int height,int layer)
        {
            Layer = layer;
            Map = new int[width,height];
            Bouncers = new List<Node>[width, height];
            Walls = new List<Rail>[width, height];
            Water = new List<Rectangle>[width, height];
            Teleports = new List<KeyValuePair<Rectangle, List<Rectangle>>>[width,height];
            Elevations = new Elevation[width,height];
            var i = 0;
            for (int r = 0; r < height; r++)
            {
                for (var c = 0; c < width; c++)
                {
                    Map[c, r] = map[i];
                    Bouncers[c, r] = WorldHelpers.AddBouncerNodes(WorldHelpers.GetBouncers(map[i]),layer);
                    Walls[c, r] = WorldHelpers.AddWallRails(WorldHelpers.GetLineSegments(map[i]), layer);
                    Water[c, r] = WorldHelpers.GetWater(map[i]);
                    Teleports[c, r] = WorldHelpers.GetTeleports(map[i]);                   
                    Elevations[c, r] = WorldHelpers.GetElevation(map[i]);
                    if (Layer == 2 && Elevations[c, r]!=Elevation.Flat)throw new Exception("Yeah nah we can only have elevations on layer 1");
                        i++;
                }
            }
            Width = width;
            Height = height;
        }

        public int GetTile(int x, int y) => Map[x, y];
        public List<Node> GetBouncers(int x, int y) => Bouncers[x, y];
        public List<Rail> GetWalls(int x, int y) => Walls[x, y];
        public List<Rectangle> GetWater(int x, int y) => Water[x, y]; 
        public Elevation GetElevation(int x, int y) => Elevations[x, y];
        public List<KeyValuePair<Rectangle, List<Rectangle>>> GetTeleports(int x, int y) => Teleports[x, y];
    }
}
