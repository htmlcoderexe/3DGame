using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    /// <summary>
    /// Defines a potential item bonus to be applied to specific items in specific item ranges.
    /// </summary>
    public class ItemAddonEntry
    {
        

        /// <summary>
        /// The amount of level/10's up or down for maximum variance.
        /// </summary>
        const float LevelUpDown = 0.5f;

        /// <summary>
        /// Stat affected by this add-on.
        /// </summary>
        public string StatType { get; set; }
        /// <summary>
        /// Starting value.
        /// </summary>
        public float BaseValue { get; set; }
        /// <summary>
        /// Value increase every 10 levels.
        /// </summary>
        public float GrowthValue { get; set; }
        /// <summary>
        /// Determines whether the resulting item add-on is a percentage.
        /// </summary>
        public bool IsPercentage { get; set; }
        /// <summary>
        /// Text displayed for this add-on in the item's tooltip, with value substituteion.
        /// </summary>
        public string EffectString { get; set; }
        /// <summary>
        /// Addon rareness.
        /// </summary>
        public int Rareness { get; set; }
        /// <summary>
        /// Minimum level tier this add-on can appear in.
        /// </summary>
        public int MinLevelTier { get; set; }
        /// <summary>
        /// If an item uses lore, addons with compatible keywords are more likely to get the addon.
        /// </summary>
        public string LoreKeyword { get; set; }
        /// <summary>
        /// List of ItemType IDs this addon can apply to
        /// </summary>
        /// 
        public List<string> ItemTypes;

        /// <summary>
        /// List of SetType IDs this addon can apply to
        /// </summary>
        /// 
        public List<string> SetTypes;

        /// <summary>
        /// Display name for the editor list.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return StatType + " +" + BaseValue.ToString();
            }
        }

        public static ItemAddonEntry CreateEmpty(string statname)
        {
            ItemAddonEntry result = new ItemAddonEntry();
            result.StatType = statname;
            result.ItemTypes = new List<string>();
            result.SetTypes = new List<string>();
            result.LoreKeyword = "";
            result.EffectString = "";
            return result;
        }


        /// <summary>
        /// Creates an item addon based on this template.
        /// </summary>
        /// <param name="TargetLevel"></param>
        /// <param name="Variance"></param>
        /// <returns></returns>
        public Items.ItemBonus  GenerateAddon(int TargetLevel, float Variance)
        {
            Variance = Microsoft.Xna.Framework.MathHelper.Clamp(Variance, -1.0f, 1.0f);
            //
            float result = BaseValue + GrowthValue  * ((float)TargetLevel + (Variance*(float)LevelUpDown));
            Items.ItemBonus b = new Items.ItemBonus()
            {
                FlatValue = IsPercentage ? 0 : result,
                Multiplier = IsPercentage ? result : 0,
                Effecttext = EffectString,
                LineColour = Item.GradeToColour("ItemAddon"),
                Type = StatType,
                Order = StatBonus.StatOrder.Equip
            };



            return b;
        }
    }
}
