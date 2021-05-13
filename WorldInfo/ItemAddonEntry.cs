using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldInfo
{
    /// <summary>
    /// Defines a potential item bonus to be applied to specific items in specific item ranges.
    /// </summary>
    public class ItemAddonEntry
    {
        /// <summary>
        /// Level bucket that this add-on applies to.
        /// </summary>
        public int LevelTier;
        /// <summary>
        /// Template that generates the actual <see cref="GameObject.Items.ItemBonus"/> to apply to the target item.
        /// </summary>
        public GameObject.Items.BonusTemplate Addon;
        /// <summary>
        /// Whether this addon is of a "rare" type.
        /// </summary>
        public bool IsRare;
        /// <summary>
        /// List of ItemType IDs this addon can apply to
        /// </summary>
        public List<string> ItemTypes;
    }
}
