using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MiniGolfMayhem.Utilities;

namespace MiniGolfMayhem.Graphics
{
    public static class Fonts
    {
        public static GameFont GameFont;
        public static GameFont GameFontGrey;

        public static void LoadContent(ContentManager content)
        {
            try
            {
                var fontFilePath = "Content\\Fonts\\Font2o.fnt";
                var fontFile = FontLoader.Load(AsyncIO.GetContentStream(fontFilePath));
                var fontTexture = content.Load<Texture2D>("Fonts/font2o_0");
                var fontGreyTexture = content.Load<Texture2D>("Fonts/font2o_1");

                GameFont = new GameFont(fontFile, fontTexture,1f);
                GameFontGrey = new GameFont(fontFile, fontGreyTexture,0.75f);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

        }
    }
}