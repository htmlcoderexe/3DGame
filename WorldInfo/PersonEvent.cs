using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldInfo
{
    public struct PersonEvent
    {
        public enum EventType
        {
            Death,Kill,Birth,Meet,Spawn, //birth is giving birth to someone, spawn is being born.
            //death/kill reciprocal the same way - although some deaths are not caused by a person so no correpsonding Kill
            //meet is self reciprocal, defines "start" of  a relationship of any kind
            LocationChange,RelationChange,
            BattleWith,BattleAgainst,
            CopulationWith,ScoreDummy
        }
        public Person Person;
        public Location Location;
        public int Time;
        public EventType Type;
        public int ScoreDelta;
        public PersonRelation.RelationType RelationChangeType;
    }
}
