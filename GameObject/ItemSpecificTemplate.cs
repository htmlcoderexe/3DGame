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

        public List<Items.ItemBonus> Adds;

        public string Grade = "Common";

        public int Type;
        public int SubType;
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
            foreach (Items.ItemBonus add in this.Adds)
            {
                Items.ItemBonus bonus = (Items.ItemBonus)add.Clone();
                result.Bonuses.Add(bonus);
            }
            result.Type = Type;
            result.SubType = SubType;


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
