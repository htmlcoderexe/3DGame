using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace _3DGame.GameObjects.Items
{
    public class Material : Item
    {
        public class MaterialType
        {
            public const int Bar = 0;
            public const int Sheet = 1;
            public const int Powder = 2;
            public const int Thread = 3;
            public const int Rod = 4;
            public const int Coil = 5;
            public const int Crystal = 6;
            public const int Max = 7;
            public static string GetTypeName(int Type)
            {
                switch (Type)
                {
                    case Bar:
                        return "bar";
                    case Sheet:
                        return "sheet";
                    case Powder:
                        return "powder";
                    case Thread:
                        return "thread";
                    case Rod:
                        return "rod";
                    case Coil:
                        return "coil";
                    case Crystal:
                        return "crystal";
                    default:
                        return "";
                }

            }

        }
        public class MaterialTemplates
        {
            public static Material Gold = new Material          {Type=Item.Types.Material, NameColour = new Color(192,0,255),        SubType = MaterialType.Bar,    Name = "Golden bar", SellPrice = 500, Density = 200, Hardness = 10, Magic = 100, Colour = Color.Gold };
            public static Material Iron = new Material          {Type=Item.Types.Material, NameColour = new Color(120, 100, 255),    SubType = MaterialType.Bar,    Name = "Iron bar", SellPrice = 25, Density = 80, Hardness = 100, Magic = 10, Colour = Color.LightGray };
            public static Material IronSheet = new Material     {Type=Item.Types.Material, NameColour = new Color(120, 100, 255),    SubType = MaterialType.Sheet,  Name = "Iron sheet", SellPrice = 25, Density = 80, Hardness = 100, Magic = 10, Colour = Color.LightGray };
            public static Material GlassSheet = new Material    {Type=Item.Types.Material, NameColour = new Color(120, 100, 255),    SubType = MaterialType.Sheet,  Name = "Glass sheet", SellPrice = 25, Density = 50, Hardness = 80, Magic = 10, Colour = new Color(200, 240, 255) };
            public static Material CopperSheet = new Material   {Type=Item.Types.Material, NameColour = new Color(120, 100, 255),    SubType = MaterialType.Sheet,  Name = "Copper sheet", SellPrice = 150, Density = 100, Hardness = 30, Magic = 10, Colour = new Color(200, 50, 50) };
            public static List<Material> Materials;

            public static Material GetMaterialById(int ID)
            {
                switch (ID)
                {
                    case 0:
                        {
                            return Gold;
                        }
                    case 1:
                        {
                            return Iron;
                        }
                    case 2:
                        {
                            return IronSheet;
                        }
                    case 3:
                        {
                            return GlassSheet;
                        }
                    case 4:
                        {
                            return CopperSheet;
                        }
                    default:
                        {
                            return Gold;
                        }
                }

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
                            return  Color.White;
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
            static Material MaterialFromLine(string Line)
            {
                string[] parts = Line.Split(':');
                Material m = new Material();
                m.Name = parts[0];
                m.SellPrice = Int32.Parse(parts[2]);
                m.Density = Int32.Parse(parts[3]);
                m.Hardness = Int32.Parse(parts[4]);
                m.Magic = Int32.Parse(parts[5]);
                string[] rgb = parts[1].Split(',');
                int r = Int32.Parse(rgb[0]);
                int g = Int32.Parse(rgb[1]);
                int b = Int32.Parse(rgb[2]);
                m.Colour = new Color(r, g, b);
                m.NameColour = GradeToColour(parts[6]);
                return m;
            }
            public static void Load()
            {
                Materials = new List<Material>();
                FileStream fs;
                string cd = System.Reflection.Assembly.GetExecutingAssembly().Location;
                cd = System.IO.Path.GetDirectoryName(cd);
                string filename = cd + "\\gamedata\\materials.gdf";
                try
                {
                    fs = new FileStream(filename, FileMode.Open);
                }
                catch
                {
                    Console.Write("^FF0000 Error loading materials");
                    Materials.Add(Iron);
                    return;
                }
                StreamReader st = new StreamReader(fs);
                string line;
                int count = 0;
                while ((line = st.ReadLine()) != null)
                {
                    Materials.Add(MaterialFromLine(line));
                    count++;
                }
                fs.Close();
                Console.Write("^00FF00 Loaded " + count + " materials.");
            }
            public static Material GetRandomMaterial()
            {
                return (Material)Materials[RNG.Next(0, Materials.Count)].Clone();

            }
        }

        public Material()
        {
            this.Type = Item.Types.Material;
        }
        //   public int Type;
        public override void Render(int X, int Y, GraphicsDevice device, GUI.Renderer Renderer, bool RenderCooldown = false, bool RenderEXP = false)
        {
            Renderer.SetColour(this.Colour);
            Renderer.SetTexture(Renderer.InventoryPartsMap);
            Renderer.RenderIconEx(device, X, Y, 128 + this.SubType); //body
            //base.Render(device, X, Y, RenderCooldown, RenderEXP);
            Renderer.SetColour(new Color(127, 127, 127, 127));
            // Volatile.Console.Write("a");

            base.Render(X, Y, device, Renderer, RenderCooldown, RenderEXP);
        }
        public override string GetName()
        {
            return this.Name + " " + MaterialType.GetTypeName(this.SubType);
        }
        public int Density { get; set; }
        public int Hardness { get; set; }
        public int Magic { get; set; }
        public Color Colour { get; set; }
        public override object Clone()
        {
            Material m = new Material
            {
                Name = this.Name,
                Density = this.Density,
                Magic = this.Magic,
                SellPrice = this.SellPrice,
                SubType = this.SubType
            };
            m.SubType = this.SubType;
            m.Hardness = this.Hardness;
            m.Colour = this.Colour;
            m.NameColour = this.NameColour;
            m.StackSize++;
            return m;
        }
        public override List<string> GetTooltip()
        {


            List<string> tip = base.GetTooltip();                                   //  string TypeName = ItemWeaponType.Names[this.SubType];
                                                 //  tip+=Rendering.GUIDraw.ColourToCode(GUIDraw.ColourGray) + TypeName);
            tip.Add("Density:           ^7F7FFF " + this.Density);
            tip.Add("Hardness:           ^7F7FFF " + this.Hardness);
            tip.Add("Price:         " + GUI.Renderer.ColourToCode(GUI.Renderer.ColourGreen) + this.SellPrice);
            return tip;
        }
    }
}

