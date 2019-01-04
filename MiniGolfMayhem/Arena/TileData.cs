using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MiniGolfMayhem.GameElements;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Arena
{
    public class TileData
    {
        private int _topTileNumber;
        private int _bottomTileNumber;
        private Point _location;

        public TileData(Point location, int bottomTileNumber, int topTileNumber, List<TileData> tiles)
        {
            Tiles = tiles;
            Location = location;
            TopTileNumber = topTileNumber;
            BottomTileNumber = bottomTileNumber;
        }

        public List<int> TopTiles = new List<int>();
        public List<int> BottomTiles = new List<int>();

        public int TopTileNumber
        {
            get { return _topTileNumber; }
            set
            {
                var connections = WorldHelpers.GetSideConnections(value);                
                TopTilesToLeft = connections.First(p => p.Key == Side.Left).Value;
                TopTilesToRight = connections.First(p => p.Key == Side.Right).Value;
                TopTilesAbove = connections.First(p => p.Key == Side.Top).Value;
                TopTilesBelow = connections.First(p => p.Key == Side.Bottom).Value;
                TopTiles =
                    new List<int>(TopTilesToLeft).Union(TopTilesToRight)
                        .Union(TopTilesAbove)
                        .Union(TopTilesBelow)
                        .ToList();
                _topTileNumber = value;
            }
        }

        public int BottomTileNumber
        {
            get { return _bottomTileNumber; }
            set
            {
                var connections = WorldHelpers.GetSideConnections(value);
                // var to = Tile.GetBottomTileOptions(TileAbove, TileBelow, TileToLeft, TileToRight);
                BottomTilesToLeft = connections.First(p => p.Key == Side.Left).Value;//connections.First(p => p.Key == Side.Left).Value.Where(p=>to.Contains(p)).ToArray();
                BottomTilesToRight = connections.First(p => p.Key == Side.Right).Value;// connections.First(p => p.Key == Side.Right).Value.Where(p => to.Contains(p)).ToArray(); //new Tile(new Point(Location.X + 1, Location.Y), 0, 0, Tiles).BottomTileOptions;// connections.First(p => p.Key == Side.Right).Value;
                BottomTilesAbove = connections.First(p => p.Key == Side.Top).Value;// connections.First(p => p.Key == Side.Top).Value.Where(p => to.Contains(p)).ToArray();// new Tile(new Point(Location.X, Location.Y - 1), 0, 0, Tiles).BottomTileOptions;//connections.First(p => p.Key == Side.Top).Value;
                BottomTilesBelow = connections.First(p => p.Key == Side.Bottom).Value;// connections.First(p => p.Key == Side.Bottom).Value.Where(p => to.Contains(p)).ToArray(); //new Tile(new Point(Location.X, Location.Y + 1), 0, 0, Tiles).BottomTileOptions;//connections.First(p => p.Key == Side.Bottom).Value;
                BottomTiles =
                    new List<int>(BottomTilesToLeft).Union(BottomTilesToRight)
                        .Union(BottomTilesAbove)
                        .Union(BottomTilesBelow)
                        .ToList();
                _bottomTileNumber = value;
            }
        }

        

        public List<Side>[] OpenSides()
        {
            var sides = new[]
            {
                new List<Side>(), new List<Side>()
            };
            var l1Sides = OpenSides(1);
            var l2Sides = OpenSides(2);
            sides[0].AddRange(l1Sides[0]);
            sides[0].AddRange(l1Sides[1]);
            sides[1].AddRange(l2Sides[0]);
            sides[1].AddRange(l2Sides[1]);
            return sides;
        }
        public List<Side>[] OpenSides(int layer)
        {
            var sides = new[]
            {
                new List<Side>(), new List<Side>()
            };

            var required = WorldHelpers.GetOpenSides(_bottomTileNumber,layer);
            if (required.Contains(Side.Top))
            {
                if (TileAbove == null) sides[0].Add(Side.Top);
                else if (
                    !(BottomTilesAbove.Contains(TileAbove.BottomTileNumber) ||
                      BottomTilesAbove.Contains(TileAbove.TopTileNumber)))
                    sides[0].Add(Side.Top);
            }
            if (required.Contains(Side.Bottom))
            {
                if (TileBelow == null) sides[0].Add(Side.Bottom);
                else if (
                    !(BottomTilesBelow.Contains(TileBelow.BottomTileNumber) ||
                      BottomTilesBelow.Contains(TileBelow.TopTileNumber)))
                    sides[0].Add(Side.Bottom);
            }
            if (required.Contains(Side.Left))
            {
                if (TileToLeft == null) sides[0].Add(Side.Left);
                else if (
                    !(BottomTilesToLeft.Contains(TileToLeft.BottomTileNumber) ||
                      BottomTilesToLeft.Contains(TileToLeft.TopTileNumber)))
                    sides[0].Add(Side.Left);
            }
            if (required.Contains(Side.Right))
            {
                if (TileToRight == null) sides[0].Add(Side.Right);
                else if (
                    !(BottomTilesToRight.Contains(TileToRight.BottomTileNumber) ||
                      BottomTilesToRight.Contains(TileToRight.TopTileNumber)))
                    sides[0].Add(Side.Right);
            }

            
            required = WorldHelpers.GetOpenSides(_topTileNumber,layer);
            if (required.Contains(Side.Top))
            {
                if (TileAbove == null) sides[1].Add(Side.Top);
                else if (!(TopTilesAbove.Contains(TileAbove.TopTileNumber) || TopTilesAbove.Contains(TileAbove.BottomTileNumber)))
                    sides[1].Add(Side.Top);
            }
            if (required.Contains(Side.Bottom))
            {
                if (TileBelow == null) sides[1].Add(Side.Bottom);
                else if (!(TopTilesBelow.Contains(TileBelow.TopTileNumber) || TopTilesBelow.Contains(TileBelow.BottomTileNumber)))
                    sides[1].Add(Side.Bottom);
            }
            if (required.Contains(Side.Left))
            {
                if (TileToLeft == null) sides[1].Add(Side.Left);
                else if (!(TopTilesToLeft.Contains(TileToLeft.TopTileNumber) || TopTilesToLeft.Contains(TileToLeft.BottomTileNumber)))
                    sides[1].Add(Side.Left);
            }
            if (required.Contains(Side.Right))
            {
                if (TileToRight == null) sides[1].Add(Side.Right);
                else if (!(TopTilesToRight.Contains(TileToRight.TopTileNumber) || TopTilesToRight.Contains(TileToRight.BottomTileNumber)))
                    sides[1].Add(Side.Right);
            }            
            return sides;
        }

        public List<TileData> Tiles { get; set; }

        public int MinX => Tiles.Min(p => p.Location.X);
        public int MinY => Tiles.Min(p => p.Location.Y);

        public int MaxX => Tiles.Max(p => p.Location.X);
        public int MaxY => Tiles.Max(p => p.Location.Y);

        public int Rows => (int)MathHelper.Distance(MinY, MaxY)+1;
        public int Columns => (int)MathHelper.Distance(MinX, MaxX)+1;

        public Point Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public Point MapLocation => new Point(Location.X + Math.Abs(MinX), Location.Y + Math.Abs(MinY));

        public TileData TileToLeft
        {
            get { return Tiles.FirstOrDefault(p => p.Location == new Point(Location.X - 1, Location.Y)); }
            set { ReplaceOrAddTile(value); }
        }

        public int[] TopTilesToLeft { get; set; }
        public int[] BottomTilesToLeft { get; set; }

        public TileData TileToRight
        {
            get { return Tiles.FirstOrDefault(p => p.Location == new Point(Location.X + 1, Location.Y)); }
            set { ReplaceOrAddTile(value); }
        }
        public int[] TopTilesToRight { get; set; }
        public int[] BottomTilesToRight { get; set; }

        public TileData TileAbove
        {
            get { return Tiles.FirstOrDefault(p => p.Location == new Point(Location.X, Location.Y - 1)); }
            set { ReplaceOrAddTile(value); }
        }
        public int[] TopTilesAbove { get; set; }
        public int[] BottomTilesAbove { get; set; }

        public TileData TileBelow
        {
            get { return Tiles.FirstOrDefault(p => p.Location == new Point(Location.X, Location.Y + 1)); }
            set { ReplaceOrAddTile(value); }
        }
        public int[] TopTilesBelow { get; set; }
        public int[] BottomTilesBelow { get; set; }
        private void ReplaceOrAddTile(TileData tile)
        {
            var i = Tiles.FindIndex(p => p.Location == tile.Location);
            if (i > 0)
            {
                Tiles[i] = tile;
            }
            else
            {
                Tiles.Add(tile);
            }
        }

        public void NextBottomTile()
        {
            if (BottomTileNumber == 0)
            {
                BottomTileNumber = WorldHelpers.BottomTiles[0];
            }
            else
            {
                var i = Array.IndexOf(WorldHelpers.BottomTiles, _bottomTileNumber);
                i++;
                if (i >= WorldHelpers.BottomTiles.Length) i = 0;
                BottomTileNumber = WorldHelpers.BottomTiles[i];
            }
        }

        public void NextTopTile()
        {
            if (TopTileNumber == 0)
            {
                TopTileNumber = WorldHelpers.TopTiles[0];
            }
            else
            {
                var i = Array.IndexOf(WorldHelpers.TopTiles, _topTileNumber);
                i++;
                if (i >= WorldHelpers.TopTiles.Length) i = 0;
                TopTileNumber = WorldHelpers.TopTiles[i];
            }
        }
    }
}