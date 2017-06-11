using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _3DGame.Interfaces
{
    public struct WorldPosition
    {
       public const int Stride=64;
       public float X;
       public float Y;
       public float Z;
       public int BX;
       public int BY;
       public void Normalize()
        {

            this.BX += ((int)Math.Floor(X / (float)Stride));
            this.X = this.X % (float)Stride;
            this.BY += ((int)Math.Floor(Z / (float)Stride));
            this.Z = this.Z % (float)Stride;
        }
        public static WorldPosition operator +(WorldPosition a, WorldPosition b)
        {
            WorldPosition result = new WorldPosition();
            result.BX = a.BX + b.BX;
            result.BY = a.BY + b.BY;
            result.X = a.X + b.X;
            result.Y = a.Y + b.Y;
            result.Z = a.Z + b.Z;

            result.Normalize();

            return result;
        }
        public static WorldPosition operator +(WorldPosition a, Vector3 b)
        {
            WorldPosition result = new WorldPosition();
            result.BY = a.BY;
            result.BX = a.BX;
            result.X = a.X + b.X;
            result.Y = a.Y + b.Y;
            result.Z = a.Z + b.Z;

            result.Normalize();

            return result;
        }
    }
}
