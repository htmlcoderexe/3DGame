using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.ItemLogic
{
    public class CraftingRecipe
    {
        public List<Tuple<Item, int>> Components= new List<Tuple<Item, int>>();
        //public List<Tuple<List<Item>,int>> Outputs= new List<Tuple<List<Item>, int>>();



        public List<Item>[] Outputs = new List<Item>[3];

        public void Normalize()
        {
            List<Tuple<Item, int>> NewComponents = new List<Tuple<Item, int>>();

            foreach (Tuple<Item, int> component in Components)
            {
                bool stacked = false;
                for (int i=0;i< NewComponents.Count;i++)
                {
                    Tuple<Item, int> nc = NewComponents[i];
                    if (component.Item1.CanStackWith(nc.Item1))
                    {
                        stacked = true;
                        NewComponents[i]=new Tuple<Item, int>(nc.Item1, nc.Item2+component.Item2);
                        break;
                    }
                }
                if (!stacked)
                    NewComponents.Add(component);

            }
            Components = NewComponents;
        }

        public int Satisfy(Items.Inventory inventory)
        {
            Normalize();
            int result = -1;

            foreach (Tuple<Item, int> component in Components)
            {
                int i = inventory.CountItem(component.Item1);
                if (i == 0) //lacking one of the items completely
                    return 0;
                if (i < component.Item2) //not enough for even one craft of one of the items
                    return 0;
                int amount = i / component.Item2;
                if (result == -1) //first item
                    result = amount;
                if (result > amount) //this item is enough for fewer crafts than all previous ones
                    result = amount;
            }


            return result;
        }

        public bool Craft(Items.Inventory inventory, int overskill = 0)
        {

            List<int> Weights = new List<int>();
            if (overskill < 0)
                overskill = 0;
            switch(overskill)
            {
                case 0:
                    {
                        Weights = new List<int>() { 87, 10, 3 };
                        break;
                    }
                case 1:
                    {
                        Weights = new List<int>() { 75, 20, 5 };
                        break;
                    }
                case 2:
                    {
                        Weights = new List<int>() { 62, 30, 8 };
                        break;
                    }
                case 3:
                    {
                        Weights = new List<int>() { 54, 36, 11 };
                        break;
                    }
                case 4:
                    {
                        Weights = new List<int>() { 45, 40, 15 };
                        break;
                    }
                case 5:
                    {
                        Weights = new List<int>() { 30, 50, 20 };
                        break;
                    }
                case 6:
                    {
                        Weights = new List<int>() { 10, 50, 40 };
                        break;
                    }
                default:
                    {
                        Weights = new List<int>() { 0, 40, 60 };
                        break;
                    }
            }


            //let the RNG pick one of the lists and return
            List<Item> result= Outputs[RNG.PickWeighted(Weights)];
            inventory.Prepare();

            //remove crafting components from inventory
            foreach(Tuple<Item,int> component in Components)
            {
                inventory.RemoveItem(component.Item1, component.Item2);
            }

            bool ok = true;
            //keep adding resulting items
            
            foreach(Item i in result)
            {
                if(inventory.AddItem((Item)i.Clone())!=null) //additem returns null on success and item it couldn't add on failure
                {
                    //inventory full, undo everything and return failure
                    inventory.Rollback();
                    ok = false;
                }
            }
            if(ok)
            {
                //all items added and removed as needed, commit changes and return success
                inventory.Commit();
                return true;
            }
            //shouldn't ever get here but well \_(o_O)_/
            return false;
        }
    }
}
