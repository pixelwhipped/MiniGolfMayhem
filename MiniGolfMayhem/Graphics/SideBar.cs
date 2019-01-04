using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniGolfMayhem.Graphics
{
    public class SideBar : IDrawable
    {
        public Golf Golf;

        public Color BaseColor = new Color(37, 87, 16);
        public Color Color => Golf.TileSet.Color;

        public float MaxWidth => Golf.Width;
        public float MinWidth => Textures.SideBar.Width;

        private float _targetWidth;

        private float _designWidth;
        public float DesignWidth
        {
            get { return MathHelper.Clamp(_designWidth,MinWidth, MaxWidth); }
            set { _targetWidth = MathHelper.Clamp(value, MinWidth, MaxWidth); }
        }

        public SideBar(Golf game)
        {
            Golf = game;
        }

        public void Update()
        {
            if (Golf._fader.Fade > 0.5f)
            {
                _designWidth = _targetWidth;
            }
            else if (Math.Abs(_designWidth - _targetWidth)<6)
            {
                
            }
            else if (_designWidth <_targetWidth)
            {
                _designWidth += 4;
            }            
            else if (_designWidth > _targetWidth)
            {
                _designWidth-=4;
            }
        }
        public void Draw(SpriteBatch batch)
        {
            var w = (Golf.Width*Golf._fader.Fade);
            batch.Draw(Golf.Pixel, new Rectangle(0, 0, (int)(w + DesignWidth - Textures.SideBar.Width)+1, (int)Golf.Height), BaseColor);
            batch.Draw(Textures.SideBar, new Rectangle((int)(w + DesignWidth - Textures.SideBar.Width), 0, Textures.SideBar.Width,(int)Golf.Height), Color.White);            
        }
    }
}