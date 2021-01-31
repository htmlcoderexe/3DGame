using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.GameplayAssets.Windows
{
    public class MultiChoiceCommand
    {
        public string Text;
        public int Icon;
        public Action<GameObject.MapEntities.Actor, GameObject.MapEntities.Actor> Command; //first is the clicker (player), second is the NPC (usually)
        public MultiActionChoice Parent;
    }
    public class MultiActionChoice : GUI.Control
    {
        public List<MultiChoiceCommand> Choices;
        public MultiActionChoice()
        {
            this.WM = Main.CurrentScene.WindowManager;
            this.Width = 300;
            this.Choices = new List<MultiChoiceCommand>();
        }
    }
}
