using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObject.MapEntities;

namespace GameObject.AbilityLogic
{
    public abstract class AbilityVFX : ITimedEffect
    {
        public abstract string EffectType { get; }
        public float Time { get; set; }
        public virtual void Apply(Actor Source, Actor Target, int Level)
        {

        }

        public static AbilityVFX CreateEffect(string type, string[] parameters)
        {
            AbilityVFX ef = null;

            switch (type)
            {
                case "throw_ball":
                    return new VisualEffects.VFX_throw_ball(parameters);
                case "charge_ball":
                    return new VisualEffects.VFX_charge_ball(parameters);
                case "animate":
                    return new VisualEffects.VFX_animate(parameters);
            }


            return ef;
        }

        public abstract string[] GetParamValues();
        public float Duration { get; set; }
    }
}
