﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObject.MapEntities;

namespace GameObject.AbilityLogic.GameEffects
{
    public class Effect_damage_bmd_full : AbilityEffect
    {
        public override string EffectType { get { return "Effect_damage_bmd_full"; } }
        public override List<string> GetParams(int Level)
        {
            List<string> p = new List<string>
            {
                Utility.GetGrowth(ParamValues[0], Level).ToString(),

                Utility.GetGrowth(ParamValues[1], Level).ToString(),
                Utility.GetGrowth(ParamValues[2], Level).ToString()
            };
            return p;
        }
        public Effect_damage_bmd_full(string[] parameters)
        {
            this.ParamValues = parameters;
        }
        public override void Apply(Actor Source, Actor Target, int Level)
        {
            float dmg = 0;
            List<string> Params = GetParams(Level);
            //thisis all a test; normally it shoudl calculate m_atk*int * param /100f(for %), (simplify with a GetBMD call), then just m_atk, then the fixie at the end 
            dmg += Source.CalculateStat("p_atk") * ((float)(int.Parse(Params[0]))) / 100f; //because %
            dmg += int.Parse(Params[2]); //because %
            Target.Hit(Source, dmg, true, 0); //#TODO: extra magic type param
            base.Apply(Source, Target, Level);
        }

        
    }
}
