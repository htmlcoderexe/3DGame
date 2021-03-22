using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.ItemLogic
{
    public interface IItemFilter
    {
        bool CheckItem(Item Item);
    }
}
