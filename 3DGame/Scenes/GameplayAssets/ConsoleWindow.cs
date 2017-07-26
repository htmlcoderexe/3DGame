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
           messages = new GUI.Controls.RichTextDisplay("", this.Width - this.Margin.X - this.Margin.Width, 420, WM);
            messages.RenderText.MaxCount = 20;
           messages.Flip = true;
           AddControl(messages);
        }
        public void AppendMessage(string Message)
        {
            this.messages.AddLine(Message);
        }
    }
}
