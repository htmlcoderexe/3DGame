using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GUI.Controls
{
    /// <summary>
    /// Allows input of integer numbers.
    /// </summary>
    public class NumberBox : Control, ITextInput
    {
        public bool MultiLine { get; set; }
        /// <summary>
        /// The numeric value of the input
        /// </summary>
        public int Value { get; set; }
        //string buffer for input to be potentially converted to int
        string EditedText = "";
        //if in edit mode, allow numeric input
        bool editmode = false;
        //stores previuos value while editing
        int prevValue;
        //accept numeric input when editing directly, else take +/- input
        public void SendCharacter(char Character)
        {
            if(editmode && Character>='0' && Character<='9')
            {
                EditedText += Character;
            }
            if (!editmode && Character == '+')
                Value++;
            if (!editmode && Character == '-')
                Value--;
        }

        public void Submit()
        {
            
        }

        public override void Render(GraphicsDevice device, Renderer Renderer, int X, int Y)
        {
            string displaytext = "";
            //either show the value or what we're currently editing
            displaytext = editmode?this.EditedText: this.Value.ToString();
            //right-justifying, measure string to know X offset
            float w = Renderer.UIFont.MeasureString(displaytext).X;
            //text offset from edge of the textbox
            float offsetX = this.Width-w-16;
            float offsetY = 2f;

            //use the 32x16 block, at offset 0,48, with 2px edges
            Renderer.Slice9 slice = new Renderer.Slice9(0, 48, 32, 16, 2, 2, 2, 2);
            //draw textbox background
            Renderer.RenderFrame(device, X + this.X, Y + this.Y, this.Width, this.Height, slice);
            //draw the spinner arrows
            Renderer.RenderQuad(device, X + this.X + this.Width - 16, Y + this.Y, 16, 16, new Renderer.Rect(32, 48, 16, 16));
            //draw text
            Renderer.RenderSmallText(device, offsetX + X + this.X, offsetY + Y + this.Y, displaytext, Microsoft.Xna.Framework.Color.White, false, true);
            base.Render(device, Renderer, X, Y);
        }

        public override void Click(float X, float Y)
        {
            //check if clicked in the rightmost 16 pixels where the arrows are
            if(X>this.Width-16)
            {
                //check for any input made in edit mode, adjust value if needed
                if(editmode)
                {
                    LoseFocus();
                }
                //upper half=up, lower half = down
                if(Y<8)
                {
                    Value++;
                }
                else
                {
                    Value--;
                }
            }
            //else go to edit mode to allow direct input of numbers
            else
            {
                editmode = true;
                //save previous value in case user clicks away without entering anything
                prevValue = Value;
            }
            base.Click(X, Y);
        }

        public void LoseFocus()
        {
            //reset value to previous if user didn't input anything
            if (EditedText == "")
                this.Value = prevValue;
            //otherwise set value to input
            else
            {
                //no need for tryparse as only 0..9 is guaranteed as input
                this.Value = int.Parse(EditedText);
                //reset edited text
                EditedText = "";
            }
            editmode = false;
        }
    }
}
