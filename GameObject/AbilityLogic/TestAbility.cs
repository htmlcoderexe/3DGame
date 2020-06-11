using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.AbilityLogic
{
    public class TestAbility : ModularAbility
    {
        public TestAbility()
        {

            this.BaseValues = new Dictionary<string, float>();
            this.GrowthValues = new Dictionary<string, float>();

            //DATA PART 2: base values.
            this.BaseValues.Add("channel_time", 1.3f);
            this.BaseValues.Add("cast_time", 1.0f);
            this.BaseValues.Add("cooldown", 1.3f);
            this.BaseValues.Add("mp_cost", 20f);
            this.GrowthValues.Add("channel_time", -0.1f);
            this.GrowthValues.Add("cast_time", -0.1f);
            this.GrowthValues.Add("cooldown", 0.0f);
            this.GrowthValues.Add("mp_cost", 5f);
            string[] ballparams = new string[] {"255:127:0","0.5" };
            AbilityLogic.VisualEffects.VFX_charge_ball charge = new VisualEffects.VFX_charge_ball(ballparams);
            charge.BaseDuration = 1.3f;
            charge.DeltaDuration = -0.1f;
            charge.BaseTime = 0f;
            AbilityLogic.VisualEffects.VFX_throw_ball launch = new VisualEffects.VFX_throw_ball(ballparams);
            launch.BaseDuration = 1.0f;
            launch.BaseTime = 1.3f;
            launch.DeltaTime = -0.1f;
            string[] bmdparams = new string[] {"150,0","10,0","10,20" };
            AbilityLogic.GameEffects.Effect_damage_bmd_full bmd = new GameEffects.Effect_damage_bmd_full(bmdparams);
            bmd.BaseTime = 2.3f;
            bmd.DeltaTime = -0.2f;
            this.Effects.Add(charge);
            this.Effects.Add(launch);
            this.Effects.Add(bmd);
            this.DescriptionString = "Launch a simple fireball at the target, burning them for ^20% base magic attack, ^21% weapon attack and an additional ^22 damage as Fire damage.";
        }
    }
}
