using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    /// <summary>
    /// Defines a single equipped item type.
    /// </summary>
    public class ItemTypeDefinition : Interfaces.IGameID
    {
        /// <summary>
        /// String ID of this ItemType.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Amounts to multiply the level-based base stat value, per mainstat.
        /// </summary>
        public float[] MainStatMultipliers = new float[Items.ItemEquip.StatCount];

        /// <summary>
        /// Item type name.
        /// </summary>
        public string Name;

        /// <summary>
        /// List of icon IDs that this item type can use.
        /// </summary>
        public List<int> Icons;

        /// <summary>
        /// Equipment slot this goes into.
        /// </summary>
        public int SlotID; //#TODO make this an enum maybe?

        /// <summary>
        /// Broad category this item type falls into, used for generation.
        /// </summary>
        public Items.ItemEquip.EquipCategories ItemCategory;
    }
}
