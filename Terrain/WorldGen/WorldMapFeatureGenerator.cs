using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrain.WorldGen
{
    public static class WorldMapFeatureGenerator
    {

        static IRandomProvider _rng;

        public static int Seed;
        /// <summary>
        /// RNG used for generation - used to seed the noise and place features
        /// </summary>
        public static IRandomProvider RNG
        {
            get
            {
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
        public static void DoRivers(WorldMap map, int amount)
        {
            for (int i = 0; i < amount; i++)
                DoRiver(map);
        }

        /// <summary>
        /// Creates a single random river
        /// </summary>
        /// <param name="map">WorldMap to use</param>
        public static void DoRiver(WorldMap map)
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

        public static void Replace(WorldMap map, WorldMap.TileType From, WorldMap.TileType To)
        {
            for (int x = 0; x < map.Width; x++)
                for (int y = 0; y < map.Height; y++)
                    if (map.TileData[x, y] == From)
                        map.TileData[x, y] = To;
        }



        public static void DoOceanDistanceField(WorldMap map)
        {
            //pass 1: set all to something stupid like 9999 except ocean
            float MAX = 9999f;
            float Highest = 0f;
            float[] neighbourhood = new float[8];
            for (int x = 0; x < map.Width; x++)
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.TileData[x, y] == WorldMap.TileType.Ocean)
                        map.OceanDistanceField[x, y] = 0;
                    else
                        map.OceanDistanceField[x, y] = MAX;

                }
            //pass 2 & 3: pathfind
            for (int x = 1; x < map.Width - 1; x++)
                for (int y = 1; y < map.Height - 1; y++)
                {
                    //iterate over 8 neighbours, pick the smallest, add 1
                    neighbourhood[0] = map.OceanDistanceField[x - 1, y - 1];
                    neighbourhood[1] = map.OceanDistanceField[x, y - 1];
                    neighbourhood[2] = map.OceanDistanceField[x + 1, y - 1];
                    neighbourhood[3] = map.OceanDistanceField[x - 1, y];
                    neighbourhood[4] = map.OceanDistanceField[x + 1, y];
                    neighbourhood[5] = map.OceanDistanceField[x - 1, y + 1];
                    neighbourhood[6] = map.OceanDistanceField[x, y + 1];
                    neighbourhood[7] = map.OceanDistanceField[x + 1, y + 1];
                    float smallest = neighbourhood.Min();
                    map.OceanDistanceField[x, y] = smallest + 1;
                }
            //go from the other direction now
            for (int x = map.Width - 2; x > 0 ; x--)
                for (int y = map.Height - 2; y > 0; y--)
                {
                    //iterate over 8 neighbours, pick the smallest, add 1
                    neighbourhood[0] = map.OceanDistanceField[x - 1, y - 1];
                    neighbourhood[1] = map.OceanDistanceField[x, y - 1];
                    neighbourhood[2] = map.OceanDistanceField[x + 1, y - 1];
                    neighbourhood[3] = map.OceanDistanceField[x - 1, y];
                    neighbourhood[4] = map.OceanDistanceField[x + 1, y];
                    neighbourhood[5] = map.OceanDistanceField[x - 1, y + 1];
                    neighbourhood[6] = map.OceanDistanceField[x, y + 1];
                    neighbourhood[7] = map.OceanDistanceField[x + 1, y + 1];
                    float smallest = neighbourhood.Min();
                    map.OceanDistanceField[x, y] = smallest + 1;
                    //find the furthest value - use it as a 1 in the next step
                    if (Highest < smallest + 1)
                        Highest = smallest + 1;
                }
            //pass 4: normalisation
            if (Highest!=0)
            for (int x = 1; x < map.Width - 1; x++)
                for (int y = 1; y < map.Height - 1; y++)
                {
                    map.OceanDistanceField[x, y] /= Highest;
                }
        }



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
                    float temp = MathHelper.Lerp(0.5f, gradient, oceandist);
                    map.TemperatureData[x, y] = temp;
                }
        }

    }
}
