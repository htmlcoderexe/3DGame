using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.GameObjects.AbilityLogic
{
    public class AbilityEffect
    {
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

        public virtual void Apply(GameObjects.MapEntities.Actor Source, GameObjects.MapEntities.Actor Target)
        {

        }
        public virtual List<string> GetParams(int Level)
        {
            return new List<string>();
        }
        public static AbilityEffect CreateEffect(string type,string[] parameters)
        {
            AbilityEffect ef= new AbilityEffect();

            switch (type)
            {
                case "damage_bmd_full":
                    return new Effects.Effect_damage_bmd_full(parameters);
                case "dot_mwp":
                    return new Effects.Effect_dot_mwp(parameters);
            }


            return ef;
        }
    }
}
