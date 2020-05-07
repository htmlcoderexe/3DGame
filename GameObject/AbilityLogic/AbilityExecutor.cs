using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.AbilityLogic
{
    public class AbilityExecutor
    {
        public Ability ability;
        public MapEntities.Actor Source;
        public MapEntities.Actor Target;
        float Timeline;
        public bool done;

        public AbilityExecutor(Ability ability,MapEntities.Actor Source, MapEntities.Actor Target)
        {
            this.ability = ability;
            this.Source = Source;
            this.Target = Target;
        }

        public float ChargeProgress
        {
            get
            {
                return Math.Min(1.0f, Timeline / ability.GetCurrentChargeTime());
            }
        }
        public float CastProgress
        {
            get
            {
                return Math.Min(1.0f, (Timeline-ability.GetCurrentChargeTime()) / ability.GetCurrentCastTime());
            }
        }
        public void Update(float dT)
        {
            if (this.done)
                return;
            Timeline += dT;
            if(this.CastProgress>=1f)
            {
                this.ability.Use(Source, Target);
                Console.Write(this.ability.FormatDescription());
                this.done = true;
            }
        }
    }
}
