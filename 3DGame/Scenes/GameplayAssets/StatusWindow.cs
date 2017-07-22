using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.GameplayAssets
{
    public class StatusWindow : GUI.Window
    {
        public GUI.Controls.Button OKButton;
        public StatusWindow()
            {
            this.Width = 200;
            this.Height = 200;
            this.Title = "Status";
            this.OKButton = new GUI.Controls.Button("Test button");
            this.OKButton.Clicked += OKButton_Clicked;
            this.OKButton.Width = 128;
            this.OKButton.Height = 48;
            this.AddControl(this.OKButton);
            }

        private void OKButton_Clicked(object sender, EventArgs e)
        {
            this.Title = "Sex";
        }
    }
}
