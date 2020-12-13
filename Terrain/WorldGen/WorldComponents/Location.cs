using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrain.WorldGen.WorldComponents
{
    public struct Location
    {
        public enum LocationType
        {
            None,
            Town,
            City,
            River,
            Mountain,
            Plain,
            Desert,
            Island,
            Bridge,
            Ocean,
            Forest
        }
        public string Name;
        public LocationType Type;
        public bool Safe;
        public int FactionID;

    }
}
