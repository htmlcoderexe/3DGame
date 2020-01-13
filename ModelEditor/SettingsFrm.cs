using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelEditor
{
    public partial class SettingsFrm : Form
    {
        SettingsContainer Settings;
       // public Microsoft.Xna.Framework.Color bGColor=Microsoft.Xna.Framework.Color.CornflowerBlue;
        public SettingsFrm(SettingsContainer Settings)
        {
            InitializeComponent();
            this.Settings = Settings;
            bgCR.Value = Settings.ViewerBackgroundColor.R;
            bgCG.Value = Settings.ViewerBackgroundColor.G;
            bgCB.Value = Settings.ViewerBackgroundColor.B;

            colprev.BackColor = Color.FromArgb(bgCR.Value, bgCG.Value, bgCB.Value);
            coltxt.Text = bgCR.Value + ", " + bgCG.Value + ", " + bgCB.Value;
        }

        private void bgCR_Scroll(object sender, EventArgs e)
        {
            Settings.ViewerBackgroundColor = new Microsoft.Xna.Framework.Color(bgCR.Value,bgCG.Value,bgCB.Value);
            colprev.BackColor = Color.FromArgb(bgCR.Value, bgCG.Value, bgCB.Value);
            coltxt.Text= bgCR.Value+", "+bgCG.Value+", "+bgCB.Value;

        }
        
    }
}
