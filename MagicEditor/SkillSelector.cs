using GameObject;
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
    public partial class SkillSelector : Form
    {
        public int SelectedLevel { get; set; }
        public string SelectedID { get; set; }

        public MainForm AbilityProvider;
        public SkillSelector(MainForm Parent)
        {
            this.AbilityProvider = Parent;
            SelectedLevel = 1;
            InitializeComponent();
        }

        private void SkillSelector_Load(object sender, EventArgs e)
        {
            foreach(ModularAbility ability in AbilityProvider.abilities)
            {
                skillist.Items.Add(ability);
            }
                
        }

        private void skillist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skillist.SelectedItems.Count != 1)
                return;
            iddisplay.Text = ((ModularAbility)skillist.SelectedItem).ID;
            SelectedID = ((ModularAbility)skillist.SelectedItem).ID;
        }

        private void suggester_TextChanged(object sender, EventArgs e)
        {
            int i = skillist.FindString(suggester.Text);
            if (i == -1)
            {
                skillist.SelectedIndex = -1;
            }
            skillist.SelectedIndex = i;
        }

        private void levelselect_ValueChanged(object sender, EventArgs e)
        {
            SelectedLevel = (int)levelselect.Value;
        }
    }
}
