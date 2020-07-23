using GameObject;
using GameObject.MapEntities.Actors;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.GameplayAssets.Windows
{
    public class FBindWindow : GUI.Window
    {
        public GameObject.MapEntities.Actors.Player Player;
        private ItemSlot[] FBinds = new ItemSlot[8];
        private ItemSlot s;
        private ItemSlot s2;
        public FBindWindow(GUI.WindowManager WM, GameObject.MapEntities.Actors.Player Player)
        {
            this.Width = 404;
            this.Height = 78;
            this.Title = "Skillz";
            this.X = 300;
            this.Y = 450;
            int y = 0;
            int x = 0;
          //  int i = y * Width + x;

            for(int i=0;i<8;i++)
            {
                ItemSlot s = new ItemSlot(null);
                s.Y = 8;
                s.X = 8 + 48 * i; //8 between each
                s.CanGrab = true;
                s.CanPut = true;
                s.RenderCooldown = true;
                s.BeforeItemChanged += CheckSkill;
                this.AddControl(s);
                FBinds[i] = s;
            }
        }

        public void GetFBind(int index, Player Player)
        {
            index = MathHelper.Clamp(index, 0, 7);
            ItemSlot s = FBinds[index];
            GUI.IActionIcon item = s.Item;
            ModularAbility a = item as ModularAbility;
            Item i = item as Item;
            //eventual 3rd value for other actions
            if(a!=null)
            {
                if (Player.Executor != null && Player.Executor.done == false)
                    return;
                if (a.CoolDown > 0)
                    return;
                if (a.GetValue("mp_cost") > Player.CurrentMP)
                    return;
                Player.Executor = new GameObject.AbilityLogic.AbilityExecutor(a, Player, Player.Target);

                Console.Write("Used " + a.Name);
                return;
            }
            if(i!=null)
            {
                if (i.CoolDown > 0)
                    return;
                i.Use();
            }
        }

        private void B_Clicked(object sender, EventArgs e)
        {
            GameObject.Ability a = s.Item as GameObject.Ability;
            if (a != null)
                a.Level++;
            a = s2.Item as GameObject.Ability;
            if (a != null)
                a.Level++;

        }
        void CheckSkill(object sender, ItemSlot.ItemEventArgs e)
        {
            //ModularAbility a = e.Item as ModularAbility;
            //if (e == null)
              //  e.Cancel = true;
        }
    }
}
