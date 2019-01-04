using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniGolfMayhem.Graphics
{
    public enum AnimationState
    {
        Play,
        Pause,
        Stop,
        Rewind,
        LoopPlay,
        LoopRewind
    }

    public class Sprite
    {
        public AnimationState Animation;
        public int Frame;
        public int Frames;
        public Golf Game;
        public Texture2D Map;
        public Routine OnFinish;
        public TimeSpan Rate;
        public TimeSpan Time;
        public int Margin;
        public int Spacing;

        public Sprite(Golf game, int frames, TimeSpan rate, AnimationState anim, Texture2D map, int margin = 0, int spacing = 0)
        {
            Game = game;
            Map = map;
            Animation = anim;            
            Rate = rate;
            Margin = margin;
            Spacing = spacing;
            Frames = frames;
            OnFinish += () => { };
        }


        public float Width => GetSource().Width;

        public float Height => GetSource().Height;

        public Vector2 Center => new Vector2(Width/2, Height/2);

        public void SetAnimation(AnimationState anim)
        {
            Animation = anim;
        }

        public void ToStart()
        {
            Frame = 0;
        }

        public void ToEnd()
        {
            Frame = Frames - 1;
        }

        public void Update(GameTime gameTime)
        {
            Time += gameTime.ElapsedGameTime;
            if (Time < Rate) return;
            Time = new TimeSpan(0);
            switch (Animation)
            {
                case AnimationState.LoopPlay:
                case AnimationState.Play:
                    Frame++;
                    if (Frame >= Frames)
                    {
                        if (Animation == AnimationState.LoopPlay)
                        {
                            Frame = 0;
                        }
                        else
                        {
                            Frame = Frames - 1;
                            OnFinish();
                        }
                    }
                    break;
                case AnimationState.LoopRewind:
                case AnimationState.Rewind:
                    Frame--;
                    if (Frame < 0)
                    {
                        if (Animation == AnimationState.LoopRewind)
                        {
                            Frame = Frames - 1;
                        }
                        else
                        {
                            Frame = 0;
                            OnFinish();
                        }
                    }
                    break;
            }
        }

        public void Draw(SpriteBatch batch, Vector2 location, float z,Color? color = null)
        {
            batch.Draw(this, location,z, color);
        }

        public Rectangle GetSource()
        {
            var frameWidth = (Map.Width-((Frames-1)*Spacing))/Frames;

            return new Rectangle(Margin + (Frame*Spacing+frameWidth*Frame), Margin, frameWidth, Map.Height);
        }
    }
}