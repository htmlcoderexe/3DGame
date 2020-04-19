using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.GameplayAssets.Windows
{
    public class NPCWindow : GUI.Window
    {
        public NPCWindow(GUI.WindowManager WM,GameObjects.MapEntities.Actors.NPC NPC)
        {
            this.Width = 320;
            this.Height = 512;
            GUI.Controls.RichTextDisplay greeting = new GUI.Controls.RichTextDisplay(NPC.Greeting,180, 200, WM);
            this.Controls.Add(greeting);
            this.Title = NPC.DisplayName;
        }
    }
}
