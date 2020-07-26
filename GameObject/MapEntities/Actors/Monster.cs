using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObject;
using Microsoft.Xna.Framework;

namespace GameObject.MapEntities.Actors
{
    public class Monster : Actor
    {
        public float LeashRadius { get; set; }
        public Dictionary<Actor, float> HateList = new Dictionary<Actor, float>();
        public int EXPDrop = 2;
        public Monster()
        {
            this.DeathCallback = new Action<MapEntity>(DeathAction);
        }
        public void DeathAction(MapEntity me)
        {
            float mosthated = 0;
            Actor mosthateda = null;
            List<Player> players = new List<Player>();
            foreach(Actor a in HateList.Keys)
            {
                if(HateList[a]>mosthated)
                {
                    mosthated = HateList[a];
                    mosthateda = a;
                }
                if (a is Player p)
                    players.Add(p);
            }
            foreach(Player p in players)
            {
                if (p == mosthateda)
                    p.GiveExp(EXPDrop);
                else
                    p.GiveExp((int)(Math.Round((float)EXPDrop/2f)));
            }

        }
        public override void Hit(Actor Source,float amount, bool Magic, int Type)
        {
            if (_isDead)
                return;
            if (HateList.ContainsKey(Source))
                HateList[Source] += amount;
            else
                HateList[Source] = amount;
            base.Hit(Source,amount, Magic, Type);

        }
        public override void Update(float dT)
        {
            this.Speed = 0;
            DoIdle(dT);
            base.Update(dT);
        }
        public void DoIdle(float dT)
        {
            if (Walking)
            {
                this.Speed=this.GetMovementSpeed();

                StepToTarget(dT);
            }
            else
            {
                this.Speed = 0.0f;
                SetWalk();
            }
        }

        public void SetWalk()
        {
            float r = (float)RNG.NextDouble() * LeashRadius;
            float a = (float)RNG.Next(0, 359);
            Vector3 v = new Vector3(r, 0, 0);
            v = Vector3.Transform(v,Matrix.CreateRotationY(MathHelper.ToRadians(a)));
            WalkTo(this.Parent.Position + v);
        }

        public void DoWalk(float dT)
        {

        }
    }
}
