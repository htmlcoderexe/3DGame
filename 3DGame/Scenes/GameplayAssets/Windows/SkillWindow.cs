using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.GameplayAssets.Windows
{
    public class SkillWindow : GUI.Window
    {
        public GameObjects.MapEntities.Actors.Player Player;
        private ItemSlot s;
        private ItemSlot s2;
        public SkillWindow(GUI.WindowManager WM, GameObjects.MapEntities.Actors.Player Player)
        {
            this.Width = 512;
            this.Height = 64;
            this.Title = "Skillz";
            this.X = 300;
            this.Y = 450;
            int y = 0;
            int x = 0;
            int i = y * Width + x;
            s = new ItemSlot(Player.Abilities[0]);
            s.X = 0;
            s.Y = 0;
            s.Width = 40;
            s.Height = 40;
            s.CanPut = true;
            s2 = new ItemSlot(Player.Abilities[1]);
            s2.X = 42;
            s2.Y = 0;
            s2.Width = 40;
            s2.Height = 40;
            s2.CanPut = true;
            GUI.Controls.Button b = new GUI.Controls.Button("Up me!");
            b.Clicked += B_Clicked;
            b.Width = 128;
            b.Height = 24;
            b.X = 100;
            this.AddControl(s);
            this.AddControl(s2);
            this.AddControl(b);
        }

        private void B_Clicked(object sender, EventArgs e)
        {
            GameObjects.Ability a = s.Item as GameObjects.Ability;
            if (a != null)
                a.Level++;
            a = s2.Item as GameObjects.Ability;
            if (a != null)
                a.Level++;

        }
    }
}
