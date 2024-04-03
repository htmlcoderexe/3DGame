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
    public class ModularAbility : GUI.IActionIcon,Interfaces.IGameID
    {
        /// <summary>
        /// Remaining cooldown amount in seconds
        /// </summary>
        public float CoolDown { get; set; }
        /// <summary>
        /// Icon used for the ability
        /// </summary>
        public int Icon { get; set; }
        /// <summary>
        /// Cooldown amount when ability has just been used
        /// </summary>
        public float MaxCoolDown { get { return GetValue("cooldown"); } set { } }
        /// <summary>
        /// Display name of the ability
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// String ID of this specific ability, used for serialisation
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// StackSize as required by the IActionIcon interface, not really used here
        /// </summary>
        public int StackSize { get; set; }

        //magic constants - to resolve into actual numbers derived during casting to Effective Ability
        //deprecated!
        public const float CHANNEL_TIME = -1.0f;
        public const float CAST_TIME = -2.0f;
        public const float BOTH = -3.0f;
        /// <summary>
        /// Current ability level
        /// </summary>
        public int Level;
        //not saved here, pulled from savegame and used to calculate level at runtime
        /// <summary>
        /// All Exp gained by this ability
        /// </summary>
        public int TotalExp;
        /// <summary>
        /// All base values of this ability
        /// </summary>
        public Dictionary<string, float> BaseValues { get; set; } = new Dictionary<string, float>();
        /// <summary>
        /// Value deltas
        /// </summary>
        public Dictionary<string, float> GrowthValues { get; set; } = new Dictionary<string, float>();
        //the below probably unused - stored in individual effects instead
        public Dictionary<string, int> ValueUnits = new Dictionary<string, int>(); //0 is raw, 1 is %, 2 is 1/10ths
        /// <summary>
        /// Retrieves a value by its key, based on current level
        /// </summary>
        /// <param name="ValueName">Value key (ID)</param>
        /// <returns></returns>
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
        /// <summary>
        /// Effect timeline
        /// </summary>
        public List<ITimedEffect> Effects { get; set; } = new List<ITimedEffect>();
        /// <summary>
        /// Description of the ability appearing in game interfaces, with formatting codes
        /// to display level-specific numbers.
        /// </summary>
        public string DescriptionString { get; set; }
        /// <summary>
        /// Inserts values into the formatted DescriptionString.
        /// </summary>
        /// <param name="Level">Level to use, if unspecified or set to -1 uses this instance's level</param>
        /// <returns>Processed string.</returns>
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
        /// <summary>
        /// Retrieves the tooltip text showing basic information and description.
        /// </summary>
        /// <returns>Text of the tooltip.</returns>
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
        /// <summary>
        /// Draws this ability's icon.
        /// </summary>
        /// <param name="X">X coordinate</param>
        /// <param name="Y">Y coordinate</param>
        /// <param name="device">Current GraphicsDevice</param>
        /// <param name="Renderer">GUI Renderer used by this instance</param>
        /// <param name="RenderCooldown">Set to true to render cooldown overlay.</param>
        /// <param name="RenderEXP">Set to true to show an Exp bar on the icon.</param>
        public void Render(int X, int Y, GraphicsDevice device, Renderer Renderer, bool RenderCooldown = false, bool RenderEXP = false)
        {
            Renderer.SetTexture(Renderer.AbilityMap);
            Renderer.SetColour(Color.Gray);
            Renderer.RenderIconEx(device, X, Y, this.Icon);
            if(this.Level<=0)
            {
                Renderer.RenderClock(device, X, Y, 0.0f);
            }
            if(RenderCooldown && this.CoolDown>0)
            {
                Renderer.RenderClock(device, X, Y,  1f-(this.CoolDown / this.MaxCoolDown));
            }
        }
        
        /// <summary>
        /// Gets the Effect timeline.
        /// </summary>
        /// <returns>Effect timeline as a list.</returns>
        public List<ITimedEffect> GetModules()
        {
            return Effects;
        }
        /// <summary>
        /// Retrieves values taken by a specific effect at a specific level. Used in description formatting.
        /// </summary>
        /// <param name="Effect">Index of the effect on the timeline.</param>
        /// <param name="Param">Index of the specific parameter.</param>
        /// <param name="Level">Level to use, defaults to current level.</param>
        /// <returns></returns>
        public string GetEffectParam(int Effect, int Param, int Level=-1)
        {
            if (Level == -1)
                Level = this.Level;

            if (this.Effects.Count <= Effect)
                return "";
            if (this.Effects[Effect].ParamValues.Length <= Param)
                return "";
            string pbase= (this.Effects[Effect].ParamValues)[Param].Split(new char[] { ',' })[0];
            string pdelta= (this.Effects[Effect].ParamValues)[Param].Split(new char[] { ',' }).Length==2? (this.Effects[Effect].ParamValues)[Param].Split(new char[] { ',' })[1]:"0";
            float.TryParse(pbase, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float floatbase);
            float.TryParse(pdelta, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float floatdelta);
            NumberFormatInfo dotdecimal = new NumberFormatInfo();
            dotdecimal.NumberDecimalSeparator = ".";
            dotdecimal.NumberGroupSeparator = "";
            return (floatbase + floatdelta * (Level - 1)).ToString(dotdecimal);
        }
        /// <summary>
        /// Creates a ready ability to execute on the character using current params.
        /// </summary>
        /// <returns>Effective ability based on current level.</returns>
        public EffectiveAbility GetEffectiveAbility()
        {
            EffectiveAbility result = new EffectiveAbility
            {
                Level = this.Level,
                EffectTimeline = new AbilityLogic.EffectTimeline(),
                MaxCoolDown=GetValue("cooldown")
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
        /// <summary>
        /// Creates an empty ability.
        /// </summary>
        /// <param name="Name">Name to be used</param>
        /// <returns>Empty ability with chosen name and defaut values.</returns>
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
            result.BaseValues.Add("range", 10f);
            result.GrowthValues.Add("channel_time", 0.0f);
            result.GrowthValues.Add("cast_time", 0.0f);
            result.GrowthValues.Add("cooldown", 0.0f);
            result.GrowthValues.Add("mp_cost", 0f);
            result.GrowthValues.Add("range", 10f);

            return result;
        }
    }
}
