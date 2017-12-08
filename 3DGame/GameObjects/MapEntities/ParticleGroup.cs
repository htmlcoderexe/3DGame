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
        public ParticleGroup()
        {
            this.Particles = new List<Particle>();
        }
        public override void Update(float dT)
        {
            foreach (Particle p in this.Particles)
            {
                p.Origin = this.Position;
                p.Heading = this.Heading;
                p.Update(dT);
            }
            if(this.Target!=null)
            {
                Aim(Target);
                
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
    }
}
