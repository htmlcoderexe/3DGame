using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _3DGame.GameObjects.MapEntities.Particles
{
    public class LightRay : Particle
    {
        public MapEntity Source;
        public MapEntity Target;
        private GameModel.TestParts.PartLight l;
        public LightRay(MapEntity Source, MapEntity Target,Color Colour, float Size = 1.0f)
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
            this.Source = Source;
            this.Target = Target;
        }
        public override void Update(float dT)
        {
            Interfaces.WorldPosition diff = Target.Position - Source.Position;
            // this.Offset =Source.Position;
            //this.l.Height = ((Vector3)diff).Length();
            this.l.Direction = diff*2f;
            this.Offset = diff * 0.5f;
            base.Update(dT);
        }
    }
}
