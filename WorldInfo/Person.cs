using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldInfo
{
    public struct Person
    {
        public string Name1;
        public string Name2;
        public string Nickname;
        public string FullName { get =>  Name1 + " \"" + Nickname + "\" " + Name2; }

        public List<PersonEvent> LifeEvents;
        public List<PersonRelation> Relationships;

        public List<PersonEvent> FindEventOfType(PersonEvent.EventType T)
        {
            return (from etype in LifeEvents where etype.Type == T select etype).ToList();
        }
        public List<PersonRelation> FindRelationOfType(PersonRelation.RelationType T)
        {
            return (from etype in Relationships where etype.Type == T select etype).ToList();
        }
    }
}
