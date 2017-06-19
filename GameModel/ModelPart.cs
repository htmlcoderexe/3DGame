using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameModel
{
    public class ModelPart
    {
        public int VertexOffset;
        public int VertexLength;
        public int IndexOffset;
        public int IndexLength;
        protected VertexPositionColor[] _vertices;
        protected int[] _indices;
        private Matrix Dislocation;
        public PartAnimation Animation;
        protected List<ModelPart> Children;
        public ModelPart()
        {
            this.Dislocation = Matrix.Identity;
        }
        public void Append(ModelPart part, Matrix transform)
        {
            part.Dislocation = transform;
            if (this.Children == null)
                this.Children = new List<ModelPart>();
            this.Children.Add(part);
        }
        public virtual void Render(GraphicsDevice device, float dT, Matrix World, BasicEffect fx)
        {
            Matrix m = Dislocation;
            Matrix a = Animation==null?Matrix.Identity:Animation.GetTransform(dT);
            //a = Matrix.Identity;
            World = a * m * World;
            if(this.Children!=null)
            foreach(ModelPart c in this.Children)
            {
                c.Render(device, dT, World, fx);
            }
            fx.World = World;
            fx.CurrentTechnique.Passes[0].Apply();
            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, IndexOffset, (int)(IndexLength/3.0f));
        }

        internal List<ModelPart> GetFlatList()
        {
            List<ModelPart> list = new List<ModelPart>();
            list.Add(this);
            if(this.Children!=null)
            {
                foreach(ModelPart c in this.Children)
                {
                    list.AddRange(c.GetFlatList());
                }
            }
            return list;
        }

        public Vector2 GetCounts()
        {
            Vector2 counts = Vector2.Zero;
            if(this.Children!=null)
            {
                foreach (ModelPart c in this.Children)
                    counts += c.GetCounts();
            }
            counts.X += this._vertices.Count();
            counts.Y += this._indices.Count();
            return counts;
        }

        public VertexPositionColor[] GetVertices()
        {
            return this._vertices;
        }

        public void SetVertices(VertexPositionColor[] vertices)
        {
            this._vertices = vertices;
            this.VertexLength = vertices.Length;
        }

        public int[] GetIndices()
        {
            int[] indices = new int[this._indices.Count()];
            for(int i=0;i<indices.Length;i++)
            {
                indices[i] = this._indices[i] + VertexOffset;
            }
            return indices;
        }
        public void SetIndices(int[] indices)
        {
            this._indices = indices;
            this.IndexLength = indices.Length;
        }
    }
}
