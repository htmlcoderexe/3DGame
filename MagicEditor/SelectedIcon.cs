using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicEditor
{
    public partial class SelectedIcon : PictureBox
    {
        public int IconValue {get;set;}
        public SelectedIcon()
        {
            InitializeComponent();
        }
    }
}
