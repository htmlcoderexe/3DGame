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

        public MapEntities.Actors.Monster GenerateOneMonster()
        {
            MapEntities.Actors.Monster monster = new MapEntities.Actors.Monster();
            monster.Model = GameModel.ModelGeometryCompiler.LoadModel("default");
            monster.DisplayName = "monster. kill me please.";
            Vector3 pos = new Vector3(RNG.Next(63), 0, RNG.Next(63));
            monster.Position = pos;
            monster.LeashRadius = 30;
            monster.PrimaryLootRollCount = 2;
            monster.PrimaryLootTable = new List<Tuple<int, Item>>();
            Items.ItemConsumable potA = new Items.ItemConsumable
            {
                Colour = new Color(200, 50, 0),
                Name = "HP potion"
            };
            Items.ItemConsumable potB = new Items.ItemConsumable
            {
                Colour = new Color(0, 100, 200),
                Name = "MP potion"
            };
            Items.Material matA = Items.Material.MaterialTemplates.GetRandomMaterial();
            monster.PrimaryLootTable.Add(new Tuple<int, Item>(5, potA));
            monster.PrimaryLootTable.Add(new Tuple<int, Item>(5, potB));
            monster.PrimaryLootTable.Add(new Tuple<int, Item>(1, matA));
            monster.PrimaryLootTable.Add(new Tuple<int, Item>(3, null));
           
            return monster;

        }

        public MapEntities.EntitySpawner PackMonster(MapEntities.Actors.Monster tpl)
        {
            GameObjects.MapEntities.EntitySpawner s = new GameObjects.MapEntities.EntitySpawner();
            s.Entity = tpl;
            s.Interval = 15;
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
