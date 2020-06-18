using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.AbilityLogic
{
    public static class EffectHelper
    {
        public static ITimedEffect CreateEmpty(string result)
        {
            string[] parts = result.Split(new char[] { '_' }, 2);
            string[] dummyparams = new string[] { "0,0", "0,0", "0,0", "0,0", "0,0", "0,0", "0,0", "0,0", "0,0", "0,0" };
            ITimedEffect eff;
            switch (parts[0])
            {
                case "VFX":
                    {
                        eff = AbilityVFX.CreateEffect(parts[1], dummyparams);
                        break;
                    }
                case "Effect":
                    {
                        eff = AbilityEffect.CreateEffect(parts[1], dummyparams);
                        break;
                    }
                case "Selector":
                    {
                        eff = AbilitySelector.CreateEffect(parts[1], dummyparams);
                        break;
                    }
                default:
                    {
                        eff = AbilityEffect.CreateEffect("null", dummyparams);
                        break;
                    }
            }
            return eff;
        }
    }
}
