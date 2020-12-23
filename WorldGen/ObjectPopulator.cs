using GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGen
{
    public class ObjectPopulator
    {
        private IRandomProvider RNG;

        public ObjectPopulator(IRandomProvider RNG)
        {
            this.RNG = RNG;
        }

        public List<MapEntity> GenerateObjectsTest(int count)
        {
            List<MapEntity> results = new List<MapEntity>();
            MonsterGenerator mg = new MonsterGenerator(this.RNG);
            NPCGenerator ng = new NPCGenerator(this.RNG);
            results.AddRange(mg.GenerateTestMonsters((int)count / 3));
            results.AddRange(ng.GenerateTestNPCs((int)count));
            return results;
        }

    }
}
