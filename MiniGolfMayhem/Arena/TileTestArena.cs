using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Devices.Bluetooth.Advertisement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniGolfMayhem.GameElements;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Arena
{
    public class TileTestArena: BaseArena
    {
        public int CurrentTile = 1;
        public List<Rail> Rails = new List<Rail>();
        public List<Rail> SpecialRails = new List<Rail>();
        public List<Node> Bouncers = new List<Node>();
        public List<Rectangle> Water = new List<Rectangle>();
        public List<Rectangle> Teleporters = new List<Rectangle>();
        public Elevation Elevation = Elevation.Flat;        
        public List<KeyValuePair<Side, int[]>> Connections = new List<KeyValuePair<Side, int[]>>();
        public TileTestArena(Golf game, BaseArena previousArena) : base(game, previousArena)
        {
            UpdateTileDefs();
        }

        private void UpdateTileDefs()
        {            
            Rails = WorldHelpers.AddWallRails(WorldHelpers.GetLineSegments(CurrentTile),1);
            SpecialRails = WorldHelpers.AddWallRails(WorldHelpers.GetLineSpecialSegments(CurrentTile,new Point(-1,-1)), 1);
            Bouncers = WorldHelpers.AddBouncerNodes(WorldHelpers.GetBouncers(CurrentTile), 1);           
            Water = WorldHelpers.GetWater(CurrentTile);
            Teleporters = WorldHelpers.GetTeleporters(CurrentTile);
            Elevation = WorldHelpers.GetElevation(CurrentTile);
            Connections = WorldHelpers.GetSideConnections(CurrentTile);
        }

        public override void Update(GameTime gameTime)
        {
            if (Game.KeyboardInput.TypedKey(Keys.Up))
            {
                CurrentTile--;
                if (CurrentTile < 1) CurrentTile = Game.TileSet.Count;
                UpdateTileDefs();
            }
            else if (Game.KeyboardInput.TypedKey(Keys.Down))
            {
                CurrentTile++;
                if (CurrentTile >= Game.TileSet.Count ) CurrentTile = 1;
                UpdateTileDefs();
            }
            else if (Game.KeyboardInput.TypedKey(Keys.Escape))
            {
                Game.Arena = PreviousArena;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch batch)
        {
            var offset = Game.Center - new Vector2(Game.TileSet.TileWidth/2f, Game.TileSet.TileHeight/2f);
            foreach (var s in WorldHelpers.GetOpenSides(CurrentTile,1))
            {
                var vec = offset-new Vector2(100,0);
                if (s == Side.Bottom)
                    batch.Draw(Textures.Starter,vec,null,Color.White,0f,Vector2.Zero, 1f,SpriteEffects.None, 1f);
                if (s == Side.Top)
                    batch.Draw(Textures.Starter, vec, null, Color.White, MathHelper.ToRadians(180), Vector2.Zero, 1f, SpriteEffects.None, 1f);
                if (s == Side.Left)
                    batch.Draw(Textures.Starter, vec, null, Color.White, MathHelper.ToRadians(90), Vector2.Zero, 1f, SpriteEffects.None, 1f);
                if (s == Side.Right)
                    batch.Draw(Textures.Starter, vec, null, Color.White, MathHelper.ToRadians(270), Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
            foreach (var s in WorldHelpers.GetOpenSides(CurrentTile, 2))
            {
                var vec = offset - new Vector2(100, 0);
                if (s == Side.Bottom)
                    batch.Draw(Textures.Starter, vec, null, Color.White * 0.5f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                if (s == Side.Top)
                    batch.Draw(Textures.Starter, vec, null, Color.White * 0.5f, MathHelper.ToRadians(180), Vector2.Zero, 1f, SpriteEffects.None, 1f);
                if (s == Side.Left)
                    batch.Draw(Textures.Starter, vec, null, Color.White * 0.5f, MathHelper.ToRadians(90), Vector2.Zero, 1f, SpriteEffects.None, 1f);
                if (s == Side.Right)
                    batch.Draw(Textures.Starter, vec, null, Color.White * 0.5f, MathHelper.ToRadians(270), Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
            Game.TileSet.DrawTile(batch, CurrentTile, offset, Vector2.Zero,null,0.5f);
            Game.TileSet.DrawTileBridges(batch, CurrentTile, offset, Vector2.Zero, null, 0.6f);

            foreach (var l in Rails)
                batch.DrawLine(l.Start + offset, l.End + offset, Color.Red,1);
            foreach (var l in SpecialRails)
                batch.DrawLine(l.Start + offset, l.End + offset, Color.Blue, 1);
            foreach (var w in Water)
                batch.FillRectangle(new Rectangle((int) offset.X + w.X, (int) offset.Y + w.Y, w.Width, w.Height),
                    Color.Red*.5f);
            foreach (var w in Teleporters)
                batch.FillRectangle(new Rectangle((int)offset.X + w.X, (int)offset.Y + w.Y, w.Width, w.Height),
                    Color.Green * .5f);
            foreach (var b in Bouncers)
                batch.DrawNode(b, offset);
            if (Elevation == Elevation.Horizontal)
            {
                batch.Draw(Textures.HorizontalElevation, new Vector2(offset.X + Game.TileSet.TileWidth, offset.Y),
                    Color.White);
            }
            else if (Elevation == Elevation.Vertical)
            {
                batch.Draw(Textures.VerticalElevation, new Vector2(offset.X + Game.TileSet.TileWidth, offset.Y),
                    Color.White);
            }
            offset += new Vector2(0,Game.TileSet.TileHeight);
            batch.DrawString(Fonts.GameFont, "Tile (" + CurrentTile + ")", offset, Color.White);
            DrawTileList(batch, Connections.First(p => p.Key == Side.Top).Value, "T", Vector2.Zero);
            DrawTileList(batch, Connections.First(p => p.Key == Side.Bottom).Value, "B", new Vector2(0, (Game.TileSet.TileWidth * 0.5f)*1));
            DrawTileList(batch, Connections.First(p => p.Key == Side.Left).Value, "L", new Vector2(0, (Game.TileSet.TileWidth * 0.5f) * 2));
            DrawTileList(batch, Connections.First(p => p.Key == Side.Right).Value, "R", new Vector2(0, (Game.TileSet.TileWidth * 0.5f) * 3));
            base.Draw(batch);
        }

        private void DrawTileList(SpriteBatch batch, int[] tiles,string text, Vector2 location)
        {
            var w = Fonts.GameFont.MeasureString(text);
            batch.DrawString(Fonts.GameFont,text,location,Color.White);
            location = new Vector2(location.X + w.X + 10,location.Y);
            foreach (var t in tiles)
            {
                Game.TileSet.DrawTile(batch, t, location,Vector2.Zero, null,.9f,.5f);
                Game.TileSet.DrawTileBridges(batch, t, location, Vector2.Zero, null, .8f,.5f);
                location = new Vector2(location.X + (Game.TileSet.TileWidth * 0.5f),location.Y);
            }
        }
    }
}
