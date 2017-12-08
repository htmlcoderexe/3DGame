using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _3DGame.GameObjects.MapEntities.Particles
{
    public class Homing : Particle
    {
        private float _spawn = 0.0f;
        public float spawnfreq = 0.01f;
        GameModel.TestParts.PartLight l;
        public Homing(Color Colour, float Size=1.0f,GameModel.PartAnimation a= null)
        {
            this.Model = new GameModel.Model();
            this.Model.Clear();
            this.Colour = Colour;
            l = new GameModel.TestParts.PartLight(Colour);
            //GameModel.TestParts.PartBugLeg b = new GameModel.TestParts.PartBugLeg();
             l.Width = Size; ;
            l.Animation = a;
            
              //  l.Dislocation = Matrix.CreateTranslation(0, 0, 1.2f);
            //this.Model.Append(b, Matrix.Identity);
            this.Model.Append(l, Matrix.Identity);
        }
        public override void Update(float dT)
        {
            StepToTarget(dT);
            _spawn += dT;
            if (IsDead)
                return;
            Particles.Floating t;
           // _spawn = 0.0f;
            if (_spawn>spawnfreq)
            {
                Color c = this.Colour;
                c = Color.Gray;
                c.A = 50;
                t = new Floating(c, l.Width / 5f);
                t.Velocity = new Vector3(0, 0f, 0);
                t.TTL = 0.5f;
                t.Position = this.Position;
                t.Gravity = false;
                t.SizeDelta = -1.0f;
                WorldSpawn.Entities.Add(t);

                //WorldSpawn.Entities.Remove(t);
                _spawn -= 0.01f;
            }
                base.Update(dT);
        }
        
        public virtual void StepToTarget(float dT)
        {
            Interfaces.WorldPosition diff = (this.Parent.Position+new Vector3(0,1.2f,0)) - this.Position;
           // diff.Y = 0;
            Vector3 v = diff;
            if (v.Length() < 0.5f)
            {
                this.Die();
                return;
            }
            v.Normalize();

            this.Heading = (float)(Math.Atan2(v.X, v.Z) / MathHelper.Pi * -180f) + 90f;
            this.Position += v * this.Speed * dT;

        }
    }
}
