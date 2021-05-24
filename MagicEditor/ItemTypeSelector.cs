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
    public partial class ItemTypeSelector : Form
    {

        public MainForm itemlistprovider;

        public List<string> SelectedValues;

        public bool SelectWeaponsOnly;

        public ItemTypeSelector(MainForm Parent)
        {
            this.itemlistprovider = Parent;
            InitializeComponent();
        }

        private void suggester_TextChanged(object sender, EventArgs e)
        {
            notselected.SelectedIndex = notselected.FindString(suggester.Text);
        }

        private void ItemTypeSelector_Load(object sender, EventArgs e)
        {
            if (this.SelectedValues == null)
                this.SelectedValues = new List<string>();
            foreach(GameObject.ItemTypeDefinition def in itemlistprovider.itemtypes)
            {
                if (SelectWeaponsOnly &&
                    def.ItemCategory != GameObject.Items.ItemEquip.EquipCategories.WeaponMagic &&
                    def.ItemCategory != GameObject.Items.ItemEquip.EquipCategories.WeaponMelee &&
                    def.ItemCategory != GameObject.Items.ItemEquip.EquipCategories.WeaponRanged)
                    continue;

                if(SelectedValues.Contains(def.ID))
                {
                    selected.Items.Add(def);
                }
                else
                {
                    notselected.Items.Add(def);
                }
            }
        }

        private void okbutt_Click(object sender, EventArgs e)
        {
            SelectedValues.Clear();
            foreach(object o in selected.Items)
            {
                GameObject.ItemTypeDefinition def = o as GameObject.ItemTypeDefinition;
                if (o == null)
                    continue;
                SelectedValues.Add(def.ID);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void rmbutt_Click(object sender, EventArgs e)
        {
            List<object> unselect = new List<object>();
            if(selected.SelectedItems.Count!=0)
            {
                foreach (object o in selected.SelectedItems)
                {
                    unselect.Add(o);
                }
                foreach (object o in unselect)
                {
                    notselected.Items.Add(o);
                    selected.Items.Remove(o);
                }

                
            }
        }

        private void addbutt_Click(object sender, EventArgs e)
        {
            List<object> select = new List<object>();
            if (notselected.SelectedItems.Count != 0)
            {
                foreach (object o in notselected.SelectedItems)
                {
                    select.Add(o);
                }
                foreach (object o in select)
                {
                    selected.Items.Add(o);
                    notselected.Items.Remove(o);
                }
            }
        }
    }
}
