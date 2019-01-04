using System;
using Microsoft.Xna.Framework;

namespace MiniGolfMayhem.Utilities
{
    public class Fader
    {
        public Routine Evnt;
        public float Fade;
        public bool FadeIn;
        public bool Finished;

        public Fader(bool fadeIn, bool start)
        {
            FadeIn = fadeIn;
            if (FadeIn)
            {
                if (start)
                {
                    Finished = false;
                    Fade = 0;
                }
                else {
                    Finished = true;
                    Fade = 1;
                }
            }
            else
            {
                if (start)
                {
                    Finished = false;
                    Fade = 1;
                }
                else
                {
                    Finished = true;
                    Fade = 0;
                }
            }
        }

        public void DoFadeIn(Routine r)
        {
            Finished = false;
            Fade = 0;
            FadeIn = true;
            Evnt = r;
        }

        public void DoFadeOut(Routine r)
        {
            Finished = false;
            Fade = 1;
            FadeIn = false;
            Evnt = r;
        }

        public void Update()
        {
            if (Finished) return;
            Fade = FadeIn ? MathHelper.Clamp(Fade += 0.025f, 0, 1) : MathHelper.Clamp(Fade -= 0.025f, 0, 1);
            if (FadeIn && Math.Abs(Fade - 1f) < float.Epsilon && !Finished)
            {
                Finished = true;
                Evnt?.Invoke();
                Evnt = () => { };
            }
            if (!FadeIn && Math.Abs(Fade - 0f) < float.Epsilon && !Finished)
            {
                Finished = true;
                Evnt?.Invoke();
                Evnt = () => { };
            }
        }
    }
}
