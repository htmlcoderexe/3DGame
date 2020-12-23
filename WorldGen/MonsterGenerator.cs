using GameObject.Items;
using GameObject;
using GameObject.MapEntities;
using GameObject.MapEntities.Actors;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGen
{
    public class MonsterGenerator
    {
        private IRandomProvider RNG;

        public MonsterGenerator(IRandomProvider RNG)
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

        public Monster GenerateOneMonster()
        {
            Monster monster = new Monster();
            monster.Model = GameModel.ModelGeometryCompiler.LoadModel("default");
            monster.DisplayName = "monster. kill me please.";
            Vector3 pos = new Vector3(RNG.NextInt(63), 0, RNG.NextInt(63));
            monster.Position = pos;
            monster.LeashRadius = 30;
            monster.PrimaryLootRollCount = 2;
            monster.PrimaryLootTable = new List<Tuple<int, Item>>();
            ItemConsumable potA = new ItemConsumable
            {
                Colour = new Color(200, 50, 0),
                Name = "HP potion"
            };
            ItemConsumable potB = new ItemConsumable
            {
                Colour = new Color(0, 100, 200),
                Name = "MP potion"
            };
            Material matA = Material.MaterialTemplates.GetRandomMaterial();
            ItemEquip weapA = new ItemEquip();

            BonusPool p = BonusPool.Load("heavy_0_10");
            weapA.Bonuses.Add(p.PickBonus());
            weapA.Bonuses.Add(p.PickBonus());
            weapA.SubType = RNG.NextInt(0, 1) * 4;
            weapA.PrimaryMaterial = Material.MaterialTemplates.GetRandomMaterial();
            weapA.SecondaryMaterial = Material.MaterialTemplates.GetRandomMaterial();
            monster.PrimaryLootTable.Add(new Tuple<int, Item>(5, potA));
            monster.PrimaryLootTable.Add(new Tuple<int, Item>(5, potB));
            monster.PrimaryLootTable.Add(new Tuple<int, Item>(1, matA));
            monster.PrimaryLootTable.Add(new Tuple<int, Item>(3, null));
            monster.PrimaryLootTable.Add(new Tuple<int, Item>(10, weapA));

            return monster;

        }

        public EntitySpawner PackMonster(Monster tpl)
        {
            EntitySpawner s = new EntitySpawner();
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
