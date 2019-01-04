using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MiniGolfMayhem.Graphics
{
    public static class Textures
    {
        public static Texture2D Title;
        public static Texture2D TitleAni;

        public static Texture2D TileSetJungle01;
        public static Texture2D TileSetJungle02;
        public static Texture2D TileSetRoad01;
        public static Texture2D TileSetGeiger01;

        public static Texture2D TileSetBridges;
        public static Texture2D TileSetBridgesRoad;
        public static Texture2D TileSetBridgesGeiger;
        public static Texture2D TileSetShadows;
        public static Texture2D HorizontalElevation;
        public static Texture2D VerticalElevation;

        public static Texture2D GolfBall;

        public static Texture2D Splash;
        public static Texture2D Starter;
        public static Texture2D StarterTop;

        public static Texture2D TopLayer;
        public static Texture2D BottomLayer;
        public static Texture2D TopLayerDelete;
        public static Texture2D BottomLayerDelete;

        public static Texture2D StartBottom;
        public static Texture2D StartTop;
        public static Texture2D StartLeft;
        public static Texture2D StartRight;

        public static Texture2D EndBottom;
        public static Texture2D EndTop;
        public static Texture2D EndLeft;
        public static Texture2D EndRight;

        public static Texture2D Play;
        public static Texture2D Save;

        public static Texture2D Back;
        public static Texture2D Forward;
        public static Texture2D MapBorder;

        public static Texture2D SideBar;

        public static Texture2D Mouse;

        public static Texture2D Cloud1;
        public static Texture2D Cloud2;
        public static Texture2D Cloud3;
        public static void LoadContent(ContentManager content)
        {
            Title = content.Load<Texture2D>("Common/Title");
            TitleAni = content.Load<Texture2D>("Common/TitleAni");

            TileSetJungle01 = content.Load<Texture2D>("Common/JungleTileSet01");
            TileSetJungle02 = content.Load<Texture2D>("Common/JungleTileSet02");
            TileSetRoad01 = content.Load<Texture2D>("Common/RoadTileSet01");
            TileSetGeiger01 = content.Load<Texture2D>("Common/GeigerTileSet01");

            TileSetBridges = content.Load<Texture2D>("Common/Bridges");
            TileSetBridgesRoad = content.Load<Texture2D>("Common/BridgesRoad");
            TileSetBridgesGeiger = content.Load<Texture2D>("Common/BridgesGeiger");
            TileSetShadows = content.Load<Texture2D>("Common/Shadows");
            HorizontalElevation = content.Load<Texture2D>("Common/Horizontal");
            VerticalElevation = content.Load<Texture2D>("Common/Vertical");
            GolfBall = content.Load<Texture2D>("Common/GolfBall");
            Splash = content.Load<Texture2D>("Common/Splash");
            Starter = content.Load<Texture2D>("Common/Starter");
            StarterTop = content.Load<Texture2D>("Common/StarterTop");

            TopLayer = content.Load<Texture2D>("Common/TopLayer");
            BottomLayer = content.Load<Texture2D>("Common/BottomLayer");

            TopLayerDelete = content.Load<Texture2D>("Common/TopLayerDelete");
            BottomLayerDelete = content.Load<Texture2D>("Common/BottomLayerDelete");


            StartTop = content.Load<Texture2D>("Common/StartTop");
            StartBottom = content.Load<Texture2D>("Common/StartBottom");
            StartLeft = content.Load<Texture2D>("Common/StartLeft");
            StartRight = content.Load<Texture2D>("Common/StartRight");

            EndTop = content.Load<Texture2D>("Common/EndTop");
            EndBottom = content.Load<Texture2D>("Common/EndBottom");
            EndLeft = content.Load<Texture2D>("Common/EndLeft");
            EndRight = content.Load<Texture2D>("Common/EndRight");

            Play = content.Load<Texture2D>("Common/Play");
            Save = content.Load<Texture2D>("Common/Save");

            Back = content.Load<Texture2D>("Common/Back");
            Forward = content.Load<Texture2D>("Common/Forward");

            MapBorder = content.Load<Texture2D>("Common/MapBorder");
            SideBar = content.Load<Texture2D>("Common/SideBar");

            Mouse = content.Load<Texture2D>("Common/Mouse");

            Cloud1 = content.Load<Texture2D>("Common/Cloud1");
            Cloud2 = content.Load<Texture2D>("Common/Cloud2");
            Cloud3 = content.Load<Texture2D>("Common/Cloud3");
        }
    }
}