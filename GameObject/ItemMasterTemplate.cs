using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    /// <summary>
    /// Manually defined master template
    /// to be used to generate specific item 
    /// templates in any given world.
    /// </summary>
    public class ItemMasterTemplate : Interfaces.IGameID
    {

        private static int IDTally=0;
        /// <summary>
        /// Retrieves an increased ID to use for item and crafting IDs.
        /// </summary>
        /// <returns></returns>
        public static int GetID()
        {
            return IDTally++;
        }
        /// <summary>
        /// Sets how a template should behave including having the item available as a drop
        /// from common monsters, bosses, dungeons, as craft only or as a quest reward.
        /// </summary>
        public enum TemplateStyle
        {
            /// <summary>
            /// 3 grades, common and uncommon drops from regular monsters, rare from elites and bosses.
            /// All three craft from regular materials with weighted chance for either.
            /// </summary>
            RegularItem,
            /// <summary>
            /// Drops from dungeon bosses or dungeon raid quest.
            /// </summary>
            DungeonReward,
            /// <summary>
            /// Crafts from mats dropped from dungeons.
            /// </summary>
            DungeonCraft,
            /// <summary>
            /// Given to the player as a reward.
            /// </summary>
            QuestReward,
            /// <summary>
            /// Generally nice items and sets obtainable in various ways.
            /// </summary>
            Legendary,
            /// <summary>
            /// Completely unique artiact items.
            /// </summary>
            Artifact
        }


        /// <summary>
        /// String ID of this specific master template
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Lower bound of item level
        /// </summary>
        public int LevelLower;
        /// <summary>
        /// Upper bound of item level
        /// </summary>
        public int LevelUpper;
        /// <summary>
        /// Array of icon IDs that can be used by this item
        /// </summary>
        public int[] IconPool;
        /// <summary>
        /// NameGenerator to use for this item
        /// </summary>
        public NameGenerator NameGenerator;
        /// <summary>
        /// Settings string for the name generator
        /// </summary>
        public string NameTemplate;
        /// <summary>
        /// Minimum value of materials used
        /// </summary>
        public int MatCostMin;
        /// <summary>
        /// Maximum value of materials used
        /// </summary>
        public int MatCostMax;
        /// <summary>
        /// ID of material pool to use ("common" for generi, dungeon ID for dungeon sets etc)
        /// </summary>
        public string MatPoolId;
        /// <summary>
        /// Minimum count of distinct items to use
        /// </summary>
        public int MatCountMin;
        /// <summary>
        /// Maximum count of distinct items to use
        /// </summary>
        public int MatCountMax;

        public int CommonAddonCountMax;
        /// <summary>
        /// Possible addons with ranges
        /// </summary>
        public List<Tuple<Items.BonusTemplate, int>> CommonAddons;
        /// <summary>
        /// Possible rare addons with ranges
        /// </summary>
        public List<Tuple<Items.BonusTemplate, int>> RareAddons;
        ///<summary>
        /// Generates a specific Item template used by the game when generating the 
        /// appropriate item from looting or crafting.
        /// </summary>
        /// <returns></returns>
        public ItemSpecificTemplate GenerateTemplate()
        {
            ItemSpecificTemplate result = new ItemSpecificTemplate();

            return result;
        }

        //public ItemLogic.CraftingRecipe Generate

    }
}
