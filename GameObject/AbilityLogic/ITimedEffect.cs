using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.AbilityLogic
{
    public interface ITimedEffect
    {
        void Apply(MapEntities.Actor Source, MapEntities.Actor Target, int Level);
        float GetDuration(int Level);
        float BaseDuration { get; set; }
        float DeltaDuration { get; set; }
        float GetTime(int Level);
        float BaseTime { get; set; }
        float DeltaTime { get; set; }
        string EffectType { get; }
        string[] GetParamValues();
        void SetParamValues(string[] Values);
    }
}
