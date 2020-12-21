using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldInfo
{
    public struct PersonRelation
    {
        public enum RelationType
        {
            None,
            Sibling,Parent,Child,
            Comrade,Superior,Subordinate,
            Enemy,Nemesis,Partner,Friend,
            Nonspecific
        }
        public Person Person;
        public int Strength;
        public RelationType Type;
    }
}
