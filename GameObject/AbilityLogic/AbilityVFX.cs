using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObject.MapEntities;

namespace GameObject.AbilityLogic
{
    public class AbilityVFX : ITimedEffect
    {
        public virtual void Apply(Actor Source, Actor Target, int Level)
        {
            
        }
        public float Duration { get; set; }
    }
}
