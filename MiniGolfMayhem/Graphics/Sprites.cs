using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MiniGolfMayhem.Graphics
{
    public static class Sprites
    {

        public static Golf Game;



        public static Sprite Title;

        public static Sprite TitleAni;

        public static Sprite Splash;
        public static void LoadContent(Golf game, ContentManager content)
        {
            Game = game;
            Title = new Sprite(Game, 1, new TimeSpan(0, 0, 0, 0, 100), AnimationState.Pause, Textures.Title);
            TitleAni = new Sprite(Game, 4, new TimeSpan(0, 0, 0, 0, 500), AnimationState.LoopRewind, Textures.TitleAni,0,2);
            Splash = new Sprite(Game, 10, new TimeSpan(0, 0, 0, 0, 100), AnimationState.Pause, Textures.Splash, 0, 2);

        }

    public static void Update(GameTime gameTime)
        {
            Title.Update(gameTime);
            TitleAni.Update(gameTime);
            Splash.Update(gameTime);
        }
    }
}