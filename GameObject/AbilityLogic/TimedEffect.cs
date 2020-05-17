using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.AbilityLogic
{
    public interface ITimedEffect
    {
        void Apply(MapEntities.Actor Source, MapEntities.Actor Target, int Level);
        float Duration { get; set; }
        string EffectType { get; }
        
    }
}
