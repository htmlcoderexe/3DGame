using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameModel.TestParts
{
    public class PartLight : ModelPart
    {
        public float Width = 1.0f;
        public float Height = 1.0f;
        public Vector3 Direction;
        public PartLight(Color Color,Vector3 Bias= new Vector3())
        {
            _vertices = new ModelVertex[4];
            _vertices[0] = new ModelVertex(Bias, Color);
            _vertices[0].TextureCoordinate = new Vector2(0, 0);
            _vertices[1] = new ModelVertex(Bias, Color);
            _vertices[1].TextureCoordinate = new Vector2(0, 1);
            _vertices[2] = new ModelVertex(Bias, Color);
            _vertices[2].TextureCoordinate = new Vector2(1, 1);
            _vertices[3] = new ModelVertex(Bias, Color);
            _vertices[3].TextureCoordinate = new Vector2(1, 0);

            _indices = new int[] { 2,0,3,2,1,0};


            SetIndices(_indices);
            SetVertices(_vertices);
        }
        public override void Render(GraphicsDevice device, float dT, Matrix World, Effect fx,bool Alpha)
        {
            if (!Alpha)
                return;
            Matrix m = Dislocation;
            Matrix a = Animation == null ? Matrix.Identity : this.Animation.GetTransform(dT);
            //a = Matrix.Identity;
            World = m * World;
            if (this.Children != null)
                foreach (ModelPart c in this.Children)
                {
                    c.Render(device, dT, World, fx,Alpha);
                }
            //fx.Parameters["xWorld2"].SetValue(Matrix.CreateTranslation(0, Y, 0));
            fx.Parameters["xWorld"].SetValue(World);
            fx.Parameters["xBone"].SetValue(a);
            fx.Parameters["xOrigin"].SetValue(Matrix.Identity);
            fx.Parameters["xPointSpriteSize"].SetValue(this.Width);
            fx.Parameters["xRayLength"].SetValue(this.Height);
            Vector3 raydir =this.Direction!=Vector3.Zero? this.Direction : Vector3.Up;
            fx.Parameters["xRayDirection"].SetValue(raydir);
            fx.CurrentTechnique = fx.Techniques["PointSprites"];
            fx.CurrentTechnique.Passes[0].Apply();
            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, IndexOffset, (int)(IndexLength / 3.0f));
            device = null;
            fx = null;
        }
    }
}
