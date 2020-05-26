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

        public override void Apply(Actor Source, Actor Target, int Level)
        {

        }
        public VFX_animate(string[] parameters)
        {
            this.AnimationName = parameters[0];
            this.Target = Utility.GetInt(parameters[1]);
        }
    }
}
