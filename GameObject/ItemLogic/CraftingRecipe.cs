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
        public List<Tuple<List<Item>,int>> Outputs= new List<Tuple<List<Item>, int>>();

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

        public bool Craft(Items.Inventory inventory)
        {

            List<int> Weights = new List<int>();
            //turn weights into a list
            foreach (Tuple<List<Item>, int> output in Outputs)
                Weights.Add(output.Item2);
            //let the RNG pick one of the lists and return
            List<Item> result= Outputs[RNG.PickWeighted(Weights)].Item1;
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
