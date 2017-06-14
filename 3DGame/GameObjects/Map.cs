using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame.GameObjects
{
    public class World : Interfaces.IGameObject
    {
        public List<MapEntity> Entities;
        private List<MapEntity> _deadEntities;
        public GameObjects.Camera Camera;
        public Terrain.Terrain Terrain; //terrain.
        public World()
        {
            this.Entities = new List<MapEntity>();
            this._deadEntities = new List<MapEntity>();
            this.Terrain = new Terrain.Terrain(Interfaces.WorldPosition.Stride);
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
                e.Render(device, dT, Camera.Position.Reference());
                e.Model.fx.View = Camera.GetView();
                e.Model.fx.Projection = Camera.GetProjection(device);
                e.Render(device, dT, Camera.Position.Reference());
            }
        }

        public void Update(float dT)
        {
            UpdateEntities(dT);
            Terrain.Update(dT);
            
        }
        private void UpdateEntities(float dT)
        {
            foreach (MapEntity e in this.Entities)
            {
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
