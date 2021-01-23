using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.GameplayAssets.Windows
{
    public class NPCWindow : GUI.Window
    {
        public NPCWindow(GUI.WindowManager WM,GameObject.MapEntities.Actors.NPC NPC)
        {
            this.Width = 320;
            this.Height = 512;
            GUI.Controls.RichTextDisplay greeting = new GUI.Controls.RichTextDisplay(NPC.Greeting,180, 200, WM);
            this.Controls.Add(greeting);
            this.Title = NPC.DisplayName;
            int yoffset = greeting.Height + 2;
            if (NPC.Commands!=null && NPC.Commands.Count>0)
            {
                for(int i=0;i<NPC.Commands.Count;i++)
                {
                    NPCMenuItem mi = new NPCMenuItem(NPC.Commands[i])
                    {
                        Y = yoffset
                    };
                    yoffset +=  mi.Height;
                    AddControl(mi);
                }
            }
            NPCMenuItem end = NPCMenuItem.Close();
            end.Y = yoffset;
            AddControl(end);

        }
    }
}
