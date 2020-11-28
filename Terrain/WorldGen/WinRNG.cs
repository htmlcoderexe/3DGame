using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrain.WorldGen
{
    //wrapper for System.Random - will be replaced with something more stable
    public class WinRNG : IRandomProvider
    {
        public int Seed { get; set; }

        System.Random RNG;

        public WinRNG(int Seed)
        {
            this.Seed = Seed;
            RNG = new Random(Seed);
        }

        public float NextFloat()
        {
            return (float)RNG.NextDouble();
        }

        public int NextInt(int Min, int Max)
        {
            return RNG.Next(Min, Max);
        }
        public int NextInt(int Max)
        {
            return RNG.Next( Max);
        }

        public void Reset()
        {
            RNG = new Random(Seed);
        }
    }
}
