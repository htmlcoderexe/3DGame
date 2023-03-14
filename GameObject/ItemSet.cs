using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    public class ItemSet : Interfaces.IGameID
    {
        /// <summary>
        /// 
        /// </summary>

        public enum IncludeWeaponCount
        {
            /// <summary>
            /// This set should not include a weapon.
            /// </summary>
            None,
            /// <summary>
            /// This set should include one weapon.
            /// </summary>
            One,
            /// <summary>
            /// This sed should include two weapons.
            /// </summary>
            Two
        }

        /// <summary>
        /// 
        /// </summary>
        public enum RingDuplicationOption
        {
            /// <summary>
            /// All rings must be distinct.
            /// </summary>
            No,
            /// <summary>
            /// All rings must be the same.
            /// </summary>
            Yes,
            /// <summary>
            /// Any combination of rings up to RingCount contributes to the set.
            /// </summary>
            Mixed
        }

        public enum RingCountOption
        {
            /// <summary>
            /// Set does not include rings.
            /// </summary>
            None,
            /// <summary>
            /// Set includes one ring.
            /// </summary>
            One,
            /// <summary>
            /// Set includes two rings.
            /// </summary>
            Two,
            /// <summary>
            /// Set includes three rings.
            /// </summary>
            Three
        }


        /// <summary>
        /// String ID of this ItemSet.
        /// </summary>
        public string ID { get; set; }

        public string Name { get; set; }


        public IncludeWeaponCount WeaponCount { get; set; }

        public RingCountOption RingCount { get; set; }

        public RingDuplicationOption RingOption { get; set; }

        public int BonusCount { get; set; }

        public Items.ItemBonus[] Bonuses { get; set; }

        public ItemSet(ItemSetTemplate Template)
        {
            BonusCount = Template.ItemList.Count;
            Bonuses = new Items.ItemBonus[BonusCount];
        }
    }
}
