using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GUI.Controls
{
    public class RichTextDisplay : Control
    {
        public RichText.RichTextMultiLine RenderText;
        public bool Flip;
        public string Text
        {
            get;
            set;
        }
        public void AddText(string Text)
        {
            this.RenderText.AppendText(Text);
        }
        public void AddLine(string Text)
        {
            this.RenderText.AppendText(Text, false);
        }
        public RichTextDisplay(string Text, int Width, int Height,WindowManager WM)
        {
            this.WM = WM;
            this.RenderText = new RichText.RichTextMultiLine(Text, this.WM.Renderer.UIFont, Width);
            this.Width = Width;
            this.Height = Height;
        }
        public override void Render(GraphicsDevice device, Renderer Renderer, int X, int Y)
        {
            string line;
            float lineoffset = 0;
            float Yoffset = 0;
            float LineHeight = 0;
            Renderer.RenderFrame(device, X + this.X, Y + this.Y, this.Width, this.Height);
            for (int i = 0; i < RenderText.Lines.Count; i++)
            {
                line = RenderText.Lines[Flip ? RenderText.Lines.Count - 1 - i : i];
                LineHeight = Renderer.UIFont.MeasureString(line).Y;
                Yoffset = LineHeight * lineoffset;
                if (Flip)
                    Yoffset = this.Height - Yoffset-LineHeight;
                Renderer.RenderRichText(device, X+this.X, Y+this.Y+Yoffset, line);
                lineoffset++;
            }
            base.Render(device, Renderer, X, Y);
        }
    }
}
