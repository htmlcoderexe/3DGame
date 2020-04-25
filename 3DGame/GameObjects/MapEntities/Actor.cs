using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _3DGame.GameObjects.MapEntities
{
    public class Actor : MapEntity
    {
        public List<Tuple<int, Item>> PrimaryLootTable;
        public int PrimaryLootRollCount;
        public virtual float CurrentHP { get; set; }
        public virtual float CurrentMP { get; set; }
        public float CurrentHPBuffer;
        public float CurrentMPBuffer;
        public string Name { get; set; }
        public List<StatBonus> StatBonuses;
        public List<Ability> Abilities;
        public GameObjects.Camera Camera;
        public Actor Target;
        public bool Walking;
        public Action<Actor> WalkCallback;
        private float _TickLength=0.9f;
        private float _TickTime;
        private Interfaces.WorldPosition WalkTarget;

        public Actor()
        {
            this.StatBonuses = new List<StatBonus>();
            this.StatBonuses.Add(new StatBonus() { FlatValue = 100, Type = "HP", Order = StatBonus.StatOrder.Template });
            this.StatBonuses.Add(new StatBonus() { FlatValue = 1, Type = "hpregen", Order = StatBonus.StatOrder.Template });
            this.Camera = new Camera();
            this.Abilities = new List<Ability>();
            CurrentHP = CalculateStat("HP");
            this.PrimaryLootTable = new List<Tuple<int, Item>>();
        }
        public float GetMovementSpeed()
        {
            return 3 + CalculateStat("movement_speed");
        }
        public float CalculateStage(float input,List<StatBonus> Bonuses,StatBonus.StatOrder Stage)
        {
            float result = 0;

            List<StatBonus> stage = Bonuses.FindAll(s => s.Order == Stage).ToList();
            float flats = stage.Sum(s => s.FlatValue);
            float multis = stage.Sum(s => s.Multiplier);
            result = (input + flats) * (1 + multis);
            return result;
        }

        public float CalculateStat(string statname)
        {
            float result = 0;
            List<StatBonus> stats = StatBonuses.FindAll(s => s.Type == statname).ToList();
            if (stats.Count < 1)
                return 0;
            for(int i=0;i<(int)StatBonus.StatOrder.Count;i++)
            {
                result=CalculateStage(result,stats,(StatBonus.StatOrder)i);
            }
            return result;
        }
        public void UpdateBuffers(float dT)
        {
            float dHP = CalculateStat("hpdelta")+CalculateStat("hpregen");
            float dMP = CalculateStat("mpdelta") + CalculateStat("mpregen");
            float MaxHP = CalculateStat("HP");
            float MaxMP = CalculateStat("MP");
            CurrentHPBuffer += dHP*dT;
            CurrentMPBuffer += dMP*dT;
            _TickTime += dT;
            if(_TickTime>=_TickLength)
            {
                CurrentHP += CurrentHPBuffer;
                if (CurrentHP > MaxHP)
                    CurrentHP = MaxHP;
                CurrentHPBuffer = 0.0f;

                CurrentMP += CurrentMPBuffer;
                if (CurrentMP > MaxMP)
                    CurrentMP = MaxMP;
                CurrentMPBuffer = 0.0f;

                _TickTime-=_TickLength;
            }
        }


        public override void Update(float dT)
        {
            UpdateBuffers(dT);
            if (CurrentHP < 0)
                Die();
            base.Update(dT);
            //that's right, FIRST update object position and THEN its camera
            //otherwise it looks very very jittery for no good reason
            this.Camera.Position = this.Position + new Vector3(0, 1.5f, 0);
        }

        public void WalkTo(Interfaces.WorldPosition Target)
        {
           // Console.Write("^FFFFFF Walking to ^FFFF00 " + Target.ToString());
            this.WalkTarget = Target;
            this.Walking = true;
        }

        public void StepToTarget(float dT)
        {
            Interfaces.WorldPosition diff = this.WalkTarget - this.Position;
            diff.Y = 0;
            Vector3 v = diff;
            if (v.Length() < 0.5f)
            {
                Walking = false;
                WalkCallback?.Invoke(this);
               // Console.Write("Arrived at ^FFFF00 " + this.WalkTarget.ToString());
                return;
                    }
            v.Normalize();

            this.Heading = (float)(Math.Atan2(v.X, v.Z) / MathHelper.Pi * -180f) + 90f ;
            this.Position += v * this.Speed*dT;
            if (!OnGround && Gravity)
                this.Position.Y += this.VerticalSpeed * dT;

        }

        public Camera GetTheCamera()
        {
            return this.Camera;
        }

        public override object Clone()
        {
            Actor a = (Actor)base.Clone();
            List<StatBonus> bb = new List<StatBonus>();
            foreach (StatBonus b in this.StatBonuses)
                bb.Add((StatBonus)b.Clone());
            a.Camera = (Camera)this.Camera.Clone();
            return a;
        }
        public override void Click(Actor Target)
        {
            Target.Target = this;
        }

        public virtual void Hit(float amount, bool Magic, int Type)
        {
            this.CurrentHPBuffer -= amount;
        }

        public static Item PickItemFromTable(List<Tuple<int, Item>> table, Random RNG)
        {
            int maxval = 0;
            foreach(Tuple<int,Item> item in table)
            {
                maxval += item.Item1;//sum up weights;
            }
            int roll = RNG.Next(0, maxval+1);

            foreach (Tuple<int, Item> item in table)
            {
                roll -= item.Item1;//sum up weights;
                if (roll <= 0)
                    return item.Item2;
            }
            return null;
        }


        public override void Die()
        {
            System.Random RNG = new Random();
            Vector3 offset = Vector3.Zero;
            if (this.PrimaryLootTable.Count>0)
            {
                for(int i=0;i<this.PrimaryLootRollCount;i++)
                {

                    Item item = PickItemFromTable(this.PrimaryLootTable,RNG);
                    if (item != null)
                    {
                        offset = new Vector3((float)(RNG.Next(0, 10) - 5) / 100f, 0, (float)(RNG.Next(0, 10) - 5) / 100f);
                        DroppedItem drop = new DroppedItem((Item)item.Clone());
                        drop.Position = this.Position + offset;
                        drop.WorldSpawn = this.WorldSpawn;
                        this.WorldSpawn.Entities.Add(drop);
                    }
                }

            }
            base.Die();
        }
    }
}
