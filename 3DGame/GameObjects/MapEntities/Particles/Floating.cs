using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _3DGame.GameObjects.MapEntities.Particles
{
    public class Floating : Particle
    {
        public Vector3 Velocity;
        GameModel.TestParts.PartLight l;
        public float SizeDelta = 0.0f;
        public Floating(Color Colour,float Size=1.0f)
        {
            this.Model = new GameModel.Model();
            this.Model.Clear();
            this.Colour = Colour;
           l = new GameModel.TestParts.PartLight(Colour);
            GameModel.TestParts.PartBugLeg b = new GameModel.TestParts.PartBugLeg();
             l.Width = Size; ;
           // this.Model.Append(b, Matrix.Identity);
            this.Model.Append(l, Matrix.Identity);
        }
        public override void Update(float dT)
        {
            this.Position += (Velocity * dT);
            base.Update(dT);
            l.Width +=( SizeDelta * dT);
        }
    }
}
