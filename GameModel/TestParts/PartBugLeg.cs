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
            this.Animation.Add(Up * Center, 1.2f*2.0f);
            this.Animation.Add(Down * Forward, 1.2f * 2.0f);
            this.Animation.Add(Down * Backward, 2.4f * 2.0f);
            this.Animation.Loop = true;
            _vertices = new ModelVertex[8];
            //origin vertex
            _vertices[0] = new ModelVertex(new Vector3(0.0f, 0.00f, -0.00f), Color.Black);
            _vertices[0].BoneWeightData.X = 0;
            //middle 3
            _vertices[1] = new ModelVertex(new Vector3(0.3f, 0.4f + 0.02f, -0.00f), Color.Black);
            _vertices[1].BoneWeightData.X = 0.33f;
            _vertices[2] = new ModelVertex(new Vector3(0.3f, 0.4f + 0.00f, -0.02f), Color.Black);
            _vertices[2].BoneWeightData.X = 0.33f;
            _vertices[3] = new ModelVertex(new Vector3(0.3f, 0.4f + 0.00f, 0.02f), Color.Black);
            _vertices[3].BoneWeightData.X = 0.33f;
            //middle 3
            _vertices[4] = new ModelVertex(new Vector3(0.7f, 0.2f + 0.02f, -0.00f), Color.Black);
            _vertices[4].BoneWeightData.X = 0.67f;
            _vertices[5] = new ModelVertex(new Vector3(0.7f, 0.2f + 0.00f, -0.02f), Color.Black);
            _vertices[5].BoneWeightData.X = 0.67f;
            _vertices[6] = new ModelVertex(new Vector3(0.7f, 0.2f + 0.00f, 0.02f), Color.Black);
            _vertices[6].BoneWeightData.X = 0.67f;
            //tip
            _vertices[7] = new ModelVertex(new Vector3(1.2f, 0.00f, -0.00f), Color.Black);
            _vertices[7].BoneWeightData.X = 1f;

            _indices = (new int[] { //--,++,-+/--,+-,++
                0,2,1
               ,0,3,2
               ,0,1,3

               ,1,2,4
               ,4,2,5

               ,2,3,5
               ,5,3,6

               ,3,1,6
               ,6,1,4

               ,7,4,5
               ,7,5,6
               ,7,6,4
            });
            SetIndices(_indices);
            SetVertices(_vertices);
        }
        public override void Render(GraphicsDevice device, float dT, Matrix World, Effect fx, Matrix W2)
        {
            base.Render(device, dT, World, fx,W2);
        }
    }
}
