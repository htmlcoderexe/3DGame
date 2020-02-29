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
    public partial class ChoreoPlayer : Form
    {
        public ChoreoPlayer()
        {
            InitializeComponent();
        }

        private void playpausebutt_Click(object sender, EventArgs e)
        {
            ProgramState.State.Playing = !ProgramState.State.Playing;
            this.Text = this.Text == "▶️" ? "||" : "▶️";
        }

        private void stop_Click(object sender, EventArgs e)
        {
            ProgramState.State.Playing = false;
            ProgramState.State.PlayTime = 0;
        }
    }
}
