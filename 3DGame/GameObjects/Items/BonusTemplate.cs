using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.GameObjects.Items
{
    public class BonusTemplate
    {
        public string Type;
        public int FlatMin;
        public int FlatMax;
        public int MultiMin;
        public int MultiMax;
        public string Effectstring;
        public int Weight;
        public BonusTemplate(string Line)
        {
            string[] parts = Line.Split(':');
            this.Type = parts[0];
            this.Effectstring = parts[1];
            string[] Flat = parts[2].Split('~');
            this.FlatMin = Int32.Parse(Flat[0]);
            this.FlatMax = Int32.Parse(Flat[1]);
            string[] Multi = parts[3].Split('~');
            this.MultiMin = Int32.Parse(Multi[0]);
            this.MultiMax = Int32.Parse(Multi[1]);
            this.Weight = Int32.Parse(parts[4]);
        }
    }
}
