using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameObject
{
    public class ModularAbility : GUI.IActionIcon
    {
        public float CoolDown { get; set; }
        public int Icon { get; set; }
        public float MaxCoolDown { get; set; }
        public string Name { get; set; }
        public int StackSize { get; set; }

        //magic constants - to resolve into actual numbers derived during casting to Effective Ability
        public const float CHANNEL_TIME = -1.0f;
        public const float CAST_TIME = -2.0f;
        public const float BOTH = -3.0f;

        public int Level;

        public Dictionary<string, float> BaseValues = new Dictionary<string, float>();
        public Dictionary<string, float> GrowthValues = new Dictionary<string, float>();
        public Dictionary<string, int> ValueUnits = new Dictionary<string, int>(); //0 is raw, 1 is %, 2 is 1/10ths

        public float GetValue(string ValueName)
        {
            if (!BaseValues.ContainsKey(ValueName) || !GrowthValues.ContainsKey(ValueName))
                return 0;
            return BaseValues[ValueName] + GrowthValues[ValueName] * (this.Level - 1);
        }

        public SortedList<float, AbilityLogic.AbilityEffect> GameEffects= new SortedList<float, AbilityLogic.AbilityEffect>();
        public SortedList<float, AbilityLogic.AbilityVFX> VisualEffects = new SortedList<float, AbilityLogic.AbilityVFX>();
        public SortedList<float, AbilityLogic.AbilitySelector> Selectors = new SortedList<float, AbilityLogic.AbilitySelector>();

        public List<string> GetTooltip()
        {
            throw new NotImplementedException();
        }

        public void Render(int X, int Y, GraphicsDevice device, Renderer Renderer, bool RenderCooldown = false, bool RenderEXP = false)
        {
            Renderer.SetTexture(Renderer.AbilityMap);
            Renderer.SetColour(Color.Gray);
            Renderer.RenderIconEx(device, X, Y, this.Icon);
        }

        public float GetTimeValue(float f)
        {
            switch (f)
            {
                case CHANNEL_TIME:
                    {
                        return GetValue("channel_time");
                    }
                case CAST_TIME:
                    {
                        return GetValue("cast_time");
                    }
                case BOTH:
                    {
                        return GetValue("channel_time") + GetValue("cast_time");
                    }
                default:
                    {
                        return f;
                    }
            }
        }

        public EffectiveAbility GetEffectiveAbility()
        {
            EffectiveAbility result = new EffectiveAbility
            {
                Level = this.Level,
                EffectTimeline = new AbilityLogic.EffectTimeline()
            };

            //this just pushes all effects onto the timeline for AbilityExecutor, resolving magic constants to actual values
            

            foreach (KeyValuePair<float, AbilityLogic.AbilityEffect> eff in this.GameEffects)
            {
                float k = eff.Key;
                k = GetTimeValue(k);
                eff.Value.Duration = GetTimeValue(eff.Value.Duration);
                eff.Value.Time = GetTimeValue(eff.Value.Time);
                result.EffectTimeline.Add(eff.Value);
            }

            foreach (KeyValuePair<float, AbilityLogic.AbilityVFX> eff in this.VisualEffects)
            {
                float k = eff.Key;
                k = GetTimeValue(k);
                eff.Value.Duration = GetTimeValue(eff.Value.Duration);
                eff.Value.Time = GetTimeValue(eff.Value.Time);
                result.EffectTimeline.Add(eff.Value);
            }
            foreach (KeyValuePair<float, AbilityLogic.AbilitySelector> eff in this.Selectors)
            {
                float k = eff.Key;
                k = GetTimeValue(k);
                eff.Value.Duration = GetTimeValue(eff.Value.Duration);
                eff.Value.Time = GetTimeValue(eff.Value.Time);
                result.EffectTimeline.Add(eff.Value);
            }

            return result;
        }
    }
}
