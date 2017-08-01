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
    public class ItemEquip :Item
    {
        public List<ItemBonus> Bonuses;
        public Items.Material PrimaryMaterial;
        public Items.Material SecondaryMaterial;
        public Enchantment Enchant;
        public ItemEquip()
        {
            this.Type = Types.Equip;
            this.Bonuses = new List<ItemBonus>();
            this.Name = "Equipment piece";
            this.NameColour= new Color(192, 0, 255);
        }
        public override void Render(int X, int Y, GraphicsDevice device, Renderer Renderer, bool RenderCooldown = false, bool RenderEXP = false)
        {
            Color PrimaryColour=this.PrimaryMaterial==null?Color.Gray:this.PrimaryMaterial.Colour;
            Color SecondaryColour=this.SecondaryMaterial==null?PrimaryColour:this.SecondaryMaterial.Colour;
            Color GlowColour=this.Enchant==null?PrimaryColour:this.Enchant.LineColour;

            Renderer.SetTexture(Renderer.InventoryPartsMap);
            Renderer.SetColour(PrimaryColour);
            Renderer.RenderIconEx(device, X, Y, 32 + this.SubType);
            Renderer.RenderIconEx(device, X, Y, 0 + this.SubType);
            Renderer.SetColour(SecondaryColour);
            Renderer.RenderIconEx(device, X, Y, 48 + this.SubType);
            Renderer.SetColour(GlowColour);
            Renderer.RenderIconEx(device, X, Y, 16 + this.SubType);

            base.Render(X, Y, device, Renderer, RenderCooldown, RenderEXP);
        }
        public override string GetName()
        {
            return base.GetName();
        }
        public override List<string> GetTooltip()
        {
            List<string> tip = base.GetTooltip();

            foreach(ItemBonus b in this.Bonuses)
            {
                tip.Add(GUI.Renderer.ColourToCode(b.LineColour)+b.BonusText);
            }
            if(this.Enchant!=null)
            {
                tip.Add(GUI.Renderer.ColourToCode(this.Enchant.LineColour) + this.Enchant.BonusText);
            }
            return tip ;
        }
    }
}
