namespace MagicEditor
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.EffectList = new System.Windows.Forms.ListView();
            this.typefriendly = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.icons = new System.Windows.Forms.ImageList(this.components);
            this.descprev = new System.Windows.Forms.Label();
            this.lvlprev = new System.Windows.Forms.NumericUpDown();
            this.iconimage = new System.Windows.Forms.PictureBox();
            this.iconcontainer = new System.Windows.Forms.Panel();
            this.spellname = new System.Windows.Forms.Label();
            this.effectmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addEffectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeEffectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.lvlprev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconimage)).BeginInit();
            this.iconcontainer.SuspendLayout();
            this.effectmenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // EffectList
            // 
            this.EffectList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.typefriendly,
            this.time,
            this.duration});
            this.EffectList.ContextMenuStrip = this.effectmenu;
            this.EffectList.HideSelection = false;
            this.EffectList.Location = new System.Drawing.Point(577, 12);
            this.EffectList.Name = "EffectList";
            this.EffectList.Size = new System.Drawing.Size(484, 336);
            this.EffectList.SmallImageList = this.icons;
            this.EffectList.TabIndex = 0;
            this.EffectList.UseCompatibleStateImageBehavior = false;
            this.EffectList.View = System.Windows.Forms.View.Details;
            this.EffectList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.EffectList_MouseDoubleClick);
            // 
            // typefriendly
            // 
            this.typefriendly.Text = "Item";
            this.typefriendly.Width = 250;
            // 
            // time
            // 
            this.time.Text = "Time";
            this.time.Width = 68;
            // 
            // duration
            // 
            this.duration.Text = "Duration";
            // 
            // icons
            // 
            this.icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("icons.ImageStream")));
            this.icons.TransparentColor = System.Drawing.Color.Magenta;
            this.icons.Images.SetKeyName(0, "icon_vfx.bmp");
            this.icons.Images.SetKeyName(1, "icon_damage.bmp");
            // 
            // descprev
            // 
            this.descprev.Location = new System.Drawing.Point(413, 113);
            this.descprev.Name = "descprev";
            this.descprev.Size = new System.Drawing.Size(158, 152);
            this.descprev.TabIndex = 1;
            this.descprev.Text = "<>";
            this.descprev.DoubleClick += new System.EventHandler(this.descprev_DoubleClick);
            // 
            // lvlprev
            // 
            this.lvlprev.Location = new System.Drawing.Point(520, 90);
            this.lvlprev.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.lvlprev.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lvlprev.Name = "lvlprev";
            this.lvlprev.Size = new System.Drawing.Size(51, 20);
            this.lvlprev.TabIndex = 2;
            this.lvlprev.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lvlprev.ValueChanged += new System.EventHandler(this.lvlprev_ValueChanged);
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
            // 
            // iconcontainer
            // 
            this.iconcontainer.Controls.Add(this.iconimage);
            this.iconcontainer.Location = new System.Drawing.Point(419, 49);
            this.iconcontainer.Margin = new System.Windows.Forms.Padding(0);
            this.iconcontainer.Name = "iconcontainer";
            this.iconcontainer.Size = new System.Drawing.Size(32, 32);
            this.iconcontainer.TabIndex = 4;
            // 
            // spellname
            // 
            this.spellname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spellname.Location = new System.Drawing.Point(416, 12);
            this.spellname.Name = "spellname";
            this.spellname.Size = new System.Drawing.Size(155, 37);
            this.spellname.TabIndex = 5;
            this.spellname.Text = "<>";
            this.spellname.DoubleClick += new System.EventHandler(this.spellname_DoubleClick);
            // 
            // effectmenu
            // 
            this.effectmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEffectToolStripMenuItem,
            this.removeEffectToolStripMenuItem});
            this.effectmenu.Name = "effectmenu";
            this.effectmenu.Size = new System.Drawing.Size(181, 70);
            this.effectmenu.Opening += new System.ComponentModel.CancelEventHandler(this.effectmenu_Opening);
            // 
            // addEffectToolStripMenuItem
            // 
            this.addEffectToolStripMenuItem.Name = "addEffectToolStripMenuItem";
            this.addEffectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addEffectToolStripMenuItem.Text = "Add effect";
            this.addEffectToolStripMenuItem.Click += new System.EventHandler(this.addEffectToolStripMenuItem_Click);
            // 
            // removeEffectToolStripMenuItem
            // 
            this.removeEffectToolStripMenuItem.Name = "removeEffectToolStripMenuItem";
            this.removeEffectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeEffectToolStripMenuItem.Text = "Remove effect";
            this.removeEffectToolStripMenuItem.Click += new System.EventHandler(this.removeEffectToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 516);
            this.Controls.Add(this.spellname);
            this.Controls.Add(this.iconcontainer);
            this.Controls.Add(this.lvlprev);
            this.Controls.Add(this.descprev);
            this.Controls.Add(this.EffectList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lvlprev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconimage)).EndInit();
            this.iconcontainer.ResumeLayout(false);
            this.effectmenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView EffectList;
        private System.Windows.Forms.ImageList icons;
        private System.Windows.Forms.ColumnHeader time;
        private System.Windows.Forms.ColumnHeader duration;
        private System.Windows.Forms.ColumnHeader typefriendly;
        private System.Windows.Forms.Label descprev;
        private System.Windows.Forms.NumericUpDown lvlprev;
        private System.Windows.Forms.PictureBox iconimage;
        private System.Windows.Forms.Panel iconcontainer;
        private System.Windows.Forms.Label spellname;
        private System.Windows.Forms.ContextMenuStrip effectmenu;
        private System.Windows.Forms.ToolStripMenuItem addEffectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeEffectToolStripMenuItem;
    }
}

