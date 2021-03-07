using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObject.Items;

namespace GameObject.Interactions
{
    public class NPCShop
    {
        public List<Tuple<string,List<Tuple<Item,int,int>>>> Selling;
        public List<Tuple<Item, int>> Buying;

        public NPCShop()
        {
            this.Selling = new List<Tuple<string, List<Tuple<Item, int, int>>>>();
            List<Tuple<Item, int, int>> gemstab = new List<Tuple<Item, int, int>>();
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("HP", 3), 120, 0));
            /*
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("MP", 3), 120, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("MAtk", 3), 120, 0));

            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("PAtk", 3), 120, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("PDef", 3), 120, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("MDef", 3), 120, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("HP", 4), 480, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("MP", 4), 480, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("MAtk", 4), 480, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("PAtk", 4), 480, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("PDef", 4), 480, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("MDef", 4), 480, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("HP", 5), 1920, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("MP", 5), 1920, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("MAtk", 5), 1920, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("PAtk", 5), 1920, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("PDef", 5), 1920, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("MDef", 5), 1920, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("HP", 8), 120000, 0));
            gemstab.Add(new Tuple<Item, int, int>(new Items.ItemGemstone("MP", 8), 120000, 0));
            //*/
            Item Item;
            
            Item = Material.MaterialTemplates.GetRandomMaterial();
            Item.SubType = RNG.Next(Material.MaterialType.Max);
            Item.Description = "A " + Material.MaterialType.GetTypeName(Item.SubType) + " made from " + Item.Name + ". Used in crafting equipment.";



            List<Tuple<Item, int, int>> crap = new List<Tuple<Item, int, int>>();
            crap.Add(new Tuple<Item, int, int>(Item, 120, 0));

            this.Selling.Add(new Tuple<string, List<Tuple<Item, int, int>>>("Gems", gemstab));
            this.Selling.Add(new Tuple<string, List<Tuple<Item, int, int>>>("Mats", crap));
        }
    }
}
