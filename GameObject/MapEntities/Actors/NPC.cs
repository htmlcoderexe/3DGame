using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.MapEntities.Actors
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
            //the following is here for posterity; actual logic referring to GUI will only be used in the game, no business for it in here
            /*
            GUI.Window w = new Scenes.GameplayAssets.Windows.NPCWindow(Scenes.Gameplay.WindowManager, this);
            Scenes.Gameplay.WindowManager.Add(w);
            w.Visible = true;
            //*/
            base.DoubleClick(Target);
        }
    }
}
