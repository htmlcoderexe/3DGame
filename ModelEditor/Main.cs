using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ModelEditor
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameModel.Model MainModel;
        Color bgColor = Color.CornflowerBlue;
        SettingsFrm f;
        MainAppFrm mainfrm;
        Effect ModelEffect;
        Camera Camera;
        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Camera = new Camera();
            Camera.Position = new Vector3(0f, 1f, 0f);
            Camera.Pitch = -67f;
            Camera.Yaw = -90f;
            Content.RootDirectory = "Content";
            mainfrm = new MainAppFrm();
            mainfrm.Show();
          //  f = new SettingsFrm();
            //f.Show();
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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ModelEffect= Content.Load<Effect>("legacy");
           // mainfrm.State.CurrentModel = new GameModel.Model();
            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Camera.Pitch++;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Camera.Pitch--;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Camera.Yaw++;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Camera.Yaw--;

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                Camera.Position += new Vector3();

            Camera.Pitch = MathHelper.Clamp(Camera.Pitch, -89f, 89f);
            // TODO: Add your update logic here
            bgColor = mainfrm.State.Settings.ViewerBackgroundColor;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Matrix viewMatrix = Camera.GetView();
            Matrix projectionMatrix = Camera.GetProjection(GraphicsDevice);
            ModelEffect.Parameters["xView"].SetValue(viewMatrix);
            ModelEffect.Parameters["xProjection"].SetValue(projectionMatrix);
            ModelEffect.Parameters["xWorld"].SetValue(Matrix.Identity);
            ModelEffect.Parameters["xCamPos"].SetValue(Camera.GetCamVector());
            ModelEffect.CurrentTechnique = ModelEffect.Techniques["GameModel"]; 
            GraphicsDevice.Clear(bgColor);

            RasterizerState rs = new RasterizerState
            {
                CullMode = CullMode.CullCounterClockwiseFace
            };
            rs.CullMode = CullMode.None;
           //  rs.FillMode = FillMode.WireFrame;
            GraphicsDevice.RasterizerState = rs;
            if (mainfrm.State.CurrentModel == null)
                return;
            // TODO: Add your drawing code here
            mainfrm.State.CurrentModel.Render(GraphicsDevice, 1, Matrix.Identity, ModelEffect, false);
            mainfrm.State.CurrentModel.Render(GraphicsDevice, 1, Matrix.Identity, ModelEffect, true);
            base.Draw(gameTime);
        }
    }
}
