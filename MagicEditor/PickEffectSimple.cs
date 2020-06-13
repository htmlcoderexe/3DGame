using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicEditor
{
    public partial class PickEffectSimple : Form
    {
        public string Effect = "";
        public PickEffectSimple()
        {
            InitializeComponent();
        }

        private void PickEffectSimple_Load(object sender, EventArgs e)
        {
            foreach(string s in AbilityEffectDefinition.Definitions.Keys)
            {
                options.Items.Add(s);
            }
        }

        private void okbutt_Click(object sender, EventArgs e)
        {
            this.Effect = (string)options.SelectedItem;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelbut_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
