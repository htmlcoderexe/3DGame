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
        public float GetTime(int Level)
        {
            return BaseTime + DeltaTime * (Level - 1);
        }
        public float GetDuration(int Level)
        {
            return BaseDuration + DeltaDuration * (Level - 1);
        }
        public float BaseDuration { get; set; }
        public float DeltaDuration { get; set; }
        public float BaseTime { get; set; }
        public float DeltaTime { get; set; }
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
        public string[] ParamValues { get; set; }
        public abstract void SetParamValues(string[] Values,bool DeleteThis);
    }
}
