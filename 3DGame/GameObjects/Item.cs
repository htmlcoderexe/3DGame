using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame.GameObjects
{
    public class Item : Interfaces.IActionIcon
    {
        public class Types
        {
            public const int Uknown = 0;
            public const int Material = 1;
            public const int Weapon = 2;
            public const int Equip = 3;
            public const int Consumable = 4;
        }
        public Color NameColour;
        public string Name;
        public string Description;
        public int SellPrice;
        public int Type;
        public int SubType;
        public int StackSize;
        public float CooldownPercentage { get; set; }
        public virtual List<string> GetTooltip()
        {
            List<string> tip = new List<string>();

            string code = GUI.Renderer.ColourToCode(this.NameColour);
            tip.Add(code + this.GetName());//+ "G" + this.Grade);

            tip.Add(GUI.Renderer.ColourToCode(GUI.Renderer.ColourYellow) + this.Description);

            return tip;
        }

        public virtual string GetName()
        {
            return GUI.Renderer.ColourToCode(this.NameColour) + this.Name;
        }

        public virtual void Render(int X, int Y, GraphicsDevice device, GUI.Renderer Renderer, bool RenderCooldown = false, bool RenderEXP = false)
        {

        }

        public void SecondaryAction()
        {

        }
        public void PrimaryAction()
        {

        }


    }
}
