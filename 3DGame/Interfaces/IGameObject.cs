using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame.Interfaces
{
    public interface IGameObject
    {

        void Render(GraphicsDevice device, float dT, Vector2 Reference,bool Alpha);
        void Update(float dT);
    }
}
