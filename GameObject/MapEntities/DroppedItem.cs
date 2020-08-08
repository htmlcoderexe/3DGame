using GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.MapEntities
{
    public class DroppedItem : MapEntity
    {
        public Item Item;
        public DroppedItem(Item Item)
        {
            this.Item = Item;
            GameModel.Model m = this.Item.GetModel();
            this.Model = m ?? GameModel.ModelGeometryCompiler.LoadModel("planttest");
            this.DisplayName = this.Item.Name;
        }
        public override void Click(Actor Target)
        {
            GetMe(Target);
            base.Click(Target);
        }
        void GetMe(Actor Target)
        {
            Target.WalkTo(this.Position);
            Target.WalkCallback = new Action<Actor>(PickMe);
        }

        void PickMe(Actor Target)
        {
            if (Target as Actors.Player != null)
            {
                (Target as Actors.Player).Inventory.Prepare();
                (Target as Actors.Player).Inventory.AddItem(this.Item);
                (Target as Actors.Player).Inventory.Commit();
                Console.Write("^00C020 Picked up " + this.Item.GetName());
                this.Die();
            }
                
        }
    }
}
