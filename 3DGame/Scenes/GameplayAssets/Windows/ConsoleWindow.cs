using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.GameplayAssets
{
    public class ConsoleWindow :GUI.Window
    {
        GUI.Controls.RichTextDisplay messages;
        public ConsoleWindow(GUI.WindowManager WM)
        {
            this.WM = WM;
            this.Width = 320;
            this.Height = 436;
            this.messages = new GUI.Controls.RichTextDisplay("", this.Width - this.Margin.X - this.Margin.Width, this.Height-this.Margin.Y-this.Margin.Height-20, WM);
            this.messages.Flip = true;
            this.messages.AddLine("Click ^BEGINLINK ^0000E7 here ^ENDLINK ^FFFFFF to write a ^BEGINLINK ^0000E7 message! Actually, the rest of this text as well. ^ENDLINK hahaha", new List<Action> { new Action(() => { Console.Write("^FF0000 It works!"); }), new Action(() => { Console.Write("^FFFF00 Hidden link"); }) });
            AddControl(messages);
            GUI.Controls.TextBox textBox = new GUI.Controls.TextBox();
            textBox.Height = 20;
            textBox.Width = this.Width - this.Margin.X - this.Margin.Width;
            textBox.Y = messages.Height;
            textBox.OnSubmit += TextBox_OnSubmit;
            textBox.KeepFocusOnSubmit = true;
            AddControl(textBox);
            this.AnchorBottom = true;
            
        }

        private void TextBox_OnSubmit(object sender, string Text)
        {
            Console.Write("Player: "+Text);
            (sender as GUI.Controls.TextBox)?.Clear();
        }

        public void AppendMessage(string Message, List<System.Action> Links)
        {
            this.messages.AddLine(Message, Links);
        }
        public void AppendMessage(string Message)
        {
            this.messages.AddLine(Message);
        }
    }
}
