using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects;
using GUI;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame.Scenes.GameplayAssets
{
    public class ItemSlot : GUI.Control
    {
        public GUI.IActionIcon Item;
        public bool CanGrab;
        public bool CanPut;
        public delegate void ItemEventHandler(object sender, ItemEventArgs e);
        public class ItemEventArgs : System.ComponentModel.CancelEventArgs
        {
            public GUI.IActionIcon Item;
            public ItemEventArgs(GUI.IActionIcon Item)
            {
                this.Item = Item;
            }
        }
        public event ItemEventHandler ItemOut;
        public event ItemEventHandler ItemIn;
        public event ItemEventHandler BeforeItemChanged;
        public ItemSlot(GUI.IActionIcon Item)
        {
            this.Item = Item;
            this.Width = 40;
            this.Height = 40;
            this.CanGrab = true;
        }
        public override void Render(GraphicsDevice device, Renderer Renderer, int X, int Y)
        {
            X += this.X;
            Y += this.Y;
            Renderer.SetTexture(Renderer.WindowSkin);
            Renderer.Rect r = new Renderer.Rect(48, 48, 40, 40); //#TODO: dynamic skin positioning!!
            Renderer.RenderQuad(device, X, Y, Width, Height, r);
            if(Item!=null)
            Item.Render(X+4, Y+4, device, Renderer, false, false);
            base.Render(device, Renderer, X, Y);
        }
        public override void Click(float X, float Y)
        {
            if (!CanGrab && !CanPut) //nothing to do here
                return;
            IActionIcon mouseItem=WM.MouseGrab;
            IActionIcon currentItem=this.Item;
            if(mouseItem==null)
            {
                if(currentItem==null) //nothing to do here
                {
                    return;
                }
                else // take the item
                {
                    WM.MouseGrab =currentItem;
                    ItemOut?.Invoke(this, new ItemEventArgs(currentItem));
                    this.Item = null;
                }
            }
            else //if mouse has something
            {
                if (!CanPut) //can't grab item: mouse full
                    return;
                ItemEventArgs iargs = new ItemEventArgs(mouseItem);
                BeforeItemChanged?.Invoke(this, iargs);
                if (iargs.Cancel) //custom function declined the item or no custom function
                    return;
                if (currentItem==null) //put in item
                {
                    this.Item = mouseItem;
                    ItemIn?.Invoke(this, new ItemEventArgs(mouseItem));
                    WM.MouseGrab = null;

                }
                else //swap items
                {
                    this.Item = mouseItem;
                    ItemIn?.Invoke(this, new ItemEventArgs(mouseItem));
                    WM.MouseGrab = currentItem;
                    ItemOut?.Invoke(this, new ItemEventArgs(currentItem));

                }
            }
            base.Click(X, Y);
        }
        public override void MouseMove(float X, float Y)
        {
            if (WM.MouseStillSeconds > 0.5f && this.Item!=null)
            {
                ToolTipWindow tip = new ToolTipWindow(this.WM, this.Item.GetTooltip(), WM.MouseX, WM.MouseY, true);
                WM.Add(tip);
            }
        }
    }
}
