using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.AbilityLogic
{
    public abstract class AbilityEffect : ITimedEffect
    {
        public abstract string EffectType { get; }
        public float Time { get; set; }
        public float Probability;
        public float ProbabilityGrowth;

        public AbilityAnimation Animation;
        public EffectTarget Target;

        internal string[] _rawparams;

        public enum EffectTarget
        {
            Target,
            User,
            TargetArea,
            UserArea,
            UserAreaExclude
        }

        public virtual void Apply(MapEntities.Actor Source, MapEntities.Actor Target, int Level)
        {

        }

        public float Duration { get; set; }
        public virtual List<string> GetParams(int Level)
        {
            return new List<string>();
        }
        public static AbilityEffect CreateEffect(string type,string[] parameters)
        {
            AbilityEffect ef = null;

            switch (type)
            {
                case "damage_bmd_full":
                    return new GameEffects.Effect_damage_bmd_full(parameters);
                case "dot_mwp":
                    return new GameEffects.Effect_dot_mwp(parameters);
            }


            return ef;
        }
    }
}
