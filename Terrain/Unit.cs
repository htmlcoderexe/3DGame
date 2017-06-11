using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Terrain
{
    public class Unit
    {
        public int X;
        public int Y;
        public TerrainVertex[] vertices;
        public int[] indices;
        public VertexBuffer buffer;
        public IndexBuffer ibuffer;
        public void Render(GraphicsDevice device, float dT)
        {
            //set buffer if it doesn't exist
            if (this.buffer == null)
            {
                buffer = new VertexBuffer(device, TerrainVertex.VertexDeclaration, this.vertices.Length, BufferUsage.WriteOnly);
                buffer.SetData(this.vertices);
                ibuffer = new IndexBuffer(device, typeof(int), this.indices.Length, BufferUsage.WriteOnly);
                ibuffer.SetData(this.indices);
            }
            device.Indices = ibuffer;
            device.SetVertexBuffer(buffer);

            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, this.indices.Length / 3);
            
        }
        public Unit()
        {

        }
    }
}
