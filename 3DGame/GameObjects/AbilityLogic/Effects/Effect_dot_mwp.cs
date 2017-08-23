using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3DGame.GameObjects.MapEntities;

namespace _3DGame.GameObjects.AbilityLogic.Effects
{
    public class Effect_dot_mwp : AbilityEffect
    {
        public Effect_dot_mwp(string[] parameters)
        {
            this._rawparams = parameters;
        }
        public override void Apply(Actor Source, Actor Target)
        {
            base.Apply(Source, Target);
        }
        public override List<string> GetParams(int Level)
        {
            List<string> p = new List<string>();

            p.Add(Utility.GetGrowth(_rawparams[0], Level).ToString());

            p.Add(Utility.GetGrowth(_rawparams[1], Level).ToString());
            p.Add(((float)Utility.GetGrowth(_rawparams[2], Level)/10f).ToString());
            return p;
        }
    }
}
