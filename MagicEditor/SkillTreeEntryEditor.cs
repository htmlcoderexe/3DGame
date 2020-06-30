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
    public partial class SkillTreeEntryEditor : Form
    {
        public GameObject.SkillTreeEntry Entry;
        public MainForm Parent;
        public SkillTreeEntryEditor(MainForm Parent,SkillTreeEntry Entry)
        {
            InitializeComponent();
            this.Entry = Entry;
            this.Parent = Parent;
            ModularAbility a = Parent.FindAbility(Entry.SkillID);
            SkillName.Text = a.Name;
            learnlevel.Value = (decimal)Entry.LearnLevel;
            column.Value = (decimal)Entry.Column;
            traininglevel.Value = (decimal)Entry.TrainingLevel;
            maxlvl.Value = (decimal)Entry.MaxLevel;
            expbase.Value = (decimal)Entry.ExpBase;
            expdelta.Value = (decimal)Entry.ExpDelta;
            requireitemid.Text = Entry.RequireItemID;
            ReloadReqList();
        }

        private void ReloadReqList()
        {
            foreach(Tuple<string, int> t in Entry.PreRequisiteSkills)
            {
                ModularAbility a = Parent.FindAbility(t.Item1);
                string[] ItemProps = new string[] { a.Name, t.Item2.ToString() };
                ListViewItem line = new ListViewItem(ItemProps);
                line.Tag = t.Item1;
                listView1.Items.Add(line);
            }
        }
    }
}
