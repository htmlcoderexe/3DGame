using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3DGame.Scenes
{
    public class CreateWorld : _3DGame.Interfaces.IGameScene
    {
        public MouseState PreviousMouseState { get; set ; }
        public KeyboardState PreviousKbState { get; set ; }

        public SpriteBatch b;
        public RenderTarget2D Screen { get; set; }
        public GUI.Renderer GUIRenderer;
        public static GUI.WindowManager WindowManager;
        public static Dictionary<string, SpriteFont> Fonts;

        public static Terrain.WorldMap map;


        public void HandleInput(GraphicsDevice device, MouseState mouse, KeyboardState kb, float dT)
        {

            WindowManager.MouseX = mouse.X;
            WindowManager.MouseY = mouse.Y;
            bool MouseHandled = WindowManager.HandleMouse(mouse, dT);

            PreviousKbState = Keyboard.GetState();
            PreviousMouseState = Mouse.GetState();
        }



        public void Init(GraphicsDevice device, ContentManager content)
        {

            Fonts = new Dictionary<string, SpriteFont>();
            Fonts["font1"] = content.Load<SpriteFont>("font1");

            GUIRenderer = new GUI.Renderer(device);
            GUIRenderer.WindowSkin = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\winskin.png", System.IO.FileMode.Open));
            GUIRenderer.InventoryPartsMap = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\itemparts.png", System.IO.FileMode.Open));
            GUIRenderer.AbilityMap = Texture2D.FromStream(device, new System.IO.FileStream("graphics\\icons.png", System.IO.FileMode.Open));
            GUIRenderer.GUIEffect = content.Load<Effect>("GUI");
            GUIRenderer.UIFont = Fonts["font1"];
            WindowManager = new GUI.WindowManager();
            WindowManager.Renderer = GUIRenderer;
            b = new SpriteBatch(device);

            map = new Terrain.WorldMap(800, 800);
            int seed = new System.Random().Next(100);
            Terrain.WorldGen.Simplex.Seed = seed;
            Terrain.WorldGen.WorldMapFeatureGenerator.Seed = seed;
            //Terrain.WorldGen.WorldMapFeatureGenerator.MakeEllipse(map, 30, 30, 720, 440);
            //Terrain.WorldGen.WorldMapFeatureGenerator.MakeEllipse(map, 30, 500, 320, 240);
            //Terrain.WorldGen.WorldMapFeatureGenerator.MakeEllipse(map, 430, 500, 350, 200);
            Terrain.WorldGen.WorldMapFeatureGenerator.MakeEllipse(map, 30, 30, 720, 640);
            Terrain.WorldGen.WorldMapFeatureGenerator.FillOcean(map);
            for (int x = 0; x < map.Width; x++)
                for (int y = 0; y < map.Height; y++)
                    if (map.TileData[x, y] == Terrain.WorldMap.TileType.Unfilled)
                        map.ElevationData[x, y] = 0.1f;
                        Terrain.WorldGen.WorldMapFeatureGenerator.Replace(map, Terrain.WorldMap.TileType.Unfilled, Terrain.WorldMap.TileType.Plain);
            map.OceanDistanceField=Terrain.WorldGen.WorldMapFeatureGenerator.DoDistanceField(map, map.OceanDistanceField, Terrain.WorldMap.TileType.Ocean);
            Terrain.WorldGen.WorldMapFeatureGenerator.DoRivers(map, 10);
            map.RiverDistanceField=Terrain.WorldGen.WorldMapFeatureGenerator.DoDistanceField(map, map.RiverDistanceField, Terrain.WorldMap.TileType.River);
            Terrain.WorldGen.WorldMapFeatureGenerator.DoTemperature(map);
            Terrain.WorldGen.WorldMapFeatureGenerator.DoMountains(map, 6666);
            Terrain.WorldGen.WorldMapFeatureGenerator.DoHumidity(map);
            map.TileData[36, 14] = Terrain.WorldMap.TileType.Beach;
            Texture2D result = map.TilesToTexture(device);
            CreateWorldAssets.Windows.MapWindow mw = new CreateWorldAssets.Windows.MapWindow(result);
            WindowManager.Add(mw);


            ScreenResized(device);
        }

        public void Render(GraphicsDevice device, float dT)
        {
            WindowManager.Render(device);

            //from here on the screen "buffer" texture is actually rendered.
            device.SetRenderTarget(null);
            b.Begin();
            //b.Draw(RefractionMap, Vector2.Zero, Color.White);
            //b.Draw(Screen, new Rectangle(0, 0, (int)(device.Viewport.Width / 2), (int)(device.Viewport.Height / 1)), new Rectangle(0, 0, (int)(device.Viewport.Width / 2), (int)(device.Viewport.Height / 1)), Color.White);
            b.Draw(Screen, Vector2.Zero, Color.White);
            // b.Draw(OverheadMapTex,)
            b.End();
        }

        public void ScreenResized(GraphicsDevice device)
        {
            int ScreenWidth = device.PresentationParameters.BackBufferWidth;
            int ScreenHeight = device.PresentationParameters.BackBufferHeight;
            Screen = new RenderTarget2D(device, ScreenWidth, ScreenHeight, false, device.PresentationParameters.BackBufferFormat, device.PresentationParameters.DepthStencilFormat);
            WindowManager.ScreenResized(ScreenWidth, ScreenHeight);
        }

        public void Update(float dT)
        {
            WindowManager.Update(dT);
        }

        

        public Texture2D LoadTex2D(GraphicsDevice device, string path)
        {
            Texture2D result;
            System.IO.FileStream s = new System.IO.FileStream(path, System.IO.FileMode.Open);
            result = Texture2D.FromStream(device, s);
            s.Close();
            return result;
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CreateWorld() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
