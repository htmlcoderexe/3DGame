using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameModel
{
    public class Model : ModelPart, IDisposable
    {
        public static Dictionary<string, Texture2D> TexturePool;

        public Dictionary<string, Dictionary<string, PartAnimation>> Choreo;
        private VertexBuffer _VB;
        private IndexBuffer _IB;
        private Dictionary<string, List<ModelPart>> AnimationMapping;
        private float maxlen=0;
        public Vector3 Offset;
        public bool Dirty = true;
        public float CurrentAnimationLength {
            get
                {
                return maxlen;
                }
            }
        public string ChoreoName;
        /// <summary>
        /// Bakes the model and creates the necessary index/vertex/animation buffers
        /// </summary>
        /// <param name="device"></param>
        private void Prepare(GraphicsDevice device)
        {
            //get index count and vertex counts
            Vector2 TotalSizes = this.Children[0].GetCounts();
            //init the index
            AnimationMapping = new Dictionary<string, List<ModelPart>>();
            //init buffers
            _IB = new IndexBuffer(device, IndexElementSize.ThirtyTwoBits, (int)TotalSizes.Y, BufferUsage.WriteOnly);
            _VB = new VertexBuffer(device, ModelVertex.VertexDeclaration, (int)TotalSizes.X, BufferUsage.WriteOnly);
            int[] indices = new int[(int)TotalSizes.Y];
            ModelVertex[] vertices = new ModelVertex[(int)TotalSizes.X];
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
                //obtain indices and vertices with proper offsetting
                p.GetIndices().CopyTo(indices, cIo);
                p.GetVertices().CopyTo(vertices, cVo);
                //update offsets
                cIo += Icount;
                cVo += Vcount;
                //add the part to animation mapping
                if (p.Title == null)
                    continue;
                if (!AnimationMapping.ContainsKey(p.Title)) //create key if needed
                    AnimationMapping.Add(p.Title, new List<ModelPart>() {  });
                AnimationMapping[p.Title].Add(p);
            }

            //set buffers
            _IB.SetData<int>(indices);
            _VB.SetData<ModelVertex>(vertices);

            this.Dirty = false;

        }

        public ModelPart FindPart(string name)
        {
            List<ModelPart> Parts = this.Children[0].GetFlatList();
            foreach (ModelPart p in Parts)
            {

                if (p.Title == name)
                    return p;
            }
            return null;
        }

        public void RebuildSkeleton()
        {
            AnimationMapping = new Dictionary<string, List<ModelPart>>();
            List<ModelPart> Parts = this.Children[0].GetFlatList();
            foreach (ModelPart p in Parts)
            {
                
                if (p.Title == null)
                    continue;
                if (!AnimationMapping.ContainsKey(p.Title)) //create key if needed
                    AnimationMapping.Add(p.Title, new List<ModelPart>() { });
                AnimationMapping[p.Title].Add(p);
            }
        }

        public void ApplyAnimation(string Name)
        {
            if(this.Choreo!=null)
                ApplyAnimation(Name, this.Choreo);
        }

        public void ClearAnimation()
        {

            List<ModelPart> Parts = this.Children[0].GetFlatList();
            foreach (ModelPart p in Parts)
            {
                p.Animation = null;
            }
        }

        public void ApplyAnimation(string Name, Dictionary<string, Dictionary<string,PartAnimation>> Choreo)
        {
            if (AnimationMapping == null)
                return;
            if (!Choreo.ContainsKey(Name))
                return;
            ClearAnimation();
            Dictionary<string, PartAnimation> Movement = Choreo[Name];
            List<ModelPart> tmppartlist;
                maxlen = 0;
            foreach(KeyValuePair<string, PartAnimation> Part in Movement)
            {
                if (!AnimationMapping.ContainsKey(Part.Key))
                    continue;
                tmppartlist = AnimationMapping[Part.Key];
                foreach (ModelPart p in tmppartlist)
                {
                    if (Part.Value.Length > maxlen)
                        maxlen = Part.Value.Length;
                    p.Animation = Part.Value;
                }
                    
            }
        }

        public void Clear()
        {
            this.Children.Clear();
        }
        
        public Model(ModelPart Root)
        {
            this.Children = new List<ModelPart>
            {
                Root
            };
        }

        public Model()
        {
            this.Children = new List<ModelPart>();
            ModelPart Root = new ModelPart();

            ModelVertex[] vertices = new ModelVertex[8];
            vertices[0] = new ModelVertex(new Vector3(-1.5f,  0.5f, -0.5f), Color.DarkGray );
            vertices[1] = new ModelVertex(new Vector3( 0.5f,  0.2f,  0.2f), Color.DarkGray );
            vertices[2] = new ModelVertex(new Vector3(-1.5f,  0.5f,  0.5f), Color.DarkGray );
            vertices[3] = new ModelVertex(new Vector3( 0.5f,  0.2f, -0.2f), Color.DarkGray );
                                                                                         
            vertices[4] = new ModelVertex(new Vector3(-1.5f, -0.5f, -0.5f), Color.DarkGray );
            vertices[5] = new ModelVertex(new Vector3( 0.5f, -0.2f,  0.2f), Color.DarkGray );
            vertices[6] = new ModelVertex(new Vector3(-1.5f, -0.5f,  0.5f), Color.DarkGray );
            vertices[7] = new ModelVertex(new Vector3( 0.5f, -0.2f, -0.2f), Color.DarkGray );
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
            ModelPart eye;
            leg = new TestParts.PartBugLeg();
            eye = new TestParts.PartLight(Color.Blue);
            eye.BoneFactor = 0.33f;
            
            leg.Append(eye, Matrix.CreateTranslation(new Vector3(0.2f, 0.8f, 0.0f)));
            Root.Append(leg, Matrix.CreateRotationY((float)Math.PI * 0.3f) * Matrix.CreateTranslation(new Vector3(0.4f, 0, -0.5f)));
            leg = new TestParts.PartBugLeg();
            leg.Animation.SetPhase(0.5f);
            Root.Append(leg, Matrix.CreateRotationY((float)Math.PI * 0.5f) * Matrix.CreateTranslation(new Vector3(0, 0, -0.5f)));
            leg = new TestParts.PartBugLeg();
            Root.Append(leg, Matrix.CreateRotationY((float)Math.PI * 0.8f) * Matrix.CreateTranslation(new Vector3(-0.4f, 0, -0.5f)));

            leg = new TestParts.PartBugLeg(true);
            leg.Animation.SetPhase(0.5f);
            eye = new TestParts.PartLight(Color.Blue);
            eye.BoneFactor = 0.33f;
            leg.Append(eye, Matrix.CreateTranslation(new Vector3(0.2f, 0.8f, 0.0f)));

            Root.Append(leg, Matrix.CreateRotationY((float)Math.PI * 1.8f) * Matrix.CreateTranslation(new Vector3(0.4f, 0, 0.5f)));
            leg = new TestParts.PartBugLeg(true);
            Root.Append(leg, Matrix.CreateRotationY((float)Math.PI * 1.5f) * Matrix.CreateTranslation(new Vector3(0, 0, 0.5f)));
            leg = new TestParts.PartBugLeg(true);
            leg.Animation.SetPhase(0.5f);
            Root.Append(leg, Matrix.CreateRotationY((float)Math.PI * 1.3f) * Matrix.CreateTranslation(new Vector3(-0.4f, 0, 0.5f)));
         
            this.Children.Add(Root);

        }

        public override void Render(GraphicsDevice device, float dT, Matrix World, Effect fx, bool Alpha)
        {
            if (this._IB == null || this.Dirty)
                Prepare(device);
            device.SetVertexBuffer(_VB);
            device.Indices = _IB;
            foreach(ModelPart c in this.Children)
            {
                c.Render(device, dT, World, fx,Alpha);
            }

        }

        public override object Clone()
        {
            Model m = (Model)base.Clone();
            m._IB = this._IB;
            m._VB = this._VB;
            return m;
        }

        public void Dispose()
        {
            this._IB?.Dispose();
            this._VB?.Dispose();
        }
    }
}
