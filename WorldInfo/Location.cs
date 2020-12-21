using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldInfo
{
    public struct Location
    {
        public enum LocationType
        {
            None,
            Town,
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
        public string Modifier;
        public LocationType Type;
        public bool Safe;
        public int Level;
        public int FactionID;
        static Location _default = new Location {
            Type = LocationType.None,
            Name = "Unknown"
        };
        public static Location Unknown { get { return _default; } }

    }
}
