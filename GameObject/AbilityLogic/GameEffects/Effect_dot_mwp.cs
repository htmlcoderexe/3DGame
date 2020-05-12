using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObject.MapEntities;

namespace GameObject.AbilityLogic.GameEffects
{
    public class Effect_dot_mwp : AbilityEffect
    {
        public Effect_dot_mwp(string[] parameters)
        {
            this._rawparams = parameters;
        }
        public override void Apply(Actor Source, Actor Target, int Level)
        {
            base.Apply(Source, Target,Level);
        }
        public override List<string> GetParams(int Level)
        {
            List<string> p = new List<string>
            {
                Utility.GetGrowth(_rawparams[0], Level).ToString(),

                Utility.GetGrowth(_rawparams[1], Level).ToString(),
                ((float)Utility.GetGrowth(_rawparams[2], Level) / 10f).ToString()
            };
            return p;
        }
    }
}
