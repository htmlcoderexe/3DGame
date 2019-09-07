using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame.GameObjects.MapEntities
{
    public class ParticleGroup : MapEntity
    {
        public MapEntity Target;
        public List<Particle> Particles;
        public GameModel.PartAnimation Animation;
        public bool FizzleOnTarget = true;
        public bool FizzleOnGround = false;
        public bool Expires = false;
        public float TTL;
        public bool FlatAim = false;
        private void DoExpire(float dT)
        {

            this.TTL -= dT;
            if (this.TTL <= 0)
                this.Die();
        }
        public ParticleGroup()
        {
            this.Particles = new List<Particle>();
        }
        public override void Update(float dT)
        {
            if (this.Expires)
                DoExpire(dT);
            foreach (Particle p in this.Particles)
            {
                p.Origin = this.Position;
               // if (!FlatAim)
                    p.Heading = this.Heading;

                if(!FlatAim)
                p.Pitch = this.Pitch;
                p.Update(dT);
            }
            if(this.Target!=null)
            {
                Aim(Target,false);
                if (FizzleOnTarget && (this.Position - Target.Position).Truncate().Length() < 0.5f)
                    this.Die();
            }
            
            base.Update(dT);
        }
        public override void Render(GraphicsDevice device, float dT, Vector2 Reference, bool Alpha)
        {
            foreach (Particle p in this.Particles)
            {
                p.Render(device, dT, Reference, Alpha);
            }
            base.Render(device, dT, Reference, Alpha);
        }
        public override void Die()
        {
            foreach (Particle p in this.Particles)
                p.Die();
            this.Particles.Clear();
            base.Die();
        }
    }
}
