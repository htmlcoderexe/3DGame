using GameObject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.GameplayAssets.Windows
{
     public class NavWindow : GUI.Window
    {
        public GameObject.MapEntities.Actors.Player Player;
        GUI.Controls.RichTextDisplay Placename;
        GUI.Controls.RichTextDisplay Coords;
        public NavWindow(GameObject.MapEntities.Actors.Player Player, Microsoft.Xna.Framework.Graphics.Texture2D tex)
        {
            this.WM = Main.CurrentScene.WindowManager;
            this.Player = Player;
            this.Width = 296;
            this.Height = 300;
            this.AnchorRight = true;
            Placename = new GUI.Controls.RichTextDisplay("World", 286, 20, WM)
            {
                Y = 0,
                X = 4
            };
            GUI.Controls.TextureContainer texcont = new GUI.Controls.TextureContainer(tex, WM)
            {
                Y = 20,
                X = 4
            };
            Coords = new GUI.Controls.RichTextDisplay("loading...", 256, 20, WM)
            {
                Y = 280,
                X = 4
            };
            this.AddControl(texcont);
            this.AddControl(Placename);
            this.AddControl(Coords);
        }
        public override void Update(float dT)
        {
            string playerloc = "";
            int X = (int)(Player.Position.X + Player.Position.BX * WorldPosition.Stride);
            int Y = (int)(Player.Position.Z + Player.Position.BY * WorldPosition.Stride);
            int H = (int)Player.Position.Y;
            playerloc = X + ", " + Y+" H" + H+" LX:"+ (int)(Player.Position.X)+" LY:"+ (int)(Player.Position.Z);
            Placename.SetText(Player.WorldSpawn.Terrain.GetPlaceName(new Microsoft.Xna.Framework.Vector2(Player.Position.BX, Player.Position.BY)));
            this.Coords.SetText(playerloc);
            base.Update(dT);
        }
    }
}
