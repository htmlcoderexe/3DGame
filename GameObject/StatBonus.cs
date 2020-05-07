using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    public class StatBonus : ICloneable
    {
        public float FlatValue;
        public float Multiplier;
        public string Type;
        public string Description;
        public StatOrder Order;
        public enum StatOrder
        {
            Template,Character,Equip,Effect,Count
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
