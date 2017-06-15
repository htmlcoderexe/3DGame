using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3DGame.Scenes
{
    class Gameplay : Interfaces.IGameScene
    {
        public KeyboardState PreviousKbState { get; set; }

        public MouseState PreviousMouseState { get; set; }

        public Dictionary<string, Texture2D> Textures;

        float RenderTime = 0.0f;

        public GameObjects.World World;
        public Effect TerrainEffect;
        public BasicEffect ModelEffect;
        public SpriteBatch b;
        private int counter;
        public RenderTarget2D Screen { get; set; }
        public Vector3 CamPosition;
        public float Accel = 0;
        private RenderTarget2D RefractionMap { get; set; }
        private RenderTarget2D ReflectionMap { get; set; }
        //  public System.Threading.Thread QThread;

            private void TakeScreenshot(GraphicsDevice device)
        {
            string fn = "screenshots\\";
            DateTime now = DateTime.Now;
            
            fn += now.ToShortDateString() + "-" + now.ToShortTimeString() + counter + ".png";
            System.IO.FileStream s = new System.IO.FileStream(fn, System.IO.FileMode.Create);
            Screen.SaveAsPng(s,device.PresentationParameters.BackBufferWidth,device.PresentationParameters.BackBufferHeight);
            s.Close();
            counter++;
        }
        public void HandleInput(GraphicsDevice device, MouseState mouse, KeyboardState kb, float dT)
        {

            if (kb.IsKeyDown(Keys.F12) && PreviousKbState.IsKeyUp(Keys.F12))
            {
                TakeScreenshot(device);
            }
            if (kb.IsKeyDown(Keys.Space) )//Diarrhea mode!! && PreviousKbState.IsKeyUp(Keys.Space))
            {
                GameObjects.MapEntity e = new GameObjects.MapEntity();
                e.Position = World.Camera.Position;
                World.Entities.Add(e);
            }
            if (kb.IsKeyDown(Keys.W))
            {
                
                Accel += 0.5f;
            }

            if (kb.IsKeyDown(Keys.S))
            {
                
                Accel -= 0.5f;
            }

            if (kb.IsKeyUp(Keys.W) && kb.IsKeyUp(Keys.S))
                Accel -=0.25f*(Accel>=0?1:-1);
            Vector3 mv= -World.Camera.GetMoveVector() * dT * Accel;
            if (mv.Length() > 1f)
            {
                mv.X += 0.0f;
            }
            else if(Accel>20f)
            {
                mv.Y += 0.0f;
            }
            World.Camera.Position += mv;




            if (mouse.RightButton == ButtonState.Pressed)
            {
               // this.IsMouseVisible = false;
              //  Volatile.WindowManager.MovingWindow = null;
                //mouselook
                float DX = mouse.X - PreviousMouseState.X;
                float DY = mouse.Y - PreviousMouseState.Y;
                World.Camera.Yaw += DX * dT*3.0f;
                World.Camera.Pitch -= DY * dT*3.0f;
                World.Camera.Pitch = MathHelper.Clamp(World.Camera.Pitch, -89.0f, 89.0f);

               
                Mouse.SetPosition(device.Viewport.Width / 2, device.Viewport.Height / 2);
            }


            else
            {
                //this.IsMouseVisible = true;



            }
            PreviousKbState = Keyboard.GetState();
            PreviousMouseState = Mouse.GetState();
        }

        public void ScreenResized(GraphicsDevice device)
        {
            int ScreenWidth = device.PresentationParameters.BackBufferWidth;
            int ScreenHeight = device.PresentationParameters.BackBufferHeight;
            ReflectionMap = new RenderTarget2D(device, ScreenWidth, ScreenHeight, false, device.PresentationParameters.BackBufferFormat, device.PresentationParameters.DepthStencilFormat);
            RefractionMap = new RenderTarget2D(device, ScreenWidth, ScreenHeight, false, device.PresentationParameters.BackBufferFormat, device.PresentationParameters.DepthStencilFormat);
            Screen = new RenderTarget2D(device, ScreenWidth, ScreenHeight, false, device.PresentationParameters.BackBufferFormat, device.PresentationParameters.DepthStencilFormat);
            b = new SpriteBatch(device);
        }

        public void Init(GraphicsDevice device, ContentManager content)
        {
            World = new GameObjects.World(device);
            Textures = new Dictionary<string, Texture2D>();
            Textures["grass_overworld"]= Texture2D.FromStream(device, new System.IO.FileStream("graphics\\grassB.png", System.IO.FileMode.Open));
            Textures["waterbump"] = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\waterbump.jpg", System.IO.FileMode.Open));
            Textures["rock"] = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\rock.jpg", System.IO.FileMode.Open));
            TerrainEffect = content.Load<Effect>("legacy");
            World.Terrain.TerrainEffect = TerrainEffect;
            World.Camera = new GameObjects.Camera();
            World.Camera.Position= new Vector3(32, 32, 32);
            World.Terrain.QThread = new Thread(new ThreadStart(ProcessQ));
            World.Terrain.QThread.Start();
            ScreenResized(device);
        }
        public static Plane CreatePlane(float height, Vector3 planeNormalDirection, Matrix currentViewMatrix, bool clipSide, Matrix projectionMatrix)
        {
            planeNormalDirection.Normalize();
            Vector4 planeCoeffs = new Vector4(planeNormalDirection, height);
            if (clipSide)
                planeCoeffs *= -1;

            Matrix worldViewProjection = currentViewMatrix * projectionMatrix;
            Matrix inverseWorldViewProjection = Matrix.Invert(worldViewProjection);
            inverseWorldViewProjection = Matrix.Transpose(inverseWorldViewProjection);
            inverseWorldViewProjection = Matrix.Identity;
            planeCoeffs = Vector4.Transform(planeCoeffs, inverseWorldViewProjection);
            Plane finalPlane = new Plane(planeCoeffs);

            return finalPlane;
        }
        public void Render(GraphicsDevice device, float dT)
        {
            RenderTime += dT;
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.CullCounterClockwiseFace;
        //   rs.FillMode = FillMode.WireFrame;
            device.RasterizerState = rs;
            Color skyColor = new Color(40, 100, 255);
            Matrix viewMatrix = World.Camera.GetView();
            Matrix projectionMatrix = World.Camera.GetProjection(device);
            Matrix reflectedView = World.Camera.GetReflectedView(device, World.Terrain.WaterHeight - 0.2f);
            TerrainEffect.Parameters["xGrass"].SetValue(Textures["grass_overworld"]);
            TerrainEffect.Parameters["xRock"].SetValue(Textures["rock"]);
            TerrainEffect.Parameters["xView"].SetValue(reflectedView);
            TerrainEffect.Parameters["xReflectionView"].SetValue(reflectedView);
            TerrainEffect.Parameters["xProjection"].SetValue(projectionMatrix);
            TerrainEffect.Parameters["xCamPos"].SetValue((Vector3)World.Camera.Position.Truncate());
            TerrainEffect.Parameters["xFog"].SetValue(false);


            Plane refractionplane = CreatePlane(World.Terrain.WaterHeight -0.2f, new Vector3(0, 1, 0), viewMatrix, false, projectionMatrix);

            TerrainEffect.Parameters["ClipPlane0"].SetValue(new Vector4(refractionplane.Normal, refractionplane.D));
            TerrainEffect.Parameters["Clipping"].SetValue(true);

            device.SetRenderTarget(ReflectionMap);
            device.Clear(skyColor);
            World.Render(device, dT,Vector2.Zero);
           // device.Clear(Color.CornflowerBlue);

            device.SetRenderTarget(Screen);

            TerrainEffect.Parameters["xView"].SetValue(viewMatrix);
            refractionplane = CreatePlane(-World.Terrain.WaterHeight-0.2f, new Vector3(0, 1, 0), Matrix.Identity, true, projectionMatrix);

            TerrainEffect.Parameters["ClipPlane0"].SetValue(new Vector4(refractionplane.Normal, refractionplane.D));
            TerrainEffect.Parameters["Clipping"].SetValue(true);
             TerrainEffect.Parameters["xFog"].SetValue(true);
            device.SetRenderTarget(RefractionMap);
            device.Clear(skyColor);
            World.Render(device, dT, Vector2.Zero);
            //device.Clear(Color.CornflowerBlue);

            device.SetRenderTarget(Screen);
            device.Clear(skyColor);

            TerrainEffect.Parameters["Clipping"].SetValue(false);
            TerrainEffect.Parameters["xFog"].SetValue(false);
            device.SetRenderTarget(Screen);
            World.Render(device, dT, Vector2.Zero);


            TerrainEffect.Parameters["xReflectionMap"].SetValue(ReflectionMap);
            TerrainEffect.Parameters["xRefractionMap"].SetValue(RefractionMap);


            TerrainEffect.Parameters["xWaveLength"].SetValue(2.0f);
            TerrainEffect.Parameters["xWaveHeight"].SetValue(0.4f);
            TerrainEffect.Parameters["xWaterBumpMap"].SetValue(Textures["waterbump"]);

            TerrainEffect.Parameters["xTime"].SetValue(RenderTime / 100.0f);
            TerrainEffect.Parameters["xWindForce"].SetValue(2.0f);
            TerrainEffect.Parameters["xWindDirection"].SetValue(new Vector3(0, 1, 0));
            TerrainEffect.CurrentTechnique = TerrainEffect.Techniques["Water"];
            World.Terrain.DrawWater(device, dT,World.Camera.Position.Reference());


            device.SetRenderTarget(null);
            b.Begin();
            b.Draw(Screen, Vector2.Zero, Color.White);
            b.End();
            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;
        }

        public void Update(float dT)
        {
     
            World.Update(dT);
        }


        private void ProcessQ()
        {
            while (true)
            {
                //World.Player.UpdateRenderPos();
                World.Terrain.BorderEvent(World.Camera.Position.BX, World.Camera.Position.BY);
                World.Terrain.ProcessQueue();
                System.Threading.Thread.Sleep(1);
                //  Utility.Trace(World.Player.

                // Program.DW.blockstackmap.Text = "";

            }

        }

        
    }
}
