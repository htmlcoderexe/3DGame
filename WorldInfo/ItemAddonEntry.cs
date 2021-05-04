using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldInfo
{
    public class ItemAddonEntry
    {
        public int LevelTier;

        public GameObject.Items.BonusTemplate Addon;

        public bool IsRare;

        public List<int> ItemTypes;
    }
}
