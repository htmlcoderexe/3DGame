using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame.GameObjects
{
    public class MapEntity : Interfaces.IGameObject
    {
        public Interfaces.WorldPosition Position;
        private Matrix World;
        private bool _isDead;
        public GameModel.Model Model;
        public MapEntity()
        {
            this.Model = new GameModel.Model();
        }
        public  bool IsDead
        {
            get
            {
                return this._isDead;
            }
        }

        public void Render(GraphicsDevice device, float dT, Vector2 Reference)
        {
            Matrix W = this.Position.CreateWorld(Reference);
            this.Model.Render(device, dT, W,GameObjects.World.ModelEffect);
        }

        public void Update(float dT)
        {
          
        }
    }
}
