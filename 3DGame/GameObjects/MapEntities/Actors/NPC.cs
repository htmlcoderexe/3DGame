using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.GameObjects.MapEntities.Actors
{
    public class NPC : Actor
    {
        public string Greeting;
        public NPC(string Greeting)
        {
            this.Greeting = Greeting;
        }
        public override void DoubleClick(Actor Target)
        {
            // this.Jump();
            GUI.Window w = new Scenes.GameplayAssets.Windows.NPCWindow(Scenes.Gameplay.WindowManager, this);
            Scenes.Gameplay.WindowManager.Add(w);
            w.Visible = true;
            base.DoubleClick(Target);
        }
    }
}
