using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    /// <summary>
    /// This is an item set template.
    /// It defines a combination of item types that can make up
    /// an item set, as well as a couple rules related to the
    /// items there can be multiples of.
    /// An ItemSet created from this will contain the list of
    /// specific items that the set will consist of, as well as
    /// an array of ItemBonus that will be granted upon 
    /// collecting a specific number of items in the set. The
    /// bonuses specific to the item set will be randomly picked
    /// from the same pools as the items comprising the set. The
    /// Rarity value will also be chosen at the time of set
    /// generation. The only variable related to ItemBonuses 
    /// that can be controlled here is the total count that will
    /// be available for the set. These will be distributed at 
    /// the time of the set generation as well.
    /// </summary>
    public class ItemSetTemplate : Interfaces.IGameID
    {
        /// <summary>
        /// String ID of this ItemSetTemplate.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Item set type name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Weapon types to include in the set if applicable
        /// </summary>
        public enum IncludeWeaponType
        {
            /// <summary>
            /// Melee weapon
            /// </summary>
            Melee, 
            /// <summary>
            /// Magic weapon
            /// </summary>
            Magic,
            /// <summary>
            /// Ranged weapon
            /// </summary>
            Ranged
        }

      


        public IncludeWeaponType WeaponType { get; set; }

        public ItemSet.IncludeWeaponCount WeaponCount { get; set; }

        public int RingCount { get; set; }

        public bool RingRequireDistinct { get; set; }

        /// <summary>
        /// List of equip types that the set should contain.
        /// </summary>
        public List<ItemTypeDefinition> ItemList;

    }
}
