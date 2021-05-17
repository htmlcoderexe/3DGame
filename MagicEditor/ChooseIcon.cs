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
    public partial class ChooseIcon : Form
    {
        public int Icon;
        public ChooseIcon(PictureBox imagesource)
        {
            
            InitializeComponent();

            iconimage.Image = imagesource.Image;
        }

        private void iconimage_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void iconimage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);
            int X = (int)((float)point.X / 32f);
            int Y = (int)((float)point.Y / 32f);
            this.Icon = X + Y * 64;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ChooseIcon_Load(object sender, EventArgs e)
        {

        }
    }
}
