using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace WorldInfo
{
    public class WorldMap
    {

        static WorldMap()
        {
            TILE_UNFILLED = new Color(255, 0, 0);
            TILE_PLAIN = new Color(51, 204, 51);
            TILE_RIVER = new Color(0, 0, 255);
            TILE_LAKE = new Color(0, 102, 255);
            TILE_MOUNTAIN = new Color(204, 102, 0);
            TILE_BEACH = new Color(255, 255, 102);
            TILE_OCEAN = new Color(0, 153, 255);
        }

        readonly Dictionary<Color, TileType> ColorMap = new Dictionary<Color, TileType>
        {
            {TILE_UNFILLED,TileType.Unfilled },
            {TILE_PLAIN, TileType.Plain},
            {TILE_RIVER,TileType.River },
            {TILE_LAKE,TileType.Lake },
            {TILE_MOUNTAIN,TileType.Mountain },
            {TILE_BEACH,TileType.Beach },
            {TILE_OCEAN,TileType.Ocean }
        };
        public enum TileType
        {
            Unfilled,
            Ocean,
            Beach,
            Plain,
            Mountain,
            River,
            Lake
        }

        public Color GetTileColour(TileType Tile)
        {
            //this is ugly as shit but supposedly compiles into a neat static table
            //who knew?
            switch (Tile)
            {
                case TileType.Unfilled:
                    return TILE_UNFILLED;
                case TileType.Plain:
                    return TILE_PLAIN;
                case TileType.River:
                    return TILE_RIVER;
                case TileType.Lake:
                    return TILE_LAKE;
                case TileType.Mountain:
                    return TILE_MOUNTAIN;
                case TileType.Beach:
                    return TILE_BEACH;
                case TileType.Ocean:
                    return TILE_OCEAN;
                default:
                    return TILE_OCEAN;
            }
        }
        public TileType CreateTileFromColour(Color colour)
        {
            if (!ColorMap.ContainsKey(colour))
                return TileType.Ocean;
            return ColorMap[colour];
        }
        public int Width { get; set; }
        public int Height { get; set; }

        public TileType[,] TileData { get; set; }
        public byte[,] PathData { get; set; }
        public float[,] ElevationData { get; set; }
        public float[,] TemperatureData { get; set; }
        public float[,] HumidityData { get; set; }
        public float[,] OceanDistanceField { get; set; }
        public float[,] RiverDistanceField { get; set; }
        public int[,] LocationData {get;set;}
        public static Color TILE_UNFILLED { get; }
        public static Color TILE_PLAIN { get; }
        public static Color TILE_RIVER { get; }
        public static Color TILE_LAKE { get; }
        public static Color TILE_MOUNTAIN { get; }
        public static Color TILE_BEACH { get; }
        public static Color TILE_OCEAN { get; }
        public Color[] ColourMap;
        public Point ColourMapSize;
        public List<Point> Towns;
        public Dictionary<int, WorldInfo.Location> Locations;
        public WorldMap(int Height, int Width)
        {
            this.Height = Height;
            this.Width = Width;
            TileData = new TileType[Width, Height];
            PathData=new byte[Width, Height];
            ElevationData= new float[Width, Height];
            TemperatureData= new float[Width, Height];
            HumidityData = new float[Width, Height];
            OceanDistanceField = new float[Width, Height];
            RiverDistanceField = new float[Width, Height];
            LocationData = new int[Width, Height];
            this.Locations = new Dictionary<int, Location>();
            Locations.Add(0, Location.Unknown);
            Towns = new List<Point>();
        }

        public Texture2D TilesToTexture(GraphicsDevice device)
        {
            Texture2D output = new Texture2D(device, Width, Height);
            Color[] data = new Color[Width * Height];
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    data[y * Width + x] = GetTerrainColour(x,y);
                }
            output.SetData(data);
            return output;
        }
        public Color GetTerrainColour(int x, int y)
        {
            //if (Towns.Contains(new Point(x, y)))
            //  return Color.Magenta;
            Location loc = Locations[LocationData[x, y]];
            if (loc.Type == Location.LocationType.Town)
                return Color.Magenta;
            TileType tile =TileData[x, y];
            if (tile == TileType.Plain)
                return GetGrassColour(x,y);
            return GetTileColour(TileData[x, y]);
        }

        public Color GetGrassColour(int x, int y)
        {
            //float dOF = OceanDistanceField[x, y];
          // dOF = TemperatureData[x, y];
            //dOF = RiverDistanceField[x, y];
            float H = HumidityData[x, y];
            float T = TemperatureData[x, y];
            H *= T;
            H = 1f - H;
            T = 1f - T;
            H *= ColourMapSize.Y;
            T *= ColourMapSize.X;
            int tX = (int)T;
            int tY = (int)H;
            return ColourMap[tX + tY * ColourMapSize.X];
            //return ColourScale(dOF);
        }

        public Color ColourScale(float M)
        {
            float step = 0.25f;
            Color Z = new Color(255, 0, 255);
            Color A = new Color(0, 0, 255);
            Color B = new Color(0, 255, 255);
            Color C = new Color(0, 255, 0);
            Color D = new Color(255, 255, 0);
            Color E = new Color(255, 0, 0);
            List<Color> Scale = new List<Color>();
            Scale.Add(Z);
            Scale.Add(A);
            Scale.Add(B);
            Scale.Add(C);
            Scale.Add(D);
            Scale.Add(E);
            step = 1f / (float)(Scale.Count() - 1);
            int offset = 0;
            for (int i = 0; i < Scale.Count - 1; i++)
            { 
                if (M < step)
                    return Color.Lerp(Scale[offset+i], Scale[offset+1+i], M / step);
                M -= step;

            }
            return Color.Black;
        }
    }
}
