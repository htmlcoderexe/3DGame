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
        public string FileName;
        public MainAppFrm mainfrmref;
        private void playpausebutt_Click(object sender, EventArgs e)
        {
            if (ProgramState.State.Playing)
                Pause();
            else Play();
           
        }

        private void Play()
        {
            //don't do anything on null
            if (movements.SelectedIndex == 0)
                return;
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
            Stop();
        }

        public void Stop()
        {
            //reset the scrubber
            ProgramState.State.Playing = false;
            playpausebutt.Text = "▶️";
            ProgramState.State.PlayTime = 0;
            scrubber.Value = 0;
            //if currently selected no animation, do nothing else - play button is already disabled
            if (movements.SelectedIndex == 0)
                return;
            //otherwise apply animation and update scrubber
            ProgramState.State.CurrentModel.ApplyAnimation(movements.SelectedItem.ToString());
            scrubber.Maximum = (int)(ProgramState.State.CurrentModel.CurrentAnimationLength * 100f);
        }

        private void barupdater_Tick(object sender, EventArgs e)
        {
            time.Text = TicksToSeconds(scrubber.Value) + "/" + TicksToSeconds(scrubber.Maximum);
            
            if (scrubbing)
                return;
            int timevalue = (int)(ProgramState.State.PlayTime * 100f);
            if(timevalue<=scrubber.Maximum)
            scrubber.Value =timevalue;
            else
            {
                Stop();
                
            }
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

        private void ChoreoPlayer_Load(object sender, EventArgs e)
        {

        }

        private void movements_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(movements.SelectedIndex==0)
            {
                ProgramState.State.CurrentModel.ClearAnimation();
                return;
            }
            ProgramState.State.CurrentModel.ApplyAnimation(movements.SelectedItem.ToString());
        }

        private void load_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText(FileName, choreocode.Text);
            mainfrmref.CompileAndRun();

            string choreofilename = System.IO.Path.GetDirectoryName(ProgramState.State.CurrentFilename) + "\\" + ProgramState.State.CurrentModel.ChoreoName + ".mcf";
            choreocode.Text = System.IO.File.ReadAllText(choreofilename);
            FileName = choreofilename;
            movements.Items.Clear();
            movements.Items.Add("<none>");
            foreach (KeyValuePair<string, Dictionary<string, GameModel.PartAnimation>> movement in ProgramState.State.CurrentModel.Choreo)
                movements.Items.Add(movement.Key);
            movements.SelectedIndex = 0;
        }
    }
}
