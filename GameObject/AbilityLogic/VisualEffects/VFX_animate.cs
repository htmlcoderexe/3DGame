using GameObject.MapEntities;
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
            this.ParamValues = parameters;
        }
        public override string[] ParamValues
        {
            set
            {
                this._rawparams = value;
                _rawparams[0] = value[0].Split(',')[0];
                _rawparams[1] = value[1].Split(',')[0];
                this.AnimationName = _rawparams[0];
                this.Target = Utility.GetInt(_rawparams[1]);
            }
            get
            {
                return _rawparams;
            }
        }
    }
}
