using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGen
{
    public class ItemGenerator
    {

        public static List<GameObject.ItemAddonEntry> addonpool;

        public static List<GameObject.ItemTypeDefinition> EquipTypes;

        public static List<int> CommonItemAddonWeights= new List<int> { 3, 10, 5, 1 };
        public static List<int> UncommonItemAddonWeights= new List<int> { 0, 3, 6, 4, 1 };
        public static List<int> UncommonItemRareAddonWeights= new List<int> { 3, 6, 1};
        public const int UncommonLevelBumpProb = 3;

        /// <summary>
        /// Get level "bucket" for specified level.
        /// </summary>
        /// <param name="Level"></param>
        /// <returns></returns>
        public static int BucketLevel(int Level)
        {
            return Level - (Level % 10);

        }
        /*
        public static float[] xxxxxxSetMainStats(int ItemType, int Level)
        {
            float[] MainStats = new float[GameObject.Items.ItemEquip.StatCount];
            float mainstat1 = GameObject.ItemSpecificTemplate.GetMainStatForLevel(Level);
            switch (ItemType)
            {
                case 0:
                case 1:
                    {
                        MainStats[2] = mainstat1;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return MainStats;
        }

        //*/

        /// <summary>
        /// Generates an Item Template for a specific level and equipment type.
        /// </summary>
        /// <param name="Level">Level for the item.</param>
        /// <param name="ItemType">Equipment type to use.</param>
        /// <returns></returns>
        public static GameObject.ItemSpecificTemplate GenerateItemBase(int Level, string ItemType)
        {
            GameObject.ItemSpecificTemplate template = GameObject.ItemSpecificTemplate.CreateEmpty();
            template.Level = Level;

            //fetch item type template
            GameObject.ItemTypeDefinition typedef;
            try
            {
                 typedef = EquipTypes.Where(t => t.ID == ItemType).ToList()[0];
            }
            catch (IndexOutOfRangeException ee)
            {
                GameObject.Console.Write("^FF0000 Invalid Equipment Type: ^FFFFFF " + ItemType);
                return null;
            }

            //create and multiply basestats

            template.MainStats = new int[GameObject.Items.ItemEquip.StatCount];

            for (int i = 0; i < GameObject.Items.ItemEquip.StatCount; i++)
            {
                template.MainStats[i] = (int)(GameObject.Items.ItemEquip.GetMainStatForLevel(Level)*typedef.MainStatMultipliers[i]);
            }

            //pick a random icon out of the possible choices
            template.Icon = typedef.Icons[GameObject.RNG.Next(typedef.Icons.Count)];




            return template;
        }

        public static GameObject.ItemSpecificTemplate GenerateCommonItem(int Level, string ItemType)
        {
            int LevelPool = BucketLevel(Level);
            GameObject.ItemSpecificTemplate result = GenerateItemBase(Level, ItemType);
            result.Grade = "Common";

            result.Adds.AddRange(GetAdds(LevelPool, ItemType, 0, CommonItemAddonWeights, 0));
            return result;
        }

        public static GameObject.ItemSpecificTemplate GenerateUncommonItem(int Level, string ItemType)
        {
            int LevelPool = BucketLevel(Level);
            GameObject.ItemSpecificTemplate result = GenerateItemBase(Level, ItemType);
            result.Grade = "Uncommon";
            result.Adds.AddRange(GetAdds(LevelPool, ItemType, 1, UncommonItemRareAddonWeights, 0));
            result.Adds.AddRange(GetAdds(LevelPool, ItemType, 0, UncommonItemAddonWeights, UncommonLevelBumpProb));
            return result;
        }


        /// <summary>
        /// Retrieves a random addon from the pool based on selected criteria.
        /// </summary>
        /// <param name="LevelTier">Addon's desired level tier.</param>
        /// <param name="ItemType">Item type to accept the addon.</param>
        /// <param name="Rareness">Addon rareness reqested.</param>
        /// <returns></returns>
        public static GameObject.ItemAddonEntry GetAddon(int LevelTier, string ItemType, int Rareness)
        {
            List<GameObject.ItemAddonEntry> e = addonpool.Where(a => a.Rareness == Rareness && a.MinLevelTier<=LevelTier && a.ItemTypes.Contains(ItemType)).ToList();
            return e[GameObject.RNG.Next(e.Count())];
        }

        public static List<GameObject.Items.ItemBonus> GetAdds(int LevelTier, string ItemType, int Rareness, List<int> Weights, int BumpProbability)
        {
            List<GameObject.Items.ItemBonus> adds = new List<GameObject.Items.ItemBonus>();
            //pick how many addons are to be gained.
            int count = GameObject.RNG.PickWeighted(Weights);
            //if zero is picked, return empty list
            if (count == 0)
                return adds;

            GameObject.ItemAddonEntry addtemplate;
            float tieradjusted;
            GameObject.Items.ItemBonus add;
            for (int i=0;i<count;i++)
            {
                addtemplate = GetAddon(LevelTier, ItemType, Rareness);
                //add a random +/-
                add = addtemplate.GenerateAddon(LevelTier, (float)(GameObject.RNG.NextDouble() - 0.5));
                adds.Add(add);
            }

            return adds;
        }

    }
}
