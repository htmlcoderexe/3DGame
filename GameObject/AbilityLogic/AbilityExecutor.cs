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
        public ModularAbility Original;
        public MapEntities.Actor Target;
        //this will get loaded from target selector blocks and will then apply next effect to all targets.
        //should enable different selectors like "user only" "allies within 5m" "enemies in a line in front of user" etc
        public List<MapEntities.Actor> TargetBuffer = new List<MapEntities.Actor>();
        float Timeline;
        public bool done;

        public AbilityExecutor(ModularAbility ability,MapEntities.Actor Source, MapEntities.Actor Target)
        {
            this.Original = ability;
            this.ability = ability.GetEffectiveAbility();
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

            if (this.ability.EffectTimeline.Count() <= 0)
            {
                this.done = true;
                this.Original.CoolDown = ability.MaxCoolDown;
                return;
            }
            while(Timeline > ability.EffectTimeline.Keys[0])
            {
                float current = ability.EffectTimeline.Keys[0];
                List<ITimedEffect> effects = ability.EffectTimeline.Get(current);
                foreach(ITimedEffect effect in effects)
                {

                    //if it is a selector, query for targets and skip to next effect
                    if (effect is AbilitySelector selector)
                    {
                        this.TargetBuffer = selector.GetTargets(Source, Target, ability.Level);
                        continue;
                    }
                    //if a selector returned valid targets before, use these, else just teh user selected target
                    if (TargetBuffer != null && TargetBuffer.Count > 0)
                    {
                        foreach (MapEntities.Actor target in TargetBuffer)
                            effect.Apply(Source, target, ability.Level); //note SMALL t
                    }
                    else
                    {
                        
                        effect.Apply(Source, Target, ability.Level);
                    }
                }
                //discard the effect
                ability.EffectTimeline.Remove(current);
                //if out of effects, stop ability (TODO: constant channel abilities should have a flag that's checked here
                if (this.ability.EffectTimeline.Count() <= 0)
                {
                    this.done = true;
                    this.Original.CoolDown = ability.MaxCoolDown;
                    Source.CurrentMPBuffer -= this.Original.GetValue("mp_cost");
                    break;
                }
            }


            Timeline += dT;
        }
    }
}
