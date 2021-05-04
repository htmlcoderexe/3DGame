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

        public static GameObject.ItemSpecificTemplate GenerateCommonItem(int LevelPool, int ItemType)
        {

            GameObject.ItemSpecificTemplate result = new GameObject.ItemSpecificTemplate();
            List<int> weights = new List<int> { 3, 10, 5, 1 };
            int addoncount = GameObject.RNG.PickWeighted(weights);
            for (int i = 0; i < addoncount; i++)
                result.Adds.Add(GetAddon(LevelPool, ItemType, false).Addon.Generate()); 

            return result;


        }

        public static WorldInfo.ItemAddonEntry GetAddon(int LevelTier, int ItemType, bool Rare)
        {
            List<WorldInfo.ItemAddonEntry> e = addonpool.Where(a => a.LevelTier == LevelTier && a.ItemTypes.Contains(ItemType) && a.IsRare == Rare).ToList();
            return e[GameObject.RNG.Next(e.Count())];
        }

    }
}
