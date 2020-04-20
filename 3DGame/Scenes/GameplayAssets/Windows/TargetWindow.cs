using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI;
using Microsoft.Xna.Framework;

namespace _3DGame.Scenes.GameplayAssets.Windows
{
    public class TargetWindow : GUI.Window
    {
        public GameObjects.MapEntities.Actors.Player Player;
        private GUI.Controls.ProgressBar HPBar;
        public override void Close()
        {

        }
        public TargetWindow(WindowManager WM, GameObjects.MapEntities.Actors.Player Player)
        {
            this.Player = Player;
            this.Width = 200;
            this.Height = 50;
            this.X = 500;
            this.HPBar = new GUI.Controls.ProgressBar();
            this.HPBar.DisplayLabel = true;
            this.HPBar.Style = 0;
            this.HPBar.Height = 16;
            this.HPBar.Width = 192;
            this.HPBar.Colour = new Color(255, 0, 80);
            this.AddControl(HPBar);
        }
        public override void Update(float dT)
        {
            if(this.Player.Target!=null && !this.Player.Target.IsDead)
            {
                this.Visible = true;
                this.HPBar.Title = ((int)(this.Player.Target.CurrentHP)).ToString() + "/" + ((int)this.Player.Target.CalculateStat("HP")).ToString();
                this.HPBar.MaxValue = (int)this.Player.Target.CalculateStat("HP");
                this.HPBar.MinValue = 0;
                this.HPBar.Value = (int)this.Player.Target.CurrentHP;
            }
            else
            {
                if (this.Player.Target!=null && this.Player.Target.IsDead)
                    this.Player.Target = null;
                this.Visible = false;
            }
        }
    }
}
