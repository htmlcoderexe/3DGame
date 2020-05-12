using GameObject;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.WorldGen
{
    public class NPCGenerator
    {
        private System.Random RNG;

        public NPCGenerator(System.Random RNG)
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

        public MapEntities.Actors.NPC GenerateOneNPC()
        {
            MapEntities.Actors.NPC npc = new MapEntities.Actors.NPC("Hello. I am an NPC.");
            Vector3 pos = new Vector3(RNG.Next(63), 0, RNG.Next(63));
            npc.Position = pos;
            npc.Heading = RNG.Next(350);
            npc.DisplayName = "npc";
            return npc;
        }
    }
}
