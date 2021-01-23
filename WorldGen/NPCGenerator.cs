//using GameObject;
using GameObject;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGen
{
    public class NPCGenerator
    {
        private IRandomProvider RNG;

        public NPCGenerator(IRandomProvider RNG)
        {
            this.RNG = RNG;
        }
        public List<MapEntity> GenerateTestNPCs(int amount)
        {
            List<MapEntity> results = new List<MapEntity>();

            for(int i=0;i<amount;i++)
            {
                results.Add(GenerateOneNPC());
            }

            return results;
        }

        public GameObject.MapEntities.Actors.NPC GenerateOneNPC()
        {
            GameObject.MapEntities.Actors.NPC npc = new GameObject.MapEntities.Actors.NPC("Hello. I am an NPC.");
            npc.Commands = new List<GameObject.Interactions.NPCCommand>();

            npc.Commands.Add(new GameObject.Interactions.NPCCommands.OpenShop() { Label = "Shop" });
            Vector3 pos = new Vector3(RNG.NextInt(63), 0, RNG.NextInt(63));
            npc.Position = pos;
            npc.Heading = RNG.NextInt(350);
            npc.DisplayName = "npc";
            return npc;
        }
    }
}
