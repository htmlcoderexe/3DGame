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

        private bool scrubbing;

        private void playpausebutt_Click(object sender, EventArgs e)
        {
            if (ProgramState.State.Playing)
                Pause();
            else Play();
           
        }

        private void Play()
        {
            playpausebutt.Text = "||";
            ProgramState.State.Playing = true;
            barupdater.Enabled = true;
            scrubber.Maximum = (int)(ProgramState.State.CurrentModel.CurrentAnimationLength * 100f);
        }

        private void Pause()
        {
            playpausebutt.Text = "▶️";
            ProgramState.State.Playing = false;
            barupdater.Enabled = false;
        }
        private void stop_Click(object sender, EventArgs e)
        {
            ProgramState.State.Playing = false;
            ProgramState.State.PlayTime = 0;
        }

        private void barupdater_Tick(object sender, EventArgs e)
        {
            time.Text = TicksToSeconds(scrubber.Value) + "/" + TicksToSeconds(scrubber.Maximum);
            
            if (scrubbing)
                return;
            scrubber.Value = (int)(ProgramState.State.PlayTime*100f);
        }

        private string TicksToSeconds(int maximum)
        {
            int seconds = (int)Math.Floor((float)maximum / 100f);
            int ticks = maximum % 100;
            return seconds + ":" + ticks;
        }

        private void scrubber_MouseDown(object sender, MouseEventArgs e)
        {
            if(ProgramState.State.Playing)
            {
                scrubbing = true;
                ProgramState.State.Playing = false;
            }
            
        }

        private void scrubber_MouseUp(object sender, MouseEventArgs e)
        {
            if (scrubbing)
                ProgramState.State.Playing = true;
           scrubbing = false;
        }

        private void scrubber_Scroll(object sender, EventArgs e)
        {
            ProgramState.State.PlayTime = scrubber.Value/100f;
            time.Text = TicksToSeconds(scrubber.Value) + "/" + TicksToSeconds(scrubber.Maximum);
        }
    }
}
