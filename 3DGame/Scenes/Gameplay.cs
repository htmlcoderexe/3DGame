using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using _3DGame.GameObjects;
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
        public RenderTarget2D OverheadMapTex { get; set; }
        public Vector3 CamPosition;
        public float Accel = 0;
        private RenderTarget2D RefractionMap { get; set; }
        private RenderTarget2D ReflectionMap { get; set; }
        public bool RotateMap { get; private set; }

        public GUI.Renderer GUIRenderer;
        public GUI.WindowManager WindowManager;
        public MapEntity HoverTarget;
        private double spinner;
        private int MapZoomLevel=1;

        //  public System.Threading.Thread QThread;

        private void TakeScreenshot(GraphicsDevice device)
        {
            string fn = "screenshots\\";
            DateTime now = DateTime.Now;
            
            fn += now.ToString("yyyyMMddhhmmss") + counter + ".png";
            System.IO.FileStream s = new System.IO.FileStream(fn, System.IO.FileMode.Create);
            Screen.SaveAsPng(s,device.PresentationParameters.BackBufferWidth,device.PresentationParameters.BackBufferHeight);
            s.Close();
            counter++;
            Console.WriteEx("^00FF00 Screenshot saved. Click ^BEGINLINK ^3030FF here ^ENDLINK ^00FF00 to view.", new List<System.Action>() { new System.Action(() => System.Diagnostics.Process.Start(fn)) });
        }

        public void ConsoleWrite(string Text)
        {
            ConsoleWriteEx(Text);
        }
        public void ConsoleWriteEx(string Text,List<System.Action> Links=null)
        {
            if (WindowManager == null)
                return;
            GameplayAssets.ConsoleWindow console = null;
            foreach (GUI.Window w in WindowManager.Windows)
            {
                if ((w as GameplayAssets.ConsoleWindow) != null)
                { 
                    console = w as GameplayAssets.ConsoleWindow;
                    break;
                }
            }
            if (console == null)
                return;
            console.AppendMessage(Text,Links);
        }
        public void TerrainClick(Interfaces.WorldPosition Position)
        {
            GameObjects.MapEntities.EntitySpawner s = new GameObjects.MapEntities.EntitySpawner();
            GameObjects.MapEntities.Actos.Hostile tpl=new GameObjects.MapEntities.Actos.Hostile();
            tpl.LeashRadius = 35;
            s.Entity = tpl;
            s.Interval = 5;
            s.CountDown = 2;
            s.MaxCount = 6;
            s.SpawnCallback = new Action<MapEntity>(e => World.Entities.Add(e));
            s.Position = Position;
            s.SpawningVolume = new BoundingBox(new Vector3(-5, 0, -5), new Vector3(5, 0, 5));
            s.Entity.Parent = s;
            
            /* Do not uncomment the following, left for posterity
            s.Entity = s;//VERY EVIL REMOVE IT WAS JUST FOR FUN
            s.Entity = (MapEntity)s.Clone();//oh #@&*
            //   forget about this */
            World.Entities.Add(s);

        }
        public void HandleInput(GraphicsDevice device, MouseState mouse, KeyboardState kb, float dT)
        {

            if (kb.IsKeyDown(Keys.F12) && PreviousKbState.IsKeyUp(Keys.F12))
            {
                TakeScreenshot(device);
            }
            if (kb.IsKeyDown(Keys.Add) && PreviousKbState.IsKeyUp(Keys.Add))
            {
                MapZoomLevel++;
                if (MapZoomLevel > 4)
                    MapZoomLevel = 4;
            }
            if (kb.IsKeyDown(Keys.Subtract) && PreviousKbState.IsKeyUp(Keys.Subtract))
            {
                MapZoomLevel--;
                if (MapZoomLevel < 1)
                    MapZoomLevel = 1;
            }
            if (kb.IsKeyDown(Keys.Multiply) && PreviousKbState.IsKeyUp(Keys.Multiply))
            {
                RotateMap = !RotateMap;            }


            if (kb.IsKeyDown(Keys.F2) && PreviousKbState.IsKeyUp(Keys.F2) && World.Player.Target!=null && !World.Player.Target.IsDead)
            {
                Color c = new Color(255, 100, 20);
                for (int i = 0; i < 1; i++)
                {
                    GameObjects.MapEntities.Particles.Homing p = new GameObjects.MapEntities.Particles.Homing(c, 2.0f);
                    p.WorldSpawn = World;
                    p.Parent = World.Player.Target;
                    Vector3 v = new Vector3(0, 3.6f, -2.0f+(1.0f*(float)i));
                    v = Vector3.Transform(v, Matrix.CreateRotationY((World.Player.Heading+90)*MathHelper.Pi/180f));
                    p.Position = World.Player.Position + v;
                   
                    p.TTL = 100;
                    p.Speed = 8f;
                    p.Gravity = false;
                    World.Entities.Add(p);
                    p = null;
                }
                World.Player.Target.Hit(World.Player.CalculateStat("p_atk") + RNG.Next(0, 10),true,0);
            }

            if (kb.IsKeyDown(Keys.F3) && World.Player.Target != null)// && PreviousKbState.IsKeyUp(Keys.F3) )
            {
                Color c = new Color(100, 255, 200);
                GameObjects.MapEntities.ParticleGroups.Ring r =
                    new GameObjects.MapEntities.ParticleGroups.Ring(0.5f, 0.4f, c);
                r.Speed = 8f;
                r.Position = World.Player.Position;
                r.Target = World.Player.Target;
                r.WorldSpawn = World;
                r.Gravity = false;
                r.OnGround = false;
              //  World.Entities.Add(r);

                GameObjects.MapEntities.Particles.LightRay ray = new GameObjects.MapEntities.Particles.LightRay(World.Player, World.Player.Target, Color.Green, 0.3f);
                ray.Expires = false;
                GameObjects.MapEntities.ParticleGroup g = new GameObjects.MapEntities.ParticleGroup();
                g.Speed = 0f ;
                g.Position = World.Player.Position;
                g.WorldSpawn = World;
                g.Gravity = false;
                g.OnGround = false;
                g.Particles.Add(ray);
                g.Model = null;
                g.Target = World.Player;
                
                World.Entities.Add(g);
                r = null;
                /*
                Vector3 source = new Vector3(0, 0.5f, 0);
                Vector3 fw = new Vector3(3.2f, 0, 0);
                Matrix spinm = Matrix.CreateRotationX((float)spinner * 10f);
                Matrix head = Matrix.CreateRotationY(MathHelper.ToRadians(-World.Player.Heading + 0));
                fw = Vector3.Transform(fw, head);
                source = Vector3.Transform(source, spinm*head);
                //source = Vector3.Transform(source, );
                GameObjects.MapEntities.Particles.Spiral p =
                    new GameObjects.MapEntities.Particles.Spiral(c, 0.2f);
               
                p.WorldSpawn = World;
                p.Parent = World.Player.Target;
                p.Origin = World.Player.Position;// + source;
                p.TTL = 100;
                p.Speed = 0.3f;
                p.Gravity = false;
                World.Entities.Add(p);
                World.Player.Target.Speed = 0;
                p = null;
                //*/
            }
            if (kb.IsKeyDown(Keys.F) && PreviousKbState.IsKeyUp(Keys.F))
            {
                World.Player.Gravity = !World.Player.Gravity;
            }
            if (kb.IsKeyDown(Keys.F5) && PreviousKbState.IsKeyUp(Keys.F5))
            {
                World.Entities.Clear();
                World.Entities.Clear();
            }



            if (kb.IsKeyDown(Keys.Space) /* )//Diarrhea mode!!*/ && (PreviousKbState.IsKeyUp(Keys.Space) || kb.IsKeyDown(Keys.LeftShift)))
            {
                /*
                GameObjects.MapEntity e = new GameObjects.MapEntity();
                e.Position = World.Player.Position;
                e.Heading = World.Player.Heading;
                e.Speed = 3.0f;
                World.Entities.Add(e);
                Console.Write("Spawned entity at " + e.Position.ToString());

                //*/
                if (!World.Player.Gravity)
                {
                    World.Player.Position.Y += 1.1f;
                }
                else
                {
                    
                        World.Player.Jump();
                    
                }
            }
            if (kb.IsKeyDown(Keys.D))
            {
                World.Player.Speed = World.Player.GetMovementSpeed();

                World.Player.Heading = World.Camera.Yaw - 180f;
            }
            else if (kb.IsKeyDown(Keys.A))
            {
                World.Player.Speed = World.Player.GetMovementSpeed();

                World.Player.Heading = World.Camera.Yaw - 0f;
            }
            

            else if (kb.IsKeyDown(Keys.S))
            {
                World.Player.Speed = World.Player.GetMovementSpeed();

                World.Player.Heading = World.Camera.Yaw - 90f;
            }
            else if (kb.IsKeyDown(Keys.W))
            {

                World.Player.Speed = World.Player.GetMovementSpeed();

                World.Player.Heading = World.Camera.Yaw + 90f;
            }
            else if(World.Player.Walking)
            {
                World.Player.Speed = World.Player.GetMovementSpeed() ;
            }
            else
            {
                World.Player.Speed = 0;
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
            if (kb.IsKeyDown(Keys.Up))
                World.Camera.Distance *= 0.99f;
            if (kb.IsKeyDown(Keys.Down))
                World.Camera.Distance /= 0.99f;
            World.Camera.Distance = MathHelper.Clamp(World.Camera.Distance, 0.01f, 1010f);
            //World.Player.Position += mv;


            WindowManager.MouseX = mouse.X;
            WindowManager.MouseY = mouse.Y;
            bool MouseHandled = WindowManager.HandleMouse(mouse,dT);

            Vector3 r0 = device.Viewport.Unproject(new Vector3(mouse.X, mouse.Y, 0), World.Camera.GetProjection(device), World.Camera.GetView(), Matrix.Identity);

            Vector3 r1 = device.Viewport.Unproject(new Vector3(mouse.X, mouse.Y, 1), World.Camera.GetProjection(device), World.Camera.GetView(), Matrix.Identity);
            Vector3 vc = (r0 - r1);

            vc.Normalize();

            Ray MouseRay = new Ray(r0, -vc);

            BoundingSphere bs;

            if (!MouseHandled)
            {
                List<MapEntity> targets = World.LocateNearby(World.Player);
                float closest = 9999;
                float? intersect = null;
                MapEntity Target = null;
                foreach(MapEntity t in targets)
                {
                    bs = new BoundingSphere(t.Position.WRT(World.Player.Position),1);
                    intersect = bs.Intersects(MouseRay);
                    if (intersect.HasValue && intersect.GetValueOrDefault(0) < closest)
                        Target = t;
                }
                HoverTarget = Target;
                targets = null;

                Interfaces.WorldPosition check = MouseRay.Position;
                check.Normalize();
                Interfaces.WorldPosition campos = World.Player.Position+((Vector3)World.Player.Camera.GetCamVector() - World.Camera.Position.Truncate());
                check.BX = World.Player.Position.BX;
                check.BY = World.Player.Position.BY;
                //*
                check.BX = campos.BX;
                check.BY = campos.BY;
                //*/
                if (mouse.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton == ButtonState.Released)
                {
                    if(HoverTarget!=null)
                    {
                        HoverTarget.Click(World.Player);
                    }
                    else if(kb.IsKeyDown(Keys.LeftShift))
                    { 
                    for (int i = 0; i < 1000; i++)
                        {
                            check += MouseRay.Direction / 1;
                            float th = World.Terrain.GetHeight(check.Truncate(), check.Reference()) == 0 ? -127f : (float)World.Terrain.GetHeight(check.Truncate(), check.Reference());
                            if (check.Y - 0 < th)
                            {
                                TerrainClick(check);
                                break;
                            }
                        }
                    }
                }
            }


            if (mouse.RightButton == ButtonState.Pressed && !MouseHandled)
            {
               // this.IsMouseVisible = false;
              //  Volatile.WindowManager.MovingWindow = null;
                //mouselook
                float DX = mouse.X - PreviousMouseState.X;
                float DY = mouse.Y - PreviousMouseState.Y;
                World.Camera.Yaw += DX * dT*3.0f;
                World.Camera.Pitch -= DY * dT*3.0f;
                World.Camera.Pitch = MathHelper.Clamp(World.Camera.Pitch, -89.0f, 89.0f);

               
                Mouse.SetPosition(PreviousMouseState.X, PreviousMouseState.Y);
               // Mouse.SetPosition(device.Viewport.Width / 2, device.Viewport.Height / 2);
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
            OverheadMapTex = new RenderTarget2D(device,256, 256, false, device.PresentationParameters.BackBufferFormat, device.PresentationParameters.DepthStencilFormat);
            b = new SpriteBatch(device);
            WindowManager.ScreenResized(ScreenWidth, ScreenHeight);
        }

        public void Init(GraphicsDevice device, ContentManager content)
        {
            RotateMap = true;
            World = new GameObjects.World(device,13);
            World.Player = new GameObjects.MapEntities.Actos.Player();
            Textures = new Dictionary<string, Texture2D>();
            Textures["grass_overworld"]= Texture2D.FromStream(device, new System.IO.FileStream("graphics\\grassB.png", System.IO.FileMode.Open));
            Textures["waterbump"] = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\waterbump.jpg", System.IO.FileMode.Open));
            Textures["rock"] = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\rock.jpg", System.IO.FileMode.Open));
            Textures["sand"] = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\sand.png", System.IO.FileMode.Open));
            Textures["point_sphere"] = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\ray.png", System.IO.FileMode.Open));
            Textures["mapsprites"] = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\mapsprites.png", System.IO.FileMode.Open));
            Textures["mapnavring"] = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\mapnavring.png", System.IO.FileMode.Open));
            Textures["mapoverlay"] = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\mapoverlay.png", System.IO.FileMode.Open));
            TerrainEffect = content.Load<Effect>("legacy");
            World.Terrain.TerrainEffect = TerrainEffect;
            World.ModelEffect = TerrainEffect;
            World.Camera = World.Player.GetTheCamera();
            World.Player.Position= new Vector3(0, 0, 0);
            World.Terrain.QThread = new Thread(new ThreadStart(ProcessQ));
            World.Terrain.QThread.Start();
            GUIRenderer = new GUI.Renderer(device);
            GUIRenderer.WindowSkin = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\winskin.png", System.IO.FileMode.Open));
            GUIRenderer.InventoryPartsMap = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\itemparts.png", System.IO.FileMode.Open));
            GUIRenderer.AbilityMap = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\icons.png", System.IO.FileMode.Open));
            GUIRenderer.GUIEffect = content.Load<Effect>("GUI");
            GUIRenderer.UIFont = content.Load<SpriteFont>("font1");
            WindowManager = new GUI.WindowManager();
            WindowManager.Renderer = GUIRenderer;
            WindowManager.Add(new GameplayAssets.ConsoleWindow(WindowManager));
            ScreenResized(device);
            Terrain.Console.WriteCallback = new Action<string>(ConsoleWrite);
            _3DGame.Console.WriteCallback = new Action<string>(ConsoleWrite);
            _3DGame.Console.WriteCallbackEx = new Action<string,List<Action>>(ConsoleWriteEx);
            GUI.Console.WriteCallback = new Action<string>(ConsoleWrite);
            GameObjects.Items.Material.MaterialTemplates.Load();
            GameObjects.AbilityLogic.AbilityLoader l = new GameObjects.AbilityLogic.AbilityLoader("Mage");

            World.Player.Abilities = l.LoadAbilities();

            GUI.Window w;
            w = new GameplayAssets.StatusWindow(WindowManager, World.Player);
            WindowManager.Add(w);
            WindowManager.Add(new GameplayAssets.Windows.TargetWindow(WindowManager, World.Player));
            WindowManager.Add(new GameplayAssets.Windows.InventoryWindow(WindowManager, World.Player));
            WindowManager.Add(new GameplayAssets.Windows.EquipWindow(WindowManager, World.Player));
            WindowManager.Add(new GameplayAssets.Windows.SkillWindow(WindowManager, World.Player));

        }
        public static Plane CreatePlane(float height, Vector3 planeNormalDirection, Matrix currentViewMatrix, bool clipSide, Matrix projectionMatrix)
        {
            return new Plane(planeNormalDirection * (clipSide ? -1f : 1f), height * (clipSide ? -1f : 1f)) ;
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



        void DrawMap(GraphicsDevice GraphicsDevice,float dT,float zoom = 1)
        {
            Color cc = Color.CornflowerBlue;
            //zoom /= 2;
            GraphicsDevice.SetRenderTarget(OverheadMapTex);
            // GraphicsDevice.Clear(ClearOptions.DepthBuffer, Color.White, 1.0f, 0);
            GraphicsDevice.Clear(ClearOptions.Target, cc, 1.0f, 0);
           TerrainEffect.CurrentTechnique =TerrainEffect.Techniques["OverHeadMap"];
         //   Terrain.Unit[] renderlist = new Terrain.Unit[World.Terrain.Blocks.Count];
            if (World.Terrain.Blocks.Count < 1)
                return;
            //   World.Terrain.Blocks.ToArray().CopyTo(renderlist,0);
            foreach (KeyValuePair<int, Terrain.Unit> bv in World.Terrain.Blocks)
            {
                Terrain.Unit blk = bv.Value;
                if (blk == null)
                    continue;
                Vector3 v1 = new Vector3((blk.X - World.Player.Position.BX) * 1 / zoom, (blk.Y - World.Player.Position.BY) * -1 / zoom, 0);
                v1.X -= (World.Player.Position.X / (Interfaces.WorldPosition.Stride * zoom));
                v1.Y -= (World.Player.Position.Z / (-Interfaces.WorldPosition.Stride * zoom));
               // v1.Z = v1.Y;v1.Y = 0;
             //  v1.Z= (World.Player.Position.Z / (-Interfaces.WorldPosition.Stride * zoom));
                Matrix rm = Matrix.CreateRotationX(MathHelper.PiOver2);
                Matrix wm = Matrix.CreateTranslation(v1);
                Matrix sm = Matrix.CreateScale( 1f / ((float)Interfaces.WorldPosition.Stride * zoom));
                Matrix ym = Matrix.Identity;
                if (RotateMap)
                    ym = Matrix.CreateRotationZ(MathHelper.ToRadians(World.Camera.Yaw + 180f));
                {
                   TerrainEffect.Parameters["xWorld"].SetValue(rm * sm * wm * ym);
                   TerrainEffect.Parameters["xZoomLevel"].SetValue(zoom);
                    foreach (EffectPass pass in TerrainEffect.CurrentTechnique.Passes)
                    {
                        pass.Apply();

                    }
                    blk.Render(GraphicsDevice,dT);
                }

            }
            //Assets.TerrainEffect.Parameters["xTexture"].Set
            RasterizerState trs = GraphicsDevice.RasterizerState;
            
                        b.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            
            for (int i = 0; i < World.Entities.Count(); i++)
            {
                MapEntity NPC = World.Entities[i];
                if (NPC == null)
                    continue;//error handling, bitch!
                Vector3 v1 = new Vector3((NPC.Position.BX - World.Player.Position.BX) * 1 / zoom, (NPC.Position.BY - World.Player.Position.BY) * -1 / zoom, 0);
                v1.X -= ((World.Player.Position.X - NPC.Position.X) / (Interfaces.WorldPosition.Stride * zoom));
                v1.Y -= ((World.Player.Position.Z - NPC.Position.Z) / (-Interfaces.WorldPosition.Stride * zoom));

                Matrix rm = Matrix.CreateRotationX(MathHelper.PiOver2); rm = Matrix.Identity;
                Matrix wm = Matrix.CreateTranslation(v1);// wm = Matrix.Identity;
                Matrix sm = Matrix.CreateScale(1f / (16f)); sm = Matrix.Identity;
                Matrix ym = Matrix.Identity;
                Matrix rym = Matrix.Identity;
                if (RotateMap)
                {
                    ym = Matrix.CreateRotationZ(MathHelper.ToRadians(World.Camera.Yaw + 180f));
                    rym = Matrix.CreateRotationZ(MathHelper.ToRadians(360f - (World.Camera.Yaw + 180f)));
                }

                {
                    Matrix zawarudo = rm * sm * wm * ym;

                   TerrainEffect.Parameters["xWorld"].SetValue(zawarudo);
                   TerrainEffect.Parameters["xZoomLevel"].SetValue(zoom);
                    foreach (EffectPass pass in TerrainEffect.CurrentTechnique.Passes)
                    {
                        pass.Apply();

                    }
                    Vector3 v = new Vector3(0, 0, 100)*2f;
                    v = Vector3.Transform(v, zawarudo);
                    Vector2 location = GUI.GFXUtility.ScreenToPixel(GraphicsDevice, v);

                    if (NPC == World.Player.Target)
                    {
                        // b.Draw(
                        b.Draw(Textures["mapsprites"], location, new Rectangle(24, 0, 8, 8), Color.Red);

                        //GraphicsDevice.DrawUserPrimitives<Declarations.TerrainVertex>(PrimitiveType.TriangleList, Assets.RedDot, 0, 2);
                    }
                    else
                    {
                        Color c = new Color(255, 255, 255);
                       
                        //todo colourise depending on object type
                        // if (NPC as Hostile != null)
                            c = new Color(0, 255, 0);

                        b.Draw(Textures["mapsprites"], location, new Rectangle(0, 0, 8, 8), c);
                        //GraphicsDevice.DrawUserPrimitives<Declarations.TerrainVertex>(PrimitiveType.TriangleList, Assets.BlueDot, 0, 2);
                    }

                }
            }
            float rot = 0;
            float rot2 = MathHelper.ToRadians(180f - (World.Camera.Yaw));
            if (!RotateMap)
                rot2 = 0;


            if (!RotateMap)
                rot = MathHelper.ToRadians((World.Camera.Yaw + 180f));
            b.Draw(Textures["mapsprites"], new Rectangle(128, 128 , 16, 16), new Rectangle(0, 8, 16, 16), Color.Blue, rot, new Vector2(8, 8), SpriteEffects.None, 0);
            b.Draw(Textures["mapnavring"], new Rectangle(128, 128, 256, 256), null, Color.White, rot2, new Vector2(64, 64), SpriteEffects.None, 0);
            b.Draw(Textures["mapoverlay"], new Rectangle(128, 128, 256, 256), null, Color.White, 0, new Vector2(64, 64), SpriteEffects.None, 0);
            //b.Draw(Textures["mapoverlat"], new Rectangle(64, 64, 128, 128), null, Color.White, rot2, new Vector2(64, 64), SpriteEffects.None, 0);
           // b.Draw(Textures["mapoverlay"], Vector2.Zero, Color.White);
            b.End();
            // GraphicsDevice.RasterizerState = trs;
            RasterizerState rs = new RasterizerState();
            //Assets.GUIEffect.
            rs.CullMode = CullMode.CullCounterClockwiseFace;
          //  if (Volatile.WireframeMode)
           //     rs.FillMode = FillMode.WireFrame;

            GraphicsDevice.RasterizerState = rs;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            return;

        }

        void DrawLabels(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.AnisotropicWrap, DepthStencilState.DepthRead, RasterizerState.CullNone);
            Vector3 labelorigin, playerorigin,distance,projectedlabelorigin;
            Vector2 stringsize,renderlocation;
            Color colour = Color.White;
            playerorigin = World.Player.Position.Truncate();
            for (int i = 0; i < World.Entities.Count(); i++)
            {
                MapEntity NPC = World.Entities[i];
                if (NPC == null)
                    continue;
                labelorigin = NPC.Position.WRT(World.Player.Position);

                distance = labelorigin - playerorigin;
                if (distance.Length() > 50)
                    continue;
                labelorigin.Y += 2.5f;//todo implement object height
                projectedlabelorigin = batch.GraphicsDevice.Viewport.Project(labelorigin, World.Camera.GetProjection(batch.GraphicsDevice), World.Camera.GetView(), Matrix.Identity);
                if (projectedlabelorigin.Z > 1)
                    continue;
                colour = Color.White;
                if ((NPC as GameObjects.MapEntities.Actor)!=null && (NPC as GameObjects.MapEntities.Actor).Target == World.Player)
                    colour = Color.Red;
                if (World.Player.Target == NPC)
                    colour = Color.Lime;
                if (NPC.DisplayName == null)
                    NPC.DisplayName = "<MissingNo.>";
                stringsize = GUIRenderer.UIFont.MeasureString(NPC.DisplayName);
                renderlocation = new Vector2((int)(projectedlabelorigin.X - (stringsize.X / 2f)), (int)(projectedlabelorigin.Y));
                batch.DrawString(GUIRenderer.UIFont, NPC.DisplayName, renderlocation, colour, 0f, Vector2.Zero, 1f, SpriteEffects.None, projectedlabelorigin.Z);
            }
            
                batch.End();
        }

        public void Render(GraphicsDevice device, float dT)
        {
            RenderTime += dT;
            RasterizerState rs = new RasterizerState
            {
                CullMode = CullMode.CullCounterClockwiseFace
            };
            //rs.CullMode = CullMode.None;
            // rs.FillMode = FillMode.WireFrame;
            device.RasterizerState = rs;
            Color skyColor = new Color(40, 100, 255);
          //  skyColor = Color.Red;
            Matrix viewMatrix = World.Camera.GetView();
            Matrix projectionMatrix = World.Camera.GetProjection(device);
            Matrix reflectedView = World.Camera.GetReflectedView(device, World.Terrain.WaterHeight - 0.2f);
            TerrainEffect.Parameters["xGrass"].SetValue(Textures["grass_overworld"]);
            TerrainEffect.Parameters["xRock"].SetValue(Textures["rock"]);
            TerrainEffect.Parameters["xSand"].SetValue(Textures["sand"]);
            TerrainEffect.Parameters["xTexture"].SetValue(Textures["point_sphere"]);
            TerrainEffect.Parameters["xView"].SetValue(reflectedView);
            //World.View = reflectedView;
            TerrainEffect.Parameters["xReflectionView"].SetValue(reflectedView);
            TerrainEffect.Parameters["xProjection"].SetValue(projectionMatrix);
            TerrainEffect.Parameters["xCamPos"].SetValue(World.Camera.GetCamVector());
            //TerrainEffect.Parameters["xCamUp"].SetValue(World.Camera.GetUpVector());
            TerrainEffect.Parameters["xFog"].SetValue(false);
            

           Plane refractionplane = CreatePlane(World.Terrain.WaterHeight-0.3f, new Vector3(0, -1, 0), viewMatrix, true, projectionMatrix);

            TerrainEffect.Parameters["ClipPlane0"].SetValue(new Vector4(new Vector3(0, 1, 0),-World.Terrain.WaterHeight+0.1f));
            TerrainEffect.Parameters["ClipPlane0"].SetValue(new Vector4(refractionplane.Normal, refractionplane.D));
            TerrainEffect.Parameters["Clipping"].SetValue(true);
                
            device.SetRenderTarget(ReflectionMap);
            device.Clear(skyColor);
            World.Render(device, dT,Vector2.Zero,false);

           
            // device.Clear(Color.CornflowerBlue);

            // device.SetRenderTarget(Screen);

            TerrainEffect.Parameters["xView"].SetValue(viewMatrix);
           // World.View = viewMatrix;
            refractionplane = CreatePlane(World.Terrain.WaterHeight +0.3f, new Vector3(0, -1, 0), viewMatrix, false, projectionMatrix);

            TerrainEffect.Parameters["ClipPlane0"].SetValue(new Vector4(new Vector3(0, -1, 0), World.Terrain.WaterHeight + 0.3f));
            TerrainEffect.Parameters["ClipPlane0"].SetValue(new Vector4(refractionplane.Normal,refractionplane.D));
            TerrainEffect.Parameters["Clipping"].SetValue(true);
             TerrainEffect.Parameters["xFog"].SetValue(true);
            device.SetRenderTarget(RefractionMap);
            device.Clear(skyColor);
            World.Render(device, dT, Vector2.Zero,false);
            //device.Clear(Color.CornflowerBlue);
            refractionplane = CreatePlane(-World.Terrain.WaterHeight - 690.3f, new Vector3(0, 1, 0), viewMatrix, true, projectionMatrix);

            //TerrainEffect.Parameters["ClipPlane0"].SetValue(new Vector4(new Vector3(0, 1, 0), -World.Terrain.WaterHeight + 0.1f));
            TerrainEffect.Parameters["ClipPlane0"].SetValue(new Vector4(refractionplane.Normal, refractionplane.D));

            device.SetRenderTarget(OverheadMapTex);


            DrawMap(device, dT, MapZoomLevel);

            device.SetRenderTarget(Screen);
            device.Clear(skyColor);

            TerrainEffect.Parameters["Clipping"].SetValue(false);
            //TerrainEffect.Parameters["Clipping"].SetValue(true);
            TerrainEffect.Parameters["xFog"].SetValue(false);
       //     device.SetRenderTarget(Screen);
           

            TerrainEffect.Parameters["xReflectionMap"].SetValue(ReflectionMap);
            TerrainEffect.Parameters["xRefractionMap"].SetValue(RefractionMap);


            TerrainEffect.Parameters["xWaveLength"].SetValue(0.025f);
            TerrainEffect.Parameters["xWaveHeight"].SetValue(0.025f);
            TerrainEffect.Parameters["xWaterBumpMap"].SetValue(Textures["waterbump"]);
                
            TerrainEffect.Parameters["xTime"].SetValue(RenderTime / 26.0f);
            TerrainEffect.Parameters["xWindForce"].SetValue(0.02f);
            TerrainEffect.Parameters["xWindDirection"].SetValue(new Vector3(1, 1, 0));
            TerrainEffect.CurrentTechnique = TerrainEffect.Techniques["Water"];
            //World.Terrain.DrawWater(device, dT, (World.Camera.Position ).Reference());
            World.Terrain.DrawWater(device, dT, (World.Camera.Position + World.Camera.GetCamVector()).Reference());

            World.Render(device, dT, Vector2.Zero, false);

             device.SetRenderTarget(Screen);
            DrawLabels(b);

            WindowManager.Render(device);

            //from here on the screen "buffer" texture is actually rendered.
            device.SetRenderTarget(null);
            b.Begin();
            //b.Draw(RefractionMap, Vector2.Zero, Color.White);
            //b.Draw(Screen, new Rectangle(0, 0, (int)(device.Viewport.Width / 2), (int)(device.Viewport.Height / 1)), new Rectangle(0, 0, (int)(device.Viewport.Width / 2), (int)(device.Viewport.Height / 1)), Color.White);
            b.Draw(Screen, Vector2.Zero, Color.White);
            b.Draw(OverheadMapTex, new Vector2(Screen.Width - 256, 0), Color.White);
           // b.Draw(OverheadMapTex,)
            b.End();
            /*
            GUIRenderer.RenderFrame(device, 32, 32, 256, 128);
            GUIRenderer.RenderBigIcon(device, 0, 0, 2, GUIRenderer.AbilityMap);
            GUIRenderer.RenderSmallText(device, 35, 56, World.Camera.Position.Y.ToString(), Color.Red, false, true);

            //*/
            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;
        }

        public void Update(float dT)
        {
            WindowManager.Update(dT);
            World.Update(dT);
            spinner += dT;
        }


        private void ProcessQ()
        {
            while (true)
            {
                //World.Player.UpdateRenderPos();
                World.Terrain.BorderEvent(World.Player.Position.BX, World.Player.Position.BY);
                World.Terrain.ProcessQueue();
                System.Threading.Thread.Sleep(1);
                //  Utility.Trace(World.Player.

                // Program.DW.blockstackmap.Text = "";

            }

        }

        public void Dispose()
        {
            World.Terrain.QThread.Abort();
        }
    }
}
