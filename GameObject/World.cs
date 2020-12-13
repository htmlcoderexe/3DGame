using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObject.Interfaces;
using GameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameObject
{
    public class World : IGameObject
    {
        /// <summary>
        /// List of all entities int the world
        /// </summary>
        public List<MapEntity> Entities;
        private List<MapEntity> _deadEntities;
        public Camera Camera;
        public MapEntities.Actors.Player Player;
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
            BoundingFrustum F = new BoundingFrustum(Camera.GetView() * Camera.GetProjection(device));
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
            Terrain.Render(device, dT, Camera.Position.Reference(),F);
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
        /// <summary>
        /// Drops the entity if it is not flying and or sticks it to the terrain
        /// </summary>
        /// <param name="e">Entity</param>
        /// <param name="dT">Time amount</param>
        private void SetGravity(MapEntity e, float dT)
        {
            if(e.Gravity && !e.OnGround)
            {
                //non-flying and not stuck to terrain
                e.VerticalSpeed -= GravityAcceleration * dT;
            }
            else
            {
                //for now set to 0, eventually only do it for stuck entities - fliers may use this
                e.VerticalSpeed = 0;
            }
            float h = 0.0f;
            float len = 0.2f;
            //get the horisontal angle in rad
            Matrix yaw = Matrix.CreateRotationY(-e.Heading * (float)Math.PI / 180f);
            //center point height
            h = Terrain.GetHeight(e.Position.Truncate(), e.Position.Reference());
            //sample a cross shape of heights around centre to determine tilting for entities that follow terrain curve
            WorldPosition fp = e.Position+Vector3.Transform(new Vector3(0.1f, 0, 0), yaw);
            WorldPosition bp = e.Position + Vector3.Transform(new Vector3(-0.1f, 0, 0), yaw);
            WorldPosition lp = e.Position + Vector3.Transform(new Vector3(0, 0, -0.1f), yaw);
            WorldPosition rp = e.Position + Vector3.Transform(new Vector3(0, 0, 0.1f),  yaw);
            float f, b, l, r;

            f = Terrain.GetHeight(fp.Truncate(), fp.Reference());
            b = Terrain.GetHeight(bp.Truncate(), bp.Reference());
            l = Terrain.GetHeight(lp.Truncate(), lp.Reference());
            r = Terrain.GetHeight(rp.Truncate(), rp.Reference());
            //if entity is lower than terrain, set it to terrain
            if (e.Position.Y < h )
            {
                e.Position.Y = h;
                e.Pitch = MathHelper.ToDegrees((float)Math.Atan2((f - b), len));
                e.Roll = MathHelper.ToDegrees((float)Math.Atan2((l - r), len));
                e.OnGround = true;
            }
            //if entity is stuck to ground, keep it stuck and aligned
            else if(e.OnGround)
            {
                e.Position.Y = h;
                e.Pitch = MathHelper.ToDegrees((float)Math.Atan2((f - b), len));
                e.Roll = MathHelper.ToDegrees((float)Math.Atan2((l - r), len));

            }
            //anything else
            else
            {
                e.Roll = 0;
                e.Pitch = 0;
                e.OnGround = false;
            }
            //reset if not sticking to terrain: #TODO refactor the branching so we don't have to do it
            if(!e.StickToTerrainCurvature)
            {

                e.Roll = 0;
                e.Pitch = 0;
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
