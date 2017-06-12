using System;
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
        public List<Unit> Blocks;
        public Effect TerrainEffect;
        public int BlockSize;
        public Queue<Vector2> Queue;
        public Thread QThread;
        private WorldGenerator WorldGenerator;
        private VertexBuffer _water;
        public float WaterHeight;
        /// <summary>
        /// Processes the block queue
        /// </summary>
        public void ProcessQueue()
        {
            //I am not sure why the function never exits, however, this is the intended behaviour.
            while (Queue.Count > 0)
            {
                Vector2 b = this.Queue.Dequeue();
                // Unit blk = WorldLoader.Load((int)b.X, (int)b.Y);
                // blk = WorldGenerator.GenerateBlock((int)b.X, (int)b.Y, 64);
                Unit blk=null;
                if (blk == null)
                {
                    blk = WorldGenerator.GenerateBlock((int)b.X, (int)b.Y);
                    //Volatile.Console.Write("^00FF00 Generated " + ((int)b.X).ToString() + "." + ((int)b.Y).ToString());
                }
                else
                {
                    //  Volatile.Console.Write("^00FF00 Loaded " + ((int)b.X).ToString() + "." + ((int)b.Y).ToString());
                }
                this.Blocks.Add(blk);


            }


        }
        public bool BlockLoaded(int X, int Y)
        {
            bool found = false;
            foreach (Unit blk in this.Blocks)
            {
                if (blk.X == X && blk.Y == Y)
                {
                    return true;

                }

            }
            return found;

        }

        public void BorderEvent(int X, int Y)
        {
            // Utility.Trace(fixedX.ToString() + "," + fixedY.ToString());
            int rd = 8;
            for (int x = X - rd; x < X + rd + 1; x++)
            {

                for (int y = Y - rd; y < Y + rd + 1; y++)
                {
                    if (!this.BlockLoaded(x, y))
                    {
                        this.Queue.Enqueue(new Vector2(x, y));
                    }


                }
                List<Unit> tmp = new List<Unit>();
                foreach (Unit blk in this.Blocks)
                {
                    if (Math.Abs(blk.X - X) > rd || Math.Abs(blk.Y - Y) > rd)
                    {
                        tmp.Add(blk);
                    }

                }
                foreach (Unit blk in tmp)
                {
                    //WorldLoader.Save(blk, blk.X, blk.Y);
                    this.Blocks.Remove(blk);

                }

            }

        }

        public Terrain(int BlockSize)
        {
            this.BlockSize = BlockSize;
           
            this.Blocks = new List<Unit>();
            this.Queue = new Queue<Vector2>();
            WorldGenerator = new WorldGenerator(BlockSize);
            this.WaterHeight = WorldGenerator.WaterHeight;
            this.Blocks.Add(WorldGenerator.GenerateBlock(0, 0));
            this.Blocks.Add(WorldGenerator.GenerateBlock(0, 1));
            this.Blocks.Add(WorldGenerator.GenerateBlock(1, 0));
            this.Blocks.Add(WorldGenerator.GenerateBlock(1, 1));
            //*//
            this.Blocks.Add(WorldGenerator.GenerateBlock(0, -1));
            this.Blocks.Add(WorldGenerator.GenerateBlock(-1, 0));
            this.Blocks.Add(WorldGenerator.GenerateBlock(-1, -1));

            this.Blocks.Add(WorldGenerator.GenerateBlock(1, -1));
            this.Blocks.Add(WorldGenerator.GenerateBlock(-1, 1));
            //*/
        }

        public void DrawWater(GraphicsDevice device, float dT)
        {
            if (_water == null)
                SetUpWaterVertices(device);
            Unit[] cpy;
            lock (_water)
            {
                cpy = new Unit[this.Blocks.Count];
                Blocks.CopyTo(0, cpy, 0, cpy.Length);
            }
            for (int i = 0; i < cpy.Length; i++)
            {
                Unit block = cpy[i];
                if (block == null)
                    continue;
              
                Matrix worldMatrix = Matrix.CreateTranslation(block.X * BlockSize, 0, block.Y * BlockSize);


                Vector3 Light = new Vector3(0.0f, -1.0f, 0.0f);
                Vector3.Transform(Light, Matrix.CreateRotationX(MathHelper.ToRadians(30)));
                TerrainEffect.Parameters["xWorld"].SetValue(worldMatrix);
                TerrainEffect.Parameters["xLightDirection"].SetValue(Light);
                TerrainEffect.CurrentTechnique.Passes[0].Apply();
                device.SetVertexBuffer(_water);
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            }



        }

        public void Render(GraphicsDevice device, float dT)
        {
            

            Unit[] cpy = new Unit[this.Blocks.Count];
            Blocks.CopyTo(0, cpy, 0, cpy.Length);
            for(int i=0;i<cpy.Length;i++)
            {
                Unit block = cpy[i];
                if (block == null)
                    continue;
                TerrainEffect.CurrentTechnique = TerrainEffect.Techniques["TexturedTinted"];

                Matrix worldMatrix = Matrix.CreateTranslation(block.X*BlockSize, 0, block.Y * BlockSize);


                Vector3 Light = new Vector3(0.0f, -1.0f, 0.0f);
                Vector3.Transform(Light, Matrix.CreateRotationX(MathHelper.ToRadians(30)));
                TerrainEffect.Parameters["xWorld"].SetValue(worldMatrix);
                TerrainEffect.Parameters["xLightDirection"].SetValue(Light);
                 TerrainEffect.CurrentTechnique.Passes[0].Apply(); 
                block.Render(device, dT);
            }

        }

        public void SetUpWaterVertices(GraphicsDevice device)
        {
            VertexPositionTexture[] waterVertices = new VertexPositionTexture[6];
            float WH = WorldGenerator.WaterHeight - 0.2f;
            waterVertices[0] = new VertexPositionTexture(new Vector3(0, WH, 0), new Vector2(0, 0));//10
            waterVertices[1] = new VertexPositionTexture(new Vector3(BlockSize, WH, BlockSize), new Vector2(1, 1)); //01
            waterVertices[2] = new VertexPositionTexture(new Vector3(0, WH, BlockSize), new Vector2(0, 1));//00

            waterVertices[3] = new VertexPositionTexture(new Vector3(0, WH, 0), new Vector2(0, 0));//01
            waterVertices[4] = new VertexPositionTexture(new Vector3(BlockSize, WH, 0), new Vector2(1, 0));//11
            waterVertices[5] = new VertexPositionTexture(new Vector3(BlockSize, WH, BlockSize), new Vector2(1, 1));//01

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
