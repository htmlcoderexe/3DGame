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
        public string DisplayName { get; set; }
        public Interfaces.WorldPosition Position;
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
        public virtual float Speed { get; set;}
        private float AnimationTimer;
        private float _pitch;
        private Matrix World;
        private bool _isDead;
        public MapEntity()
        {
            this.Model = new GameModel.Model();
            this.Speed = 0.0f;
            
        }
        public  bool IsDead
        {
            get
            {
                return this._isDead;
            }
        }

        public virtual void Render(GraphicsDevice device, float dT, Vector2 Reference)
        {
            Matrix W = Matrix.Identity;
            //   W *= Matrix.CreateRotationZ(MathHelper.ToRadians(this.Pitch));
            W *= Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(-this.Heading), MathHelper.ToRadians(this.Roll), MathHelper.ToRadians(this.Pitch));
            W *= this.Position.CreateWorld(Reference);
            this.Model.Render(device, dT*(Speed), W,GameObjects.World.ModelEffect);
        }

        public virtual void Update(float dT)
        {
           // this.Heading += dT*10;
            Vector3 advance = Vector3.Transform(new Vector3(dT, 0, 0),Matrix.CreateRotationY(MathHelper.ToRadians(-this.Heading)));
            this.Position += advance*Speed;
        }

        public virtual object Clone()
        {
            MapEntity e = (MapEntity)this.MemberwiseClone();
            e.Model = (GameModel.Model)this.Model.Clone();
            return e;
        }
    }
}
