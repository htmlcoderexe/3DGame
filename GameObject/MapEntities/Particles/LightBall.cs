using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameObject.MapEntities.Particles
{
    public class LightBall : Particle
    {
        private GameModel.TestParts.PartLight l;
        public LightBall(Color Colour, float Size = 1.0f)
        {
            this.Model = new GameModel.Model();
            this.Model.Clear();
            this.Colour = Colour;
            l = new GameModel.TestParts.PartLight(Colour);
            //GameModel.TestParts.PartBugLeg b = new GameModel.TestParts.PartBugLeg();
            l.Width = Size; 

            //  l.Dislocation = Matrix.CreateTranslation(0, 0, 1.2f);
            //this.Model.Append(b, Matrix.Identity);
            this.Model.Append(l, Matrix.Identity);
        }
        public override void Update(float dT)
        {
            base.Update(dT);
        }
    }
}
