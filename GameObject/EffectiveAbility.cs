using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    public class EffectiveAbility
    {
        public SortedList<float, AbilityLogic.ITimedEffect> EffectTimeline;
        public int Level;
        public float CastTime;
        public float ChannelTime;
    }
}
