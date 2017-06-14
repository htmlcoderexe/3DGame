using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame.GameObjects
{
    public class Camera
    {
        public Interfaces.WorldPosition Position;
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
            Matrix Rotation = Matrix.CreateRotationX(MathHelper.ToRadians(-this.Pitch)) * Matrix.CreateRotationY(MathHelper.ToRadians(-this.Yaw));
            Vector3 CamVector = new Vector3(0, 0, -this.Distance);
            Vector3 CamUp = new Vector3(0, 1, 0);
            CamVector = Vector3.Transform(CamVector, Rotation);
            CamVector += this.Position.Truncate();
            //CamVector += this.Position;
            return Matrix.CreateLookAt(CamVector, this.Position.Truncate(), CamUp);

        }
        public Matrix GetReflectedView(GraphicsDevice device, float WaterHeight)
        {
            Matrix Rotation = Matrix.CreateRotationX(MathHelper.ToRadians(-this.Pitch)) * Matrix.CreateRotationY(MathHelper.ToRadians(-this.Yaw));
            Vector3 CamVector = new Vector3(0, 0, -this.Distance);
            Vector3 CamUp = new Vector3(0, 1, 0);
            CamVector = Vector3.Transform(CamVector, Rotation);
             CamVector += this.Position.Truncate();
            //CamVector += this.Position;
            Vector3 RCV = CamVector;
            Vector3 RPV = this.Position.Truncate();
            RCV.Y = -RCV.Y + WaterHeight * 2;
            RPV.Y = -RPV.Y + WaterHeight * 2;

            return Matrix.CreateLookAt(RCV, RPV, CamUp);
        }
        public Vector3 GetMoveVector()
        {
            Matrix Rotation = Matrix.CreateRotationX(MathHelper.ToRadians(-this.Pitch)) * Matrix.CreateRotationY(MathHelper.ToRadians(-this.Yaw));
            Vector3 CamVector = new Vector3(0, 0, -this.Distance);
            Vector3 CamUp = new Vector3(0, 1, 0);
            CamVector = Vector3.Transform(CamVector, Rotation);
            CamVector.Normalize();
            return CamVector;


        }
        public Vector3 GetCamVector()
        {
            Matrix Rotation = Matrix.CreateRotationX(MathHelper.ToRadians(-this.Pitch)) * Matrix.CreateRotationY(MathHelper.ToRadians(-this.Yaw));
            Vector3 CamVector = new Vector3(0, 0, -this.Distance);
            Vector3 CamUp = new Vector3(0, 1, 0);
            CamVector = Vector3.Transform(CamVector, Rotation);
             CamVector += this.Position.Truncate();
         //   CamVector += this.Position;
            return CamVector;

        }
        public Vector3 GetUpVector()
        {
            Matrix Rotation = Matrix.CreateRotationX(MathHelper.ToRadians(-this.Pitch)) * Matrix.CreateRotationY(MathHelper.ToRadians(-this.Yaw));
            Vector3 CamUp = new Vector3(0, 1, 0);
            CamUp = Vector3.Transform(CamUp, Rotation);
            CamUp.Normalize();
            return CamUp;


        }
        public Matrix GetWorld()
        {
            return Matrix.Identity;
        }
        internal Matrix GetProjection(GraphicsDevice GraphicsDevice)
        {
            return Matrix.CreatePerspectiveFieldOfView(
           // MathHelper.ToRadians(_3drpg.Terrain.GameSettings.FOV),
           MathHelper.ToRadians(60),
            (float)GraphicsDevice.Viewport.Width /
            (float)GraphicsDevice.Viewport.Height,
            0.1f,
            1000f);
        }
    }
}
