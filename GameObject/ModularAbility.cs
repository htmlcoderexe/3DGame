using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObject.AbilityLogic;
using GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameObject
{
    public class ModularAbility : GUI.IActionIcon
    {
        public float CoolDown { get; set; }
        public int Icon { get; set; }
        public float MaxCoolDown { get { return GetValue("cooldown"); } set { } }
        public string Name { get; set; }
        public string ID { get; set; }
        public int StackSize { get; set; }

        //magic constants - to resolve into actual numbers derived during casting to Effective Ability
        //deprecated!
        public const float CHANNEL_TIME = -1.0f;
        public const float CAST_TIME = -2.0f;
        public const float BOTH = -3.0f;

        public int Level;
        //not saved here, pulled from savegame and used to calculate level at runtime
        public int TotalExp;

        public Dictionary<string, float> BaseValues = new Dictionary<string, float>();
        public Dictionary<string, float> GrowthValues = new Dictionary<string, float>();
        //the below probably unused - stored in individual effects instead
        public Dictionary<string, int> ValueUnits = new Dictionary<string, int>(); //0 is raw, 1 is %, 2 is 1/10ths

        public float GetValue(string ValueName)
        {
            if (!BaseValues.ContainsKey(ValueName) || !GrowthValues.ContainsKey(ValueName))
                return 0;
            float combedvalue = BaseValues[ValueName] + GrowthValues[ValueName] * (this.Level - 1);
            combedvalue *= 10;
            combedvalue =(float) Math.Round(combedvalue);
            combedvalue /= 10f;
            return combedvalue;
        }
        /*
        public SortedList<float, AbilityLogic.AbilityEffect> GameEffects= new SortedList<float, AbilityLogic.AbilityEffect>();
        public SortedList<float, AbilityLogic.AbilityVFX> VisualEffects = new SortedList<float, AbilityLogic.AbilityVFX>();
        public SortedList<float, AbilityLogic.AbilitySelector> Selectors = new SortedList<float, AbilityLogic.AbilitySelector>();

        //*/

        public List<ITimedEffect> Effects = new List<ITimedEffect>();

        public string DescriptionString;

        public string FormatDescription(int Level=-1)
        {
            if (Level == -1)
                Level = this.Level;
            string output = "";
           for(int i=0;i<this.DescriptionString.Length;i++)
            {
                if (DescriptionString[i]!='@')
                {
                    output += DescriptionString[i];
                    continue;
                }
                if (DescriptionString.Length < i + 3)
                    break;
                int eff = DescriptionString[i + 1] - 48;
                int param = DescriptionString[i + 2] - 48;
                i += 2;
                output += GetEffectParam(eff, param, Level);
            }
            return output;

        }

        public List<string> GetTooltip()
        {
            List<string> tip = new List<string>();
            string head = this.Name;
            string s1 = "Cost: " + GetValue("mp_cost");
            string s2 = "Cooldown: " + GetValue("cooldown");
            string s3 = "Channeling: " + GetValue("channel_time");
            string s4 = "Cast: " + GetValue("cast_time");
            string desc = FormatDescription();
            tip.Add(head);
            tip.Add(s1);
            tip.Add(s2);
            tip.Add(s3);
            tip.Add(s4);
            tip.Add(desc);
            return tip;
        }

        public void Render(int X, int Y, GraphicsDevice device, Renderer Renderer, bool RenderCooldown = false, bool RenderEXP = false)
        {
            Renderer.SetTexture(Renderer.AbilityMap);
            Renderer.SetColour(Color.Gray);
            Renderer.RenderIconEx(device, X, Y, this.Icon);
        }
        

        public List<ITimedEffect> GetModules()
        {
            return Effects;
        }

        public string GetEffectParam(int Effect, int Param, int Level=-1)
        {
            if (Level == -1)
                Level = this.Level;

            if (this.Effects.Count <= Effect)
                return "";
            if (this.Effects[Effect].GetParamValues().Length <= Param)
                return "";
            string pbase= (this.Effects[Effect].GetParamValues())[Param].Split(new char[] { ',' })[0];
            string pdelta= (this.Effects[Effect].GetParamValues())[Param].Split(new char[] { ',' }).Length==2? (this.Effects[Effect].GetParamValues())[Param].Split(new char[] { ',' })[1]:"0";
            float.TryParse(pbase, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float floatbase);
            float.TryParse(pdelta, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float floatdelta);
            NumberFormatInfo dotdecimal = new NumberFormatInfo();
            dotdecimal.NumberDecimalSeparator = ".";
            dotdecimal.NumberGroupSeparator = "";
            return (floatbase + floatdelta * (Level - 1)).ToString(dotdecimal);
        }

        public EffectiveAbility GetEffectiveAbility()
        {
            EffectiveAbility result = new EffectiveAbility
            {
                Level = this.Level,
                EffectTimeline = new AbilityLogic.EffectTimeline()
            };

            //this just pushes all effects onto the timeline for AbilityExecutor, resolving magic constants to actual values
            

            foreach (ITimedEffect eff in this.Effects)
            {
                float k = eff.GetTime(Level);
                result.EffectTimeline.Add(eff,Level);
            }
            /*
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
            //*/
            return result;
        }

        public static ModularAbility CreateEmpty(string Name)
        {
            ModularAbility result = new ModularAbility
            {
                Name = "<Untitled ability>",
                ID = Name,
                BaseValues = new Dictionary<string, float>(),
                GrowthValues = new Dictionary<string, float>(),
                Icon = 0,
                DescriptionString = "Insert description here!"
            };
            result.BaseValues.Add("channel_time", 1.0f);
            result.BaseValues.Add("cast_time", 1.0f);
            result.BaseValues.Add("cooldown", 1.0f);
            result.BaseValues.Add("mp_cost", 1f);
            result.GrowthValues.Add("channel_time", 0.0f);
            result.GrowthValues.Add("cast_time", 0.0f);
            result.GrowthValues.Add("cooldown", 0.0f);
            result.GrowthValues.Add("mp_cost", 0f);

            return result;
        }
    }
}
