using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.UI;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Arena
{
    public class MapChooser:MenuItem
    {
        public ILevelSelector LevelSelector { get; set; }
        public PlayerSelectArena PlayerSelectArena { get; set; }
        private Rectangle RightRegion => new Rectangle((int)(Bounds.X + Textures.Back.Width + Textures.MapBorder.Width), Bounds.Y + ((Bounds.Height / 2) - (Textures.Back.Height / 2)), Textures.Back.Width, Textures.Back.Height);
        private Rectangle LeftRegion => new Rectangle(Bounds.X, Bounds.Y + ((Bounds.Height / 2) - (Textures.Back.Height / 2)), Textures.Back.Width, Textures.Back.Height);

        private Rectangle EditRegion => new Rectangle((int)(Bounds.Left + (Textures.MapBorder.Width / 2f) - Textures.Back.Width), Bounds.Bottom, (int)Fonts.GameFont.MeasureString(Strings.Edit).X, (int)Fonts.GameFont.MeasureString(Strings.Edit).Y);
        public MapChooser(Golf game, Rectangle bounds, string name, ILevelSelector levelSelector, PlayerSelectArena playerSelectArena) : base(game, bounds, name)
        {
            LevelSelector = levelSelector;
            PlayerSelectArena = playerSelectArena;
            SetBounds();
        }

        public void SetBounds()
        {
            _bounds = new Rectangle((int) Game.Width - ((Textures.MapBorder.Width + (Textures.Back.Width*2))), 100,
                Textures.MapBorder.Width, Textures.MapBorder.Height);
        }
        public override void Update(GameTime gameTime)
        {
            SetBounds();
            if (Selected && Game.KeyboardInput.TypedKey(Keys.Left))
            {
                Game.Sounds.Menu.Play();
                PreviousLevel();
            }
            if (Selected && Game.KeyboardInput.TypedKey(Keys.Right))
            {
                Game.Sounds.Menu.Play();
                NextLevel();
            }

            if (RightRegion.Contains(TapLoaction))
            {
                Game.Sounds.Menu.Play();
                NextLevel();                
            }
            if (LeftRegion.Contains(TapLoaction))
            {
                Game.Sounds.Menu.Play();
                PreviousLevel();
            }
            if (EditRegion.Contains(TapLoaction) && LevelSelector.EditMap!=null)
            {
                Game.Sounds.Menu.Play();
                Game.Arena= new EditArena(Game, PlayerSelectArena,LevelSelector.EditMap);
            }
            TapLoaction = Vector2.Zero;
        }

        private void PreviousLevel()
        {
            LevelSelector.Previous();
        }

        private void NextLevel()
        {
            LevelSelector.Next();
        }

        public override void Draw()
        {
            
            SpriteBatch.Begin();
            {
                
                if (LevelSelector.Count > 1)
                {
                    SpriteBatch.Draw(Textures.Forward, RightRegion,
                        (RightRegion.Contains(Game.UnifiedInput.Location)) ? Color.White*Game.FadeX2 : Color.Gray);
                    SpriteBatch.Draw(Textures.Back, LeftRegion,
                        (LeftRegion.Contains(Game.UnifiedInput.Location)) ? Color.White * Game.FadeX2 : Color.Gray);
                }                
                
                if (LevelSelector.EditMap != null)
                {
                    SpriteBatch.DrawString(Fonts.GameFont, Strings.Edit, new Vector2(EditRegion.X + 2, EditRegion.Y + 3), Color.Black * 0.25f);
                    SpriteBatch.DrawString(Fonts.GameFont,Strings.Edit, new Vector2(EditRegion.X, EditRegion.Y), Color.White);                                        
                }
                SpriteBatch.Draw(Textures.MapBorder, new Vector2(LeftRegion.Right, Bounds.Y), Color.White);
            } 
            SpriteBatch.End();
            DrawMiniMap();
        }

        private void DrawMiniMap()
        {
            var start = new Vector2(LeftRegion.Right + 6, Bounds.Y + 6);
            
            
            
            var i = 0;
            var map = LevelSelector.CurrentMap;
            var max = Math.Max(map.MapWidth, map.MapHeight);
            var s = 226f/(96*max);
            var wh = 96f*s;
            start += new Vector2((226f-(wh*map.MapWidth))/2f, (226f - (wh * map.MapHeight)) / 2f);
            SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone);
            {
                for(var r = 0; r < map.MapHeight; r++)
                {
                    for (var c = 0; c < map.MapWidth; c++)
                    {
                        var location = new Vector2(c*wh, r*wh) + start;
                        Game.TileSet.DrawTile(SpriteBatch, map.Layer1[i], location, Vector2.Zero, null, .9f, s,Color.White);
                        Game.TileSet.DrawTile(SpriteBatch, map.Layer2[i], location, Vector2.Zero, null, .8f, s, Color.White);
                        i++;
                    }
                }
            }
            SpriteBatch.End();

        }
    }
}