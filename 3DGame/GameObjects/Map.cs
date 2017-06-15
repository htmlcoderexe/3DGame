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
        }

        public void Update(float dT)
        {
            UpdateEntities(dT);
            Terrain.Update(dT);
            
        }
        private void UpdateEntities(float dT)
        {
            float h = 0.0f;
            foreach (MapEntity e in this.Entities)
            {
                h = Terrain.GetHeight(e.Position.Truncate(), e.Position.Reference());
                e.Position.Y = h;
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
