using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameObject.Interfaces
{
    public interface IGameScene : IDisposable
    {
        MouseState PreviousMouseState { get; set; }
        KeyboardState PreviousKbState { get; set; }
        void Render(GraphicsDevice device, float dT);
        void Update(float dT);
        void HandleInput(GraphicsDevice device, MouseState mouse, KeyboardState kb, float dT);
        void Init(GraphicsDevice device,ContentManager content);
        void ScreenResized(GraphicsDevice device);
    }
}
