using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame.GameObjects
{
    public class Map : Interfaces.IGameObject
    {
        public List<MapEntity> Entities;
        private List<MapEntity> _deadEntities;
        public Terrain.Terrain Terrain; //terrain.
        public Map()
        {
            this.Entities = new List<MapEntity>();
            this._deadEntities = new List<MapEntity>();
            this.Terrain = new Terrain.Terrain(Interfaces.WorldPosition.Stride);
        }
        public void Render(GraphicsDevice device, float dT)
        {
            Terrain.Render(device, dT);
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
