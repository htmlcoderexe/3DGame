using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.Interfaces
{
    /// <summary>
    /// Items that can be added onto an eqipment piece ("socketed") use this interface.
    /// </summary>
    public interface ISocketable
    {
        /// <summary>
        /// Returns the effect the socketable item has on equipment when socketed.
        /// </summary>
        Items.ItemBonus AddedEffect { get; }
    }
}
