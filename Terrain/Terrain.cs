using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terrain
{
    public class Terrain
    {
        public ConcurrentDictionary<int,Unit> Blocks;
        public Effect TerrainEffect;
        public int BlockSize;
        public Queue<Vector2> Queue;
        public Thread QThread;
        //ublic WorldGen.WorldGenerator WorldGenerator;
        private VertexBuffer _water;
        public float WaterHeight;
        public const float TileMapUVStep = 0.0625f;
        public const int TileAmount = 16;
        public const float TileMapUVTexelOffset = 0.00025f;

        int LastX;
        int LastY;

        public int RenderDistance { get; set; }
        public bool BlockLoaded(int X, int Y)
        {
            bool found = false;
            foreach (KeyValuePair<int,Unit> bv in this.Blocks)
            {
                Unit blk = bv.Value;
                if (blk.X == X && blk.Y == Y)
                {
                    return true;

                }

            }
            return found;

        }
        public Unit GetBlock(int X, int Y)
        {
            Unit blk; 
            foreach(KeyValuePair<int,Unit> bv in this.Blocks)
            {
                    blk = bv.Value;
                if (blk == null)
                    continue;
                if (blk.X == X && blk.Y == Y)
                {
                    return blk;

                }

            }
            
            return null;

        }
        public float GetHeight(Vector3 position, Vector2 Block)
        {
            Unit blk = GetBlock((int)Block.X, (int)Block.Y);
            if (blk == null)
                return -1;
            return blk.GetHeight(position.X, position.Z);
        }
        public string GetPlaceName(Vector2 Block)
        {
            Unit blk = GetBlock((int)Block.X, (int)Block.Y);
            if (blk == null)
                return "unknown";
            return blk.Name;
        }
        

        public Terrain(int BlockSize, int Seed=4)
        {
            this.BlockSize = BlockSize;

            this.Blocks = new ConcurrentDictionary<int,Unit>();
            this.Queue = new Queue<Vector2>();
           // WorldGenerator = new WorldGenerator(BlockSize,Seed);
          //  this.WaterHeight = WorldGenerator.WaterHeight;
            lock (this.Queue)
            {
                
            }
            //*/
        }

        public void DrawWater(GraphicsDevice device, float dT, Vector2 Reference)
        {
            if (_water == null)
                SetUpWaterVertices(device);
            /*
            foreach(KeyValuePair<int,Unit> bv in this.Blocks)
            {

                Unit block = bv.Value;
                if (block == null)
                    continue;
              
                Matrix worldMatrix = Matrix.CreateTranslation((block.X - Reference.X) * BlockSize, 0, (block.Y - Reference.Y) * BlockSize);


                Vector3 Light = new Vector3(0.0f, -1.0f, 0.0f);
                Vector3.Transform(Light, Matrix.CreateRotationX(MathHelper.ToRadians(30)));
                
            }
            //*/
            TerrainEffect.Parameters["xWorld"].SetValue(Matrix.Identity);
           // TerrainEffect.Parameters["xLightDirection"].SetValue(Light);
            TerrainEffect.CurrentTechnique.Passes[0].Apply();
            device.SetVertexBuffer(_water);
            device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);


        }

        public void Render(GraphicsDevice device, float dT, Vector2 Reference, BoundingFrustum F)
        {

            foreach(KeyValuePair<int,Unit> bv in this.Blocks)
            {
                Unit block = bv.Value;
                if (block == null)
                    continue;
                BoundingBox bb = new BoundingBox(new Vector3((block.X - Reference.X) * BlockSize - 10, -1255, (block.Y - Reference.Y) * BlockSize - 10), new Vector3(((block.X - Reference.X) + 1) * BlockSize + 10, 1255, ((block.Y - Reference.Y) + 1) * BlockSize + 10));
                if (!F.Intersects(bb))
                    continue;
                TerrainEffect.CurrentTechnique = TerrainEffect.Techniques["TexturedTinted"];

                Matrix worldMatrix = Matrix.CreateTranslation((block.X-Reference.X)*BlockSize, 0, (block.Y - Reference.Y) * BlockSize);


                Vector3 Light = new Vector3(0.0f, -1.0f, 0.0f);
                Vector3.Transform(Light, Matrix.CreateRotationX(MathHelper.ToRadians(60)));
                TerrainEffect.Parameters["xWorld"].SetValue(worldMatrix);
                TerrainEffect.Parameters["xLightDirection"].SetValue(Light);
                 TerrainEffect.CurrentTechnique.Passes[0].Apply(); 
                block.Render(device, dT);
            }

        }

        public void SetUpWaterVertices(GraphicsDevice device)
        {
            VertexPositionTexture[] waterVertices = new VertexPositionTexture[6];
            float WH = WaterHeight-0.2f;
            waterVertices[0] = new VertexPositionTexture(new Vector3(BlockSize * -10, WH, BlockSize * -10), new Vector2(0, 0));//10
            waterVertices[1] = new VertexPositionTexture(new Vector3(BlockSize * 10, WH, BlockSize * 10), new Vector2(1, 1)); //01
            waterVertices[2] = new VertexPositionTexture(new Vector3(BlockSize * -10, WH, BlockSize * 10), new Vector2(0, 1));//00
                
            waterVertices[3] = new VertexPositionTexture(new Vector3(BlockSize * -10, WH, BlockSize * -10), new Vector2(0, 0));//01
            waterVertices[4] = new VertexPositionTexture(new Vector3(BlockSize * 10, WH, BlockSize * -10), new Vector2(1, 0));//11
            waterVertices[5] = new VertexPositionTexture(new Vector3(BlockSize * 10, WH, BlockSize * 10), new Vector2(1, 1));//01

            //waterVertexBuffer = new VertexBuffer(device, waterVertices.Length * VertexPositionTexture.SizeInBytes, BufferUsage.WriteOnly);
            VertexDeclaration vertexDeclaration = VertexPositionTexture.VertexDeclaration;
            _water = new VertexBuffer(device, vertexDeclaration, waterVertices.Count(), BufferUsage.WriteOnly);
            _water.SetData(waterVertices);
        }
        public void Update(float dT)
        {

        }
    }
}
