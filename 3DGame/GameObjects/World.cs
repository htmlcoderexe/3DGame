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
        public static BasicEffect ModelEffect;
        public World(GraphicsDevice device)
        {
            this.Entities = new List<MapEntity>();
            this._deadEntities = new List<MapEntity>();
            this.Terrain = new Terrain.Terrain(Interfaces.WorldPosition.Stride);
            ModelEffect = new BasicEffect(device);
        }
        public void Render(GraphicsDevice device, float dT, Vector2 Reference)
        {
            Terrain.Render(device, dT,Camera.Position.Reference());
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
                
                ModelEffect.View = Camera.GetView();
                ModelEffect.Projection = Camera.GetProjection(device);
                ModelEffect.VertexColorEnabled = true;
                e.Render(device, dT, Camera.Position.Reference());
            }

            ModelEffect.View = Camera.GetView();
            ModelEffect.Projection = Camera.GetProjection(device);
            ModelEffect.VertexColorEnabled = true;
            Player.Render(device, dT, Camera.Position.Reference());
        }

        public void Update(float dT)
        {
            UpdateEntities(dT);
            Terrain.Update(dT);
            SetGravity(Player);
            Player.Update(dT);
        }
        private void SetGravity(MapEntity e)
        {
            float h = 0.0f;
            float len = 0.2f;
            Matrix yaw = Matrix.CreateRotationY(-e.Heading * (float)Math.PI / 180f);
            h = Terrain.GetHeight(e.Position.Truncate(), e.Position.Reference());
            e.Position.Y = h+0.5f;

            WorldPosition fp = e.Position+Vector3.Transform(new Vector3(0.1f, 0, 0), yaw);
            WorldPosition bp = e.Position + Vector3.Transform(new Vector3(-0.1f, 0, 0), yaw);
            WorldPosition lp = e.Position + Vector3.Transform(new Vector3(0, 0, -0.1f), yaw);
            WorldPosition rp = e.Position + Vector3.Transform(new Vector3(0, 0, 0.1f),  yaw);
            float f, b, l, r;

            f = Terrain.GetHeight(fp.Truncate(), fp.Reference());
            b = Terrain.GetHeight(bp.Truncate(), bp.Reference());
            l = Terrain.GetHeight(lp.Truncate(), lp.Reference());
            r = Terrain.GetHeight(rp.Truncate(), rp.Reference());
            e.Pitch = MathHelper.ToDegrees((float)Math.Atan2((f - b),len));
            e.Roll = MathHelper.ToDegrees((float)Math.Atan2((l - r),len));
        }
        private void UpdateEntities(float dT)
        {
            
            foreach (MapEntity e in this.Entities)
            {
                SetGravity(e);
                e.Update(dT);
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


    }
}
