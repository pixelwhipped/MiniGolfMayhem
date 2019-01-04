using System;
using Windows.UI.ViewManagement;
using MiniGolfMayhem.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniGolfMayhem.Arena;
using MiniGolfMayhem.GameElements;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.Levels;
using MiniGolfMayhem.UI;
using MiniGolfMayhem.Utilities;
using KeyboardInput = MiniGolfMayhem.Input.KeyboardInput;

namespace MiniGolfMayhem
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Golf : Game
    {
        public GraphicsDeviceManager Graphics;
        public float Width => Graphics.GraphicsDevice.Viewport.Width;
        public float Height => Graphics.GraphicsDevice.Viewport.Height;
        public Vector2 Center => new Vector2(Width / 2f, Height / 2f);
        public Rectangle Bounds => new Rectangle(0, 0, (int)Width, (int)Height);
        public SpriteBatch SpriteBatch;
        public static Random Random = new Random();     
        public static Texture2D Pixel;

        public TitleLogo Logo;

        public SideBar SideBar;

        public Clouds Clouds;
        public Sounds Sounds { get; private set; }

        public TouchInput TouchInput { get; private set; }
        public MouseInput MouseInput { get; private set; }
        public KeyboardInput KeyboardInput { get; private set; }
        public UnifiedInput UnifiedInput { get; private set; }

        public CustomGameStorage CustomGameStorage { get; set; }

        public GameSettings GameSettings { get; set; }

        public readonly Fader _fader;        
        private BaseArena _arena;

        private bool _fade1Xin;
        private bool _fade2Xin;
        public bool Transitioning;
        public BaseArena Arena
        {
            get { return _arena; }
            set
            {
                if (Transitioning) return;
                Transitioning = true;
                _fader.DoFadeIn(() =>
                {
                    if (value is MenuArena)
                    {
                        MenuArena = new MenuArena(this);
                        value = MenuArena;
                        Sounds.FadeIn = true;
                        Sounds.ShortIntro.Play();
                    }
                    else Sounds.FadeOut = true;
                    _arena = value;
                     Transitioning = false;
                    _fader.DoFadeOut(() => { });
                });                
            }
        }

        public float FadeX2 { get; set; }

        public float FadeX1 { get; set; }

        public PauseArena PauseArena;

        public MenuArena MenuArena;

        public MenuMapScroller MenuMap;

        // private bool js = false;
        public TileSet TileSet01;
        public TileSet TileSet02;
        public TileSet TileSet03;
        public TileSet TileSet04;

        // public TileSet JungleTileSet02;
        public TileSet TileSet { get; set; }

        public TileSet[] TileSets { get; set; }

        public Golf()
        {
            
            Graphics = new GraphicsDeviceManager(this);
          //  Graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            Branding.BackgroundColor = new Color(37, 92, 64);
            CustomGameStorage = new CustomGameStorage();
            GameSettings = CustomGameStorage.LoadSettings();
            MenuMap = new MenuMapScroller(this);
            _fader = new Fader(false,false);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            
            IsMouseVisible = false;
            FadeX1 = 1f;
            FadeX2 = 1f;
            _fade1Xin = false;
            _fade2Xin = false;           
             
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            Pixel.SetData(new[] { Color.White });
            Logo = new TitleLogo(this);
            SideBar = new SideBar(this);

            MouseInput = new MouseInput();
            TouchInput = new TouchInput();
            UnifiedInput = new UnifiedInput(this);
            KeyboardInput = new KeyboardInput(this);
            UnifiedInput.TapListeners.Add(t => Arena?.OnTap(t));

            Textures.LoadContent(Content);
            Sprites.LoadContent(this,Content);
            Fonts.LoadContent(Content);
            Sounds = new Sounds(this);

            TileSet01 = new TileSet(Strings.Park,new Color(37f/255f,92f/255f,64f/255f),Textures.TileSetJungle01, Textures.TileSetShadows,Textures.TileSetBridges);
            TileSet02 = new TileSet(Strings.Jungle, new Color(13f / 255f, 69f / 255f, 17f / 255f),Textures.TileSetJungle02, Textures.TileSetShadows, Textures.TileSetBridges);
            TileSet03 = new TileSet(Strings.RoadBlock, new Color(101f / 255f, 97f / 255f, 96f / 255f), Textures.TileSetRoad01, Textures.TileSetShadows, Textures.TileSetBridgesRoad);
            TileSet04 = new TileSet(Strings.Geiger, new Color(39f / 255f, 37f / 255f, 92f / 255f), Textures.TileSetGeiger01, Textures.TileSetShadows, Textures.TileSetBridgesGeiger);


            TileSets = new TileSet[] { TileSet01 , TileSet02, TileSet03, TileSet04};
            TileSet = TileSets[GameSettings.TileSet];

            MenuMap.Initialize();

            MenuArena = new MenuArena(this);
            PauseArena = new PauseArena(this,MenuArena);
                       
            _arena = MenuArena;
            _arena.Initialise();
          }
        

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            SideBar.Update();
          //  Clouds.Update(gameTime);
            TouchInput.Update(gameTime);
            MouseInput.Update(gameTime);
            UnifiedInput.Update(gameTime);
            KeyboardInput.Update(gameTime);
            
            UpdateFaders(gameTime);
            MenuMap.Update(gameTime);
            Arena.Update(gameTime);
            Sprites.Update(gameTime);
            Sounds.Update();
            base.Update(gameTime);
        }

        private void UpdateFaders(GameTime gameTime)
        {
            _fader.Update();
            if (_fade1Xin)
            {
                FadeX1 += 0.025f;
                if (FadeX1 >= 1f) _fade1Xin = false;
            }
            else
            {
                FadeX1 -= 0.025f;
                if (FadeX1 <= .5f) _fade1Xin = true;
            }
            if (_fade2Xin)
            {
                FadeX2 += 0.05f;
                if (FadeX2 >= 1f) _fade2Xin = false;
            }
            else
            {
                FadeX2 -= 0.05f;
                if (FadeX2 <= 0.5f) _fade2Xin = true;
            }
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(TileSet.Color);
            

            Arena.Draw(SpriteBatch);
            KeyboardInput.Draw(SpriteBatch);
            SpriteBatch.End();
            SpriteBatch.Begin();
            SpriteBatch.Draw(Pixel, Vector2.Zero, new Rectangle(0, 0, (int) Width, (int) Height), Color.White * _fader.Fade);
            SpriteBatch.Draw(Textures.Mouse,UnifiedInput.Location,Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        
    }
}
