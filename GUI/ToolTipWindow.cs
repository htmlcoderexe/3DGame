using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GUI
{
    public class ToolTipWindow : Window
    {
        private Controls.RichTextDisplay TextDisplay;
        
        public void SetText(string Text)
        {
            TextDisplay.SetText(Text);
        }
        public ToolTipWindow(WindowManager WM, string Text, int X, int Y, bool SelfDismissing = true)
        {
            this.Width = 256;

            this.SelfDismissing = SelfDismissing;
            TextDisplay = new GUI.Controls.RichTextDisplay(Text, this.Width - this.Margin.X - this.Margin.Width, (int)WM.Screen.Y, WM);
            TextDisplay.X = 0;
            TextDisplay.Y = 0;
            this.AddControl(TextDisplay);
            this.Margin.Y = this.Margin.Height;
        }
        public ToolTipWindow(WindowManager WM, List<string> Text, int X, int Y, bool SelfDismissing = true)
        {
            this.Width = 256;
            this.X = X;
            this.Y = Y;
            this.SelfDismissing = SelfDismissing;
            TextDisplay = new GUI.Controls.RichTextDisplay( this.Width - this.Margin.X - this.Margin.Width, (int)WM.Screen.Y, WM);
            foreach (string line in Text)
                TextDisplay.AddLine(line);
            TextDisplay.X = 0;
            TextDisplay.Y = 0;
            this.AddControl(TextDisplay);
            this.Margin.Y = this.Margin.Height;
        }
        public bool SelfDismissing;
        public override void Render(GraphicsDevice device, Renderer Renderer, int X, int Y)
        {
            this.Height = this.Margin.Y + this.Margin.Height +(int)(this.TextDisplay.RenderText.Lines.Count * this.TextDisplay.LineHeight);
            Renderer.Slice9 slice = new Renderer.Slice9(0, 80, 48, 48, 5, 5, 5, 5);
            Renderer.RenderFrame(device, this.X, this.Y, this.Width, this.Height,slice);
            foreach (Control c in this.Controls)
            {
                c.Render(device, Renderer, this.X + X+this.Margin.X, this.Y + Y+this.Margin.Y);
            }
        }
        public override void Update(float dT)
        {
            if (WM.MouseStillSeconds == 0 && SelfDismissing)
                this.Close();
        }
    }
}
