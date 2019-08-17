using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GUI.Controls
{
    /// <summary>
    /// This control contains a single Texture2D and draws it.
    /// </summary>
    public class TextureContainer : Control
    {
        public Texture2D Texture;

        public TextureContainer(Texture2D Texture, int Width, int Height, WindowManager WM)
        {
            this.Texture = Texture;
            this.Width = Width;
            this.Height = Height;
            this.WM = WM;
        }

        public TextureContainer(Texture2D Texture, WindowManager WM) : this(Texture, Texture.Width, Texture.Height, WM)
        {

        }

        public override void Render(GraphicsDevice device, Renderer Renderer, int X, int Y)
        {
            Renderer.RenderTexturedQuad(device, X + this.X,Y + this.Y, this.Width, this.Height, Texture);
            base.Render(device, Renderer, X, Y);
        }
    }
}
