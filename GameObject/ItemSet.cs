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
            Two,
            /// <summary>
            /// This set should include two weapons, any combination contributes to the set.
            /// </summary>
            TwoAny
        }
        
        /*
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
        //*/

        /// <summary>
        /// String ID of this ItemSet.
        /// </summary>
        public string ID { get; set; }

        public string Name { get; set; }


        public IncludeWeaponCount WeaponCount { get; set; }

        public int RingCount { get; set; }

        public bool RingRequireDistinct { get; set; }

        //public int BonusCount { get; set; }

        public Items.ItemBonus[] Bonuses { get; set; }

        public ItemSet(ItemSetTemplate Template)
        {
            int BonusCount = Template.ItemList.Count;
            Bonuses = new Items.ItemBonus[BonusCount];
        }

        public int GetSetCount(List<Items.ItemEquip> ItemList)
        {
            int result = 0;
            //two lists to keep track of rings and weapons due to special handling
            List<Items.ItemEquip> rings = new List<Items.ItemEquip>();
            List<Items.ItemEquip> weps = new List<Items.ItemEquip>();
            //go through every item in the list given
            foreach(Items.ItemEquip item in ItemList)
            {
                //only count items that specify this as their set
                if(item.Set==this)
                {
                    //if it's a ring, add it to the rings list
                    if (Items.ItemEquip.EquipSlot.IsRing(item.EquipmentSlot))
                    {
                        rings.Add(item);
                    }
                    //if it's a weapon, add it to the weapons list
                    else if (Items.ItemEquip.EquipSlot.IsWeapon(item.EquipmentSlot))
                    {
                        weps.Add(item);
                    }
                    //else increase the tally as no other types can have duplicate slots
                    else
                    {
                        result++;
                    }
                }

            }

            //add either the number of all weapons and rings equipped, or just unique ones as the set requires.
            //items are compared by their IGameID.ID as two distinct copies of the same ring item will count as
            //unique if compared with regular equality as the player will never be able to equip the same 
            //instance of an item twice.
            //rings only count up to maximum ring count in the set - for example, if a set includes 1 ring,
            //equipping 2 or 3 of that ring will not "satisfy" additional set slots.

            //if ring mixing is allowed, or same ring option was set in template, add all rings we found
            if(RingRequireDistinct)
            {
                result += Math.Max(rings.Select(r => r.ID).Distinct().ToList().Count,this.RingCount);
            }
            //else only add the unique ones.
            else
            {
                result += Math.Min(rings.Count,this.RingCount);
            }
            //if specifically two different weapons are required, count uniques in the list in case two of the same are eqipped;
            if(WeaponCount== IncludeWeaponCount.Two)
            {
                result += weps.Select(r => r.ID).Distinct().ToList().Count;
            }
            else
            {
                result += weps.Count;
            }



            return result;
        }

        public List<Items.ItemBonus> GetBonuses(int ItemCount)
        {
            List<Items.ItemBonus> result = new List<Items.ItemBonus>();
            if (ItemCount > Bonuses.Length)
                ItemCount = Bonuses.Length;
            for (int i = 0; i < ItemCount; i++)
                if (Bonuses[i] != null)
                    result.Add(Bonuses[i]);
            return result;
        }
    }
}
