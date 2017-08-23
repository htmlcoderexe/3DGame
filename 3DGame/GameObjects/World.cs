﻿using System;
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
        public World(GraphicsDevice device)
        {
            this.Entities = new List<MapEntity>();
            this._deadEntities = new List<MapEntity>();
            this.Terrain = new Terrain.Terrain(Interfaces.WorldPosition.Stride);
            
           // ModelEffect = new BasicEffect(device);
        }
        public void Render(GraphicsDevice device, float dT, Vector2 Reference)
        {
            MapEntity[] cpy;
            
            lock(_deadEntities)
            {
                cpy = new MapEntity[Entities.Count];
                Entities.CopyTo(0, cpy, 0, cpy.Length);
            }
            for (int i = 0; i < cpy.Length; i++)
            {
                MapEntity e = cpy[i];
                if (e == null)
                    continue;
                
                e.Render(device, dT, Camera.Position.Reference());
            }
            
            Player.Render(device, dT, Camera.Position.Reference());
            Terrain.Render(device, dT, Camera.Position.Reference());
        }

        public void Update(float dT)
        {
            UpdateEntities(dT);
            Terrain.Update(dT);
           if(Player.Gravity)
            SetGravity(Player,dT);
            Player.Update(dT);
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
                if (e.IsDead)
                    _deadEntities.Add(e);
            }
            foreach (MapEntity e in this._deadEntities)
            {
                this.Entities.Remove(e);
            }
        }
        public List<MapEntity> LocateNearby(MapEntity Target)
        {
            List<MapEntity> results = this.Entities.FindAll(e => e.Position.BX > Target.Position.BX - 2 && e.Position.BX < Target.Position.BX + 2 && e.Position.BY > Target.Position.BY - 2 && e.Position.BY < Target.Position.BY + 2).ToList();
            return results;
        }

    }
}