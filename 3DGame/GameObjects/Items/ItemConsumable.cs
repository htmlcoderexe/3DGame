using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame.GameObjects.Items
{
    public class ItemConsumable :Item
    {
        public Color Colour;

        

        public override void Render(int X, int Y, GraphicsDevice device, Renderer Renderer, bool RenderCooldown = false, bool RenderEXP = false)
        {
            Renderer.SetColour(this.Colour);
            Renderer.SetTexture(Renderer.InventoryPartsMap);
            Renderer.RenderIconEx(device, X, Y, 144 + this.SubType);
            //base.Render(device, X, Y, RenderCooldown, RenderEXP);
            Renderer.SetColour(new Color(127, 127, 127,127));
            base.Render(X, Y, device, Renderer, RenderCooldown, RenderEXP);
        }
    }
}
