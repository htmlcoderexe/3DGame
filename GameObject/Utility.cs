using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameObject
{
    public class Utility
    {
        public static Color GetColor(string csv)
        {
            string[] rgb = csv.Split(',');
            int r = Int32.Parse(rgb[0]);
            int g = Int32.Parse(rgb[1]);
            int b = Int32.Parse(rgb[2]);
            return new Color(r, g, b);
        }
        public static int GetGrowth(string param,int Level)
        {
            string[] parts = param.Split(',');
            int a = int.Parse(parts[0]);
            int b = int.Parse(parts[1]);
            return a + b * Level;
        }
    }
}
