using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameModel;
using Microsoft.Xna.Framework;

namespace GameObject.MapEntities.Particles
{
    public class Spiral : Homing
    {
        public float SpinTimer;
        public Spiral(Color Colour, float Size = 1, PartAnimation a = null) : base(Colour, Size, a)
        {
            this.spawnfreq = 0;
        }

        public override void Update(float dT)
        {
            SpinTimer += dT;

            base.Update(dT);
        }
        public override void StepToTarget(float dT)
        {

            Interfaces.WorldPosition diff = (this.Parent.Position + new Vector3(0, 1.2f, 0)) - this.Origin;
            // diff.Y = 0;
            Vector3 v = diff;
            if (v.Length() < 0.5f)
            {
                this.Die();
                return;
            }
            v.Normalize();
            Vector3 s = new Vector3(0f, 0, 0.5f);
            this.Heading = (float)(Math.Atan2(v.X, v.Z) / MathHelper.Pi * -180f) + 90f;
            Matrix spinm = Matrix.CreateRotationX((float)SpinTimer * 3f);
            Matrix head = Matrix.CreateRotationY(MathHelper.ToRadians(-this.Heading + 0));
            s = Vector3.Transform(s, spinm * head);

            this.Origin += v * this.Speed * dT;
            this.Position = this.Origin + s;
        }
    }
}
