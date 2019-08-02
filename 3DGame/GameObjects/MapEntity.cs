using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame.GameObjects
{
    public class MapEntity : Interfaces.IGameObject,ICloneable
    {
        public World WorldSpawn;
        public MapEntities.EntitySpawner Parent;
        public string DisplayName { get; set; }
        public Interfaces.WorldPosition Position;
        public bool Gravity;
        public float Heading;
        public float Roll;
        public float Pitch
        {
            set
            {
                _pitch = MathHelper.Clamp(value, -89f, 89f);
            }
            get
            {
                return MathHelper.Clamp(_pitch, -89f, 89f);
            }
        }
        public float DefaultSpeed=5.0f; //m/s
        public GameModel.Model Model;
        public virtual float Speed { get; set; }
        public float VerticalSpeed;
        private bool _onGround;
        public bool OnGround
        {
            get
            {
                return _onGround;
            }
            set
            {
                _onGround = value;
                if (value)
                    JumpCount = 0;
            }
        }
        public float JumpStrength=5;
        public int MaxJumps;

        private int JumpCount;
        private float AnimationTimer;
        private float _pitch;
        private Matrix World;
        private bool _isDead;

        public MapEntity()
        {
            this.Model = new GameModel.Model();
            this.Speed = 0.0f;
            this.Gravity = true;
            this.MaxJumps = 2;
        }
        public void Jump()
        {
            if(this.JumpCount<this.MaxJumps)
            {
                this.VerticalSpeed = this.JumpStrength;
                this.OnGround = false;
                JumpCount++;
            }
        }
        public  bool IsDead
        {
            get
            {
                return this._isDead;
            }
        }
        public virtual void Die()
        {
            this._isDead = true;
            if(this.Model!=null)
            this.Model.Dispose();
            this.Model = null;
            this.WorldSpawn = null;
        }
        public virtual void Render(GraphicsDevice device, float dT, Vector2 Reference,bool Alpha)
        {
            if (this.Model == null)
                return;
            Matrix W = Matrix.Identity;
            Matrix W2 = Matrix.CreateTranslation(0, this.Position.Y, 0);
            W2 = new Matrix();
            //   W *= Matrix.CreateRotationZ(MathHelper.ToRadians(this.Pitch));
            W *= Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(-this.Heading), MathHelper.ToRadians(this.Roll), MathHelper.ToRadians(this.Pitch));
            W2 = W*W2;
            W *= this.Position.CreateWorld(Reference);
           
           // W = Matrix.CreateTranslation((this.Position.BX - Reference.X) * Interfaces.WorldPosition.Stride, this.Position.Y, (this.Position.BY - Reference.Y) * Interfaces.WorldPosition.Stride);
            this.Model.Render(device, dT*(Speed), W,GameObjects.World.ModelEffect,Alpha);
        }

        public virtual void Update(float dT)
        {
            // this.Heading += dT*10;
            Vector3 Movevector = new Vector3(dT, 0, 0);
            Vector3 advance = Point(Movevector, OnGround || Gravity);
            this.Position += advance*Speed;
            if(!OnGround && Gravity)
            this.Position.Y+= this.VerticalSpeed*dT;
        }

        public virtual object Clone()
        {
            MapEntity e = (MapEntity)this.MemberwiseClone();
            e.Model = (GameModel.Model)this.Model.Clone();
            return e;
        }
        public virtual void Click(_3DGame.GameObjects.MapEntities.Actor Target)
        {

        }

        public void Aim(MapEntity e,bool flatten=true)
        {
            Aim(e.Position,flatten);
        }
        public void Aim(Interfaces.WorldPosition Target,bool flatten)
        {
            Interfaces.WorldPosition diff = Target - this.Position;
            Vector3 v = diff;
            v.Normalize();
            this.Heading = (float)(Math.Atan2(v.X, v.Z) / MathHelper.Pi * -180f) + 90f;
            if(!flatten)
            {
                this.Pitch = (float)(Math.Asin(-v.Y) / MathHelper.Pi * -180f);
            }
        }

        public Vector3 Point(Vector3 Offset,bool flatten=true)
        {
            if(flatten)
                return Vector3.Transform(Offset, Matrix.CreateRotationY(MathHelper.ToRadians(-this.Heading)));
            return Vector3.Transform(Vector3.Transform(Offset, Matrix.CreateRotationZ(MathHelper.ToRadians(this.Pitch))), Matrix.CreateRotationY(MathHelper.ToRadians(-this.Heading)));
        }
    }
}
