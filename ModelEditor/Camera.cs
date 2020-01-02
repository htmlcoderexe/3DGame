using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEditor
{

    public class Camera : ICloneable
    {
        public Vector3 Position;
        // public Vector3 Position;
        public float Pitch;
        public float Yaw;
        public float Distance;
        public Camera()
        {
            this.Position = new Vector3(0, 0, 0);
            this.Yaw = MathHelper.ToDegrees(MathHelper.PiOver2);
            this.Pitch = MathHelper.ToDegrees(-MathHelper.Pi / 10.0f);
            this.Distance = 1;
        }
        public Matrix GetView()
        {
            Vector3 CamUp = new Vector3(0, 1, 0);
            Vector3 CamVector = this.GetCamVector();
            //CamVector += this.Position.Truncate();
            return Matrix.CreateLookAt(CamVector, this.Position, GetUpVector());

        }
        public Matrix GetReflectedView(GraphicsDevice device, float WaterHeight)
        {
            Matrix Rotation = Matrix.CreateRotationX(MathHelper.ToRadians(-this.Pitch)) * Matrix.CreateRotationY(MathHelper.ToRadians(-this.Yaw));
            Vector3 CamVector = new Vector3(0, 0, -this.Distance);
            Vector3 CamUp = new Vector3(0, 1, 0);
            CamVector = Vector3.Transform(CamVector, Rotation);
            CamVector += this.Position;
            //CamVector += this.Position.Truncate();
            CamVector = this.GetCamVector();
            Vector3 RCV = CamVector;
            Vector3 RPV = this.Position;
            RCV.Y = -RCV.Y + WaterHeight * 2;
            RPV.Y = -RPV.Y + WaterHeight * 2;

            return Matrix.CreateLookAt(RCV, RPV, GetUpVector());
        }
        public Vector3 GetMoveVector()
        {
            Matrix Rotation = Matrix.CreateRotationX(MathHelper.ToRadians(-this.Pitch)) * Matrix.CreateRotationY(MathHelper.ToRadians(-this.Yaw));
            Vector3 CamVector = new Vector3(0, 0, -this.Distance);
            CamVector = Vector3.Transform(CamVector, Rotation);
            CamVector.Normalize();
            return CamVector;


        }
        public Vector3 GetCamVector()
        {
            Matrix Rotation = Matrix.CreateRotationX(MathHelper.ToRadians(-this.Pitch)) * Matrix.CreateRotationY(MathHelper.ToRadians(-this.Yaw));
            Vector3 CamVector = new Vector3(0, 0, -this.Distance);
            CamVector = Vector3.Transform(CamVector, Rotation);
            CamVector += this.Position;
            return CamVector;

        }
        public Vector3 GetUpVector()
        {
            Matrix Rotation = Matrix.CreateRotationX(MathHelper.ToRadians(-this.Pitch)) * Matrix.CreateRotationY(MathHelper.ToRadians(-this.Yaw));
            Vector3 CamUp = new Vector3(0, 1, 0);
            CamUp = Vector3.Transform(CamUp, Rotation);
            CamUp.Normalize();
            return Vector3.Up;
            return CamUp;


        }
        public Matrix GetWorldDeprecated()
        {
            Matrix Rotation = Matrix.CreateRotationX(MathHelper.ToRadians(-this.Pitch)) * Matrix.CreateRotationY(MathHelper.ToRadians(this.Yaw));
            Vector3 CamVector = new Vector3(0, 0, -this.Distance);
            CamVector = Vector3.Transform(CamVector, Rotation);
            Matrix result = Matrix.CreateTranslation(CamVector);
            //CamVector = Vector3.Transform(CamVector, Rotation);
            return Matrix.Identity;
            return result;
        }
        internal Matrix GetProjection(GraphicsDevice GraphicsDevice)
        {
            return Matrix.CreatePerspectiveFieldOfView(
           // MathHelper.ToRadians(_3drpg.Terrain.GameSettings.FOV),
           MathHelper.ToRadians(30),
            (float)GraphicsDevice.Viewport.Width /
            (float)GraphicsDevice.Viewport.Height,
            0.5f,
            3000f);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
