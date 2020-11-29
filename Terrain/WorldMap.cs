using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrain
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
                    return TILE_PLAIN;
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
        public static Color TILE_UNFILLED { get; }
        public static Color TILE_PLAIN { get; }
        public static Color TILE_RIVER { get; }
        public static Color TILE_LAKE { get; }
        public static Color TILE_MOUNTAIN { get; }
        public static Color TILE_BEACH { get; }
        public static Color TILE_OCEAN { get; }

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
            TileType tile =TileData[x, y];
            if (tile == TileType.Ocean)
                return TILE_OCEAN;
            float dOF = OceanDistanceField[x, y];
            float R = MathHelper.Clamp(dOF * 2f, 0f, 1f);
            float G = MathHelper.Clamp((1f-dOF) * 2f, 0f, 1f);
            return new Color(R, G, 0);
            return Color.Multiply(GetTileColour(TileData[x, y]), ((TileData[x, y] == TileType.Plain|| TileData[x, y] == TileType.River) ? MathHelper.Clamp(1f - ElevationData[x, y] * 0.5f, 0.5f, 1f) : 1f));
        }
    }
}
