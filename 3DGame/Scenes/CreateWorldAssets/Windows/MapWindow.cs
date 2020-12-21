using GUI.Controls;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.CreateWorldAssets.Windows
{
    public class MapWindow : GUI.Window
    {

        public MapWindow(Texture2D MapTex)
        {
            this.WM = CreateWorld.WindowManager;
            this.Width = 820;
            this.Height = 830;
            TextureContainer map = new TextureContainer(MapTex, WM);
            AddControl(map);
            map.OnClick += new ClickEventHandler((sender, m)=>GoToMap(m.X,m.Y));
        }

        void GoToMap(float X, float Y)
        {
            Scenes.Gameplay worldscene = new Gameplay();
            _3DGame.Main.CurrentScene = worldscene;
            Main.ReloadScene();
            worldscene.Generator.Map = CreateWorld.map;
            worldscene.World.Player.Position.BX = (int)X;
            worldscene.World.Player.Position.BY = (int)Y;
        }
    }
}
