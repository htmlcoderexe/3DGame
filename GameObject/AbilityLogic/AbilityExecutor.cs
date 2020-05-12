using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.AbilityLogic
{
    public class AbilityExecutor
    {
        public EffectiveAbility ability;
        public MapEntities.Actor Source;
        public MapEntities.Actor Target;
        float Timeline;
        public bool done;

        public AbilityExecutor(EffectiveAbility ability,MapEntities.Actor Source, MapEntities.Actor Target)
        {
            this.ability = ability;
            this.Source = Source;
            this.Target = Target;
        }

        public float ChargeProgress
        {
            get
            {
                return Math.Min(1.0f, Timeline / ability.ChannelTime);
            }
        }
        public float CastProgress
        {
            get
            {
                return Math.Min(1.0f, (Timeline-ability.ChannelTime) / ability.CastTime);
            }
        }
        public void Update(float dT)
        {
            if (this.done)
                return;

            if (this.ability.EffectTimeline.Count <= 0)
            {
                this.done = true;
                return;
            }
            while(Timeline > ability.EffectTimeline.Keys[0])
            {

                AbilityLogic.ITimedEffect effect = ability.EffectTimeline.Values[0];
                effect.Apply(Source, Target, ability.Level);
                ability.EffectTimeline.Remove(ability.EffectTimeline.Keys[0]);
                if (this.ability.EffectTimeline.Count <= 0)
                {
                    this.done = true;
                    break;
                }
            }


            Timeline += dT;
        }
    }
}
