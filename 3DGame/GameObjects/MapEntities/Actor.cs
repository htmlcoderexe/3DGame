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

        private float _TickLength=0.9f;
        private float _TickTime;
        private Interfaces.WorldPosition WalkTarget;

        public Actor()
        {
            this.StatBonuses = new List<StatBonus>();
            this.StatBonuses.Add(new StatBonus() { FlatValue = 100, Type = "HP", Order = StatBonus.StatOrder.Template });
            this.StatBonuses.Add(new StatBonus() { FlatValue = 15, Type = "hpregen", Order = StatBonus.StatOrder.Template });
            this.Camera = new Camera();
            this.Abilities = new List<Ability>();
        }
        public float GetMovementSpeed()
        {
            return 5 + CalculateStat("movement_speed");
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
            this.Camera.Position = this.Position;
            UpdateBuffers(dT);
            base.Update(dT);
        }

        public void WalkTo(Interfaces.WorldPosition Target)
        {
            this.WalkTarget = Target;
            this.Walking = true;
        }

        public void StepToTarget(float dT)
        {
            Interfaces.WorldPosition diff = this.WalkTarget - this.Position;
            Vector3 v = diff;
            if (v.Length() < 0.5f)
            {
                Walking = false;
                return;
                    }
            v.Normalize();
            v *= dT;
            this.Position += v * Speed;
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
      
    }
}
