using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameModel.TestParts
{
    public class PartBugLeg : ModelPart
    {
        public PartBugLeg(bool Flip=false)
        {
            Matrix Up = Matrix.CreateRotationZ(MathHelper.ToRadians(30.0f));
            Matrix Down = Matrix.CreateRotationZ(MathHelper.ToRadians(0.0f));
            Matrix Forward = Matrix.CreateRotationY(MathHelper.ToRadians(Flip?30.0f:-30.0f));
            Matrix Backward = Matrix.CreateRotationY(MathHelper.ToRadians(Flip ? -30.0f : 30.0f));
            Matrix Center = Matrix.CreateRotationY(MathHelper.ToRadians(1f));
            this.Animation = new PartAnimation();
            this.Animation.Add(Down * Backward, 0);
            this.Animation.Add(Up * Center, 1.2f);
            this.Animation.Add(Down * Forward, 1.2f);
            this.Animation.Add(Down * Backward, 2.4f);
            this.Animation.Loop = true;
            _vertices = new VertexPositionColor[8];
            _vertices[0] = new VertexPositionColor(new Vector3( 0.0f,    0.02f,   -0.02f),   Color.Black);
            _vertices[1] = new VertexPositionColor(new Vector3( 1.0f,    0.02f,    0.02f),   Color.Black);
            _vertices[2] = new VertexPositionColor(new Vector3( 0.0f,    0.02f,    0.02f),   Color.Black);
            _vertices[3] = new VertexPositionColor(new Vector3( 1.0f,    0.02f,   -0.02f),   Color.Black);
                                                                                                   
            _vertices[4] = new VertexPositionColor(new Vector3( 0.0f,   -0.02f,   -0.02f),   Color.Black);
            _vertices[5] = new VertexPositionColor(new Vector3( 1.0f,   -0.02f,    0.02f),   Color.Black);
            _vertices[6] = new VertexPositionColor(new Vector3( 0.0f,   -0.02f,    0.02f),   Color.Black);
            _vertices[7] = new VertexPositionColor(new Vector3( 1.0f,   -0.02f,   -0.02f),   Color.Black);

            _indices= (new int[] { //--,++,-+/--,+-,++
                 0, 1, 2, 0, 3, 1
                ,4, 6, 5, 4, 5, 7
                ,4, 2, 6, 4, 0, 2
                ,7, 5, 1, 7, 1, 3
                ,6, 2, 1, 6, 1, 5
                ,4, 3, 0, 4, 7, 3
            });
            SetIndices(_indices);
            SetVertices(_vertices);
        }
        public override void Render(GraphicsDevice device, float dT, Matrix World, BasicEffect fx)
        {
            base.Render(device, dT, World, fx);
        }
    }
}
