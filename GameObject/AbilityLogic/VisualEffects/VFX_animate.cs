﻿using GameObject.MapEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.AbilityLogic.VisualEffects
{
    public class VFX_animate : AbilityVFX
    {
        public override string EffectType { get { return "VFX_animate"; } }
        public string AnimationName;
        public int Target;
        public const int ANIMATE_USER = 0;
        public const int ANIMATE_TARGET = 1;
        string[] _rawparams;
        public override void Apply(Actor Source, Actor Target, int Level)
        {

        }
        public VFX_animate(string[] parameters)
        {
            SetParamValues(parameters);
        }
        public override void SetParamValues(string[] parameters)
        {
            this._rawparams = parameters;
            parameters[0] = parameters[0].Split(',')[0];
            parameters[1] = parameters[1].Split(',')[0];
            this.AnimationName = parameters[0];
            this.Target = Utility.GetInt(parameters[1]);
        }
        public override string[] GetParamValues()
        {
            return _rawparams;
        }
    }
}
