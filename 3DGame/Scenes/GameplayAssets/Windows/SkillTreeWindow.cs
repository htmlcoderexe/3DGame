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
    public class SkillTreeWindow : GUI.Window
    {
        SkillTree tree;
        List<ModularAbility> abilities;
        Player Player;
        public bool Dirty;
        public SkillTreeWindow(GUI.WindowManager WM, Player Player, List<ModularAbility> globalAbilityList)
        {
            this.tree = Player.SkillTree;
            this.abilities = globalAbilityList;
            this.Width = 400;
            this.Height = 500;
            this.Title = "Skill tree";
            this.Player = Player;
            RefreshSkillTree();
        }
        public override void Update(float dT)
        {
            if(Dirty)
            {
                RefreshSkillTree();
                Dirty = false;
            }
           // 
            base.Update(dT);
        }
        void RefreshSkillTree()
        {
            this.Controls.Clear();
            foreach (SkillTreeEntry e in this.tree.Entries)
            {
                ModularAbility a = null;
                foreach (ModularAbility f in abilities)
                    if (f.ID == e.SkillID)
                        a = f;
                ItemSlot s = new ItemSlot(a);
                s.X = e.GetLocation().X;
                s.Y = e.GetLocation().Y;
                s.CanPut = false;
                s.CanGrab = false;
                s.Tint = Color.Red;
//                s.RenderCooldown = true;
                bool learned = false;
                foreach(ModularAbility f2 in Player.Abilities)
                {
                    if (f2.ID == e.SkillID && f2.Level>0)
                        learned = true;
                }
                if(learned)
                {

                    s.CanGrab = true;
                    s.ItemOut += ItemOutS;
                    s.RenderCooldown = true;
                    s.Tint = Color.Green;
                }
                s.WM = WM;
                //s.BeforeItemChanged += ItemOutS;
                this.Controls.Add(s);
            }
        }
        void ItemOutS(object sender, ItemSlot.ItemEventArgs args)
        {
            ItemSlot a = sender as ItemSlot;
            a.Item = args.Item;
            WM.MouseGrab = a.Item;
            args.Cancel = true;
        }
    }
}
