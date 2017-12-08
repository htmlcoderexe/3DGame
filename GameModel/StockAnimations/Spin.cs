using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameModel.StockAnimations
{
    public class Spin : PartAnimation
    {
        public enum SpinAxis
        {
            X,Y,Z
        }
        public Spin(float SpinTime, SpinAxis Axis)
        {
            Matrix A, B, C;
            switch(Axis)
            {
                case SpinAxis.X:
                    {
                        A = Matrix.CreateRotationX(0);
                        B = Matrix.CreateRotationX(MathHelper.ToRadians(120f));
                        C = Matrix.CreateRotationX(MathHelper.ToRadians(240f));
                        break;
                    }
                default:
                    {
                        A = Matrix.CreateRotationX(0);
                        B = Matrix.CreateRotationX(MathHelper.ToRadians(120f));
                        C = Matrix.CreateRotationX(MathHelper.ToRadians(240f));
                        break;
                    }
                case SpinAxis.Y:
                    {
                        A = Matrix.CreateRotationY(0);
                        B = Matrix.CreateRotationY(MathHelper.ToRadians(120f));
                        C = Matrix.CreateRotationY(MathHelper.ToRadians(240f));
                        break;
                    }
                case SpinAxis.Z:
                    {
                        A = Matrix.CreateRotationZ(0);
                        B = Matrix.CreateRotationZ(MathHelper.ToRadians(120f));
                        C = Matrix.CreateRotationZ(MathHelper.ToRadians(240f));
                        break;
                    }
            }
            this.Add(A, 0f);
            this.Add(B, SpinTime*1f / 3f);
            this.Add(C, SpinTime * 2f / 3f);
            this.Add(A, SpinTime * 3f / 3f);
            this.Loop = true;
        }
    }
}
