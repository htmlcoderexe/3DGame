using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _3DGame.GameObjects.MapEntities
{
    public class Particle : MapEntity
    {
        public float TTL;
        public MapEntity Parent;
        public Color Colour;
        public Interfaces.WorldPosition Origin;
        public Interfaces.WorldPosition Offset;
        public bool Expires=false;
        private void DoExpire(float dT)
        {

            this.TTL -= dT;
            if (this.TTL <= 0)
                this.Die();
        }
        public override void Update(float dT)
        {

            if (Expires)
                DoExpire(dT);
            this.Position = Origin + Vector3.Transform(Vector3.Transform(Offset, Matrix.CreateRotationZ(MathHelper.ToRadians(this.Pitch))),Matrix.CreateRotationY(MathHelper.ToRadians(-this.Heading)));
        }
    }
}
