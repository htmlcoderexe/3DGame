using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.GameObjects.Items
{
    /// <summary>
    /// Represents possible choices for item add-ons during random generation
    /// </summary>
    public class BonusPool
    {
        /// <summary>
        /// List of possible choices
        /// </summary>
        public List<BonusTemplate> Bonuses;
        /// <summary>
        /// Globally accessible, fixed list of all pools
        /// </summary>
        public static Dictionary<string, BonusPool> Pools;
        /// <summary>
        /// Selects specific add-on from possible options
        /// </summary>
        /// <returns>A template which contains possible ranges for the specific add-on</returns>
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
        /// <summary>
        /// Initializes the pool
        /// </summary>
        public BonusPool()
        {
            this.Bonuses = new List<BonusTemplate>();
        }

        /// <summary>
        /// Create an item add-on from the pool and pick values for it
        /// </summary>
        /// <returns>An ItemBonus with specific values</returns>
        public ItemBonus PickBonus()
        {
            BonusTemplate t = PickTemplate();
            return t.Generate();
        }
        /// <summary>
        /// Loads a specific addon pool on demand
        /// </summary>
        /// <param name="Name">The name of the pool to be loaded, located in gamedata/itemtemplates</param>
        /// <returns>An addon pool or null on failure</returns>
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
