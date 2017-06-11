using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame.GameObjects
{
    public class MapEntity : Interfaces.IGameObject
    {
        private bool _isDead;
        public  bool IsDead
        {
            get
            {
                return this._isDead;
            }
        }

        public void Render(GraphicsDevice device, float dT)
        {
            
        }

        public void Update(float dT)
        {
            
        }
    }
}
