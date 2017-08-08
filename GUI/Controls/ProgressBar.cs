using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GUI.Controls
{
    public class ProgressBar : Control
    {
        /// <summary>
        /// Maximum value
        /// </summary>
        public int MaxValue { get; set; }
        /// <summary>
        /// Minimum value
        /// </summary>
        public int MinValue { get; set; }
        int _value;
        /// <summary>
        /// Current value
        /// </summary>
        public int Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = (int)Microsoft.Xna.Framework.MathHelper.Clamp(value, this.MinValue, this.MaxValue);
            }
        }

        public bool DisplayLabel { get; set; }
        /// <summary>
        /// Progress as a floating point number between 0.0 and 1.0.
        /// </summary>
        public float Progress
        {
            get
            {
                int range = this.MaxValue - this.MinValue;
                int val = this.Value - this.MinValue;
                return ((float)val / (float)range);
            }

        }
        public int Style;
        public Color Colour { get; set; }
        public override void Render(Microsoft.Xna.Framework.Graphics.GraphicsDevice device, Renderer Renderer, int X, int Y)
        {
            base.Render(device,Renderer, X, Y);
            Renderer.SetColour(this.Colour);
            Renderer.RenderBar(device, X + this.X, Y + this.Y, this.Width, this.Height, this.Progress, this.Style);
            if (this.DisplayLabel)
            {
                Renderer.RenderSmallText(device, X + this.X + (this.Width / 2f), Y + this.Y, this.Title, Color.White, true, true);
            }
        }
    }
}
