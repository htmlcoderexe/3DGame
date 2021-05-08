using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameObject
{
    public class Item :ICloneable, GUI.IActionIcon, Interfaces.IGameID
    {
        public class Types
        {
            public const int Uknown = 0;
            public const int Material = 1;
            public const int Weapon = 2;
            public const int Equip = 3;
            public const int Consumable = 4;
        }
        public static Color GradeToColour(string Grade)
        {
            switch (Grade)
            {
                case "Rare":
                    {
                        return new Color(120, 100, 255);
                    }
                case "Common":
                    {
                        return Color.White;
                    }
                case "Epic":
                    {
                        return new Color(192, 0, 255);

                    }
                case "Legendary":
                    {
                        return new Color(255, 100, 20);
                    }
                case "Uncommon":
                    {
                        return new Color(0, 204, 0);
                    }
                default:
                    {
                        return Color.Gray;
                    }
            }
        }
        public static string FormatMoney(int Amount, bool UseColours=true)
        {

            string raw = Amount.ToString();
            string buffer = "";
            for(int i=0;i<raw.Length;i++)
            {
                if (i > 1 && (i) % 3 == 0)
                {
                    buffer = " " + buffer;
                }
                buffer = raw[raw.Length-1-i] +buffer;
            }
            string prefix = "";
            if(UseColours)
            {
                if (Amount >= 5000)
                    prefix = "^FFFF7F ";
                if (Amount >= 50000)
                    prefix = "^FF7F00 ";
            }
            return prefix+buffer;
        }
        public virtual bool CanStackWith(Item other)
        {
            if (this.Name != other.Name)
                return false;
            if (this.GetName() != other.GetName())
                return false;
            if (this.Type != other.Type)
                return false;
            if (this.SubType != other.SubType)
                return false;
            if (this.NameColour != other.NameColour)
                return false;
            if (this.SellPrice != other.SellPrice)
                return false;


            return true;
        }
        private string ModelName = "";
        public Color NameColour;
        public string Name;
        public string Description;
        public int SellPrice;
        public int Type;
        public int SubType;
        public int Variant;
        /// <summary>
        /// Amount of this item in this Item stack.
        /// </summary>
        public int StackSize { get; set; }

        /// <summary>
        /// Maximum amount a stack of this item can be.
        /// </summary>
        public int MaxStackSize { get; set; }
        /// <summary>
        /// Current cooldown value.
        /// </summary>
        public float CoolDown { get; set; }
        /// <summary>
        /// Cooldown value in seconds when item is used, if applicable.
        /// </summary>
        public float MaxCoolDown { get; set; }
        /// <summary>
        /// Item icon.
        /// </summary>
        public int Icon { get; set; }
        /// <summary>
        /// Item ID for storage and reference purposes.
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Name of the item to be displayed in some contets. I don't actually think this is fucking used...
        /// </summary>
        string IActionIcon.Name { get; set; }

        public Item()
        {
            this.StackSize = 1;
            this.MaxStackSize = 100;
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone(); 
        }
        public virtual List<string> GetTooltip()
        {
            List<string> tip = new List<string>();

            string code = GUI.Renderer.ColourToCode(this.NameColour);
            tip.Add(code+ this.GetName());//+ "G" + this.Grade);

            tip.Add(GUI.Renderer.ColourToCode(GUI.Renderer.ColourYellow) + this.Description);

            return tip;
        }
        
        public virtual string GetName()
        {
            return GUI.Renderer.ColourToCode(this.NameColour) + this.Name;
        }

        public virtual void Render(int X, int Y, GraphicsDevice device, GUI.Renderer Renderer, bool RenderCooldown = false, bool RenderEXP = false)
        {
          if (this.StackSize > 1)
                Renderer.RenderSmallText(device, X,Y + 20, this.StackSize.ToString(), Color.White, true, false);
        }
        

        public virtual void Use()
        {
            
        }

        public virtual GameModel.Model GetModel()
        {
            if (this.ModelName != null && this.ModelName != "")
            return GameModel.ModelGeometryCompiler.LoadModel(this.ModelName);
            return null;
        }
    }
}
