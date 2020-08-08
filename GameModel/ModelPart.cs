using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameModel
{
    public class ModelPart : ICloneable
    {

        public static Dictionary<string, Texture2D> Textures;
        public int VertexOffset;
        public int VertexLength;
        public int IndexOffset;
        public int IndexLength;
        public PartAnimation Animation;
        protected ModelVertex[] _vertices;
        protected int[] _indices;
        public List<ModelPart> Children;
        public Matrix Dislocation;
        public float BoneFactor;
        public string Title;
        public float Phase;
        public string TextureName;

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
        public virtual void Render(GraphicsDevice device, float Time, Matrix World, Effect fx,bool Alpha)
        {
            //get bone animation
            Matrix a = Animation==null?Matrix.Identity:this.Animation.GetTransformAt(Time);
           
            Matrix b;
            //a = Matrix.Identity;
            //if any children, render each in this part's frame
            if (this.Children!=null)
            foreach(ModelPart c in this.Children)
            {
                    //animate part's location
                    b = Matrix.Lerp(Dislocation, Dislocation * a, c.BoneFactor);
                    b = Matrix.Lerp(Matrix.Identity, a, c.BoneFactor);
                    b = b*Dislocation;
                    c.Render(device, Time, b*World, fx,Alpha);
                }
           if (Alpha)
               return;
            World = Dislocation * World;

            //fx.Parameters["xWorld2"].SetValue(Matrix.CreateTranslation(0, Y, 0));
            fx.Parameters["xWorld"].SetValue(World);
            fx.Parameters["xBone"].SetValue(a);
            fx.Parameters["xOrigin"].SetValue(Matrix.Identity);
            if (TextureName != null && TextureName != "" && Model.TexturePool != null && Model.TexturePool.ContainsKey(TextureName))
                fx.Parameters["xModelSkin"].SetValue(Model.TexturePool[TextureName]);
            fx.CurrentTechnique = fx.Techniques["GameModel"];
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

        public ModelVertex[] GetVertices()
        {
            return this._vertices;
        }

        public void SetVertices(ModelVertex[] vertices)
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

        public virtual object Clone()
        {
            ModelPart p = (ModelPart)this.MemberwiseClone();
            if (this.Animation != null)
                p.Animation = (PartAnimation)this.Animation.Clone();
            if(this.Children!=null)
            { 
                List<ModelPart> c = new List<ModelPart>();
                foreach (ModelPart part in this.Children)
                    c.Add((ModelPart)part.Clone());

                p.Children = c;
            }
            return p;
        }
    }
}
