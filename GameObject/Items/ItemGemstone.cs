using GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.Items
{
    public class ItemGemstone :Item, Interfaces.ISocketable
    {
        public int Level { get; set; }
        public string Stat { get; set; }
        public int Boost { get; set; }
        public string BoostString { get { return "^60FFFF " + this.Stat + " +" + this.Boost; } }

        private ItemBonus _effect;

        public ItemBonus AddedEffect
        { get
            {
                if(_effect==null)
                {
                    _effect = new ItemBonus();
                    _effect.Effecttext = this.Stat + " Gem: " + this.Stat + " +" + this.Boost;
                    _effect.FlatValue = this.Boost;
                    _effect.LineColour = new Color(96, 255, 255);
                    _effect.Order = StatBonus.StatOrder.Socket;
                    _effect.Type = this.Stat;
                }
                return _effect;
            }
        }

        //#TODO: load this from a file
        public static Dictionary<string, Color> StatGemColours = new Dictionary<string, Color>()
        {
            { "HP", new Color(237, 27, 84) },

            { "MP", new Color(102, 153, 255) },

            { "PDef", new Color(153, 77, 0) },

            { "MDef", new Color(0, 204, 153) },

            { "PAtk", new Color(230, 92, 0) },

            { "MAtk", new Color(153, 102, 255) }

        };

        


        public ItemGemstone(string Stat, int Level)
        {
            this.Level = Level;
            this.Stat = Stat;
            this.Name = this.Stat + " gem";
            this.Boost = -5 + Level * 10;
            this.NameColour = GradeToColour("Common");
            switch(Level)
            {
                case 3:
                case 4:
                case 5:
                    {
                        this.NameColour = GradeToColour("Uncommon");
                        break;
                    }
                case 6:
                case 7:
                case 8:
                case 9:
                    {
                        this.NameColour = GradeToColour("Rare");
                        break;
                    }
                case 10:
                case 11:
                case 12:
                case 13:
                    {
                        this.NameColour = GradeToColour("Epic");
                        break;
                    }
                case 14:
                case 15:
                case 16:
                    {
                        this.NameColour = GradeToColour("Legendary");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public override bool CanStackWith(Item other)
        {
            if (this.Level != (other as ItemGemstone)?.Level)
                return false;
            return base.CanStackWith(other);
        }

        public override void Render(int X, int Y, GraphicsDevice device, Renderer Renderer, bool RenderCooldown = false, bool RenderEXP = false)
        {
            Color C = new Color(0.5f, 0.5f, 0.5f);
            StatGemColours.TryGetValue(this.Stat, out C);
            Renderer.SetColour(C);
            Renderer.SetTexture(Renderer.InventoryPartsMap);
            Renderer.RenderIconEx(device, X, Y, 64*31 + this.Level-1);
            //base.Render(device, X, Y, RenderCooldown, RenderEXP);
            Renderer.SetColour(new Color(127, 127, 127, 127));
            base.Render(X, Y, device, Renderer, RenderCooldown, RenderEXP);
        }
        public override List<string> GetTooltip()
        {
            List<string> tip = base.GetTooltip();

            tip.Add("Level " + this.Level);
            tip.Add(BoostString);


            return tip;
        }

    }
}
