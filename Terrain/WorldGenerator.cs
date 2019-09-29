using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terrain.WorldGen;

namespace Terrain
{
    class WorldGenerator
    {
        private TerrainVertex[] _vertices;
        private int[] _indices;
        private int[] _indices2;
        private int BlockSize;
        public float WaterHeight = 80f;
        public int Seed;
        public WorldGenerator(int BlockSize,int Seed=4)
        {
            this.Seed = Seed;
            this.BlockSize = BlockSize;
        }
        public void CreateGrid()
        {
            //width +1_vertices + 2_vertices padding all sides for lighting
            _vertices = new TerrainVertex[(BlockSize + 1 + 2) * (BlockSize + 1 + 2)];
        }
        public void SetHeights(int X, int Y)
        {
            for (int x = 0; x < (BlockSize + 1+2); x++)
                for (int y = 0; y < (BlockSize  +1+2); y++)
                {
                    
                    TerrainVertex v = GainVertex(x-1, y-1, X, Y);
                    _vertices[(x) + (y * (BlockSize + 1 + 2))] = v;
                }
        }
        public void SetColours()
        {

        }
        public void CalculateNormals()
        {
            for (int i = 0; i < _indices2.Length / 3; i++)
            {
                int index3 = _indices2[i * 3 + 2];
                int index1 = _indices2[i * 3];
                int index2 = _indices2[i * 3 + 1];

                Vector3 side1 =_vertices[index1].Position -_vertices[index3].Position;
                Vector3 side2 =_vertices[index1].Position -_vertices[index2].Position;
                Vector3 normal = Vector3.Cross(side1, side2);
                
               _vertices[index1].Normal += normal;
               _vertices[index2].Normal += normal;
               _vertices[index3].Normal += normal;
            }
        }
        public void CreateIndices()
        {
            _indices = new int[(BlockSize) * (BlockSize) * 6];
            _indices2 = new int[(BlockSize+2) * (BlockSize+2) * 6];
            int counter = 0;
            for (int y = 0; y < (BlockSize); y++)
            {
                for (int x = 0; x < (BlockSize); x++)
                {

                    int lowerLeft = x + (y + 1) * (BlockSize + 1);
                    int lowerRight = (x + 1) + (y + 1) * (BlockSize + 1);
                    int topLeft = x + y * (BlockSize + 1);
                    int topRight = (x + 1) + y * (BlockSize + 1);

                    _indices[counter++] = topLeft;
                    _indices[counter++] = lowerRight;
                    _indices[counter++] = lowerLeft;

                    _indices[counter++] = topLeft;
                    _indices[counter++] = topRight;
                    _indices[counter++] = lowerRight;
                }
            }
            counter = 0;
            for (int y = 0; y < (BlockSize+2); y++)
            {
                for (int x = 0; x < (BlockSize+2); x++)
                {

                    int lowerLeft = x + (y + 1) * (BlockSize + 1+2);
                    int lowerRight = (x + 1) + (y + 1) * (BlockSize + 1+2);
                    int topLeft = x + y * (BlockSize + 1 + 2);
                    int topRight = (x + 1) + y * (BlockSize + 1 + 2);

                    _indices2[counter++] = topLeft;
                    _indices2[counter++] = lowerRight;
                    _indices2[counter++] = lowerLeft;
                            
                    _indices2[counter++] = topLeft;
                    _indices2[counter++] = topRight;
                    _indices2[counter++] = lowerRight;
                }
            }
        }
        public void FinalizeGrid()
        {

        }
        public Unit GenerateBlock(int X, int Y)
        {
            Unit block = new Unit();
            block.X = X;
            block.Y = Y;
            CreateGrid();
            SetHeights(X,Y);
            CreateIndices();
            CalculateNormals();
            block.indices = _indices;
            block.vertices = new TerrainVertex[(BlockSize + 1 ) * (BlockSize + 1 )];
            block.heightmap = new float[BlockSize+1, BlockSize+1];
            TerrainVertex current;
            int x, y;
            for (x = 0; x < BlockSize+1; x++)
                for (y = 0; y < BlockSize+1; y++)
                {
                    current = _vertices[(x + 1) + ((y + 1) * (BlockSize + 1 + 2))];
                    block.vertices[x + y * (BlockSize+1)] = current;
                    block.heightmap[x, y] = current.Position.Y;
                   // block.vertices[x + y * BlockSize].Position.X = x;
                    //block.vertices[x + y * BlockSize].Position.Z = y;

                }
           // Console.Write("^00A000 Generated block at " + X.ToString() + "," + Y.ToString() + ".");
            return block;
        }

        TerrainVertex GainVertex(int X, int Y, int DX, int DY, int Seed = 0)
        {
            TerrainVertex v = new TerrainVertex();

            float One32nd = 1.0f / 64.0f;
            v.Position.X = X;
            v.Position.Z = Y;
            int MapX = X + DX * (BlockSize);
            int MapY = Y + DY * (BlockSize);
            // Z = 0;
            // v.Position.Y= noise.PerlinNoise3F(MapX, MapY, Z, 1, One32nd / 1, One32nd / 1, One32nd / 1);
            //*
            //  v.Position.Y *= 10;
            //v.Position.Y += 6;
            // v.Position.Y *= 10;
            float to256 = 1 / 256;
            float H = 0.0f;
            Simplex.Seed = this.Seed;
            MapY += 65535;
            MapX += 65535;
            //land/water scaling
            H = Simplex.CalcPixel2D(MapX, MapY, 1f/16384f) - 35f;
            //values from -35 to 220ish.
            H += (Simplex.CalcPixel2D(MapX, MapY, 1f / 256) / 16);
            //+-16 variation, values -51..246
            float Hills = Simplex.CalcPixel2D(MapX, MapY, 1f / 256) - 128;
            Hills /= 16; 
            //+-8 noise
            //*/
            v.Position.Y = H;
            float Temp = Simplex.CalcPixel2D(MapX, MapY, 1f / 4024f);
            //0 to 255 temp
            int Blend1 = Math.Min((int)(Temp * 2), 160);
            int Blend2 = Math.Min((510 - (int)(Temp * 2)), 200);

            Color grass = new Color(Blend1, Blend2, 0);
            Color sea = new Color(0, 50, 255);
            Color Snow = new Color(245, 245, 255);
            Color sand = new Color(200, 200, 100);
            Color sand2 = new Color(150, 150, 50);
            sea = sand;
            v.Color = grass;
            // v.Color = 
            float BeachRange = 12;
            float SnowRange = 185;
            float GroundBaseline = WaterHeight + BeachRange;

            if (H < WaterHeight)
            {

                v.MultiTexData.Z = 1;
                v.Color = sea;
            }
            if (H > WaterHeight && H <GroundBaseline)
            {
                float dif = H - WaterHeight;
                dif /= BeachRange;
                dif = (float)Math.Pow(dif, 7.3);
                v.Position.Y = WaterHeight + dif*BeachRange;
                v.Color = sand;
                v.MultiTexData.Z = 1;
            }

            if (H > GroundBaseline)
            {
                unchecked
                { 
                Simplex.Seed = Simplex.Seed ^ (int)0xFFFFFFFF;
              }
                //hill horizontal scale
                float ground = Simplex.CalcPixel2D(MapX+1112, MapY+13123, 0.0006125f/1f);
                float beachdist= H - GroundBaseline;
                float evenness = 16f;
                ground /= 256f;
                ground = (float)(Math.Pow(ground, 4.4));
                ground *= 256f;
                beachdist /= evenness;
                beachdist = MathHelper.Clamp(beachdist, 0.0f, 1.0f);
                float finalground= GroundBaseline + ground * beachdist + Hills * beachdist;

               
                v.Position.Y = finalground;

                v.MultiTexData.Z = (float)Math.Pow((float)MathHelper.Clamp((((Temp / 2f) - finalground + 76f) / 100f), 0f, 1f),3f) ;
                if(v.MultiTexData.Z<0.2f)
                {
                    v.MultiTexData.Z = 0f;
                }
                if (finalground > SnowRange)
                {
                    v.Color = Snow;
                    v.MultiTexData.Z = 0.20f;
                }

            }
            //  v.Color = new Color(Math.Min(255, (int)H), 0, 0);
            v.TextureCoordinate.X = X/4f;
            v.TextureCoordinate.Y = Y/4f;
            v.Normal = new Vector3(0, 0, 0);
           // v.Position.Y = 6;
            return v;
        }
    }
}
