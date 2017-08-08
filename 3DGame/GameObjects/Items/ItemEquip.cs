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
        public string Adjective;
        public string Subject;
        public Enchantment Enchant;
        public int EquipmentSlot;
        public class EquipSlot
        {
            public const int RightArm = 0;
            public const int LeftArm = 1;
            public const int Chest = 2;
            public const int Legs = 3;
            public const int Arms = 4;
            public const int Feet = 5;
            public const int Ring0 = 6;
            public const int Ring1 = 7;
            public const int Ring2 = 8;
            public const int Head = 9;
            public const int Gloves = 10;
            public const int Scarf = 11;
            public const int Necklase = 12;
            public const int Spirit = 13;
            public const int Bonus = 14;

            public const int Max = 15;
        }

        public class EquipType
        {
            public const int Sword = 0;
            public const int Blade = 1;
            public const int Dagger = 2;
            public const int Axe = 3;
            public const int Hammer = 4;
            public const int Staff = 5;
            public const int Wand = 6;
            public const int SpellRing = 7;
            public static string GetTypeName(int Type)
            {
                switch (Type)
                {
                    case Sword:
                        return "Sword";
                    case Blade:
                        return "Blade";
                    case Dagger:
                        return "Dagger";
                    case Axe:
                        return "Axe";
                    case Hammer:
                        return "Hammer";
                    case Staff:
                        return "Staff";
                    case Wand:
                        return "Wand";
                    default:
                        return "Weapon";
                }

            }

        }
        public static List<string> Adjectives = new List<string> { "Dwarven", "Menacing", "Great", "Slaying", "Flawless" };
        public static List<string> Subjects = new List<string> { "Killing", "Victory", "Greatness", "Destruction", "Glory" };

        public ItemEquip()
        {
            this.Type = Types.Equip;
            this.Bonuses = new List<ItemBonus>();
            this.Name = "Equipment piece";
            this.NameColour= new Color(192, 0, 255);
            this.Adjective = Adjectives[RNG.Next(0, Adjectives.Count - 1)];
            this.Subject = Subjects[RNG.Next(0, Subjects.Count - 1)];
            this.SubType = RNG.Next(7);
            string[] grades = new string[] { "Common","Uncommon","Rare","Epic" };
            this.NameColour = Items.Material.MaterialTemplates.GradeToColour(grades[RNG.Next(0, grades.Length)]);
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
            string name = "";
            if (this.Adjective != "")
                name+= this.Adjective + " ";
            name += ItemEquip.EquipType.GetTypeName(this.SubType) + " ";
            if (this.Subject != "")
                name+= "of " + this.Subject;
            return name;
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
