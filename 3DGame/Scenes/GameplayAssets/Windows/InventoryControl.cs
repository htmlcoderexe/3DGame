using GameObject.Items;
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
                    //s.BeforeItemChanged += new ItemSlot.ItemEventHandler((sender, e) => { if(((e as ItemSlot.ItemEventArgs).Item as GameObject.Item) ==null) e.Cancel=true; });
                    s.BeforeItemChanged += CheckItem;
                    s.ItemOut += new ItemSlot.ItemEventHandler((sender, e) => { Inventory.Items[i] = s.Item as GameObject.Item;
                        Inventory.Prepare();
                        Inventory.Changed = true;
                    });
                    s.ItemIn += new ItemSlot.ItemEventHandler((sender, e) => {

                        GameObject.Item itemin = ((e as ItemSlot.ItemEventArgs).Item as GameObject.Item);

                        if (s.Item != null && itemin != null && itemin.CanStackWith(s.Item as GameObject.Item))
                        {
                            itemin.StackSize += (s.Item as GameObject.Item).StackSize;
                            e.Cancel = true;
                            s.Item = itemin;
                            WM.MouseGrab = null;
                            Inventory.Items[i] = (s.Item as GameObject.Item);
                            Inventory.Changed = true;
                        }
                        else
                        {

                            Inventory.Prepare();
                            Inventory.AddItem(itemin, i);
                            Inventory.Commit();
                        }

                    });
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
                    s.BeforeItemChanged += CheckItem;
                    s.ItemOut += new ItemSlot.ItemEventHandler((sender, e) => { Inventory.Items[i] = s.Item as GameObject.Item;
                        Inventory.Prepare();
                        Inventory.Changed = true;
                    });
                    s.ItemIn += new ItemSlot.ItemEventHandler((sender, e) => {

                        GameObject.Item itemin = ((e as ItemSlot.ItemEventArgs).Item as GameObject.Item);

                        if (s.Item != null && itemin != null && itemin.CanStackWith(s.Item as GameObject.Item))
                        {
                            itemin.StackSize += (s.Item as GameObject.Item).StackSize;
                            e.Cancel = true;
                            s.Item = itemin;
                            WM.MouseGrab = null;
                            Inventory.Items[i] = (s.Item as GameObject.Item);
                            Inventory.Changed = true;
                        }
                        else
                        {

                            Inventory.Prepare();
                            Inventory.AddItem(itemin, i);
                            Inventory.Commit();
                        }

                    });
                    this.AddControl(s);
                }
        }

        void CheckItem(object sender, ItemSlot.ItemEventArgs e)
		{
            GameObject.Item i = ((e as ItemSlot.ItemEventArgs).Item as GameObject.Item);
            if (i == null)
                e.Cancel = true;
        }


    }
}
