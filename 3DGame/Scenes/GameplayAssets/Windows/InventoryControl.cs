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
        public int RowWidth;

        public InventoryControl(GUI.WindowManager WM, Inventory Inventory, int Width =8)
        {
            this.RowWidth = Width;
            int slotwidth = 40;
            this.Width =RowWidth * slotwidth;
            this.Inventory = Inventory;
            int Rows = (int)Math.Ceiling((float)Inventory.Items.Length / (float)RowWidth);
            this.Height = Rows* slotwidth;
            for(int y=0;y<Rows;y++)
                for(int x=0;x<RowWidth;x++)
                {
                    int i = y * RowWidth + x;
                    ItemSlot s = new ItemSlot(Inventory.Items[i])
                    {
                        X = x * slotwidth,
                        Y = y * slotwidth,
                        CanGrab = true,
                        CanPut = true
                    };
                    //s.BeforeItemChanged += new ItemSlot.ItemEventHandler((sender, e) => { if(((e as ItemSlot.ItemEventArgs).Item as GameObjects.Item) ==null) e.Cancel=true; });
                    s.ItemOut += new ItemSlot.ItemEventHandler((sender, e) => { Inventory.Items[i] = null; });
                    s.ItemIn += new ItemSlot.ItemEventHandler((sender, e) => { Inventory.Items[i] = ((e as ItemSlot.ItemEventArgs).Item as GameObjects.Item); });
                    this.AddControl(s);
                }
        }
        public void Reload()
        {
            int slotwidth = 40;
            int Rows = (int)Math.Ceiling((float)Inventory.Items.Length / (float)RowWidth);
            this.Height = Rows * slotwidth;
            this.Controls.Clear();
            for (int y = 0; y < Rows; y++)
                for (int x = 0; x < RowWidth; x++)
                {
                    int i = y * RowWidth + x;
                    ItemSlot s = new ItemSlot(Inventory.Items[i])
                    {
                        X = x * slotwidth,
                        Y = y * slotwidth,
                        CanGrab = true,
                        CanPut = true
                    };
                    //s.BeforeItemChanged += new ItemSlot.ItemEventHandler((sender, e) => { if(((e as ItemSlot.ItemEventArgs).Item as GameObjects.Item) ==null) e.Cancel=true; });
                    s.ItemOut += new ItemSlot.ItemEventHandler((sender, e) => { Inventory.Items[i] = null; });
                    s.ItemIn += new ItemSlot.ItemEventHandler((sender, e) => { Inventory.Items[i] = ((e as ItemSlot.ItemEventArgs).Item as GameObjects.Item); });
                    this.AddControl(s);
                }
        }

                
    }
}
