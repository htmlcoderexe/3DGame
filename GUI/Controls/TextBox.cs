using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Controls
{
    public class TextBox : Control, ITextInput
    {
        public string Text;

        public bool MultiLine { get; set; }

        public bool KeepFocusOnSubmit { get; set; }

        //Processes character input.
        //if backspace, deletes last character if any
        //if enter and not multiline, triggers the submit event
        //else just appends character
        public void SendCharacter(char Character)
        {
            if (Character == (char)Keys.Back && Text.Length > 0)
            {
                Text = Text.Substring(0, Text.Length - 1);
                return;
            }
            if (Character == (char)Keys.Enter && !MultiLine)
            {
                Submit();
                if(!KeepFocusOnSubmit)
                this.GetParentWindow().WM.FocusedText = null;
                return;
            }
            this.Text += Character;


        }

        public delegate void InputSubmitHandler(object sender, string Text);

        public event InputSubmitHandler OnSubmit;

        public void Submit()
        {
            OnSubmit?.Invoke(this, this.Text);
        }

        public void Clear()
        {
            this.Text = "";
        }

        public override void Render(GraphicsDevice device, Renderer Renderer, int X, int Y)
        {
            //text offset from edge of the textbox
            float offsetX = 3f;
            float offsetY = 2f;
            //use the 48x16 block, at offset 0,48, with 2px edges
            Renderer.Slice9 slice = new Renderer.Slice9(0, 48, 48, 16, 2, 2, 2, 2);
            //draw textbox background
            Renderer.RenderFrame(device, X + this.X, Y + this.Y, this.Width, this.Height, slice);
            //draw text
            Renderer.RenderSmallText(device, offsetX + X + this.X, offsetY + Y + this.Y, this.Text ?? " ", Microsoft.Xna.Framework.Color.White, false, true);
            base.Render(device, Renderer, X, Y);
        }
    }
}
