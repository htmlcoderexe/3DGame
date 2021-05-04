using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameModel;
using GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameObject.Items
{
    /// <summary>
    /// Equippable Item that directly modifies a character's stats.
    /// </summary>
    public class ItemEquip : Item
    {
        /// <summary>
        /// Bonuses on the item applying on equip.
        /// </summary>
        public List<ItemBonus> Bonuses;
        public Items.Material PrimaryMaterial;
        public Items.Material SecondaryMaterial;
        public string Adjective;
        public string Subject;
        public Enchantment Enchant;
        public int EquipmentSlot;
        public int SocketCount;
        public Interfaces.ISocketable[] Sockets;
        /// <summary>
        /// Backing variable for refining level, also used to force recalculation
        /// </summary>
        private int _refiningLevel = 0;
        /// <summary>
        /// Item refining level, affects mainstats
        /// </summary>
        public int RefiningLevel
        {
            get
            {
                return _refiningLevel;
            }
            set
            {
                if(value!=_refiningLevel)
                {
                    _refiningLevel = value;
                    //delete precalculated mainstats, forcing a recalculation to current refine
                    _MainStats = null;
                }
            }
        }
        public const int StatCount= 6;
        /// <summary>
        /// Item's "main stat" values
        /// </summary>
        public int[] StatValues = new int[StatCount];
        /// <summary>
        /// Maps stat values to actor stats
        /// </summary>
        public string[] statmapping = new string[] { "HP", "MP", "PAtk", "MAtk", "PDef", "MDef" };
        /// <summary>
        /// Maps stat names to tooltip names
        /// </summary>
        public string[] statnames = new string[] { "HP", "MP", "Physical Attack", "Magic Attack", "Physical Defence", "Magic Defence" };

        /// <summary>
        /// Refining multipliers
        /// </summary>
        float[] multipliers = new float[]
        {
            1.0f,
            1.01f,1.02f,1.03f,1.05f,
            1.07f,1.09f,1.012f,1.15f,
            1.19f,1.24f,1.31f,1.40f,
            1.51f,1.64f,1.80f,2.0f,
            2.3f
        };

        /// <summary>
        /// Multiplier to apply to mainstats based on refine
        /// </summary>
        /// <param name="RefiningLevel">Refining level to calculate multiplier for</param>
        /// <returns>Multiplier</returns>
        public float GetRefinerMultiplier(int RefiningLevel)
        {
            //bounds checking
            if (RefiningLevel >= multipliers.Length)
                RefiningLevel = multipliers.Length - 1;
            if (RefiningLevel < 0)
                RefiningLevel = 0;
            //get result from table
            return multipliers[RefiningLevel];
        }

        /// <summary>
        /// This is the cached version used for (d)equipping
        /// </summary>
        List<ItemBonus> _MainStats;

        /// <summary>
        /// Item's mainstats converted to ItemBonus to apply on equip
        /// </summary>
        public List<ItemBonus> MainStats
        {
            get
            {
                //this always gets overwritten
                if(_MainStats==null)
                {
                    _MainStats = new List<ItemBonus>();


                    for (int i = 0; i < StatCount; i++)
                    {
                        //add any nonzero mainstats
                        if (StatValues[i] != 0)
                        {
                            ItemBonus b = new ItemBonus
                            {
                                //won't ever be displayed anyway
                                Effecttext = "",
                                FlatValue = StatValues[i]*GetRefinerMultiplier(this.RefiningLevel), //#TODO make refining affect this
                                Type = statmapping[i]
                            };
                            _MainStats.Add(b);
                        }
                    }
                }
                return _MainStats;
            }
        }

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
            public const int Necklace = 12;
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
            public const int Spear = 8;
            public const int SpellBlade = 9;
            public const int Fists = 10;
            public const int Knuckles = 11;
            public const int Claws = 12;
            public const int Saber = 13;
            public const int MagicOrb = 14;
            public const int HeavyHelmet = 15;
            public const int LightHelmet = 16;
            public const int Cap = 17;
            public const int ChestPlate = 18;
            public const int LightArmor = 19;
            public const int Robe = 20;
            public const int Greaves = 21;
            public const int Cuisses = 22;
            public const int Leggings = 23;
            public const int Gauntlets = 24;
            public const int Gloves = 25;
            public const int Mitts = 26;
            public const int Vambraces = 27;
            public const int Bracers = 28;
            public const int Sleeves = 29;
            public const int Sabatons = 30;
            public const int Boots = 31;
            public const int Shoes = 32;
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
                    case HeavyHelmet:
                        return "Helm";
                    case ChestPlate:
                        return "Chestplate";
                    case Vambraces:
                        return "Vambraces";
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
            this.StackSize = 1;
            this.Bonuses = new List<ItemBonus>();
            this.Name = "Equipment piece";
            this.NameColour= new Color(192, 0, 255);
            this.Adjective = Adjectives[RNG.Next(0, Adjectives.Count - 1)];
            this.Subject = Subjects[RNG.Next(0, Subjects.Count - 1)];
            this.SubType = RNG.Next(7);
            this.StatValues[2] = 35;
            this.RefiningLevel = 9;
            if (this.SubType == 1)
                this.Variant = RNG.Next(2);
            string[] grades = new string[] { "Common","Uncommon","Rare","Epic" };
            this.NameColour = Item.GradeToColour(grades[RNG.Next(0, grades.Length)]);
        }
        public override void Render(int X, int Y, GraphicsDevice device, Renderer Renderer, bool RenderCooldown = false, bool RenderEXP = false)
        {
            Color PrimaryColour=this.PrimaryMaterial==null?Color.Gray:this.PrimaryMaterial.Colour;
            Color SecondaryColour=this.SecondaryMaterial==null?PrimaryColour:this.SecondaryMaterial.Colour;
            Color GlowColour=this.Enchant==null?PrimaryColour:this.Enchant.LineColour;
            int subsubtype = this.SubType * 4 + this.Variant;
            int iconindex = (subsubtype%64) + (192 * (int)((float)subsubtype /64f));
            Renderer.SetTexture(Renderer.InventoryPartsMap);
            Renderer.SetColour(PrimaryColour);
            Renderer.RenderIconEx(device, X, Y, 0 + iconindex);
            Renderer.SetColour(SecondaryColour);
            Renderer.RenderIconEx(device, X, Y, 64 + iconindex);
            Renderer.SetColour(GlowColour);
            Renderer.RenderIconEx(device, X, Y, 128 + iconindex);

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

            if (this.RefiningLevel > 0)
                name += " +" + RefiningLevel;
            return name;
        }
        /// <summary>
        /// Gets a tooltip describing the item and its properties.
        /// </summary>
        /// <returns>Item tooltip as a list of lines.</returns>
        public override List<string> GetTooltip()
        {
            //get basic tooltip with title, description etc
            List<string> tip = base.GetTooltip();
            //debug stuff!
            tip.Add("Variant " + this.Variant.ToString());
            tip.Add("Made from:");
           
            if (PrimaryMaterial != null)
                tip.Add(PrimaryMaterial.GetName());
            if (SecondaryMaterial != null)
                tip.Add(SecondaryMaterial.GetName());
            //--end debug stuff

            //item "main stats" displayed if nonzero

            for(int i=0;i<StatCount;i++)
            {
                if (StatValues[i] != 0)
                    tip.Add(statnames[i] + ": " + StatValues[i]*GetRefinerMultiplier(this.RefiningLevel));
            }

            //show an enchant if any, coloured with enchant colour
            if (this.Enchant != null)
            {
                tip.Add(GUI.Renderer.ColourToCode(this.Enchant.LineColour) + this.Enchant.BonusText);
            }
            //show socketed items with bonuses if any
            if (SocketCount != 0)
            {
                for (int i = 0; i < SocketCount; i++)
                    tip.Add(GUI.Renderer.ColourToCode(Sockets[i].AddedEffect.LineColour) + Sockets[i].AddedEffect.BonusText);
                
            }
            //Show item adds
            foreach (ItemBonus b in this.Bonuses)
            {
                tip.Add(GUI.Renderer.ColourToCode(b.LineColour)+b.BonusText);
            }


            return tip ;
        }

        public override GameModel.Model GetModel()
        {
            Color PrimaryColour = this.PrimaryMaterial == null ? Color.Gray : this.PrimaryMaterial.Colour;
            Color SecondaryColour = this.SecondaryMaterial == null ? PrimaryColour : this.SecondaryMaterial.Colour;
            Color GlowColour = this.Enchant == null ? PrimaryColour : this.Enchant.LineColour;
            Dictionary<string, string> matcolor = new Dictionary<string, string>
            {
                { "$handle", GameModel.ModelGeometryCompiler.ColourToCode(SecondaryColour) },
                { "$blade", GameModel.ModelGeometryCompiler.ColourToCode(PrimaryColour) },
                { "$enchant", GameModel.ModelGeometryCompiler.ColourToCode(GlowColour) }
            };
            string modelname = "sword1"; //will get edited to select specific model

            return ModelGeometryCompiler.LoadModel(modelname, matcolor);
           // return base.GetModel();
        }
    }
}
