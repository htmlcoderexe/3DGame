using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace WorldGen
{
    public class DisTreeItem
    {
        public DisTreeItem Parent;
        public int Rank;
        public int ID;

        public DisTreeItem(int ID)
        {
            this.Rank = 0;
            this.ID = ID;
            this.Parent = this;
        }

        public DisTreeItem Root()
        {
            //if the item has no parent, it is the root
            if (this.Parent == this)
                return this;
            //otherwise, request parent to find root
            return this.Parent.Root();
        }
        public void Combine(DisTreeItem second)
        {
            //get both roots
            DisTreeItem rootA = this.Root();
            DisTreeItem rootB = second.Root();
            //if both share root, do nothing, already combined
            if (rootA == rootB)
                return;
            //if A is bigger, attach B to A's root
            if (rootA.Rank > rootB.Rank)
            {
                rootB.Parent = rootA;
            }
            //if B is bigger, attach A to B's root
            else if (rootA.Rank < rootB.Rank)
            {
                rootA.Parent = rootB;
            }
            //if both trees are equal, parent B to A and upgrade A
            else
            {
                rootB.Parent = rootA;
                rootA.Rank++;
            }
        }
        public static List<Vector3> PruneEdges(List<Vector3> Edges, float connectedness, IRandomProvider RNG)
        {
            //this random will be used for randomly adding back some pruned edges.
           // System.Random RNG = new Random();

            //this list will be returned.
            List<Vector3> result = new List<Vector3>();
            //sort edges by weight (stored in Vector3.Z)
            List<Vector3> SortedEdges = Edges.OrderBy(v => v.Z).ToList();
            //List of tree items
            List<DisTreeItem> tree = new List<DisTreeItem>();
            //list of nodes
            //this implementation allows for arbitrary numeric indices
            List<int> itemlist = new List<int>();
            //find all possible nodes from the edges
            foreach (Vector3 edge in SortedEdges)
            {
                //Get edge's start and end nodes
                int A = (int)edge.X;
                int B = (int)edge.Y;
                //check if either already exists, add if not
                if (!itemlist.Contains(A))
                    itemlist.Add(A);
                if (!itemlist.Contains(B))
                    itemlist.Add(B);

            }
            //create a tree item from each found node
            foreach (int i in itemlist)
            {
                tree.Add(new DisTreeItem(i));
            }
            //sort the tree by ID (node number) so we can tree[i]
            tree = tree.OrderBy(v => v.ID).ToList();
            //go through all edges starting with lowest
            foreach (Vector3 edge in SortedEdges)
            {
                //get start and end node id
                int A = (int)edge.X;
                int B = (int)edge.Y;
                //get the relevant tree items
                DisTreeItem a = tree[A];
                DisTreeItem b = tree[B];
                //if the roots are different, the nodes connected by this edge belong to different trees
                //this edge becomes part of the final spanning tree.
                if (a.Root() != b.Root())
                {
                    //merge the trees, smaller tree gets attached to the larger one.
                    a.Combine(b);
                    result.Add(edge);
                }
                //otherwise the edge creates a cycle and should be discarded
                else
                {
                    //however, a certain percentage is kept to keep things interesting
                    if (connectedness > RNG.NextFloat())
                    {
                        //we do not update the tree here
                        result.Add(edge);
                    }
                }
            }

            return result;
        }
    }
}
