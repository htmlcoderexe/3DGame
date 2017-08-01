using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.GameObjects
{
    public class StatBonus
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

    }
}
