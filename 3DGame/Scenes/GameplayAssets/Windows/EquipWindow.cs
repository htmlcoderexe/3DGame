using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.GameplayAssets.Windows
{
    public class EquipWindow : GUI.Window
    {
        public GameObjects.MapEntities.Actos.Player Player;
        public EquipWindow(GUI.WindowManager WM, GameObjects.MapEntities.Actos.Player Player)
        {
            this.Width = 128;
            this.Height = 64;
            this.Title = "Equipment";
            this.X = 400;
            this.Y = 400;
            int y = 0;
            int x = 0;
            int i = y * Width + x;
            ItemSlot s = new ItemSlot(Player.Equipment[0]);
            s.X = x * 40;
            s.Y = y * 40;
            s.CanGrab = true;
            s.CanPut = true;
            s.BeforeItemChanged += new ItemSlot.ItemEventHandler((sender, e) => 
            {
                GameObjects.Item item = (GameObjects.Item)(e as ItemSlot.ItemEventArgs).Item;
                if ((item as GameObjects.Items.ItemEquip) == null)
                {
                    e.Cancel = true;
                    if (item == null)
                    {
                        Console.Write("^FF0000 No item.");
                    }
                    else {
                        List<string> ToolTip = item.GetTooltip();
                    Console.WriteEx("^BEGINLINK " + GUI.Renderer.ColourToCode(item.NameColour) + "[" + item.GetName() + "] ^ENDLINK ^FF0000 is not a suitable item.", new List<Action> { new Action(() => {GUI.ToolTipWindow tip = new GUI.ToolTipWindow(this.WM,ToolTip, WM.MouseX, WM.MouseY, false);
                WM.Add(tip); })});
                }
                }
            });
            s.ItemOut += new ItemSlot.ItemEventHandler((sender, e) =>
            {
                int thisslot = GameObjects.Items.ItemEquip.EquipSlot.RightArm;
                GameObjects.Items.ItemEquip item = (GameObjects.Items.ItemEquip)(e as ItemSlot.ItemEventArgs).Item;
                List<string> ToolTip = item.GetTooltip();
                Console.WriteEx("^BEGINLINK " + GUI.Renderer.ColourToCode(item.NameColour) + "[" + item.GetName() + "] ^ENDLINK ^FFFFFF is removed.", new List<Action> { new Action(() => {GUI.ToolTipWindow tip = new GUI.ToolTipWindow(this.WM,ToolTip, WM.MouseX, WM.MouseY, false);
                WM.Add(tip); })});
                Player.UnequipItem(item, thisslot);
            });
            s.ItemIn += new ItemSlot.ItemEventHandler((sender, e) =>
            {
                int thisslot = GameObjects.Items.ItemEquip.EquipSlot.RightArm;
                GameObjects.Items.ItemEquip item = (GameObjects.Items.ItemEquip)(e as ItemSlot.ItemEventArgs).Item;
                List<string> ToolTip = item.GetTooltip();
                Console.WriteEx("^BEGINLINK " + GUI.Renderer.ColourToCode(item.NameColour) + "[" + item.GetName() + "] ^ENDLINK ^FFFFFF is equipped.", new List<Action> { new Action(() => {GUI.ToolTipWindow tip = new GUI.ToolTipWindow(this.WM,ToolTip, WM.MouseX, WM.MouseY, false);
                WM.Add(tip); })});
                Player.EquipItem(item, thisslot);
            });
            this.AddControl(s);
        }
    }
}
