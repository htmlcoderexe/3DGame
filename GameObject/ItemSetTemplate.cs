using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
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

        public ItemSet.RingCountOption RingCount { get; set; }

        public ItemSet.RingDuplicationOption RingOption { get; set; }

        /// <summary>
        /// List of equip types that the set should contain.
        /// </summary>
        public List<ItemTypeDefinition> ItemList;

    }
}
