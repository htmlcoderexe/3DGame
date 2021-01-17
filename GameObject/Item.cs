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
                case "Uncommon":
                    {
                        return new Color(120, 100, 255);
                    }
                case "Common":
                    {
                        return Color.White;
                    }
                case "Rare":
                    {
                        return new Color(192, 0, 255);

                    }
                case "Epic":
                    {
                        return new Color(255, 100, 20);
                    }
                default:
                    {
                        return Color.Gray;
                    }
            }
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
        public int StackSize { get; set; }

        public float CoolDown { get; set; }

        public float MaxCoolDown { get; set; }

        public int Icon { get; set; }

        public string ID { get; set; }

        string IActionIcon.Name { get; set; }

        public Item()
        {
            this.StackSize = 1;
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
