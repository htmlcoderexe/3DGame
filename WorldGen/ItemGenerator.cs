using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGen
{
    public class ItemGenerator
    {

        public static List<WorldInfo.ItemAddonEntry> addonpool;

        public static List<int> CommonItemAddonWeights= new List<int> { 3, 10, 5, 1 };
        public static List<int> UncommonItemAddonWeights= new List<int> { 0, 3, 6, 4, 1 };
        public static List<int> UncommonItemRareAddonWeights= new List<int> { 3, 6, 1};
        public const int UncommonLevelBumpProb = 3;

        public static GameObject.ItemSpecificTemplate GenerateCommonItem(int LevelPool, int ItemType)
        {
            GameObject.ItemSpecificTemplate result = new GameObject.ItemSpecificTemplate();
            result.Grade = "Common";
            result.Adds.AddRange(GetAdds(LevelPool, ItemType, false, CommonItemAddonWeights, 0));
            return result;
        }

        public static GameObject.ItemSpecificTemplate GenerateUncommonItem(int LevelPool, int ItemType)
        {
            GameObject.ItemSpecificTemplate result = new GameObject.ItemSpecificTemplate();
            result.Grade = "Uncommon";
            result.Adds.AddRange(GetAdds(LevelPool, ItemType, true, UncommonItemRareAddonWeights, 0));
            result.Adds.AddRange(GetAdds(LevelPool, ItemType, false, UncommonItemAddonWeights, UncommonLevelBumpProb));
            return result;
        }


        /// <summary>
        /// Retrieves a random addon from the pool based on selected criteria.
        /// </summary>
        /// <param name="LevelTier">Addon's desired level tier.</param>
        /// <param name="ItemType">Item type to accept the addon.</param>
        /// <param name="Rare">Whether the addon should come from the regular or the rare addon pool.</param>
        /// <returns></returns>
        public static WorldInfo.ItemAddonEntry GetAddon(int LevelTier, int ItemType, bool Rare)
        {
            List<WorldInfo.ItemAddonEntry> e = addonpool.Where(a => a.LevelTier == LevelTier && a.ItemTypes.Contains(ItemType) && a.IsRare == Rare).ToList();
            return e[GameObject.RNG.Next(e.Count())];
        }

        public static List<GameObject.Items.ItemBonus> GetAdds(int LevelTier, int ItemType, bool Rare, List<int> Weights, int BumpProbability)
        {
            List<GameObject.Items.ItemBonus> adds = new List<GameObject.Items.ItemBonus>();


            return adds;
        }

    }
}
