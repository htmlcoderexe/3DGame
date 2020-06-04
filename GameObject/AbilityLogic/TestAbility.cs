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
            string[] ballparams = new string[] {"255:127:0","0.5" };
            AbilityLogic.VisualEffects.VFX_charge_ball charge = new VisualEffects.VFX_charge_ball(ballparams);
            charge.Duration = ModularAbility.CHANNEL_TIME;
            charge.Time = 0f;
            AbilityLogic.VisualEffects.VFX_throw_ball launch = new VisualEffects.VFX_throw_ball(ballparams);
            launch.Duration = 2.0f;
            launch.Time = CHANNEL_TIME;
            string[] bmdparams = new string[] {"150,0","10,0","10,20" };
            AbilityLogic.GameEffects.Effect_damage_bmd_full bmd = new GameEffects.Effect_damage_bmd_full(bmdparams);
            bmd.Time = BOTH;
            this.VisualEffects.Add(0, charge);
            this.VisualEffects.Add(CHANNEL_TIME, launch);
            this.GameEffects.Add(BOTH, bmd);

            this.BaseValues = new Dictionary<string, float>();
            this.GrowthValues = new Dictionary<string, float>();

            //DATA PART 2: base values.
            this.BaseValues.Add("channel_time", 1.3f);
            this.BaseValues.Add("cast_time", 1.0f);
            this.GrowthValues.Add("channel_time", -0.1f);
            this.GrowthValues.Add("cast_time", -0.1f);
        }
    }
}
