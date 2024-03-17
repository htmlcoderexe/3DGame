using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameObject
{
    public class MapEntity :Interfaces.IGameObject,ICloneable
    {
        public World WorldSpawn;
        public float AnimationMultiplier = 1.0f;
        public Vector3 TargetOffset;
        public bool LetPlayOnce = false;
        public void SetPlayOnce()
        {
            aTimer = 0;
            LetPlayOnce = true;
        }
        public MapEntities.EntitySpawner Parent;
        public string DisplayName { get; set; }
        public Interfaces.WorldPosition Position;
        float aTimer = 0;
        public bool Gravity;
        public bool StickToTerrainCurvature;
        public float Heading; 
        public float Roll;
        public Action<MapEntity> DeathCallback;
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
        private Matrix _World;
        internal bool _isDead;

        public MapEntity()
        {
            this.Model = GameModel.ModelGeometryCompiler.LoadModel("dude1");
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
            this.DeathCallback?.Invoke(this);
            if(this.Model!=null)
            this.Model.Dispose();
            this.Model = null;
            this.WorldSpawn = null;
            //if it came from a spawner, reduce count of current entities so it can restock
            if (this.Parent != null)
                this.Parent.Count--;
        }
        public virtual void Render(GraphicsDevice device, float dT, Vector2 Reference,bool Alpha)
        {
            if (this.Model == null)
                return;
            Matrix W = Matrix.CreateTranslation(this.Model.Offset);
            Matrix W2 = Matrix.CreateTranslation(0, this.Position.Y, 0);
            W2 = new Matrix();
            //   W *= Matrix.CreateRotationZ(MathHelper.ToRadians(this.Pitch));
            W *= Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(-this.Heading), MathHelper.ToRadians(this.Roll), MathHelper.ToRadians(this.Pitch));
            W2 = W*W2;
            W *= this.Position.CreateWorld(Reference);
            float animationspeed = 1;
            
            // W = Matrix.CreateTranslation((this.Position.BX - Reference.X) * Interfaces.WorldPosition.Stride, this.Position.Y, (this.Position.BY - Reference.Y) * Interfaces.WorldPosition.Stride);
            aTimer += dT * (AnimationMultiplier);
            if (aTimer > this.Model.CurrentAnimationLength)
            {
                LetPlayOnce = false;
                aTimer -= this.Model.CurrentAnimationLength;
            }
            this.Model.Render(device, aTimer, W,World.ModelEffect,Alpha);
        }

        public virtual void Update(float dT)
        {
            if (IsDead)
                return;
            // this.Heading += dT*10;
            float dH = 0.1f;
            Vector3 Movevector = new Vector3(dT, 0, 0);
            Vector3 advance = Point(Movevector, OnGround || Gravity);
            Interfaces.WorldPosition next = this.Position + advance * Speed;

            float h1 = WorldSpawn.Terrain.GetHeight(this.Position.Truncate(), this.Position.Reference());
            float h2 = WorldSpawn.Terrain.GetHeight(next.Truncate(), next.Reference());
            if (h2-h1 < dH)
                this.Position = next;

            if (!OnGround && Gravity)
            this.Position.Y+= this.VerticalSpeed*dT;
            if(this.Model!=null)
            {
            //    if (this.Speed == 0)
                   // this.Model.ApplyAnimation("Straighten");
            //    else
                   // this.Model.ApplyAnimation("Walk");
            }
        }

        public virtual object Clone()
        {
            MapEntity e = (MapEntity)this.MemberwiseClone();
            e.Model = (GameModel.Model)this.Model.Clone();
            return e;
        }
        public virtual void Click(MapEntities.Actor Target)
        {

        }
        public virtual void DoubleClick(MapEntities.Actor Target)
        {

        }

        public void Aim(MapEntity e,bool flatten=true)
        {
            Aim(e.Position+TargetOffset,flatten);
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
