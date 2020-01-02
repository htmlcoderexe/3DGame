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

        public Microsoft.Xna.Framework.Color bGColor=Microsoft.Xna.Framework.Color.CornflowerBlue;
        public SettingsFrm()
        {
            InitializeComponent();
        }

        private void bgCR_Scroll(object sender, EventArgs e)
        {
            bGColor = new Microsoft.Xna.Framework.Color(bgCR.Value,bgCG.Value,bgCB.Value);
        }
    }
}
