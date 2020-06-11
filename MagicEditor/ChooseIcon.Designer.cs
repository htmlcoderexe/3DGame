namespace MagicEditor
{
    partial class ChooseIcon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseIcon));
            this.iconcontainer = new System.Windows.Forms.Panel();
            this.iconimage = new System.Windows.Forms.PictureBox();
            this.iconcontainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconimage)).BeginInit();
            this.SuspendLayout();
            // 
            // iconcontainer
            // 
            this.iconcontainer.AutoScroll = true;
            this.iconcontainer.Controls.Add(this.iconimage);
            this.iconcontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iconcontainer.Location = new System.Drawing.Point(0, 0);
            this.iconcontainer.Margin = new System.Windows.Forms.Padding(0);
            this.iconcontainer.Name = "iconcontainer";
            this.iconcontainer.Size = new System.Drawing.Size(800, 450);
            this.iconcontainer.TabIndex = 5;
            // 
            // iconimage
            // 
            this.iconimage.Image = ((System.Drawing.Image)(resources.GetObject("iconimage.Image")));
            this.iconimage.Location = new System.Drawing.Point(0, 0);
            this.iconimage.Margin = new System.Windows.Forms.Padding(0);
            this.iconimage.Name = "iconimage";
            this.iconimage.Size = new System.Drawing.Size(2048, 2048);
            this.iconimage.TabIndex = 3;
            this.iconimage.TabStop = false;
            this.iconimage.DoubleClick += new System.EventHandler(this.iconimage_DoubleClick);
            this.iconimage.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.iconimage_MouseDoubleClick);
            // 
            // ChooseIcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.iconcontainer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseIcon";
            this.Text = "ChooseIcon";
            this.iconcontainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconimage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel iconcontainer;
        private System.Windows.Forms.PictureBox iconimage;
    }
}