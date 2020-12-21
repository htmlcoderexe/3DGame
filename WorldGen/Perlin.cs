using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGen
{
    public class PerlinNoise
    {
        private int _PrimeOne;
        private int _PrimeTwo;
        private int _PrimeThree;
        private int _maxX;
        private int _maxY;
        private int _maxZ;
        float[] _lookup;

        //private MathFunctions.FloatTernaryFunction _interpolate;
        float _interpolate(float x, float y, float a)
        {
            double ft = a * Math.PI;
            double f = (1 - Math.Cos(ft)) / 2;

            return (x * (1.0f - (float)f) + y * (float)f);
        }

        #region Constructors
        /// <summary>
        /// InitialintegerZes a new instance of the <see cref="PerlinNoise"/> class.
        /// </summary>
        public PerlinNoise()
        {
            _PrimeOne = 15731; //15731
            _PrimeTwo = 789221; //789221
            _PrimeThree = 1376312589; //1376312589
            _lookup = null;
            _maxX = 0;
            _maxY = 0;
            _maxZ = 0;

            // _interpolate = MathFunctions.FloatCosineInterpolationFunction;
        }
        public PerlinNoise(int seed)
        {
            Random rnd = new Random(seed);

            _PrimeOne = rnd.Next(10000, 20000); ; //15731
            _PrimeTwo = rnd.Next(600000, 800000); //789221
            _PrimeThree = rnd.Next(900000000, 1500000000); //1376312589
            _lookup = null;
            _maxX = 0;
            _maxY = 0;
            _maxZ = 0;

            // _interpolate = MathFunctions.FloatCosineInterpolationFunction;
        }
        #endregion

        /// <summary>
        /// Generates a pseudo-random number based upon one value(dimension).
        /// </summary>
        /// <param name="x">An integer value.</param>
        /// <returns>A single-precision floating point value between -1 and 1.</returns>
        public float Noise(int x)
        {
            if (_lookup != null)
                return _lookup[x];

            x = (x << 13) ^ x;
            double d = (double)((x * (x * x * _PrimeOne + _PrimeTwo) + _PrimeThree) & 0x7fffffff);
            return (1.0f - (float)(d / 1073741824.0));
        }

        /// <summary>
        /// Generates a pseudo-random number based upon two value(dimensions).
        /// </summary>
        /// <param name="x">An integer value.</param>
        /// <param name="y">An integer value.</param>
        /// <returns>A single-precision floating point value between -1 and 1.</returns>
        public float Noise(int x, int y)
        {
            if (_lookup != null)
                return _lookup[x + y * _maxX];

            uint N = (uint)(x + y * 57);
            N = (N << 13) ^ N;
            double d = (double)((N * (N * N * _PrimeOne + _PrimeTwo) + _PrimeThree) & 0x7fffffff);
            return (1.0f - (float)(d / 1073741824.0));
        }
        /// <summary>
        /// Generates a pseudo-random number based upon three value(dimensions).
        /// </summary>
        /// <param name="x">An integer value.</param>
        /// <param name="y">An integer value.</param>
        /// <param name="z">An integer value.</param>
        /// <returns>A single-precision floating point value between -1 and 1.</returns>
        public float Noise(int x, int y, int z)
        {

            if (_lookup != null)
                return _lookup[x + (z * _maxY + y) * _maxX];

            int L = (x + y * 57);
            int M = (y + z * 57);
            uint N = (uint)(L + M * 57);
            N = (N << 13) ^ N;
            double d = (double)((N * (N * N * _PrimeOne + _PrimeTwo) + _PrimeThree) & 0x7fffffff);
            return (1.0f - (float)(d / 1073741824.0));
        }


        public float SmoothNoise(int x)
        {
            return (Noise(x) / 2.0f) + (Noise(x - 1) / 4.0f) + (Noise(x + 1) / 4.0f);
        }
        public float SmoothNoise(int x, int y)
        {
            float corners = (Noise(x - 1, y - 1) + Noise(x + 1, y - 1) + Noise(x - 1, y + 1) + Noise(x + 1, y + 1)) / 16.0f;
            float sides = (Noise(x - 1, y) + Noise(x + 1, y) + Noise(x, y - 1) + Noise(x, y + 1)) / 8.0f;
            float center = Noise(x, y) / 4.0f;
            return (corners + sides + center);
        }
        public float SmoothNoise(int x, int y, int z)
        {
            float corners, sides, center;
            float averageZM1, averageZ, averageZP1;

            // average of neighbours in z-1
            corners = (Noise(x - 1, y - 1, z - 1) + Noise(x + 1, y - 1, z - 1) + Noise(x - 1, y + 1, z - 1) + Noise(x + 1, y + 1, z - 1)) / 16.0f;
            sides = (Noise(x - 1, y, z - 1) + Noise(x + 1, y, z - 1) + Noise(x, y - 1, z - 1) + Noise(x, y + 1, z - 1)) / 8.0f;
            center = Noise(x, y, z - 1) / 4.0f;
            averageZM1 = corners + sides + center;

            // average of neighbours in z
            corners =   (Noise(x - 1, y - 1, z) + Noise(x + 1, y - 1, z) + Noise(x - 1, y + 1, z) + Noise(x + 1, y + 1, z)) / 16.0f;
            sides =     (Noise(x - 1, y, z) + Noise(x + 1, y, z) + Noise(x, y - 1, z) + Noise(x, y + 1, z)) / 8.0f;
            center =    Noise(x, y, z) / 4.0f;
            averageZ = corners + sides + center;

            // average of neighbours in z+1
            corners = (Noise(x - 1, y - 1, z + 1) + Noise(x + 1, y - 1, z + 1) + Noise(x - 1, y + 1, z + 1) + Noise(x + 1, y + 1, z + 1)) / 16.0f;
            sides = (Noise(x - 1, y, z + 1) + Noise(x + 1, y, z + 1) + Noise(x, y - 1, z + 1) + Noise(x, y + 1, z + 1)) / 8.0f;
            center = Noise(x, y, z + 1) / 4.0f;
            averageZP1 = corners + sides + center;

            return ((averageZM1 / 4.0f) + (averageZ / 2.0f) + (averageZP1 / 4.0f));
        }


        public float InterpolateNoise(float x)
        {
            int integerX = (int)x;
            float fracX = x - (float)integerX;

            float v1 = SmoothNoise(integerX);
            float v2 = SmoothNoise(integerX + 1);

            return _interpolate(v1, v2, fracX);
        }
        public float InterpolateNoise(float x, float y)
        {
            int integerX = (int)x;
            float fracX = x - (float)integerX;

            int integerY = (int)y;
            float fracY = y - (float)integerY;

            float v1 = SmoothNoise(integerX, integerY);
            float v2 = SmoothNoise(integerX + 1, integerY);
            float v3 = SmoothNoise(integerX, integerY + 1);
            float v4 = SmoothNoise(integerX + 1, integerY + 1);

            float i1 = _interpolate(v1, v2, fracX);
            float i2 = _interpolate(v3, v4, fracX);

            return _interpolate(i1, i2, fracY);

        }
        public float InterpolateNoise(float x, float y, float z)
        {
            int integerX = (int)x;
            float fracX = x - (float)integerX;

            int integerY = (int)y;
            float fracY = y - (float)integerY;

            int integerZ = (int)z;
            float fracZ = z - (float)integerZ;

            float v1 = SmoothNoise(integerX, integerY, integerZ);
            float v2 = SmoothNoise(integerX + 1, integerY, integerZ);
            float v3 = SmoothNoise(integerX, integerY + 1, integerZ);
            float v4 = SmoothNoise(integerX + 1, integerY + 1, integerZ);
            float v5 = SmoothNoise(integerX, integerY, integerZ + 1);
            float v6 = SmoothNoise(integerX + 1, integerY, integerZ + 1);
            float v7 = SmoothNoise(integerX, integerY + 1, integerZ + 1);
            float v8 = SmoothNoise(integerX + 1, integerY + 1, integerZ + 1);

            float i1 = _interpolate(v1, v2, fracX);
            float i2 = _interpolate(v3, v4, fracX);
            float i3 = _interpolate(v5, v6, fracX);
            float i4 = _interpolate(v7, v8, fracX);

            float i5 = _interpolate(i1, i2, fracY);
            float i6 = _interpolate(i3, i4, fracY);

            return _interpolate(i5, i6, fracZ);
        }


        public float PerlinNoise1F(float x, float amplitude, float frequencyX)
        {
            x += 100000;

            return InterpolateNoise(x * frequencyX) * amplitude;
        }
        public float PerlinNoise2F(float x, float y, float amplitude, float frequencyX, float frequencyY)
        {
            x += 100000;
            y += 100000;

            return InterpolateNoise(x * frequencyX, y * frequencyY) * amplitude;
        }
        public float PerlinNoise3F(float x, float y, float z, float amplitude, float frequencyX, float frequencyY, float frequencyZ)
        {
            x += 100000;
            y += 100000;
            z += 100000;

            return InterpolateNoise(x * frequencyX, y * frequencyY, z * frequencyZ) * amplitude;
        }


        /**
        * Generates the 1d noise and stores it in the lookup table (for faster processing).
        * \param uiMaxX                 max x value (generates noise from 0 to uiMaxX)
        */
        void GenerateLookup(int maxX)
        {
            _maxX = maxX;
            _lookup = new float[maxX];

            for (int x = 0; x < maxX; x++)
                _lookup[x] = Noise(x);
        }


        /**
        * Generates the 2d noise and stores it in the lookup table (for faster processing).
        * \param uiMaxX                 max x value (generates noise from 0 to uiMaxX)
        * \param uiMaxY                 max y value (generates noise from 0 to uiMaxY)
        */
        void GenerateLookup(int maxX, int maxY)
        {
            _maxX = maxX;
            _maxY = maxY;
            _lookup = new float[maxX * maxY];

            int offsetY = 0;

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                    _lookup[x + offsetY] = Noise(x, y);

                offsetY += maxX;
            }
        }

        /**
        * Generates the 3d noise and stores it in the lookup table (for faster processing).
        * \param uiMaxX                 max x value (generates noise from 0 to uiMaxX)
        * \param uiMaxY                 max y value (generates noise from 0 to uiMaxY)
        * \param uiMaxZ                 max z value (generates noise from 0 to uiMaxZ)
        */
        void GenerateLookup(int maxX, int maxY, int maxZ)
        {
            _maxX = maxX;
            _maxY = maxY;
            _maxZ = maxZ;
            _lookup = new float[maxX * maxY * maxZ];

            int offsetY = 0;
            int offsetZ = 0;
            int stepZ = maxX * maxY;

            for (int z = 0; z < maxZ; z++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    for (int x = 0; x < maxX; x++)
                        _lookup[x + offsetY + offsetZ] = Noise(x, y, z);

                    offsetY += maxX;
                }

                offsetY = 0;
                offsetZ += stepZ;
            }
        }

    }

}
