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
    public partial class TextPrompt : Form
    {
        public string Input = "";
        public TextPrompt()
        {
            InitializeComponent();
        }

        private void cancelbutt_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void okbutt_Click(object sender, EventArgs e)
        {
            this.Input = inputbox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TextPrompt_Load(object sender, EventArgs e)
        {
            if (this.Input != null)
                this.inputbox.Text = this.Input;
        }
    }
}
