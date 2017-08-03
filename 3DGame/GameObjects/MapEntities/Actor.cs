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

        public Actor()
        {
            this.StatBonuses = new List<StatBonus>();
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

        public override void Update(float dT)
        {
            this.Camera.Position = this.Position;
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
    }
}
