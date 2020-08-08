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
        Texture2D dummy;
        Camera Camera;
        MouseState PreviousMouseState;
        KeyboardState PreviousKeyboardState;
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
            mainfrm.device = GraphicsDevice;
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
            dummy = Content.Load<Texture2D>("gray");
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
            KeyboardState CurrentKeyboardState = Keyboard.GetState();
            MouseState CurrentMouseState = Mouse.GetState();
            float dT = (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;




            if (CurrentMouseState.RightButton == ButtonState.Pressed)
            {
                // this.IsMouseVisible = false;
                //  Volatile.WindowManager.MovingWindow = null;
                //mouselook
                float DX = CurrentMouseState.X - PreviousMouseState.X;
                float DY = CurrentMouseState.Y - PreviousMouseState.Y;
                Camera.Yaw += DX * dT * 6.0f;
                Camera.Pitch -= DY * dT * 6.0f;
                Camera.Pitch = MathHelper.Clamp(Camera.Pitch, -89.0f, 89.0f);


                Mouse.SetPosition(PreviousMouseState.X, PreviousMouseState.Y);
                CurrentMouseState = PreviousMouseState;
                //Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            }




            Vector3 MoveVector = new Vector3(0.05f,0, 0);
            float angle  = -Camera.Yaw-90f;
                
            if (CurrentKeyboardState.IsKeyDown(Keys.S))
                angle+=180f;
            if (CurrentKeyboardState.IsKeyDown(Keys.A))
                angle+=90f;
            if (CurrentKeyboardState.IsKeyDown(Keys.D))
                angle-=90f;
            if (CurrentKeyboardState.IsKeyDown(Keys.W)
                || CurrentKeyboardState.IsKeyDown(Keys.S)
                || CurrentKeyboardState.IsKeyDown(Keys.A)
                || CurrentKeyboardState.IsKeyDown(Keys.D))
            {
                Camera.Position +=
                    Vector3.Transform(
                        MoveVector,
                        Matrix.CreateRotationY(
                            MathHelper.ToRadians(angle)
                            )
                        );
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                Camera.Position += new Vector3(0, 0.05f, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                Camera.Position += new Vector3(0, -0.05f, 0);

            Camera.Pitch = MathHelper.Clamp(Camera.Pitch, -89f, 89f);
            // TODO: Add your update logic here
            bgColor = ProgramState.State.Settings.ViewerBackgroundColor;
            base.Update(gameTime);
            PreviousKeyboardState = CurrentKeyboardState;
            PreviousMouseState = CurrentMouseState;
            if (ProgramState.State.Playing && ProgramState.State.PlayTime < ProgramState.State.CurrentModel.CurrentAnimationLength)
            {
                ProgramState.State.PlayTime += dT;
                if (ProgramState.State.PlayTime > ProgramState.State.CurrentModel.CurrentAnimationLength)
                    ProgramState.State.PlayTime = ProgramState.State.CurrentModel.CurrentAnimationLength;
            }
                
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            float dT = (float)gameTime.ElapsedGameTime.Milliseconds / 1000f;
            Matrix viewMatrix = Camera.GetView();
            Matrix projectionMatrix = Camera.GetProjection(GraphicsDevice);
            ModelEffect.Parameters["xView"].SetValue(viewMatrix);
            ModelEffect.Parameters["xProjection"].SetValue(projectionMatrix);
            ModelEffect.Parameters["xWorld"].SetValue(Matrix.Identity);
            ModelEffect.Parameters["xCamPos"].SetValue(Camera.GetCamVector());
            ModelEffect.Parameters["xModelSkin"].SetValue(dummy);
            ModelEffect.CurrentTechnique = ModelEffect.Techniques["GameModel"]; 
            GraphicsDevice.Clear(bgColor);

            RasterizerState rs = new RasterizerState
            {
                CullMode = CullMode.CullCounterClockwiseFace
            };
           // rs.CullMode = CullMode.None;
             rs.FillMode = ProgramState.State.Settings.WireFrameMode? FillMode.WireFrame:FillMode.Solid;
            GraphicsDevice.RasterizerState = rs;
            if (ProgramState.State.CurrentModel == null)
                return;
            // TODO: Add your drawing code here

            float offset = ProgramState.State.Playing ? ProgramState.State.PlayTime : 0;
            offset = ProgramState.State.PlayTime;
            ProgramState.State.CurrentModel.Render(GraphicsDevice, offset, Matrix.Identity, ModelEffect, false);
            ProgramState.State.CurrentModel.Render(GraphicsDevice, offset, Matrix.Identity, ModelEffect, true);
            base.Draw(gameTime);
        }
    }
}
