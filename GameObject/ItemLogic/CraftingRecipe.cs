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
        public List<Item> Apply(List<Item> Inputs)
        {
            List<int> Weights = new List<int>();
            //turn weights into a list
            foreach (Tuple<List<Item>, int> output in Outputs)
                Weights.Add(output.Item2);
            //let the RNG pick one of the lists and return
            return Outputs[RNG.PickWeighted(Weights)].Item1;
        }
    }
}
