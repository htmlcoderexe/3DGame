﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WorldInfo;

namespace WorldGen
{
    public static class WorldMapFeatureGenerator
    {
        public const int DIR_W = 0;
        public const int DIR_N = 1;
        public const int DIR_E = 2;
        public const int DIR_S = 3;


        static IRandomProvider _rng;

        public static int Seed;
        /// <summary>
        /// RNG used for generation - used to seed the noise and place features
        /// </summary>
        public static IRandomProvider RNG
        {
            get
            {
                //Lazy init to default windows generator
                if (_rng == null)
                    _rng = new WinRNG(Seed);
                return _rng;
            }
            set
            {
                _rng = value;
            }
        }
        /// <summary>
        /// Makes a circle inside a square starting at X,Y with side D
        /// </summary>
        /// <param name="map">WorldMap to apply to</param>
        /// <param name="X">Corner X</param>
        /// <param name="Y">Corner Y</param>
        /// <param name="D">Diametre</param>
        public static void MakeCircle(WorldMap map, int X, int Y, int D)
        {
            Vector2 centre = new Vector2((float)X + ((float)D) / 2f, (float)Y + ((float)D) / 2f);
            for (int x = X; x < X + D; x++)
                for (int y = Y; y < Y + D; y++)
                    if (((new Vector2(x, y)) - centre).Length() < D / 2f)
                    {
                        map.TileData[x, y] = WorldMap.TileType.Plain;
                    }
                        
        }
        /// <summary>
        /// Makes a continent based on a noisy ellipse inside a defined rectangle
        /// </summary>
        /// <param name="map">WorldMap to apply to</param>
        /// <param name="X">X of rectangle's start corner</param>
        /// <param name="Y">Y of rectangle's start corner</param>
        /// <param name="W">Width of rectangle</param>
        /// <param name="H">Height of rectangle</param>
        public static void MakeEllipse(WorldMap map, int X, int Y, int W, int H)
        {
            Vector2 centre = new Vector2((float)X + ((float)W) / 2f, (float)Y + ((float)H) / 2f);
            Vector2 Left = new Vector2(X, (float)Y + ((float)H / 2f));
            Vector2 Top = new Vector2((float)X + ((float)W) / 2f,Y);
            float a = (centre - Left).Length();
            float b = (centre - Top).Length();

            float c = (float)Math.Sqrt(a * a - b * b);

            Vector2 F1 = centre - (new Vector2(c, 0));
            Vector2 F2 = centre + (new Vector2(c, 0));

            float f = (F2 - F1).Length();
            float o=2*a;
            for (int x = X; x < X + W; x++)
                for (int y = Y; y < Y + H; y++)
                    if (true || (((new Vector2(x, y)) - F1).Length()+ ((new Vector2(x, y)) - F2).Length()) < o)
                    {
                        float dist = (((new Vector2(x, y)) - F1).Length() + ((new Vector2(x, y)) - F2).Length());
                        float factor = 1-((dist - f) / (o - f));
                        factor *= 0.6f;
                        //*
                        factor += (float)(Simplex.CalcPixel2D(x, y, 1f / 64f) - 128f) / 256f;
                        factor += (float)(Simplex.CalcPixel2D(x, y, 1f / 256f) - 128f) /256f;
                        factor += (float)(Simplex.CalcPixel2D(x, y, 1f / 16f) - 192f) / 1024f;
                        factor += (float)(Simplex.CalcPixel2D(x, y, 1f / 2f) - 128f) / 2048f;
                        //*/
                        if (factor>0.0f)
                            map.TileData[x, y] = WorldMap.TileType.Plain;
                        //if (factor > 0.9f)
                          //  map.TileData[x, y] = WorldMap.TileType.Mountain;
                        map.ElevationData[x, y] = factor;
                    }
        }

        /// <summary>
        /// Returns the direction to the closest ocean tile.
        /// </summary>
        /// <param name="map">WorldMap to use</param>
        /// <param name="X">X position to look from</param>
        /// <param name="Y">Y position to look from</param>
        /// <returns></returns>
        public static int ClosestOcean(WorldMap map, int X, int Y)
        {
            int T = 0;
            int B = 0;
            int R = 0;
            int L = 0;

            for (int x = X; x < map.Width; x++)
                if (map.TileData[x, Y] == WorldMap.TileType.Ocean)
                {
                    R = x - X;
                    break;
                }
            for (int x = X; x >= 0; x--)
                if (map.TileData[x, Y] == WorldMap.TileType.Ocean)
                {
                    L = X - x;
                    break;
                }
            for (int y = Y; y < map.Height; y++)
                if (map.TileData[X, y] == WorldMap.TileType.Ocean)
                {
                    B = y - Y;
                    break;
                }
            for (int y = Y; y >= 0; y--)
                if (map.TileData[X, y] == WorldMap.TileType.Ocean)
                {
                    T = Y - y;
                    break;
                }
            int Min = (new int[] { T, B, R, L }).Min();
            if (Min == T)
                return 0;
            if (Min == R)
                return 1;
            if (Min == B)
                return 2;
            if (Min == L)
                return 3;
            return 0;
        }

        /// <summary>
        /// Determines the next river tile
        /// </summary>
        /// <param name="map">WorldMap to use</param>
        /// <param name="X">Current position X</param>
        /// <param name="Y">Current position Y</param>
        /// <returns></returns>
        public static int PointRiver(WorldMap map, int X,int Y)
        {
            float H = map.ElevationData[X, Y];

            float TH = Y == 0 ? 9999 : map.ElevationData[X, Y - 1];
            float RH = X == map.Width-1 ? 9999 : map.ElevationData[X+1, Y];
            float BH = Y == map.Height-1 ? 9999 : map.ElevationData[X, Y + 1];
            float LH = X == 0 ? 9999 : map.ElevationData[X-1, Y];

            float Min = (new float[] { TH, RH, BH, LH }).Min();
            if (Min > H)
                return ClosestOcean(map, X, Y);
            if (Min == TH && Min!=9999 && map.TileData[X,Y-1]!= WorldMap.TileType.River)
                return 0;
            if (Min == RH && Min != 9999 && map.TileData[X+1, Y] != WorldMap.TileType.River)
                return 1;
            if (Min == BH && Min != 9999 && map.TileData[X, Y + 1] != WorldMap.TileType.River)
                return 2;
            if (Min == LH && Min != 9999 && map.TileData[X-1, Y] != WorldMap.TileType.River)
                return 3;
            return ClosestOcean(map, X, Y);
        }

        /// <summary>
        /// Creates multiple rivers
        /// </summary>
        /// <param name="map">WorldMap to use</param>
        /// <param name="amount">Amount of rivers</param>
        public static List<Point> DoRivers(WorldMap map, int amount)
        {
            List<Point> l;
            List<Point> all = new List<Point>();
            for (int i = 0; i < amount; i++)
            {
                l= DoRiver(map);
                if (l.Count() > 1)
                {
                    all.AddRange(l);
                }
                else
                {
                    i--;
                }
                    
            }
            return all;
        }

        /// <summary>
        /// Creates a single random river
        /// </summary>
        /// <param name="map">WorldMap to use</param>
        public static List<Point> DoRiver(WorldMap map)
        {
            int x = 0;
            int y = 0;
            List<Point> visited = new List<Point>();
            x = RNG.NextInt(map.Width);
            y = RNG.NextInt(map.Height);
            int oldx = 0;
            int oldy = 0;

            map.TileData[x, y] = WorldMap.TileType.River;
            int overflow = 0;
            while(overflow<1024)
            {
                overflow++;
                int next = PointRiver(map, x, y);
                oldx = x;
                oldy = y;
                switch(next)
                {
                    case 3:
                        {
                            x--;
                            break;
                        }

                    case 2:
                        {
                            y++;
                            break;
                        }

                    case 1:
                        {
                            x++;
                            break;
                        }

                    default:
                        {
                            y--;
                            break;
                        }

                }

                if(visited.Contains(new Point(x,y)))
                {
                    x = oldx;
                    y = oldy;
                    next = ClosestOcean(map, x, y);
                    switch (next)
                    {
                        case 3:
                            {
                                x--;
                                break;
                            }

                        case 2:
                            {
                                y++;
                                break;
                            }

                        case 1:
                            {
                                x++;
                                break;
                            }

                        default:
                            {
                                y--;
                                break;
                            }

                    }
                }


                //stop if we reached edge of the map
                if (x < 0 || x >= map.Width || y < 0 || y > map.Height)
                    break;
                //stop if we reached the ocean
                if (map.TileData[x, y] == WorldMap.TileType.Ocean)
                    break;
                visited.Add(new Point(x, y));
                map.TileData[x, y] = WorldMap.TileType.River;
            }
            foreach(Point p in visited)
            {

                map.ElevationData[p.X, p.Y] = (float)Math.Pow((MathHelper.Clamp(map.ElevationData[x, y], 0f, 999f)), 6f);
                if (map.TileData[p.X - 1, p.Y] != WorldMap.TileType.River)
                    map.ElevationData[p.X - 1, p.Y] = (float)Math.Pow((MathHelper.Clamp(map.ElevationData[p.X - 1, p.Y], 0f, 999f)), 2f);
                if (map.TileData[p.X + 1, p.Y] != WorldMap.TileType.River)
                    map.ElevationData[p.X + 1, p.Y] = (float)Math.Pow((MathHelper.Clamp(map.ElevationData[p.X + 1, p.Y], 0f, 999f)), 2f);
                if (map.TileData[p.X, p.Y - 1] != WorldMap.TileType.River)
                    map.ElevationData[p.X , p.Y - 1] = (float)Math.Pow((MathHelper.Clamp(map.ElevationData[p.X, p.Y - 1], 0f, 999f)), 2f);
                if (map.TileData[p.X ,p.Y + 1] != WorldMap.TileType.River)
                    map.ElevationData[p.X, p.Y + 1] = (float)Math.Pow((MathHelper.Clamp(map.ElevationData[p.X , p.Y + 1], 0f, 999f)), 2f);
            }
            return visited;
        }

        /// <summary>
        /// Fills a region with a given tile.
        /// </summary>
        /// <param name="map">WorldMap to use</param>
        /// <param name="X">X coordinate of starting point</param>
        /// <param name="Y">Y coordinate of starting point</param>
        /// <param name="From">Tile to replace</param>
        /// <param name="To">Tile to replace with</param>
        /// <returns>Amount of tiles filled</returns>
        static List<Point> FloodFill(WorldMap map, int X, int Y, WorldMap.TileType From, WorldMap.TileType To)
        {
            List<Point> points = new List<Point>();
            if (X < 0 || Y < 0 || X >= map.Width || Y >= map.Height)
                return points;
            if (map.TileData[X, Y] != From)
                return points;
            map.TileData[X, Y] = To;

            points.Add(new Point(X, Y));
            Queue<Point> Q = new Queue<Point>();
            Q.Enqueue(new Point(X, Y));

            while(Q.Count()>0)
            {
                Point n = Q.Dequeue();
                X = n.X;
                Y = n.Y;
                if (X + 1 < map.Width && Y < map.Height && map.TileData[X + 1, Y] == From)
                {
                    map.TileData[X + 1, Y] = To;
                    points.Add(new Point(X+1, Y));
                    Q.Enqueue(new Point(X + 1, Y));
                }
                if (X >= 0 && Y + 1 < map.Height && map.TileData[X, Y + 1] == From)
                {
                    map.TileData[X, Y + 1] = To;
                    points.Add(new Point(X, Y+1));
                    Q.Enqueue(new Point(X, Y + 1));
                }
                if (X - 1 >=0 && Y >= 0 && map.TileData[X - 1, Y] == From)
                {
                    map.TileData[X - 1, Y] = To;
                    points.Add(new Point(X-1, Y));
                    Q.Enqueue(new Point(X - 1, Y));
                }
                if (X < map.Width && Y - 1 >= 0 && map.TileData[X , Y - 1] == From)
                {
                    map.TileData[X , Y - 1] = To;
                    points.Add(new Point(X, Y-1));
                    Q.Enqueue(new Point(X, Y - 1));
                }
            }
            return points;
            /*
            FloodFill(map, X + 1, Y + 1, From, To);
            FloodFill(map, X + 1, Y - 1, From, To);
            FloodFill(map, X - 1, Y - 1, From, To);
            FloodFill(map, X - 1, Y + 1, From, To);
            //*/    
        }

        /// <summary>
        /// Fills the ocean - this ensures no isolated "lakes" of ocean are left after
        /// generating the continents.
        /// </summary>
        /// <param name="map">WorldMap to use.</param>
        public static void FillOcean(WorldMap map)
        {
            FloodFill(map, 1, 1, WorldMap.TileType.Unfilled, WorldMap.TileType.Ocean);
        }

        /// <summary>
        /// Simple search and replace.
        /// </summary>
        /// <param name="map">Working map</param>
        /// <param name="From">Tile to replace</param>
        /// <param name="To">Replacement tile</param>
        public static void Replace(WorldMap map, WorldMap.TileType From, WorldMap.TileType To)
        {
            for (int x = 0; x < map.Width; x++)
                for (int y = 0; y < map.Height; y++)
                    if (map.TileData[x, y] == From)
                        map.TileData[x, y] = To;
        }


        /// <summary>
        /// Populates a float 2D array with distance values on a given map from given tile type
        /// </summary>
        /// <param name="map">Working map</param>
        /// <param name="DistanceField">Initial 2D distance field</param>
        /// <param name="Target">Tile to calculate distances from</param>
        /// <returns>Computed 2D array distance field</returns>
        public static float[,] DoDistanceField(WorldMap map, float[,] DistanceField, WorldMap.TileType Target)
        {
            //pass 1: set all to something stupid like 9999 except target
            float MAX = 9999f;
            float Highest = 0f;
            float[] neighbourhood = new float[8];
            for (int x = 0; x < map.Width; x++)
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.TileData[x, y] == Target)
                        DistanceField[x, y] = 0;
                    else
                        DistanceField[x, y] = MAX;

                }
            //pass 2 & 3: pathfind
            for (int x = 1; x < map.Width - 1; x++)
                for (int y = 1; y < map.Height - 1; y++)
                {
                    //iterate over 8 neighbours, pick the smallest, add 1
                    neighbourhood[0] = DistanceField[x - 1, y - 1];
                    neighbourhood[1] = DistanceField[x, y - 1];
                    neighbourhood[2] = DistanceField[x + 1, y - 1];
                    neighbourhood[3] = DistanceField[x - 1, y];
                    neighbourhood[4] = DistanceField[x + 1, y];
                    neighbourhood[5] = DistanceField[x - 1, y + 1];
                    neighbourhood[6] = DistanceField[x, y + 1];
                    neighbourhood[7] = DistanceField[x + 1, y + 1];
                    float smallest = neighbourhood.Min();
                    //do not make any changes if no neighbours are lower and not the default value
                    if(smallest<MAX && smallest<DistanceField[x,y])
                    DistanceField[x, y] = smallest + 1;
                }
            //go from the other direction now
            for (int x = map.Width - 2; x > 0 ; x--)
                for (int y = map.Height - 2; y > 0; y--)
                {
                    //iterate over 8 neighbours, pick the smallest, add 1
                    neighbourhood[0] = DistanceField[x - 1, y - 1];
                    neighbourhood[1] = DistanceField[x, y - 1];
                    neighbourhood[2] = DistanceField[x + 1, y - 1];
                    neighbourhood[3] = DistanceField[x - 1, y];
                    neighbourhood[4] = DistanceField[x + 1, y];
                    neighbourhood[5] = DistanceField[x - 1, y + 1];
                    neighbourhood[6] = DistanceField[x, y + 1];
                    neighbourhood[7] = DistanceField[x + 1, y + 1];
                    float smallest = neighbourhood.Min();
                    //do not make any changes if no neighbours are lower and not the default value
                    if (smallest < MAX && smallest < DistanceField[x, y])
                    {
                        //add 1 to the distance - regardless if diagonal or straight
                        DistanceField[x, y] = smallest + 1;
                        //find the furthest value - use it for scaling down to highest being 1.0f and lowest being 0.0f
                        if (Highest < smallest + 1)
                            Highest = smallest + 1;
                    }
                }
            //pass 4: normalisation
            if (Highest!=0)
            for (int x = 1; x < map.Width - 1; x++)
                for (int y = 1; y < map.Height - 1; y++)
                {
                    DistanceField[x, y] /= Highest;
                }
            return DistanceField;
        }


        /// <summary>
        /// Check for presence of a specific tile type in a specific direction (orthogonal)
        /// </summary>
        /// <param name="map">Working map</param>
        /// <param name="X">Starting point</param>
        /// <param name="Y">Starting point</param>
        /// <param name="Direction">Direction 0,1,2,3 - West is 0 and then clockwise</param>
        /// <param name="distance">Maximum distance to look</param>
        /// <param name="Tile"></param>
        /// <returns>distance or 9999 if not found</returns>
        public static int LookRayTile(WorldMap map, int X, int Y, int Direction, int distance,  WorldMap.TileType Tile)
        {
            int D = 9999;
            int dX = 0;
            int dY = 0;
            if (Direction == DIR_E)
                dX = 1;
            if (Direction == DIR_W)
                dX = -1;
            if (Direction == DIR_N)
                dY = -1;
            if (Direction == DIR_S)
                dY = 1;
            for(int i=0;i<distance;i++)
            {
                X += dX;
                Y += dY;
                if (X < 0 || X >= map.Width || Y < 0 || Y > map.Height)
                    return D;
                WorldMap.TileType Target = map.TileData[X, Y];
                if (Target == Tile)
                    return i;
            }

            return D;
        }



        /// <summary>
        /// Computes humidity based on water distance, wind gradient (fixed) and mountain rain shadows.
        /// </summary>
        /// <param name="map">Working map</param>
        public static void DoHumidity(WorldMap map)
        {
            float waterinfluence = 0.99980f;
            float mntradius = 60f;
            float mntinfluence = 0.75f;
            float step = 1f / (float)map.Width;
            for (int x = 0; x < map.Width; x++)
                for (int y = 0; y < map.Height; y++)
                {
                    float O = map.OceanDistanceField[x, y];
                    float R = map.RiverDistanceField[x, y];
                    float H = Math.Min(O,R );
                    //H *= waterinfluence;
                    //  H += (1f - waterinfluence);
                    H = 1f - H;
                    float G = (float)(map.Width-x) * step;
                    int mntdist = LookRayTile(map, x,y,DIR_W, (int)mntradius,  WorldMap.TileType.Mountain);
                    float mntrange = 1f;
                    if(mntdist<=mntradius)
                    {
                        mntrange = (float)mntdist / mntradius;
                    }
                    G *=(mntinfluence+ mntrange*(1f-mntinfluence));
                    float tmp = Math.Max(G, H);
                     //tmp = H;
                    tmp = (float)Math.Pow(tmp,4);
                    map.HumidityData[x, y] = tmp;

                }
        }
    

        /// <summary>
        /// Computes temperature based on ocean distance and heat gradient (fixed, colder North and hotter South)
        /// </summary>
        /// <param name="map">Working map</param>
        public static void DoTemperature(WorldMap map)
        {
            //closer to the ocean the temperature "wants" to be more average
            float oceaninfluence = 0.25f;
            float step = 1f/(float)map.Height;
            for(int x=0;x<map.Width;x++)
                for(int y=0;y<map.Height;y++)
                {
                    float oceandist = map.OceanDistanceField[x, y];
                    oceandist *= oceaninfluence;
                    oceandist += (1f - oceaninfluence);
                    float gradient = (float)y * step;
                    float tmp = MathHelper.Lerp(0.5f, gradient, oceandist);
                    // temp = 1f;
                    tmp = (float)Math.Pow(tmp, 1f/2f);
                    map.TemperatureData[x, y] = tmp;
                }
        }

        /// <summary>
        /// Places mountain tiles
        /// </summary>
        /// <param name="map">Working map</param>
        /// <param name="amount">Amount to place - currently unused</param>
        public static void DoMountains(WorldMap map, int amount)
        {
            int cutoff = 200;
            int cutoff2 = 160;
            int cutoff3 = 128;
            float MountainSizeDivisor = 1f / 32f;
            float MountainSpreadDivisor = 1f / 128f;
            float MountainFrequencyDivisor = 1f / 512f;
            float Noise1 = 1f / 4f;
            float Noise2 = 1f / 6f;
            for (int x = 1; x < map.Width-1; x++)
                for (int y = 1; y < map.Height-1; y++)
                {
                    int H = (int)Simplex.CalcPixel2D(x, y, MountainSizeDivisor);
                    int M = (int)Simplex.CalcPixel2D(x, y, MountainFrequencyDivisor);
                    int S = (int)Simplex.CalcPixel2D(x, y, MountainSpreadDivisor);
                    int N1 = (int)Simplex.CalcPixel2D(x, y, Noise1);
                    int N2 = (int)Simplex.CalcPixel2D(x, y, Noise2);
                    if (H>cutoff && M>cutoff2 && S>cutoff3 &&N1>cutoff3 &&N2>cutoff3)
                    {

                        WorldMap.TileType Target = map.TileData[x, y];
                        if (Target != WorldMap.TileType.Ocean && Target != WorldMap.TileType.River)
                        {
                            map.TileData[x, y] = WorldMap.TileType.Mountain;
                            //greatly raise actual mountain tiles
                            map.ElevationData[x, y] = (float)Math.Pow((MathHelper.Clamp(map.ElevationData[x, y], 0f, 999f)), 1f/10f);
                            //raise tiles around them too
                            if (map.TileData[x - 1, y] != WorldMap.TileType.Mountain)
                                map.ElevationData[x - 1, y] = (float)Math.Pow((MathHelper.Clamp(map.ElevationData[x - 1, y], 0f, 999f)), 1f/3f);
                            if (map.TileData[x + 1,y] != WorldMap.TileType.Mountain)
                                map.ElevationData[x + 1, y] = (float)Math.Pow((MathHelper.Clamp(map.ElevationData[x + 1, y], 0f, 999f)), 1f / 3f);
                            if (map.TileData[x, y - 1] != WorldMap.TileType.Mountain)
                                map.ElevationData[x, y - 1] = (float)Math.Pow((MathHelper.Clamp(map.ElevationData[x, y - 1], 0f, 999f)), 1f / 3f);
                            if (map.TileData[x, y + 1] != WorldMap.TileType.Mountain)
                                map.ElevationData[x,y + 1] = (float)Math.Pow((MathHelper.Clamp(map.ElevationData[x, y + 1], 0f, 999f)), 1f / 3f);
                        }
                           
                    }

                }
            /*/
            for (int i=0;i<amount;i++)
            {
                int X = RNG.NextInt(0, map.Width);
                int Y = RNG.NextInt(0, map.Height);
                WorldMap.TileType Target = map.TileData[X, Y];
                if (Target != WorldMap.TileType.Ocean && Target != WorldMap.TileType.River)
                    map.TileData[X, Y] = WorldMap.TileType.Mountain;
            }
            //*/
        }


        public static Point PlaceTown(WorldMap map, List<Point> Previous, float Separation, List<Point> Rivers, float RiverPreference)
        {
            int X=0;
            int Y=0;
            bool found = false;
            while(!found)
            {
                X = RNG.NextInt(0, map.Width);
                Y = RNG.NextInt(0, map.Height);
                Vector2 current = new Vector2(X, Y);
                float dist = 9999f;
                float smallest = dist;
                foreach(Point p in Rivers)
                {
                    Vector2 rpoint = new Vector2(p.X, p.Y);
                    dist = (rpoint - current).Length();
                    if(dist<RiverPreference && dist< smallest)
                    {
                        smallest = dist;
                        X = p.X;
                        Y = p.Y;
                    }
                }
                current = new Vector2(X, Y);
                foreach (Point p in Previous)
                {
                    Vector2 rpoint = new Vector2(p.X, p.Y);
                    dist = (rpoint - current).Length();
                    if (dist < Separation)
                        continue;
                }
                if (map.OceanDistanceField[X, Y] == 0)
                    continue;
                found = true;
            }

            return new Point(X, Y);
        }

        public static List<Point> PlaceTownCentres(WorldMap map, int amount, float Separation, List<Point> Rivers, float RiverPreference)
        {
            List<Point> Centres = new List<Point>();

            for(int i=0;i<amount;i++)
            {
                Centres.Add(PlaceTown(map, Centres, Separation, Rivers, RiverPreference));
            }

            return Centres;
        }

        public static void GrowTowns(WorldMap map, int Min, int Max)
        {
            foreach(Point t in map.Towns)
            {
                GrowTown(map, Min, Max, t);
            }
        }

        public static Point RandomAdjacent(Point p)
        {
            List<Point> options = new List<Point>()
            {
                new Point(p.X,p.Y-1),
                new Point(p.X,p.Y+1),
                new Point(p.X-1,p.Y),
                new Point(p.X+1,p.Y)
            };

            return options[RNG.NextInt(0, 3)];
        }
        public static string GetRandomTownName()
        {
            return "Town #" + RNG.NextInt(0, 65536);
        }
        public static void GrowTown(WorldMap map, int Min, int Max, Point Centre)
        {
            int amount = RNG.NextInt(Min, Max);
            float fAmount = (float)amount;
            float fMax = (float)Max;
            amount = (int)(fAmount*fAmount*fAmount*fAmount/fMax/fMax/fMax);
            amount = (int)MathHelper.Max(Min, amount);
            int LocationID = map.Locations.Count();
            List<Point> Town = new List<Point>{Centre};
            for (int i=0;i<amount;i++)
            {
                int nextI = RNG.NextInt(0, Town.Count);
                Point growth = new Point();
                Point next = Town[nextI];
                int loopguard = 0; //if after 100+ rolls none of the squares picked are free, we are in a corner
                do
                {
                    growth = RandomAdjacent(next);
                    loopguard++;
                } while (Town.Contains(growth) && loopguard<129 && growth.X>=0 && growth.X< map.Width && growth.Y>=0 && growth.Y< map.Height);
                Town.Add(growth);
            }
            Location loc = new Location()
            {
                Type = Location.LocationType.Town,
                 Name=GetRandomTownName(),
                  Safe=true
            };
            map.Locations.Add(LocationID, loc);
            foreach(Point p in Town)
            {
                if(p.X >= 0 && p.X < map.Width && p.Y >= 0 && p.Y < map.Height)
                map.LocationData[p.X, p.Y] = LocationID;
            }
        }





    }
}
