using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.Interactions
{
    public class NPCShop
    {
        public List<Tuple<Item,int,int>> Selling;
        public List<Tuple<Item, int>> Buying;

        public NPCShop()
        {
            this.Selling = new List<Tuple<Item, int, int>>();
            this.Selling.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("HP", 3), 120, 0));
        }
    }
}
