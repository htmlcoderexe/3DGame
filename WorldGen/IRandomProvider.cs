using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGen
{
    public interface IRandomProvider
    {
        int Seed { get; }
        void Reset();
        float NextFloat();
        int NextInt(int Min, int Max);
        int NextInt(int Max);
    }
}
