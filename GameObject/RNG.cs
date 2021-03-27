using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    public class RNG
    {
        private static System.Random _random;
        public static void Init(int Seed)
        {
            _random = new Random(Seed);
        }
        public static void Init()
        {
            _random = new Random();
        }
        public static int Next(int max)
        {
            if (_random == null)
                Init();
            return _random.Next(max);
        }
        public static int Next(int min, int max)
        {
            if (_random == null)
                Init();
            return _random.Next(min,max);
        }
        public static double NextDouble()
        {
            if (_random == null)
                Init();
            return _random.NextDouble();
        }

        public static int PickWeighted(List<int> Inputs)
        {
            int Total = Inputs.Sum();
            int pick = Next(Total);
            if (pick == 0)
                return 0;
            int running = 0;
            for(int i=0;i<Inputs.Count();i++)
            {
                running += Inputs[i];
                if (pick < running)
                    return i;
            }
            return -1;//should not happen but well
        }
    }
}
