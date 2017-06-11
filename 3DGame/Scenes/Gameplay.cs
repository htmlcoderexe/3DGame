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

        public GameObjects.Map Map;
        public GameObjects.Camera Cam;
        public Effect TerrainEffect;
        public Vector3 CamPosition;
        public float Accel = 0;
        private RenderTarget2D RefractionMap { get; set; }
        private RenderTarget2D ReflectionMap { get; set; }
        //  public System.Threading.Thread QThread;
        public void HandleInput(GraphicsDevice device, MouseState mouse, KeyboardState kb, float dT)
        {
           
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

            Cam.Position += -Cam.GetMoveVector()*dT * Accel;




            if (mouse.RightButton == ButtonState.Pressed)
            {
               // this.IsMouseVisible = false;
              //  Volatile.WindowManager.MovingWindow = null;
                //mouselook
                float DX = mouse.X - PreviousMouseState.X;
                float DY = mouse.Y - PreviousMouseState.Y;
                Cam.Yaw += DX * dT*3.0f;
                Cam.Pitch -= DY * dT*3.0f;
                Cam.Pitch = MathHelper.Clamp(Cam.Pitch, -89.0f, 89.0f);

               
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

        }

        public void Init(GraphicsDevice device, ContentManager content)
        {
            Map = new GameObjects.Map();
            Textures = new Dictionary<string, Texture2D>();
            Textures["grass_overworld"]= Texture2D.FromStream(device, new System.IO.FileStream("graphics\\grassB.png", System.IO.FileMode.Open));
            Textures["waterbump"] = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\waterbump.jpg", System.IO.FileMode.Open));
            Textures["rock"] = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\rock.jpg", System.IO.FileMode.Open));
            TerrainEffect = content.Load<Effect>("legacy");
            Map.Terrain.TerrainEffect = TerrainEffect;
            Cam = new GameObjects.Camera();
            Cam.Position= new Vector3(1032, 32, 10032);
            Map.Terrain.QThread = new Thread(new ThreadStart(ProcessQ));
            Map.Terrain.QThread.Start();
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
            Matrix viewMatrix = Cam.GetView();
            Matrix projectionMatrix = Cam.GetProjection(device);
            Matrix reflectedView = Cam.GetReflectedView(device, Map.Terrain.WaterHeight - 0.2f);
            TerrainEffect.Parameters["xGrass"].SetValue(Textures["grass_overworld"]);
            TerrainEffect.Parameters["xRock"].SetValue(Textures["rock"]);
            TerrainEffect.Parameters["xView"].SetValue(reflectedView);
            TerrainEffect.Parameters["xReflectionView"].SetValue(reflectedView);
            TerrainEffect.Parameters["xProjection"].SetValue(projectionMatrix);
            TerrainEffect.Parameters["xCamPos"].SetValue((Vector3)Cam.Position);
            TerrainEffect.Parameters["xFog"].SetValue(false);


            Plane refractionplane = CreatePlane(Map.Terrain.WaterHeight -0.2f, new Vector3(0, 1, 0), viewMatrix, false, projectionMatrix);

            TerrainEffect.Parameters["ClipPlane0"].SetValue(new Vector4(refractionplane.Normal, refractionplane.D));
            TerrainEffect.Parameters["Clipping"].SetValue(true);

            device.SetRenderTarget(ReflectionMap);
            device.Clear(skyColor);
            Map.Render(device, dT);
           // device.Clear(Color.CornflowerBlue);

            device.SetRenderTarget(null);

            TerrainEffect.Parameters["xView"].SetValue(viewMatrix);
            refractionplane = CreatePlane(-Map.Terrain.WaterHeight-0.2f, new Vector3(0, 1, 0), Matrix.Identity, true, projectionMatrix);

            TerrainEffect.Parameters["ClipPlane0"].SetValue(new Vector4(refractionplane.Normal, refractionplane.D));
            TerrainEffect.Parameters["Clipping"].SetValue(true);
             TerrainEffect.Parameters["xFog"].SetValue(true);
            device.SetRenderTarget(RefractionMap);
            device.Clear(skyColor);
            Map.Render(device, dT);
            //device.Clear(Color.CornflowerBlue);

            device.SetRenderTarget(null);
            device.Clear(skyColor);

            TerrainEffect.Parameters["Clipping"].SetValue(false);
            TerrainEffect.Parameters["xFog"].SetValue(false);
            device.SetRenderTarget(null);
            Map.Render(device, dT);


            TerrainEffect.Parameters["xReflectionMap"].SetValue(ReflectionMap);
            TerrainEffect.Parameters["xRefractionMap"].SetValue(RefractionMap);


            TerrainEffect.Parameters["xWaveLength"].SetValue(0.25f);
            TerrainEffect.Parameters["xWaveHeight"].SetValue(0.4f);
            TerrainEffect.Parameters["xWaterBumpMap"].SetValue(Textures["waterbump"]);

            TerrainEffect.Parameters["xTime"].SetValue(RenderTime / 100.0f);
            TerrainEffect.Parameters["xWindForce"].SetValue(2.0f);
            TerrainEffect.Parameters["xWindDirection"].SetValue(new Vector3(0, 1, 0));
            TerrainEffect.CurrentTechnique = TerrainEffect.Techniques["Water"];
            Map.Terrain.DrawWater(device, dT);
        }

        public void Update(float dT)
        {
     
            Map.Update(dT);
        }


        private void ProcessQ()
        {
            while (true)
            {
                //World.Player.UpdateRenderPos();
                int DX = (int)Math.Floor(Cam.Position.X / Map.Terrain.BlockSize);
                int DY = (int)Math.Floor(Cam.Position.Z / Map.Terrain.BlockSize);
                Map.Terrain.BorderEvent(DX, DY);
                Map.Terrain.ProcessQueue();
                System.Threading.Thread.Sleep(1);
                //  Utility.Trace(World.Player.

                // Program.DW.blockstackmap.Text = "";

            }

        }

        
    }
}
