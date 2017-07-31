using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame.Scenes.GameplayAssets
{
    public class ItemSlot : GUI.Control
    {
        public GameObjects.Item Item;
        public ItemSlot(GameObjects.Item Item)
        {
            this.Item = Item;
            this.Width = 40;
            this.Height = 40;
        }
        public override void Render(GraphicsDevice device, Renderer Renderer, int X, int Y)
        {
            X += this.X;
            Y += this.Y;
            Renderer.SetTexture(Renderer.WindowSkin);
            Renderer.Rect r = new Renderer.Rect(48, 48, 40, 40);
            Renderer.RenderQuad(device, X, Y, Width, Height, r);
            if(Item!=null)
            Item.Render(X+4, Y+4, device, Renderer, false, false);
            base.Render(device, Renderer, X, Y);
        }
        public override void Click(float X, float Y)
        {
            if(this.Item!=null)
            {
                ToolTipWindow tip = new ToolTipWindow(this.WM,this.Item.GetTooltip(), (int)this.X+(int)X, this.Y+(int)Y, false);
                WM.Add(tip);
            }
            base.Click(X, Y);
        }
    }
}
