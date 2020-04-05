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
            Model result;
            string modeldata = System.IO.File.ReadAllText(filename);
            modelcode.Text = modeldata;
            ModelGeometryCompiler.ModelBaseDir = System.IO.Path.GetDirectoryName(filename);
            ModelGeometryCompiler compiler = new ModelGeometryCompiler(modeldata);
            result = compiler.ReturnOutput();
            result.RebuildSkeleton();
            result.ApplyAnimation("Walk");
            ProgramState.State.CurrentModel = result;
            string choreofilename = ModelGeometryCompiler.ModelBaseDir + "\\" + result.ChoreoName + ".mcf";
            p.choreocode.Text = System.IO.File.ReadAllText(choreofilename);
            p.FileName = choreofilename;
            p.movements.Items.Clear();
            foreach (KeyValuePair<string, Dictionary<string,PartAnimation>> movement in result.Choreo)
                p.movements.Items.Add(movement.Key);
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
    }
}
