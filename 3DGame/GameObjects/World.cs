using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3DGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame.GameObjects
{
    public class World : Interfaces.IGameObject
    {
        /// <summary>
        /// List of all entities int the world
        /// </summary>
        public List<MapEntity> Entities;
        private List<MapEntity> _deadEntities;
        public Camera Camera;
        public GameObjects.MapEntities.Actos.Player Player;
        public Terrain.Terrain Terrain; //terrain.
        public static Effect ModelEffect;
        public Matrix View = Matrix.Identity;
        public float GravityAcceleration = 9.81f;
        public World(GraphicsDevice device, int Seed)
        {
            this.Entities = new List<MapEntity>();
            this._deadEntities = new List<MapEntity>();
            this.Terrain = new Terrain.Terrain(Interfaces.WorldPosition.Stride,Seed);
            
           // ModelEffect = new BasicEffect(device);
        }
        public void Render(GraphicsDevice device, float dT, Vector2 Reference,bool Alpha)
        {
            MapEntity[] cpy;
            
            
                cpy = new MapEntity[Entities.Count];
                Entities.CopyTo(0, cpy, 0, cpy.Length);
            
            for (int i = 0; i < cpy.Length; i++)
            {
                MapEntity e = cpy[i];
                if (e == null)
                    continue;

                e.Render(device, dT, Camera.Position.Reference(),false);
                
            }
            Player.Render(device, dT, Camera.Position.Reference(),false);
            Terrain.Render(device, dT, Camera.Position.Reference());
            /*
            BlendState b = new BlendState();
            b.AlphaBlendFunction = BlendFunction.Add;
            b.AlphaDestinationBlend = Blend.One;
            b.AlphaSourceBlend = Blend.SourceAlpha;
            b.ColorBlendFunction = BlendFunction.Max;
            //*/
            device.DepthStencilState = DepthStencilState.DepthRead;
            //device.BlendState = BlendState.AlphaBlend;
            device.BlendState = BlendState.Additive;
            //device.BlendState = BlendState.NonPremultiplied;
            //device.BlendState = b;
            for (int i = 0; i < cpy.Length; i++)
            {
                MapEntity e = cpy[i];
                if (e == null)
                    continue;

                e.Render(device, dT, Camera.Position.Reference(),true);
            }
            Player.Render(device, dT, Camera.Position.Reference(), true);

            device.DepthStencilState = DepthStencilState.Default;
            device.BlendState = BlendState.Opaque;
            device = null;
            cpy = null;
        }

        public void Update(float dT)
        {
            UpdateEntities(dT);
            Terrain.Update(dT);
            Player.Update(dT);
            if (Player.Gravity)
            SetGravity(Player,dT);
        }
        private void SetGravity(MapEntity e, float dT)
        {
            if(e.Gravity && !e.OnGround)
            {
                e.VerticalSpeed -= GravityAcceleration * dT;
            }
            else
            {
                e.VerticalSpeed = 0;
            }
            float h = 0.0f;
            float len = 0.2f;
            Matrix yaw = Matrix.CreateRotationY(-e.Heading * (float)Math.PI / 180f);
            h = Terrain.GetHeight(e.Position.Truncate(), e.Position.Reference());
           
            WorldPosition fp = e.Position+Vector3.Transform(new Vector3(0.1f, 0, 0), yaw);
            WorldPosition bp = e.Position + Vector3.Transform(new Vector3(-0.1f, 0, 0), yaw);
            WorldPosition lp = e.Position + Vector3.Transform(new Vector3(0, 0, -0.1f), yaw);
            WorldPosition rp = e.Position + Vector3.Transform(new Vector3(0, 0, 0.1f),  yaw);
            float f, b, l, r;

            f = Terrain.GetHeight(fp.Truncate(), fp.Reference());
            b = Terrain.GetHeight(bp.Truncate(), bp.Reference());
            l = Terrain.GetHeight(lp.Truncate(), lp.Reference());
            r = Terrain.GetHeight(rp.Truncate(), rp.Reference());

            if (e.Position.Y < h )
            {
                e.Position.Y = h;
                e.Pitch = MathHelper.ToDegrees((float)Math.Atan2((f - b), len));
                e.Roll = MathHelper.ToDegrees((float)Math.Atan2((l - r), len));
                e.OnGround = true;
            }
            else if(e.OnGround)
            {
                e.Position.Y = h;
                e.Pitch = MathHelper.ToDegrees((float)Math.Atan2((f - b), len));
                e.Roll = MathHelper.ToDegrees((float)Math.Atan2((l - r), len));

            }
            else
            {
                e.OnGround = false;
            }
        }
        private void UpdateEntities(float dT)
        {
            MapEntity e;
            int i = 0;
            while(i<this.Entities.Count)
            {
                e = this.Entities[i];
                if(e.Gravity)
                SetGravity(e,dT);
                e.Update(dT);
                i++;
            }

            RemoveDeadEntities();
        }
        private void RemoveDeadEntities()
        {
            this._deadEntities.Clear();
            foreach (MapEntity e in this.Entities)
            {
                MapEntity ed = e;
                if (ed.IsDead)
                    _deadEntities.Add(ed);
                ed = null;
            }
            foreach (MapEntity e in this._deadEntities)
            {
                MapEntity ed = e;
                this.Entities.Remove(ed);
                ed = null;
               
            }
            this._deadEntities.Clear();

        }
        public List<MapEntity> LocateNearby(MapEntity Target)
        {
            List<MapEntity> results = this.Entities.FindAll(e => !e.IsDead && e.Position.BX > Target.Position.BX - 2 && e.Position.BX < Target.Position.BX + 2 && e.Position.BY > Target.Position.BY - 2 && e.Position.BY < Target.Position.BY + 2).ToList();
            return results;
        }

    }
}
