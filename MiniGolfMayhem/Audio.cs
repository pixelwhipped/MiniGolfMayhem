using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace MiniGolfMayhem
{
    public class Sounds
    {
        public SoundEffectInstance ShortIntro;
        public SoundEffectInstance Bounce;
        public SoundEffectInstance DropBounce;
        public SoundEffectInstance Hole;
        public SoundEffectInstance Splash;
        public SoundEffectInstance Swing;
        public SoundEffectInstance Bouncing;
        public SoundEffectInstance Menu;

        private float _musicFade = 1f;
        private bool _fadeIn;
        private bool _fadeOut;

        public bool FadeIn
        {
            get { return _fadeIn; }
            set
            {
                if (value) _fadeOut = false;
                _fadeIn = value; }
        }

        public bool FadeOut
        {
            get { return _fadeOut; }
            set
            {
                if (value) _fadeIn = false;
                _fadeOut = value;
            }
        }

        public float MusicVolume { get; set; }
        public float EffectVolume { get; set; }

        public Golf Game { get; }
        public ContentManager Content => Game.Content;
        public Sounds(Golf game)
        {
            Game = game;
            Bounce = Content.Load<SoundEffect>("Audio\\Bounce").CreateInstance();
            ShortIntro = Content.Load<SoundEffect>("Audio\\ShortIntro").CreateInstance();
            DropBounce = Content.Load<SoundEffect>("Audio\\DropBounce").CreateInstance();
            Hole = Content.Load<SoundEffect>("Audio\\Hole").CreateInstance();
            Splash = Content.Load<SoundEffect>("Audio\\Splash").CreateInstance();
            Swing = Content.Load<SoundEffect>("Audio\\Swing").CreateInstance();
            Bouncing = Content.Load<SoundEffect>("Audio\\Bouncing").CreateInstance();
            Menu = Content.Load<SoundEffect>("Audio\\Menu").CreateInstance();
            SetEffectVolume(Game.GameSettings.EffectVolume);
            SetMusicVolume(Game.GameSettings.MusicVolume);
        }

        public void SetEffectVolume(float volume)
        {
            EffectVolume = volume;
            Bounce.Volume = MathHelper.Clamp(volume, 0f, 1f);
            DropBounce.Volume = MathHelper.Clamp(volume, 0f, 1f);
            Hole.Volume = MathHelper.Clamp(volume, 0f, 1f);
            Splash.Volume = MathHelper.Clamp(volume, 0f, 1f);
            Swing.Volume = MathHelper.Clamp(volume, 0f, 1f);
            Bouncing.Volume = MathHelper.Clamp(volume, 0f, 1f);
            Menu.Volume = MathHelper.Clamp(volume, 0f, 1f);
        }

        public void SetMusicVolume(float volume)
        {
            MusicVolume = volume;
            ShortIntro.Volume = MathHelper.Clamp(volume*_musicFade, 0f, 1f);
        }

        public void Update()
        {
            if (FadeIn)
            {
                _musicFade = MathHelper.Clamp(_musicFade + 0.1f,0f,1f);
                SetMusicVolume(MusicVolume);
                if (Math.Abs(_musicFade - 1f) < 0.001f) FadeIn = false;
            }else if (FadeOut)
            {
                _musicFade = MathHelper.Clamp(_musicFade - 0.1f, 0f, 1f);
                if (Math.Abs(_musicFade) < 0.001f) FadeOut = false;
                SetMusicVolume(MusicVolume);
            }
        }
    }
}