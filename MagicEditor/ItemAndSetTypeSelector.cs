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
    public partial class ItemAndSetTypeSelector : Form
    {
        GameObject.ItemAddonEntry Current;

        public ItemAndSetTypeSelector(GameObject.ItemAddonEntry addon, List<GameObject.ItemTypeDefinition> itemTypes, List<GameObject.ItemSetTemplate> itemSetTemplates)
        {
            InitializeComponent();
            Current = addon;
            ItemTypes.DisplayMember = "Name";
            SetTypes.DisplayMember = "Name";
            //Populate both list boxes with all possible item/set types
            foreach (GameObject.ItemTypeDefinition def in itemTypes)
            {
                ItemTypes.Items.Add(def);
            }
            foreach (GameObject.ItemSetTemplate set in itemSetTemplates)
            {
                SetTypes.Items.Add(set);
            }
            //check all that are enabled for current addon
            for (int i=0;i<ItemTypes.Items.Count;i++)
            {
                if (addon.ItemTypes.Contains((ItemTypes.Items[i] as GameObject.ItemTypeDefinition).ID))
                    ItemTypes.SetItemChecked(i, true);
            }
            for (int i = 0; i < SetTypes.Items.Count; i++)
            {
                if (addon.ItemTypes.Contains((SetTypes.Items[i] as GameObject.ItemSetTemplate).ID))
                    SetTypes.SetItemChecked(i, true);
            }

        }

        private void ItemAndSetTypeSelector_Load(object sender, EventArgs e)
        {

        }

        private void okbutty_Click(object sender, EventArgs e)
        {
            Current.ItemTypes.Clear();

            for (int i = 0; i < ItemTypes.Items.Count; i++)
            {
                if (ItemTypes.GetItemChecked(i))
                    Current.ItemTypes.Add((ItemTypes.Items[i] as GameObject.ItemTypeDefinition).ID);
            }
            for (int i = 0; i < SetTypes.Items.Count; i++)
            {
               if(SetTypes.GetItemChecked(i))
                    Current.ItemTypes.Add((SetTypes.Items[i] as GameObject.ItemSetTemplate).ID);
            }
        }

        private void hellno_Click(object sender, EventArgs e)
        {

        }
    }
}
