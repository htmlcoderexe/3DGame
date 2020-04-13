using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _3DGame.GameObjects.MapEntities.Actos
{
    public class Hostile : Actor
    {
        public float LeashRadius { get; set; }
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
