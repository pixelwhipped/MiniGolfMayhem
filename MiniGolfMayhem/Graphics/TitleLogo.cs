using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniGolfMayhem.Graphics
{
    public class TitleLogo:IDrawable
    {
        public Golf Golf;
        public Vector2 Center => Golf.Center;

        private Vector2 _titleCenter => Sprites.Title.Center;
        public TitleLogo(Golf game)
        {
            Golf = game;
        }
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(Sprites.Title, new Vector2(Center.X, _titleCenter.Y) - _titleCenter,1f);
            batch.Draw(Sprites.TitleAni, (new Vector2(Center.X, _titleCenter.Y) - _titleCenter) + new Vector2(320, 77),1f);
        }
    }


    public class Clouds : IDrawable
    {
        public class Cloud
        {
            public Texture2D texture;
            public Vector2 location;
            public Vector2 velocity;
            public float rotation;
            public float rSpeed;
        }
        public Golf Golf;
        public Vector2 Center => Golf.Center;
        public TimeSpan NextEvent;
        public List<Cloud> CloudList = new List<Cloud>(); 
        public Clouds(Golf game)
        {
            Golf = game;
            NextEvent = TimeSpan.FromTicks(Golf.Random.Next(9000000));
        }

        public void Update(GameTime gameTime)
        {
            NextEvent -= gameTime.ElapsedGameTime;
            if (NextEvent <= TimeSpan.Zero)
            {
                NextEvent = TimeSpan.FromTicks(Golf.Random.Next(9000000));
                var tex = Golf.Random.Next(2);
                var texture = tex == 0 ? Textures.Cloud1 : tex == 1 ? Textures.Cloud2 : Textures.Cloud3;
                CloudList.Add(new Cloud
                {
                    texture = texture,
                    location = new Vector2(Golf.Width, Golf.Random.Next((int) Golf.Height + texture.Width)),
                    velocity = new Vector2(((float)(Golf.Random.NextDouble() + 0.5) * 3f), ((float)(Golf.Random.NextDouble()+0.25) * 1f)),
                    rotation = (float)Golf.Random.NextDouble(),
                    rSpeed = MathHelper.ToRadians((float)Golf.Random.NextDouble())
                });
            }
            foreach (var c in CloudList)
            {
                c.location -= c.velocity;
                c.rotation += c.rSpeed;
            }
            CloudList.RemoveAll(c => c.location.X < -c.texture.Width);
        }
        public void Draw(SpriteBatch batch)
        {
            batch.Begin();
            foreach (var c in CloudList)
            {
                batch.Draw(c.texture,c.location,null,Color.White,c.rotation,new Vector2(c.texture.Width/2f, c.texture.Height/2f), 1f,SpriteEffects.None,1f);
            }
            batch.End();
        }
    }
}
