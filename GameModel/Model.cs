using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameModel
{
    public class Model
    {
        private VertexBuffer _vertices;
        private IndexBuffer _indices;
        public BasicEffect fx;
        private void Prepare(GraphicsDevice device)
        {
            fx = new BasicEffect(device);
            _indices = new IndexBuffer(device, IndexElementSize.ThirtyTwoBits, 36, BufferUsage.WriteOnly);
            _indices.SetData<int>(new int[] { //--,++,-+/--,+-,++
                 0, 1, 2, 0, 3, 1 
                ,4, 6, 5, 4, 5, 7
                ,4, 2, 6, 4, 0, 2
                ,7, 5, 1, 7, 1, 3
                ,6, 2, 1, 6, 1, 5
                ,4, 3, 0, 4, 7, 3
            });
            VertexPositionColor[] vertices = new VertexPositionColor[8];
            vertices[0] = new VertexPositionColor(new Vector3(-0.5f, 0.5f, -0.5f), Color.Blue);
            vertices[1] = new VertexPositionColor(new Vector3(0.5f, 0.5f, 0.5f), Color.Blue);
            vertices[2] = new VertexPositionColor(new Vector3(-0.5f, 0.5f, 0.5f), Color.Blue);
            vertices[3] = new VertexPositionColor(new Vector3(0.5f, 0.5f, -0.5f), Color.Blue);

            vertices[4] = new VertexPositionColor(new Vector3(-0.5f, -0.5f, -0.5f), Color.Blue);
            vertices[5] = new VertexPositionColor(new Vector3(0.5f, -0.5f, 0.5f), Color.Blue);
            vertices[6] = new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0.5f), Color.Blue);
            vertices[7] = new VertexPositionColor(new Vector3(0.5f, -0.5f, -0.5f), Color.Blue);
            VertexDeclaration dd = VertexPositionColor.VertexDeclaration;
            _vertices = new VertexBuffer(device, dd, 8, BufferUsage.WriteOnly);
            _vertices.SetData<VertexPositionColor>(vertices);

        }
        public Model()
        {
           
        }
        public void Render(GraphicsDevice device, float dT, Matrix World, BasicEffect fx)
        {
            if (this._indices == null)
                Prepare(device);
            fx.World = World;
            fx.VertexColorEnabled = true;
            fx.CurrentTechnique.Passes[0].Apply();
            device.SetVertexBuffer(_vertices);
            device.Indices = _indices;
            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 12);

        }

    }
}
