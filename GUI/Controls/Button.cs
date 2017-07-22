using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GUI.Controls
{
    /// <summary>
    /// Clickable button.
    /// </summary>
    public class Button : Control
    {
        bool Hot = false;
        int textoffset = 0;
        bool md = false;
        public delegate void ClickHandler(object sender, EventArgs e);
        public event ClickHandler Clicked;
        public Button(string Label) : base()
            {
            this.Title = Label;
            }
        public override void Render(GraphicsDevice device, Renderer Renderer, int X, int Y)
        {
            WM.Renderer.RenderButton(device, X + this.X, Y + this.Y, this.Width, this.Height, this.Hot);
            float textwidth = GFXUtility.StrW(this.Title,this.WM.Renderer.UIFont);
            float offsetX = (this.Width - textwidth) / 2f;
            float offsetY = (this.Height - 16f) / 2f;
            if (md)
                textoffset = 1;
            else
            {
                textoffset = 0;
            }
            if (!Hot)
                textoffset = 0;
            Renderer.RenderSmallText(device, offsetX + X + this.X + textoffset, offsetY + Y + this.Y + textoffset, this.Title, Microsoft.Xna.Framework.Color.White, false, true);
            this.Hot = false;
        }
        public override void MouseMove(float X, float Y)
        {
            this.Hot = true;
            base.MouseMove(X, Y);
        }
        public override void MouseDown(float X, float Y)
        {
            this.md = true;
            base.MouseDown(X, Y);
        }
        public override void MouseUp(float X, float Y)
        {
            this.md = false;
            base.MouseUp(X, Y);
        }
        public override void Click(float X, float Y)
        {
            if (this.Clicked != null)
                this.Clicked(this, new EventArgs());
            base.Click(X, Y);
        }
    }
}
