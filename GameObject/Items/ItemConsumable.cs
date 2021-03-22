using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameObject.Items
{
    public class ItemConsumable :Item
    {
        public Color Colour;

        

        public override void Render(int X, int Y, GraphicsDevice device, Renderer Renderer, bool RenderCooldown = false, bool RenderEXP = false)
        {
            Renderer.SetColour(this.Colour);
            Renderer.SetTexture(Renderer.InventoryPartsMap);
            Renderer.RenderIconEx(device, X, Y, 64*33 + this.SubType);
            Renderer.SetColour(new Color(127, 127, 127,127));
            base.Render(X, Y, device, Renderer, RenderCooldown, RenderEXP);
        }
    }
}
