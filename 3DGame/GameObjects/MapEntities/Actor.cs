using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public GameObjects.Camera Camera;
        public Actor Target;

        private float _TickLength=0.9f;
        private float _TickTime;

        public Actor()
        {
            this.StatBonuses = new List<StatBonus>();
            this.StatBonuses.Add(new StatBonus() { FlatValue = 100, Type = "HP", Order = StatBonus.StatOrder.Template });
            this.StatBonuses.Add(new StatBonus() { FlatValue = 15, Type = "hpregen", Order = StatBonus.StatOrder.Template });
            this.Camera = new Camera();
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
