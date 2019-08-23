using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.GameplayAssets.Windows
{
    public class InventoryControl :GUI.Control
    {
        public Inventory Inventory;

        public InventoryControl(GUI.WindowManager WM, Inventory Inventory, int Width =8)
        {
            int slotwidth = 40;
            this.Width = Width * slotwidth;
            int Rows = (int)Math.Ceiling((float)Inventory.Items.Length / (float)Width);
            this.Height = Rows* slotwidth;
            for(int y=0;y<Rows;y++)
                for(int x=0;x<Width;x++)
                {
                    int i = y * Width + x;
                    ItemSlot s = new ItemSlot(Inventory.Items[i]);
                    s.X = x * slotwidth;
                    s.Y = y * slotwidth;
                    s.CanGrab = true;
                    s.CanPut = true;
                    //s.BeforeItemChanged += new ItemSlot.ItemEventHandler((sender, e) => { if(((e as ItemSlot.ItemEventArgs).Item as GameObjects.Item) ==null) e.Cancel=true; });
                    s.ItemOut += new ItemSlot.ItemEventHandler((sender, e) => { Inventory.Items[i] = null; });
                    s.ItemIn += new ItemSlot.ItemEventHandler((sender, e) => { Inventory.Items[i] = ((e as ItemSlot.ItemEventArgs).Item as GameObjects.Item); });
                    this.AddControl(s);
                }
        }

                
    }
}
