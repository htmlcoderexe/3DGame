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
        public ProgramState State;
        public MainAppFrm()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
            State = new ProgramState();
            State.Settings = LoadSettings();
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
            SettingsFrm s = new SettingsFrm(State.Settings);
            SettingsContainer old = (SettingsContainer)State.Settings.Clone();
            DialogResult d = s.ShowDialog();
            if (d != DialogResult.OK)
                State.Settings = old;

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
            Model result;
            string modeldata = System.IO.File.ReadAllText(filename);
            modelcode.Text = modeldata;
            ModelGeometryCompiler.ModelBaseDir = System.IO.Path.GetDirectoryName(filename);
            ModelGeometryCompiler compiler = new ModelGeometryCompiler(modeldata);
            result = compiler.ReturnOutput();
            result.ApplyAnimation("Walk");
            State.CurrentModel = result;
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ModelGeometryCompiler compiler = new ModelGeometryCompiler(modelcode.Text);
            GameModel.Model result = compiler.ReturnOutput();
            result.ApplyAnimation("Walk");
            State.CurrentModel = result;
        }
    }
}
