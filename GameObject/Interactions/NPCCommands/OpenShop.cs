using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.Interactions.NPCCommands
{
    public class OpenShop : NPCCommand
    {
        public NPCShop Shop;
        public OpenShop()
        {
            this.Shop = new NPCShop();

        }
    }
}
