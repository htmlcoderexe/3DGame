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
        public float LineHeight = 0;
        public void AddText(string Text)
        {
            this.RenderText.AppendText(Text,true);
        }
        public void SetText(string Text)
        {
            this.RenderText.SetText(Text);
        }
        public void AddLine(string Text,List<System.Action> Links=null)
        {
            this.RenderText.AppendText(Text, false,Links);
        }
        public RichTextDisplay(string Text, int Width, int Height, WindowManager WM)
        {
            this.WM = WM;
            this.RenderText = new RichText.RichTextMultiLine(Text, this.WM.Renderer.UIFont, Width);
            this.Width = Width;
            this.Height = Height;
            LineHeight = this.WM.Renderer.UIFont.MeasureString(" ").Y;

            this.RenderText.MaxCount = (int)(this.Height / LineHeight);
        }
        public RichTextDisplay(int Width, int Height, WindowManager WM)
        {
            this.WM = WM;
            this.RenderText = new RichText.RichTextMultiLine(this.WM.Renderer.UIFont, Width);
            this.Width = Width;
            this.Height = Height;
            LineHeight = this.WM.Renderer.UIFont.MeasureString(" ").Y;

            this.RenderText.MaxCount = (int)(this.Height / LineHeight);
        }
        public override void Render(GraphicsDevice device, Renderer Renderer, int X, int Y)
        {
            string line;
            float lineoffset = 0;
            float Yoffset = 0;
           // Renderer.RenderFrame(device, X + this.X, Y + this.Y, this.Width, this.Height);
            for (int i = 0; i < RenderText.Lines.Count; i++)
            {
                line = RenderText.Lines[Flip ? RenderText.Lines.Count - 1 - i : i];
                Yoffset = LineHeight * lineoffset;
                if (Flip)
                    Yoffset = this.Height - Yoffset-LineHeight;
                Renderer.RenderRichText(device, X+this.X, Y+this.Y+Yoffset, line);
                lineoffset++;
            }
            base.Render(device, Renderer, X, Y);
        }
        public int LineID(int Y)
        {
            int ID = -1;

            
                ID = (int)( Y/ LineHeight);
            if(Flip)
            {
                ID -= (this.RenderText.MaxCount - this.RenderText.Lines.Count);
            }

            if (ID >= this.RenderText.Lines.Count)
                return -1;
            return ID;
        }
        public override void Click(float X, float Y)
        {
            int ID = LineID((int)Y);
            if (ID != -1)
            {
                this.RenderText.TryAction(ID, (int)X);
            }
            base.Click(X, Y);
        }
    }
}
