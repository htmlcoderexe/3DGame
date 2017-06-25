using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameModel
{
    public class Model : ModelPart
    {
        private VertexBuffer _VB;
        private IndexBuffer _IB;
        private void Prepare(GraphicsDevice device)
        {
            Vector2 TotalSizes = this.Children[0].GetCounts();


            _IB = new IndexBuffer(device, IndexElementSize.ThirtyTwoBits, (int)TotalSizes.Y, BufferUsage.WriteOnly);
            _VB = new VertexBuffer(device, VertexPositionColor.VertexDeclaration, (int)TotalSizes.X, BufferUsage.WriteOnly);
            int[] indices = new int[(int)TotalSizes.Y];
            VertexPositionColor[] vertices = new VertexPositionColor[(int)TotalSizes.X];
            //current vertex/index offset
            int cIo = 0;
            int cVo = 0;
            List<ModelPart> Parts = this.Children[0].GetFlatList();
            foreach(ModelPart p in Parts)
            {
                int Icount = p.IndexLength;
                int Vcount = p.VertexLength;
                p.IndexOffset = cIo;
                p.VertexOffset = cVo;
                p.GetIndices().CopyTo(indices, cIo);
                p.GetVertices().CopyTo(vertices, cVo);
                cIo += Icount;
                cVo += Vcount;
            }


            _IB.SetData<int>(indices);
            _VB.SetData<VertexPositionColor>(vertices);

        }
        public Model()
        {
            this.Children = new List<ModelPart>();
            ModelPart Root = new ModelPart();

            VertexPositionColor[] vertices = new VertexPositionColor[8];
            vertices[0] = new VertexPositionColor(new Vector3(-1.5f,  0.5f, -0.5f), Color.DarkGray );
            vertices[1] = new VertexPositionColor(new Vector3( 0.5f,  0.2f,  0.2f), Color.DarkGray );
            vertices[2] = new VertexPositionColor(new Vector3(-1.5f,  0.5f,  0.5f), Color.DarkGray );
            vertices[3] = new VertexPositionColor(new Vector3( 0.5f,  0.2f, -0.2f), Color.DarkGray );
                                                                                         
            vertices[4] = new VertexPositionColor(new Vector3(-1.5f, -0.5f, -0.5f), Color.DarkGray );
            vertices[5] = new VertexPositionColor(new Vector3( 0.5f, -0.2f,  0.2f), Color.DarkGray );
            vertices[6] = new VertexPositionColor(new Vector3(-1.5f, -0.5f,  0.5f), Color.DarkGray );
            vertices[7] = new VertexPositionColor(new Vector3( 0.5f, -0.2f, -0.2f), Color.DarkGray );
            Root.SetVertices(vertices);
            Root.SetIndices(new int[] { //--,++,-+/--,+-,++
                 0, 1, 2, 0, 3, 1
                ,4, 6, 5, 4, 5, 7
                ,4, 2, 6, 4, 0, 2
                ,7, 5, 1, 7, 1, 3
                ,6, 2, 1, 6, 1, 5
                ,4, 3, 0, 4, 7, 3
            });

            ModelPart leg;
            leg = new TestParts.PartBugLeg();
            Root.Append(leg, Matrix.CreateRotationY((float)Math.PI * 0.3f) * Matrix.CreateTranslation(new Vector3(0.4f, 0, -0.5f)));
            leg = new TestParts.PartBugLeg();
            leg.Animation.SetPhase(0.5f);
            Root.Append(leg, Matrix.CreateRotationY((float)Math.PI * 0.5f) * Matrix.CreateTranslation(new Vector3(0, 0, -0.5f)));
            leg = new TestParts.PartBugLeg();
            Root.Append(leg, Matrix.CreateRotationY((float)Math.PI * 0.8f) * Matrix.CreateTranslation(new Vector3(-0.4f, 0, -0.5f)));

            leg = new TestParts.PartBugLeg(true);
            leg.Animation.SetPhase(0.5f);
            Root.Append(leg, Matrix.CreateRotationY((float)Math.PI * 1.8f) * Matrix.CreateTranslation(new Vector3(0.4f, 0, 0.5f)));
            leg = new TestParts.PartBugLeg(true);
            Root.Append(leg, Matrix.CreateRotationY((float)Math.PI * 1.5f) * Matrix.CreateTranslation(new Vector3(0, 0, 0.5f)));
            leg = new TestParts.PartBugLeg(true);
            leg.Animation.SetPhase(0.5f);
            Root.Append(leg, Matrix.CreateRotationY((float)Math.PI * 1.3f) * Matrix.CreateTranslation(new Vector3(-0.4f, 0, 0.5f)));


            this.Children.Add(Root);

        }
        public override void Render(GraphicsDevice device, float dT, Matrix World, BasicEffect fx)
        {
            if (this._IB == null)
                Prepare(device);
            device.SetVertexBuffer(_VB);
            device.Indices = _IB;
            foreach(ModelPart c in this.Children)
            {
                c.Render(device, dT, World, fx);
            }

        }

    }
}
