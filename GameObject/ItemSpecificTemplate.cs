using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    public class ItemSpecificTemplate : Interfaces.IGameID
    {
        /// <summary>
        /// String ID used for identification
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Item name
        /// </summary>
        public string Name;
        /// <summary>
        /// Item bonuses to apply.
        /// </summary>
        public List<Items.ItemBonus> Adds;
        /// <summary>
        /// Item level.
        /// </summary>
        public int Level;

        public int[] MainStats = new int[Items.ItemEquip.StatCount];
        /// <summary>
        /// Item grade, uses same strings as <see cref="Item.GradeToColour(string)"/>
        /// </summary>
        public string Grade = "Common";
        /// <summary>
        /// Equip type corresponding to <see cref="Item.SubType"/>.
        /// </summary>
        public int Type;
        /// <summary>
        /// Icon to be used by this specific item (must have 2 more icons underneath)
        /// </summary>
        public int Icon;

        public static ItemSpecificTemplate CreateEmpty()
        {
            ItemSpecificTemplate result = new ItemSpecificTemplate();

            return result;
        }

        /// <summary>
        /// Creates an ItemEquip based on the template.
        /// </summary>
        /// <returns>The created ItemEquip.</returns>
        public Items.ItemEquip CreateResult()
        {


            Items.ItemEquip result = new Items.ItemEquip
            {
                Name = Name,
                NameColour = Item.GradeToColour(this.Grade)
            };
            result.StatValues = this.MainStats;


            foreach (Items.ItemBonus add in this.Adds)
            {
                Items.ItemBonus bonus = (Items.ItemBonus)add.Clone();
                result.Bonuses.Add(bonus);
            }
            result.SubType = Type;
            result.Icon = Icon;


            return result;
        }
        /// <summary>
        /// Creates an ItemEquip based on the template and an existing ItemEquip.
        /// </summary>
        /// <param name="BaseItem">The ItemEquip to be upgraded.</param>
        /// <returns>The new ItemEquip.</returns>
        public Items.ItemEquip CreateResult(Items.ItemEquip BaseItem)
        {
            Items.ItemEquip result = CreateResult();

            result.SocketCount = BaseItem.SocketCount;
            result.Sockets = (Interfaces.ISocketable[])BaseItem.Sockets.Clone();
            result.RefiningLevel = BaseItem.RefiningLevel;
            return result;
        }
    }
}
