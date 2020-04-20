using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.GameObjects.WorldGen
{
    public class MonsterGenerator
    {
        private System.Random RNG;

        public MonsterGenerator(System.Random RNG)
        {
            this.RNG = RNG;
        }

        public List<MapEntity> GenerateTestMonsters(int amount)
        {
            List<MapEntity> results = new List<MapEntity>();
            for (int i = 0; i < amount; i++)
            {
                results.Add(PackMonster(GenerateOneMonster()));
            }
            return results;
        }

        public MapEntities.Actors.Hostile GenerateOneMonster()
        {
            MapEntities.Actors.Hostile monster = new MapEntities.Actors.Hostile();
            monster.Model = GameModel.ModelGeometryCompiler.LoadModel("default");
            monster.DisplayName = "monster. kill me please.";
            Vector3 pos = new Vector3(RNG.Next(63), 0, RNG.Next(63));
            monster.Position = pos;
            return monster;

        }

        public MapEntities.EntitySpawner PackMonster(MapEntities.Actors.Hostile tpl)
        {
            GameObjects.MapEntities.EntitySpawner s = new GameObjects.MapEntities.EntitySpawner();
            s.Entity = tpl;
            s.Interval = 5;
            s.CountDown = 2;
            s.MaxCount = 6;
            s.SpawnCallback = new Action<MapEntity>(e => s.WorldSpawn.Entities.Add(e));
            s.Position = tpl.Position;
            s.SpawningVolume = new BoundingBox(new Vector3(-5, 0, -5), new Vector3(5, 0, 5));
            s.Entity.Parent = s;
            return s;
        }
    }
}
