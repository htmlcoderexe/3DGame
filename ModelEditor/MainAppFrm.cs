using GameModel;
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
    public partial class MainAppFrm : Form
    {
        public ChoreoPlayer p;
        public string FileName;
        private bool _changes;
        private bool Changes {
            get { return _changes; }
            set { _changes = value;this.Text = value ? "* Model Editor" : "Model Editor"; }
        }
        public MainAppFrm()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
            ProgramState.State.Settings = LoadSettings();
             p = new ChoreoPlayer();
            p.mainfrmref = this;
        }

        public SettingsContainer LoadSettings()
        {
            SettingsContainer result = new SettingsContainer();
            result.ViewerBackgroundColor = new Microsoft.Xna.Framework.Color(0, 127, 127);

            return result;
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsFrm s = new SettingsFrm(ProgramState.State.Settings);
            SettingsContainer old = (SettingsContainer)ProgramState.State.Settings.Clone();
            DialogResult d = s.ShowDialog();
            if (d != DialogResult.OK)
                ProgramState.State.Settings = old;

        }

        private void MainAppFrm_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fopendlg = new OpenFileDialog();
            fopendlg.AddExtension = true;
            fopendlg.Filter = "Model geometry files (*.mgf)|*.mgf|Any File|*.*";
            fopendlg.RestoreDirectory = true;
            if (fopendlg.ShowDialog() == DialogResult.OK)
                OpenModel(fopendlg.FileName);
            
        }

        private void OpenModel(string filename)
        {
            FileName = filename;
            ProgramState.State.CurrentFilename = filename;
            Model result;
            string modeldata = System.IO.File.ReadAllText(filename);
            modelcode.Text = modeldata;
            ModelGeometryCompiler.ModelBaseDir = System.IO.Path.GetDirectoryName(filename);
            ModelGeometryCompiler compiler = new ModelGeometryCompiler(modeldata);
            result = compiler.ReturnOutput();
            result.RebuildSkeleton();
            //result.ApplyAnimation("Walk");
            ProgramState.State.CurrentModel = result;
            string choreofilename = ModelGeometryCompiler.ModelBaseDir + "\\" + result.ChoreoName + ".mcf";
            p.choreocode.Text = System.IO.File.ReadAllText(choreofilename);
            p.FileName = choreofilename;
            p.movements.Items.Clear();
            p.movements.Items.Add("<none>");
            foreach (KeyValuePair<string, Dictionary<string,PartAnimation>> movement in result.Choreo)
                p.movements.Items.Add(movement.Key);
            p.movements.SelectedIndex = 0;
            Changes = false;
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CompileAndRun();
        }

        public void CompileAndRun()
        {
            ModelGeometryCompiler compiler = new ModelGeometryCompiler(modelcode.Text);
            GameModel.Model result = compiler.ReturnOutput();
            result.RebuildSkeleton();
            result.ApplyAnimation("Walk");
            ProgramState.State.CurrentModel = result;
        }

        private void playerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            p.Show();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSave();
        }

        private bool DoSave()
        {
            if (FileName != null)
            {
                System.IO.File.WriteAllText(FileName, modelcode.Text);
                Changes = false;
                return true;
            }
            else
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Model geometry files (*.mgf)|*.mgf|Any File|*.*";
                saveFile.RestoreDirectory = true;
                saveFile.AddExtension = true;
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(saveFile.FileName, modelcode.Text);
                    FileName = saveFile.FileName;
                    Changes = false;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// This ensures no unintentional data loss occurs
        /// </summary>
        /// <returns></returns>
        public bool ConfirmSaveOrLose()
        {
            //skip dialog if no changes, just proceed
            if (!Changes)
                return true;
            DialogResult r =MessageBox.Show("You have unsaved changes in your model. Would you like to save?", "Model Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            switch(r)
                {
                case DialogResult.Cancel:
                    {
                        //user cancels the whole thing, abort
                        return false;
                    }
                case DialogResult.No:
                    {
                        //user intends to NOT save the changes, proceed
                        return true;
                    }
                case DialogResult.Yes:
                    {
                        //user wants to save
                        //offer to save changes, if true returned - saved, may proceed
                        if (DoSave())
                            return true;
                        //unsaved changes  were not saved successfully (user closed the save dialog?), abort action
                        return false;

                    }
                default: //should never happen but just in case
                    return false;
            }
            //fallback
            return false;
        }
        private void modelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ConfirmSaveOrLose())
                return;
            FileName = null;
            modelcode.Text = LoadBlank();


        }

        private string LoadBlank(string template="blank")
        {
            string result = "";
            string path = "templates\\" + template + ".mgf";
            result = System.IO.File.ReadAllText(path);
            return result;
        }

        private void modelcode_TextChanged(object sender, EventArgs e)
        {
            Changes = true;
        }

        private void wireframetoggle_Click(object sender, EventArgs e)
        {
            ProgramState.State.Settings.WireFrameMode = wireframetoggle.Checked;
        }

        //this flips triangles by swapping every second and 3rd integer in the selected string
        private void fliptrianglebutton_Click(object sender, EventArgs e)
        {

            //bail early if nothing is selected
            if (modelcode.SelectedText == null || modelcode.SelectedText == "")
                return;
            //remember the text around the selection
            string pre = modelcode.Text.Substring(0, modelcode.SelectionStart);
            string cur = modelcode.Text.Substring(modelcode.SelectionStart, modelcode.SelectionLength);
            string post = modelcode.Text.Substring(modelcode.SelectionStart + modelcode.SelectionLength);

            string intbuffer = ""; //builds up digits to eventually parse as integer
            string curbuffer = ""; //output accumulator that will replace the selection at the end
            int[] tribuffer = new int[3]; //triangle accumulator, holds 3 ints to build a triangle
            int counter = 0; //keeps track of "current" integer being built
            bool charrun = false; //used to keep track of char flushing
            for(int i=0;i<cur.Length;i++) 
            {
                char A = cur[i]; //take input 1 char at a time
                switch (A)
                {
                    case ',':
                    case ' ': //end of potential integer - separator char
                        {
                            charrun = false;
                            if(intbuffer!="") //if there are any digits captured, try getting an int
                            {
                                int.TryParse(intbuffer, out int result);
                                intbuffer = ""; //reset buffer
                                tribuffer[counter] = result; //place int in correct slot
                                counter++; //advance slot counter
                                if(counter>=3) //if 3 (or more, abnormal! throw a warning?), flush flipped triangle and reset counter
                                {
                                    counter = 0;
                                    curbuffer += tribuffer[0].ToString() + "," + tribuffer[2].ToString() + "," + tribuffer[1].ToString() + " ";

                                }
                            }
                            break;
                        }
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9': //digits - pile on the intbuffer
                        {
                            charrun = false;
                            intbuffer += A;
                            break;
                        }
                    default:
                        {
                            if(!charrun) //if this is the first "other" character
                            {
                                if (intbuffer != "")//if there are any digits still waiting, try getting an int
                                {
                                    int.TryParse(intbuffer, out int result);
                                    intbuffer = ""; //reset buffer
                                    tribuffer[counter] = result; //place int in slot
                                    counter++; //advance slot counter
                                    if (counter >= 3) //emit swapped tri, reset counter
                                    {
                                        counter = 0;
                                        curbuffer += tribuffer[0].ToString() + "," + tribuffer[2].ToString() + "," + tribuffer[1].ToString() + " ";

                                    }
                                    else //flush remaining buffer
                                    {
                                        for (int c = 0; c < counter; c++) //emit remaining tribuffer slots
                                            curbuffer += tribuffer[c].ToString() + ",";
                                        counter = 0;
                                    }
                                }
                                //set this to not repeat the above until more valid characters occur
                                charrun = true;
                            }
                            curbuffer += A; //copy char as is
                            break;
                        }
                }

                

            }

            //treat "end of selection" as a separator

            if (intbuffer != "") //any captured digits, get the int
            {
                int.TryParse(intbuffer, out int result);
                intbuffer = ""; //reset? not really needed at the end really
                tribuffer[counter] = result; //place in slot
                counter++; //advance slot counter 
                if (counter >= 3) //emit tri if we got 3 slots full
                {
                    counter = 0;
                    curbuffer += tribuffer[0].ToString() + "," + tribuffer[2].ToString() + "," + tribuffer[1].ToString() + " ";

                }
                else //flush remaining buffer
                {
                    for (int c = 0; c < counter; c++) //dump remaining slots, comma separated
                        curbuffer += tribuffer[c].ToString() + ",";
                    counter = 0;
                }
            }
            modelcode.Text = pre + curbuffer + post; //set text
            modelcode.SelectionStart = pre.Length; //create selection identical to original
            modelcode.SelectionLength = curbuffer.Length;
            modelcode.ScrollToCaret(); //scroll, works weird but better than jumping to top, heard some WinAPI magic can do better by calling like 5 functions from user32.dll or something
        }
    }
}
