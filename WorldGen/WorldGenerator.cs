using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terrain;
using WorldInfo;

namespace WorldGen
{
    public class WorldGenerator
    {
        Terrain.Terrain TerrainRef;
        int LastX;
        int LastY;
        private Terrain.TerrainVertex[] _vertices;
        private int[] _indices;
        private int[] _indices2;
        private int BlockSize;
        private string BlockTitle = "";
        public float WaterHeight = 80f;
        public int Seed;
        public WorldInfo.WorldMap Map;
        public WorldGenerator(int BlockSize,int Seed=4)
        {
            this.Seed = Seed;
            this.BlockSize = BlockSize;
        }
        const int RIVER_SIDE_TOP = 0;
        const int RIVER_SIDE_RIGHT = 1;
        const int RIVER_SIDE_BOTTOM = 2;
        const int RIVER_SIDE_LEFT = 3;
        const int RIVER_CORNER_TL = 0;
        const int RIVER_CORNER_TR = 1;
        const int RIVER_CORNER_BR = 2;
        const int RIVER_CORNER_BL = 3;

        #region Terrain data generation
        public void DoErosionDrop(int X, int Y, float weight,float loss)
        {
            
            int overflow = 0;
            while(overflow<1024&&weight>0f)
            {
                if (X <= 0 || X >= BlockSize + 1 || Y <= 0 || Y >= BlockSize + 1)
                    return;
                float C = _vertices[(X) + ((Y) * (BlockSize + 1 + 2))].Position.Y;
                float T = _vertices[(X) + ((Y - 1) * (BlockSize + 1 + 2))].Position.Y;
                float B = _vertices[(X) + ((Y + 1) * (BlockSize + 1 + 2))].Position.Y;
                float R = _vertices[(X + 1) + ((Y) * (BlockSize + 1 + 2))].Position.Y;
                float L = _vertices[(X - 1) + ((Y) * (BlockSize + 1 + 2))].Position.Y;
                float min = (new float[] { T, R, B, L }).Min();
                if(C<80f)
                {
                    return;
                }
                if (min > C)
                {
                    _vertices[(X) + ((Y) * (BlockSize + 1 + 2))].Position.Y += loss*2;
                    return;
                }
                _vertices[(X) + ((Y) * (BlockSize + 1 + 2))].Position.Y -= loss;
                if (min == T)
                {
                 //   _vertices[(X) + ((Y - 1) * (BlockSize + 1 + 2))].Position.Y -= loss;
                    Y--;
                }
                if (min == B)
                {
                 //   _vertices[(X) + ((Y + 1) * (BlockSize + 1 + 2))].Position.Y -= loss;
                    Y++;
                }
                if (min == R)
                {
                 //   _vertices[(X+1) + ((Y) * (BlockSize + 1 + 2))].Position.Y -= loss;
                    X++;
                }
                if (min == L)
                {
                 //   _vertices[(X-1) + ((Y) * (BlockSize + 1 + 2))].Position.Y -= loss;
                    Y++;
                }
                overflow++;
                weight-=loss;
            }
        }
        public void CreateGrid()
        {
            //width +1_vertices + 2_vertices padding all sides for lighting
            _vertices = new TerrainVertex[(BlockSize + 1 + 2) * (BlockSize + 1 + 2)];
        }
        public void SetHeights(int X, int Y)
        {
            //generate initial geometry
            for (int x = 0; x < (BlockSize + 1+2); x++)
                for (int y = 0; y < (BlockSize  +1+2); y++)
                {
                    
                    TerrainVertex v = GainVertex(x-1, y-1, X, Y);
                    _vertices[(x) + (y * (BlockSize + 1 + 2))] = v;
                }
            //rivers
            if (this.Map == null)
                return;
            try
            {
                if (Map.TileData[X, Y] == WorldMap.TileType.River)
                    DoRiver(X, Y);
                Location l = Map.Locations[Map.LocationData[X, Y]];
                BlockTitle = l.Name + " " + l.Modifier ?? "";
            }
            catch (Exception)
            {

               
            }
        }
        public void DoRiver(int X, int Y)
        {
           // return;
            bool T, B, L, R;
            T = B = L = R = false;
            //eventually check for ocean
            T = Map.TileData[X, Y - 1] == WorldMap.TileType.River|| Map.TileData[X, Y - 1] == WorldMap.TileType.Ocean;
            B = Map.TileData[X, Y + 1] == WorldMap.TileType.River|| Map.TileData[X, Y + 1] == WorldMap.TileType.Ocean;
            L = Map.TileData[X - 1, Y] == WorldMap.TileType.River|| Map.TileData[X - 1, Y] == WorldMap.TileType.Ocean;
            R = Map.TileData[X + 1, Y] == WorldMap.TileType.River|| Map.TileData[X + 1, Y] == WorldMap.TileType.Ocean;

            if(T)
            {
                if(B)
                {
                    if(R)
                    {   //TBRL
                        if(L)
                        {
                            DoRiverPond(X,Y);
                        }
                        else
                        {
                            Do3SideRiver(X, Y, RIVER_SIDE_RIGHT);
                        }
                    }
                    else
                    {   //TBrL
                        if (L)
                        {
                            Do3SideRiver(X, Y, RIVER_SIDE_LEFT);
                        }
                        else
                        {
                            DoStraightRiver(X, Y, false);
                        }
                    }
                }
                else
                {
                    if (R)
                    {   //TbRL
                        if (L)
                        {
                            Do3SideRiver(X, Y, RIVER_SIDE_TOP);
                        }
                        else
                        {
                            DoCurvedRiver(X, Y, RIVER_CORNER_TR);
                        }
                    }
                    else
                    {   //TbrL
                        if (L)
                        {
                            DoCurvedRiver(X, Y, RIVER_CORNER_TL);
                        }
                        else
                        {
                            DoRiverEnd(X, Y, RIVER_SIDE_TOP);
                        }
                    }
                }
            }
            else
            {
                if (B)
                {
                    if (R)
                    {   //tBRL
                        if (L)
                        {
                            Do3SideRiver(X, Y, RIVER_SIDE_BOTTOM);
                        }
                        else
                        {
                            DoCurvedRiver(X, Y, RIVER_CORNER_BR);
                        }
                    }
                    else
                    {   //tBrL
                        if (L)
                        {
                            DoCurvedRiver(X, Y, RIVER_CORNER_BL);
                        }
                        else
                        {
                            DoRiverEnd(X, Y, RIVER_SIDE_BOTTOM);
                        }
                    }
                }
                else
                {   //tbRL
                    if (R)
                    {
                        if (L)
                        {
                            DoStraightRiver(X, Y, true);
                        }
                        else
                        {
                            DoRiverEnd(X, Y, RIVER_SIDE_RIGHT);
                        }
                    }
                    else
                    {   //tbrL
                        if (L)
                        {
                            DoRiverEnd(X, Y, RIVER_SIDE_LEFT);
                        }
                        else
                        {
                            //not really supposed to happen!!!
                        }
                    }
                }
            }
        }
        public void DoStraightRiver(int X, int Y, bool Horizontal)
        {

            float pow = 2f;
            float offset = 0;
            float rivermin = 0.1f;
            float rivermax = 0.6f;
            //how deep the river carves
            float strength = 16f;
            float rivernoisescale = 1f / 64f;
            float meanderscale = 1f / 4f;
            //river midpoint should be in the middle of block
            float mid = ((float)BlockSize) / 2f;
            //river width randomness - uses noise to align to tiles and smooth tranition
            //make this separate function returning vector2
            float widthA = Horizontal ? ((Simplex.CalcPixel2D(X - 1, Y, rivernoisescale) + Simplex.CalcPixel2D(X, Y, rivernoisescale)) / 512f) : (((Simplex.CalcPixel2D(X, Y - 1, rivernoisescale) + Simplex.CalcPixel2D(X, Y, rivernoisescale)) / 512f));
            float widthB = Horizontal ? ((Simplex.CalcPixel2D(X, Y, rivernoisescale) + Simplex.CalcPixel2D(X + 1, Y, rivernoisescale)) / 512f) : (((Simplex.CalcPixel2D(X, Y, rivernoisescale) + Simplex.CalcPixel2D(X, Y + 1, rivernoisescale)) / 512f));

            float slideA = Horizontal ? ((Simplex.CalcPixel2D(X - 1, Y, meanderscale) + Simplex.CalcPixel2D(X, Y, meanderscale)) / 512f) : (((Simplex.CalcPixel2D(X, Y - 1, meanderscale) + Simplex.CalcPixel2D(X, Y, meanderscale)) / 512f));
            float slideB = Horizontal ? ((Simplex.CalcPixel2D(X, Y, meanderscale) + Simplex.CalcPixel2D(X + 1, Y, meanderscale)) / 512f) : (((Simplex.CalcPixel2D(X, Y, meanderscale) + Simplex.CalcPixel2D(X, Y + 1, meanderscale)) / 512f));
            //this makes these values range from -0.5f to 0.5f
            slideA -= 0.5f;
            slideB -= 0.5f;
            widthA = MathHelper.Clamp(widthA, rivermin, rivermax);
            widthB = MathHelper.Clamp(widthB, rivermin, rivermax);
            /*
            if(Horizontal)
            {
                BlockTitle = "LW:" + widthA.ToString("F2") + " RW:" + widthB.ToString("F2") + " LS: " + slideA.ToString("F2") + " RS:" + slideB.ToString("F2");
            }
            else
            {
                BlockTitle = "TW:" + widthA.ToString("F2") + " BW:" + widthB.ToString("F2") + " TS: " + slideA.ToString("F2") + " BS:" + slideB.ToString("F2");

            }
            //*/
            for (int x = 0; x < (BlockSize + 1 + (Horizontal ? 2 : 0)); x++)
                for (int y = 0; y < (BlockSize + 1 + (Horizontal ? 0 : 2)); y++)
                {
                    float M = (Horizontal ? y : x);
                    float W = (Horizontal ? x : y);
                    // offset = ((float)Math.Pow((((float)M - mid) / strength),pow) * -1f) + (float)Math.Pow(mid / strength, pow) * MathHelper.Lerp(widthA, widthB, Horizontal ? (float)(x)/(float)(BlockSize+1+2) : (float)(y) / (float)(BlockSize + 1 + 2)); ;
                    // TerrainVertex v = GainVertex(x - 1, y - 1, X, Y);
                    offset = GainDepth(M,W, widthA, widthB, slideA, slideB,strength);
                    offset = MathHelper.Clamp(offset, 0, 999f);
                    if (offset > 0)
                        _vertices[(x + (Horizontal ? 0 : 1) + ((y + (Horizontal ? 1 : 0)) * (BlockSize + 1 + 2)))].MultiTexData.Z = 1f;
                    _vertices[(x+(Horizontal?0:1) + ((y + (Horizontal ? 1 : 0)) * (BlockSize + 1 + 2)))].Position -= new Vector3(0, offset, 0) ;
                }
        }
        public float GainDepth(float M,float W, float widthA, float widthB, float slideA, float slideB,float depth)
        {
            float result = 0f;
            float mid = 0.5f;
            float width = 0f;
            //+1 because a tile mesh needs one more vertex than there are tiles (fenceposts)
            //+2 because we add a tile wide edge on all sides when generating the block so normal data is good on edges. The border is later discareded.
            float blockwidth = (float)BlockSize + 1f;
            float nM = M / blockwidth;
            float nW = W / blockwidth;
            float modulatedwidth = MathHelper.Lerp(widthA, widthB, nW);
            float modulatedslide = MathHelper.Lerp(slideA, slideB, nW);
            modulatedslide = MathHelper.Clamp(modulatedslide, modulatedwidth - 1f, 1f - modulatedwidth);

            float wiggle = (float)Math.Sin(nW * MathHelper.Pi * 2f) * 0.14f;
            modulatedslide += wiggle;
            //modulatedslide = 0f;
            //modulatedwidth = 0.5f;
            
            float edgestart = 0.5f*(1f - modulatedwidth + modulatedslide);
            edgestart = MathHelper.Clamp(edgestart, 0f, 1f);
            float edgeend = 0.5f*(1f + modulatedwidth + modulatedwidth);

            float shrinkfactor = 1f;// modulatedwidth;

            float param = 0f;
            param = nM  -(0.5f-modulatedwidth/2f);
            param /= modulatedwidth;
            param *= 2f;
            param -= 1f;
            param += modulatedslide;
            param = MathHelper.Clamp(param, -1f, 1f);

            result = 1f-(float)Math.Pow((param)*1f*shrinkfactor, 2f)*1f;
            result *= depth;
            result = MathHelper.Clamp(result, 0f, 999f);

            return result;
        }
        public void DoCurvedRiver(int X, int Y, int Corner)
        {
            float pow = 2f;
            float rivermin = 0.1f;
            float rivermax = 0.6f;
            //how deep the river carves
            float strength = 16f;
            //river width randomness - uses noise to align to tiles and smooth tranition
            //make this separate function returning vector2
            float rivernoisescale = 1f / 64f;
            float meanderscale = 1f / 4f;
            float offset = 0;
            //river midpoint should be in the middle of block
            float mid = ((float)BlockSize) / 2f;

            float Width0, Width1,Slide0,Slide1;


            float widthL = (Simplex.CalcPixel2D(X - 1, Y, rivernoisescale) + Simplex.CalcPixel2D(X, Y, rivernoisescale)) / 512f;

            float widthR = (Simplex.CalcPixel2D(X + 1, Y, rivernoisescale) + Simplex.CalcPixel2D(X, Y, rivernoisescale)) / 512f;

            float widthT = (Simplex.CalcPixel2D(X, Y - 1, rivernoisescale) + Simplex.CalcPixel2D(X, Y, rivernoisescale)) / 512f;

            float widthB = (Simplex.CalcPixel2D(X, Y + 1, rivernoisescale) + Simplex.CalcPixel2D(X, Y, rivernoisescale)) / 512f;


            float slideL = (Simplex.CalcPixel2D(X - 1, Y, meanderscale) + Simplex.CalcPixel2D(X, Y, meanderscale)) / 512f;     
            float slideR = (Simplex.CalcPixel2D(X + 1, Y, meanderscale) + Simplex.CalcPixel2D(X, Y, meanderscale)) / 512f;
            float slideT = (Simplex.CalcPixel2D(X, Y - 1, meanderscale) + Simplex.CalcPixel2D(X, Y, meanderscale)) / 512f;
            float slideB = (Simplex.CalcPixel2D(X, Y + 1, meanderscale) + Simplex.CalcPixel2D(X, Y, meanderscale)) / 512f;
            float degreebias = 0f;
            Vector2 CornerPoint;
            Vector2 bias = new Vector2(1, 1);
            float P0 = -1;
            float P1 = 2 +  BlockSize;
            switch(Corner)
            {

                case RIVER_CORNER_TL:
                    {
                        CornerPoint = new Vector2(P0, P0);
                        Width0 = widthT;
                        Width1 = widthL;
                        Slide0 = slideT;
                        Slide1 = slideL;
                        Slide0 -= 0.5f;
                        Slide1 -= 0.5f;
                        degreebias -= 0f;
                        break;
                    }
                case RIVER_CORNER_TR:
                    {
                        CornerPoint = new Vector2(P1, P0);
                        Width0 = widthR;
                        Width1 = widthT;
                        Slide0 = slideR;
                        Slide1 = 1f-slideT;
                        Slide0 -= 0.5f;
                        Slide1 -= 0.5f;
                        degreebias -= 90f;
                        bias = new Vector2(1, 0);
                        break;
                    }
                case RIVER_CORNER_BL:
                    {
                        CornerPoint = new Vector2(P0, P1);
                        Width0 = widthL;
                        Width1 = widthB;
                        Slide0 = 1-slideL;
                        Slide1 = slideB; //

                        Slide0 -= 0.5f;
                        Slide1 -= 0.5f;
                        degreebias += 90f;
                        bias = new Vector2(0, 1);
                        break;
                    }
                default:
                    {
                        CornerPoint = new Vector2(P1, P1);
                        Width0 = widthB;
                        Width1 = widthR;
                        Slide0 = 1f-slideB; //
                        Slide1 = 1f-slideR; //
                        Slide0 -= 0.5f;
                        Slide1 -= 0.5f;
                        degreebias += 180f;
                        bias = new Vector2(1, 1);
                        break;
                    }
            }


            Width1 = MathHelper.Clamp(Width1, rivermin, rivermax);
            Width0 = MathHelper.Clamp(Width0, rivermin, rivermax);

            BlockTitle = "W0:" + Width0.ToString("F2") + " W1:" + Width1.ToString("F2") + " S0: " + Slide0.ToString("F2") + " S1:" + Slide1.ToString("F2");
            for (int x = 0; x < (BlockSize + 1 +2); x++)
                for (int y = 0; y < (BlockSize + 1+2 ); y++)
                {
                    Vector2 pointer =  (new Vector2(x-1, y-1))- CornerPoint;
                    float M = pointer.Length();
                    pointer = Vector2.Normalize(pointer);
                    float degrees = MathHelper.ToDegrees((float)Math.Atan2(pointer.Y, pointer.X));
                    degrees += degreebias;
                    if (degrees > 90f)
                        degrees = 1f / 0f;
                    degrees += 360f;
                    degrees = degrees % 360;
                    degrees /= 90f;
                    float W = degrees;
                    if (W < 0f || W > 1f)
                        W /= 0f;
                    W *= (BlockSize + 1);
                   // Slide0 = -0.2f;
                   // Slide1 = 0.2f;
                    offset = GainDepth(M, W, Width0, Width1, Slide0, Slide1, strength);
                   // offset = ((float)Math.Pow((((float)(Z) - mid) / strength), pow) * -1f) + (float)Math.Pow(mid / strength, pow) * MathHelper.Lerp(Width0, Width1, Z/ (float)(BlockSize + 1 + 2)); ;
                    // TerrainVertex v = GainVertex(x - 1, y - 1, X, Y);
                    offset = MathHelper.Clamp(offset, 0, 999f);
                    if (offset > 0)
                        _vertices[(x + 0) + ((y + 0) * (BlockSize + 1 + 2))].MultiTexData.Z = 1f;
                    _vertices[(x + 0) + ((y + 0) * (BlockSize + 1 + 2))].Position -= new Vector3(0, offset, 0);
                    //_vertices[(x + 0) + ((y + 0) * (BlockSize + 1 + 2))].Color = new Color(W/(BlockSize+1+2), 0f, 0f);
                }

        }
        public void Do3SideRiver(int X, int Y, int side)
        {
            TerrainVertex[] Original = new TerrainVertex[_vertices.Length];
            _vertices.CopyTo(Original, 0);
            TerrainVertex[] Layer1 = new TerrainVertex[_vertices.Length];
            TerrainVertex[] Layer2 = new TerrainVertex[_vertices.Length];
            switch (side)
            {
                case RIVER_SIDE_BOTTOM:
                    {
                        DoCurvedRiver(X, Y, RIVER_CORNER_BL);
                        _vertices.CopyTo(Layer1, 0);
                        Original.CopyTo(_vertices, 0);
                        DoCurvedRiver(X, Y, RIVER_CORNER_BR);
                        _vertices.CopyTo(Layer2, 0);
                        Original.CopyTo(_vertices, 0);
                        DoStraightRiver(X, Y, true);
                        break;
                    }
                case RIVER_SIDE_TOP:
                    {
                        DoCurvedRiver(X, Y, RIVER_CORNER_TL);
                        _vertices.CopyTo(Layer1, 0);
                        Original.CopyTo(_vertices, 0);
                        DoCurvedRiver(X, Y, RIVER_CORNER_TR);
                        _vertices.CopyTo(Layer2, 0);
                        Original.CopyTo(_vertices, 0);
                        DoStraightRiver(X, Y, true);
                        break;
                    }
                case RIVER_SIDE_RIGHT:
                    {
                        DoCurvedRiver(X, Y, RIVER_CORNER_TR);
                        _vertices.CopyTo(Layer1, 0);
                        Original.CopyTo(_vertices, 0);
                        DoCurvedRiver(X, Y, RIVER_CORNER_BR);
                        _vertices.CopyTo(Layer2, 0);
                        Original.CopyTo(_vertices, 0);
                        DoStraightRiver(X, Y, false);
                        break;
                    }
                default:
                    {
                        DoCurvedRiver(X, Y, RIVER_CORNER_TL);
                        _vertices.CopyTo(Layer1, 0);
                        Original.CopyTo(_vertices, 0);
                        DoCurvedRiver(X, Y, RIVER_CORNER_BL);
                        _vertices.CopyTo(Layer2, 0);
                        Original.CopyTo(_vertices, 0);
                        DoStraightRiver(X, Y, false);
                        break;
                    }
            }

            for (int x = 0; x < (BlockSize + 1 + 2); x++)
                for (int y = 0; y < (BlockSize + 1 + 2); y++)
                {
                    TerrainVertex a = _vertices[(x + 0) + ((y + 0) * (BlockSize + 1 + 2))];
                    TerrainVertex b = Layer1[(x + 0) + ((y + 0) * (BlockSize + 1 + 2))];
                    TerrainVertex c = Layer2[(x + 0) + ((y + 0) * (BlockSize + 1 + 2))];
                    Vector3 o = a.Position;
                    float H = o.Y;
                    float L1 = b.Position.Y;
                    float L2 = c.Position.Y;
                    float Z = a.MultiTexData.Z;
                    if (Z < b.MultiTexData.Z)
                        Z = b.MultiTexData.Z;
                    if (Z < c.MultiTexData.Z)
                        Z = c.MultiTexData.Z;
                    if (H > L1)
                        H = L1;
                    if (H > L2)
                        H = L2;
                    _vertices[(x + 0) + ((y + 0) * (BlockSize + 1 + 2))].Position = new Vector3(o.X, H, o.Z);
                    _vertices[(x + 0) + ((y + 0) * (BlockSize + 1 + 2))].MultiTexData.Z = Z;
                }


        }
        public void DoRiverPond(int X, int Y)
        {

        }
        public void DoRiverEnd(int X, int Y,int side)
        {

        }
        public void SetColours()
        {

        }


        /// <summary>
        /// Processes the block queue
        /// </summary>
        public void ProcessQueue()
        {
            //I am not sure why the function never exits, however, this is the intended behaviour.
            while (TerrainRef.Queue.Count > 0)
            {
                Vector2 b = TerrainRef.Queue.Dequeue();
                int rd = TerrainRef.RenderDistance == 0 ? 8 : TerrainRef.RenderDistance;
                // Unit blk = WorldLoader.Load((int)b.X, (int)b.Y);
                // blk = WorldGenerator.GenerateBlock((int)b.X, (int)b.Y, 64);
                //Math.Abs(blk.Value.X - X) > rd || Math.Abs(blk.Value.Y - Y) > rd
                Unit blk = null;
                if (blk == null)
                {
                    if (Math.Abs(b.X - LastX) > rd || Math.Abs(b.Y - LastY) > rd)
                        continue;
                    if (b.X < 0 || b.Y < 0)
                        continue;
                    blk = GenerateBlock((int)b.X, (int)b.Y);
                    Terrain.Console.Write("^00FF00 Generated " + ((int)b.X).ToString() + "." + ((int)b.Y).ToString());

                }
                else
                {
                    //  Volatile.Console.Write("^00FF00 Loaded " + ((int)b.X).ToString() + "." + ((int)b.Y).ToString());
                }
                lock (blk)
                {
                    TerrainRef.Blocks.GetOrAdd(blk.X + blk.Y * BlockSize, blk);
                }

            }


        }

        public void BorderEvent(int X, int Y)
        {
            LastX = X;
            LastY = Y;
            // Utility.Trace(fixedX.ToString() + "," + fixedY.ToString());
            int rd = TerrainRef.RenderDistance == 0 ? 8 : TerrainRef.RenderDistance;
            for (int x = X - rd; x < X + rd + 1; x++)
            {

                for (int y = Y - rd; y < Y + rd + 1; y++)
                {
                    if (!TerrainRef.BlockLoaded(x, y))
                    {
                        TerrainRef.Queue.Enqueue(new Vector2(x, y));
                    }


                }
                List<KeyValuePair<int, Unit>> tmp = new List<KeyValuePair<int, Unit>>();
                Unit d;
                lock (tmp)
                {
                    foreach (KeyValuePair<int, Unit> blk in TerrainRef.Blocks)
                    {
                        if (Math.Abs(blk.Value.X - X) > rd || Math.Abs(blk.Value.Y - Y) > rd)
                        {
                            tmp.Add(blk);
                        }

                    }
                    foreach (KeyValuePair<int, Unit> blk in tmp)
                    {
                        //WorldLoader.Save(blk, blk.X, blk.Y);
                        TerrainRef.Blocks.TryRemove(blk.Key, out d);

                    }
                }
            }

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
            BlockTitle = "";
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
            block.Name = BlockTitle;
            return block;
        }




        //this function calculates the actual height/colour/etc of a vertex. 
        //this should be corrected to generate from a WorldMap.

        TerrainVertex GainVertex2(int X, int Y, int DX, int DY, int Seed = 0)
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
                float random1 = Simplex.CalcPixel2D(MapX + 1112, MapY + 13123, 2f)/512f;
                float random2 = Simplex.CalcPixel2D(MapX + 1112, MapY + 13123, 1f)/1024f;

                float ground = Simplex.CalcPixel2D(MapX+1112, MapY+13123, 0.0006125f/1f);
                float beachdist= H - GroundBaseline;
                float evenness = 16f;
                ground /= 256f;
                ground = (float)(Math.Pow(ground, 4.4));
                ground *= 256f;
                beachdist /= evenness;
                beachdist = MathHelper.Clamp(beachdist, 0.0f, 1.0f);
                float finalground= GroundBaseline + ground * beachdist + Hills * beachdist+(random1+random2)*beachdist;


                v.Position.Y = finalground;// + random1 + random2;

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

        public float GetBilineaer(int X, int Y, int DX, int DY,float[,] data)
        {
            int X0 = DX - 1;
            int Y0 = DY - 1;
            //*
            if (X >= (int)((float)BlockSize / 2f))
                X0++;
            if (Y >= (int)((float)BlockSize / 2f))
                Y0++;



            float TL, TR, BL, BR;

            try
            {
                TL = data[X0, Y0];
                TR = data[X0 + 1, Y0];
                BL = data[X0, Y0 + 1];
                BR = data[X0 + 1, Y0 + 1];
            }
            catch (Exception)
            {

                return 0;
            }

            float LX = (float)((X - (BlockSize / 2) + BlockSize) % BlockSize) / (float)BlockSize;
            float LY = (float)((Y - (BlockSize / 2) + BlockSize) % BlockSize) / (float)BlockSize;

            LX = (float)((X + (BlockSize / 2)) % BlockSize) / (float)BlockSize;
            LY = (float)((Y + (BlockSize / 2)) % BlockSize) / (float)BlockSize;


            // LX = (float)((X + (32)) % 64) / (float)64;
            // LY = (float)((Y + (32)) % 64) / (float)64;


            TL *= ((1 - LX) * (1 - LY));
            BL *= ((1 - LX) * (LY));
            TR *= ((LX) * (1 - LY));
            BR *= ((LX) * (LY));
            return TL + BR + TR + BL;
        }

        TerrainVertex GainVertex(int X, int Y, int DX, int DY, int Seed = 0)
        {
            TerrainVertex v = new TerrainVertex();

            v.Position.X = X;
            v.Position.Z = Y;
            v.Position.Y = 0; v.Color = Color.Red;
            v.TextureCoordinate.X = X / 4f;
            v.TextureCoordinate.Y = Y / 4f;
            //this will contain calculated height
            float H = 0;
            if (this.Map == null)
            { 
                return v;
            }
            //return v;
            int MapX = X + DX * (BlockSize);
            int MapY = Y + DY * (BlockSize);
            //get lerped "rough" elevation
            
            float Elevation =  GetBilineaer(X,Y,DX,DY,Map.ElevationData);
            //return v;
            //*
            float base1 = Simplex.CalcPixel2D(MapX + 1112, MapY + 13123, 1f / 1024f) / 1f;
            H = 80f + Elevation * base1;
            float random2 = Simplex.CalcPixel2D(MapX + 1112, MapY + 13123, 1f / 32f) / 256f;
            random2 += Simplex.CalcPixel2D(MapX + 1112, MapY + 13123, 1f / 512f) / 32f;
            H += random2;
            //*/
            bool isRiver = false;
            bool isMountain = false;// 90f;// H;
            //return v;
            try
            {
                v.Color = Map.GetGrassColour(DX, DY);
                isRiver = Map.TileData[DX, DY] == WorldMap.TileType.River;
                isMountain = Map.TileData[DX, DY] == WorldMap.TileType.Mountain;

            }
            catch (Exception)
            {

                v.Color = Color.Red;
            }
         //   if (Elevation <= 0.0f&&!isRiver)
           //     H = 70f;

            v.Position.Y = H;

            if (v.Position.Y < 80f)
                v.MultiTexData.Z = 1f;
            return v;
        }
        #endregion



    }
}
