using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameObject.MapEntities.ParticleGroups
{
    public class Ring : ParticleGroup
    {
        public Ring(float Size, float Thickness, Color Colour)
        {
            this.Model = null;
            Vector3 Offset = new Vector3(0, 0, Size);
            Matrix XForm = Matrix.CreateRotationX(MathHelper.ToRadians(20));
            Particles.LightBall p;
            for(int i=0;i<18;i++)
            {
                p = new MapEntities.Particles.LightBall(Colour, Thickness);
                p.Offset = Offset;
                Offset = Vector3.Transform(Offset, XForm);
                this.Particles.Add(p);
            }
        }
        
    }
}
