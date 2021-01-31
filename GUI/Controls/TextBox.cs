﻿using Microsoft.Xna.Framework.Graphics;
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

        public override void Render(GraphicsDevice device, Renderer Renderer, int X, int Y)
        {

            float offsetX = 3f;
            float offsetY = 2f;
            Renderer.RenderSmallText(device, offsetX + X + this.X, offsetY + Y + this.Y, this.Text ?? " ", Microsoft.Xna.Framework.Color.White, false, true);
            base.Render(device, Renderer, X, Y);
        }
    }
}