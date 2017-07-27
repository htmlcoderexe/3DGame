using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI;

namespace _3DGame.Scenes.GameplayAssets
{
    public class StatusWindow : GUI.Window
    {
        public GUI.Controls.Button OKButton;
        public GUI.Controls.RichTextDisplay Texst;
        public StatusWindow(WindowManager WM)
            {
            this.WM = WM;
            this.Width = 360;
            this.Height = 200;
            this.Title = "Status";
            this.OKButton = new GUI.Controls.Button("Test button");
            this.OKButton.Clicked += OKButton_Clicked;
            this.OKButton.Width = 128;
            this.OKButton.Height = 48;
            this.AddControl(this.OKButton);
            string plagueis = "Did you ever hear the tragedy of ^FF0000 Darth Plagueis ^FFFFFF The Wise? I thought not. It's not a story the Jedi would tell you. It's a Sith legend. ^00A000 Darth Plagueis was a Dark Lord of the Sith, so powerful and so wise ^FFFFFF he could use the Force to influence the midichlorians to create life... He had such a knowledge of the dark side that he could even keep the ones he cared about from dying. The dark side of the Force is a pathway to many abilities some consider to be unnatural. He became so powerful... the only thing he was afraid of was losing his power, which eventually, of course, he did. Unfortunately, he taught his apprentice everything he knew, then his apprentice killed him in his sleep. Ironic. He could save others from death, but not himself.";
            //plagueis = "bepis ";
            Texst = new GUI.Controls.RichTextDisplay(plagueis, 256, 64, WM);
            Texst.X = 0;
            Texst.Y = 52;
            Texst.Flip = true;
            this.AddControl(Texst);
        }

        private void OKButton_Clicked(object sender, EventArgs e)
        {
            this.Title = "Sex";

            OKButton.Title = e.ToString();

            return;
            System.Random r = new Random();
            if (r.NextDouble()>0.5)
                this.Texst.AddText(" ^00FF00 green text ");
            else
                this.Texst.AddText(" ^FF0000 red text ");
        }
    }
}
