using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObject.MapEntities;

namespace GameObject.AbilityLogic
{
    public abstract class AbilitySelector : ITimedEffect
    {
        public abstract string EffectType { get; }
        public float Time { get; set; }
        public virtual void Apply(Actor Source, Actor Target, int Level)
        {

        }

        public abstract List<MapEntities.Actor> GetTargets(Actor Source, Actor Target, int Level);

        public static AbilitySelector CreateEffect(string type, string[] parameters)
        {
            AbilitySelector ef = null;

            switch (type)
            {
                //case "around_target":
                  //  return new Selectors.SEL_around_target(parameters);
                
            }


            return ef;
        }
        public abstract string[] GetParamValues();
        public float Duration { get; set; }
    }
}
