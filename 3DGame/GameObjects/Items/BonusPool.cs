using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.GameObjects.Items
{
    class BonusPool
    {
        public List<BonusTemplate> Bonuses;
        public static Dictionary<string, BonusPool> Pools;
        private BonusTemplate PickTemplate()
        {
            int Total = Bonuses.Sum(b => b.Weight);
            int Pick = RNG.Next(1, Total);
            foreach(BonusTemplate b in this.Bonuses)
            {
                if (Pick < b.Weight)
                    return b;
                Pick -= b.Weight;
            }
            return null;
        }
        public BonusPool()
        {
            this.Bonuses = new List<BonusTemplate>();
        }
        public ItemBonus PickBonus()
        {
            BonusTemplate t = PickTemplate();
            float Flat = RNG.Next(t.FlatMin, t.FlatMax);
            float Multi = RNG.Next(t.MultiMin, t.MultiMax)/100.0f;
            ItemBonus b = new ItemBonus() { FlatValue = Flat, Multiplier = Multi, Effecttext = t.Effectstring, LineColour = GUI.Renderer.ColourBlue };
            return b;
        }
        public static BonusPool Load(string Name)
        {
            if (Pools == null)
                Pools = new Dictionary<string, BonusPool>();
            if (Pools.ContainsKey(Name))
                return Pools[Name];
            BonusPool p = new BonusPool();
            FileStream fs;
            string cd = System.Reflection.Assembly.GetExecutingAssembly().Location;
            cd = System.IO.Path.GetDirectoryName(cd);
            string filename = cd + "\\gamedata\\itemtemplates\\"+Name+".gdf";
            try
            {
                fs = new FileStream(filename, FileMode.Open);
            }
            catch
            {
                Console.Write("^FF0000 Error loading bonus pool ^FFFF00 "+Name);
                
                return null;
            }
            StreamReader st = new StreamReader(fs);
            string line;
            int count = 0;
            while ((line = st.ReadLine()) != null)
            {
                p.Bonuses.Add(new BonusTemplate(line));
                count++;
            }
            fs.Close();
            Console.Write("^00FF00 Loaded bonus pool ^FFFF00 " + Name);
            Pools.Add(Name, p);
            return p;
        }
    }
}
