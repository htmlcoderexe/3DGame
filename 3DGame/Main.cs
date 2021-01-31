using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3DGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {

        private void HandleTextInput(object sender, TextInputEventArgs e)
        {
            CurrentScene.WindowManager.HandleTextInput(e.Character, e.Key);
        }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Microsoft.Xna.Framework.Content.ContentManager ContentRef;
        public static GraphicsDevice GraphicsRef;
        public static Interfaces.IGameScene CurrentScene;
        public Main()
        {
            Window.TextInput += HandleTextInput;

            this.IsFixedTimeStep = false;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //CurrentScene = new Scenes.Gameplay();
            CurrentScene = new Scenes.CreateWorld();
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;

            graphics.PreparingDeviceSettings += new System.EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
           // graphics.IsFullScreen = true;
            Window.ClientSizeChanged += new System.EventHandler<System.EventArgs>(Window_ClientSizeChanged);
            //Window.AllowUserResizing = true;
            Window.Position = new Point();
            Window.IsBorderless = true;
            this.IsMouseVisible = true;
            graphics.ApplyChanges();
        }
        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {

            e.GraphicsDeviceInformation.GraphicsProfile = GraphicsProfile.HiDef;
        }

        void Window_ClientSizeChanged(object sender, System.EventArgs e)
        {
            // Make changes to handle the new window size.   
            GraphicsDevice.Viewport = new Viewport(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            CurrentScene.ScreenResized(GraphicsDevice);
            // Mouse.SetPosition(device.Viewport.Width / 2, device.Viewport.Height / 2);
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            GraphicsRef = GraphicsDevice;
            ContentRef = Content;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            CurrentScene.Init(GraphicsDevice,Content);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            CurrentScene.Dispose();
            // TODO: Unload any non ContentManager content here
        }

        public static void ReloadScene()
        {
            CurrentScene.Init(GraphicsRef, ContentRef);
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {



            float seconds = (float)gameTime.ElapsedGameTime.Milliseconds/1000f;
            MouseState ms = Mouse.GetState();
            KeyboardState kb = Keyboard.GetState();
            CurrentScene.HandleInput(GraphicsDevice, ms, kb, seconds);
            CurrentScene.Update( seconds);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            float seconds = (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
            CurrentScene.Render(GraphicsDevice, seconds);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
