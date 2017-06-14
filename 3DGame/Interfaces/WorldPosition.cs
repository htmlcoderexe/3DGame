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
       public const int Stride=32;
       public float X;
       public float Y;
       public float Z;
       public int BX;
       public int BY;
       public void Normalize()
        {

            this.BX += ((int)Math.Floor(X / (float)Stride));
            this.X = ((this.X % (float)Stride+(float)Stride)% (float)Stride);
            this.BY += ((int)Math.Floor(Z / (float)Stride));
            this.Z = ((this.Z % (float)Stride+ (float)Stride)% (float)Stride);

           
        }
        //returns only local coordinates
        public Vector3 Truncate()
        {
            return new Vector3(X, Y, Z);
        }
        public Vector2 Reference()
        {
            return new Vector2(BX, BY);
        }
        public WorldPosition WRT(WorldPosition a)
        {
            WorldPosition w = new WorldPosition();
            w.X = this.X;
            w.Y = this.Y;
            w.Z = this.Z;
            w.BX = this.BX - a.BX;
            w.BY = this.BY - a.BY;
            return w;
        }
        public Matrix CreateWorld(WorldPosition a)
        {
            return Matrix.CreateTranslation(this.WRT(a));
        }
        public Matrix CreateWorld(Vector2 Reference)
        {
            WorldPosition a = new WorldPosition();
            a.BX = (int)Reference.X;
            a.BY = (int)Reference.Y;
            return Matrix.CreateTranslation((Vector3)this.WRT(a));
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

        //conversion from and to Vector3 maps it to real coordinates. Use Truncate to get only the local coordinates.

        public static implicit operator Vector3(WorldPosition a)
        {
            return new Vector3(a.X+a.BX*Stride, a.Y, a.Z+a.BY*Stride);
        }

        public static implicit operator WorldPosition(Vector3 a)
        {
            WorldPosition result = new WorldPosition();
            result.X = a.X;
            result.Y = a.Y;
            result.Z = a.Z;
            result.Normalize();
            return result;
        }
    }
}
