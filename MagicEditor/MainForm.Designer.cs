namespace MagicEditor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.EffectList = new System.Windows.Forms.ListView();
            this.typefriendly = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.effectmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addEffectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeEffectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.icons = new System.Windows.Forms.ImageList(this.components);
            this.descprev = new System.Windows.Forms.Label();
            this.lvlprev = new System.Windows.Forms.NumericUpDown();
            this.iconimage = new System.Windows.Forms.PictureBox();
            this.iconcontainer = new System.Windows.Forms.Panel();
            this.spellname = new System.Windows.Forms.Label();
            this.castbase = new System.Windows.Forms.NumericUpDown();
            this.channelbase = new System.Windows.Forms.NumericUpDown();
            this.mpbase = new System.Windows.Forms.NumericUpDown();
            this.cdbase = new System.Windows.Forms.NumericUpDown();
            this.cddelta = new System.Windows.Forms.NumericUpDown();
            this.mpdelta = new System.Windows.Forms.NumericUpDown();
            this.channeldelta = new System.Windows.Forms.NumericUpDown();
            this.castdelta = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.abilityselector = new System.Windows.Forms.ListBox();
            this.abilitymenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createAbilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAbilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveabilities = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.abilitylistpage = new System.Windows.Forms.TabPage();
            this.classespage = new System.Windows.Forms.TabPage();
            this.comppage = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.effectmenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvlprev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconimage)).BeginInit();
            this.iconcontainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.castbase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.channelbase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mpbase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdbase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cddelta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mpdelta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.channeldelta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.castdelta)).BeginInit();
            this.abilitymenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.abilitylistpage.SuspendLayout();
            this.classespage.SuspendLayout();
            this.SuspendLayout();
            // 
            // EffectList
            // 
            this.EffectList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.typefriendly,
            this.time,
            this.duration});
            this.EffectList.ContextMenuStrip = this.effectmenu;
            this.EffectList.Dock = System.Windows.Forms.DockStyle.Right;
            this.EffectList.HideSelection = false;
            this.EffectList.Location = new System.Drawing.Point(588, 3);
            this.EffectList.Name = "EffectList";
            this.EffectList.Size = new System.Drawing.Size(474, 562);
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
            // effectmenu
            // 
            this.effectmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEffectToolStripMenuItem,
            this.removeEffectToolStripMenuItem});
            this.effectmenu.Name = "effectmenu";
            this.effectmenu.Size = new System.Drawing.Size(151, 48);
            this.effectmenu.Opening += new System.ComponentModel.CancelEventHandler(this.effectmenu_Opening);
            // 
            // addEffectToolStripMenuItem
            // 
            this.addEffectToolStripMenuItem.Name = "addEffectToolStripMenuItem";
            this.addEffectToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.addEffectToolStripMenuItem.Text = "Add effect";
            this.addEffectToolStripMenuItem.Click += new System.EventHandler(this.addEffectToolStripMenuItem_Click);
            // 
            // removeEffectToolStripMenuItem
            // 
            this.removeEffectToolStripMenuItem.Name = "removeEffectToolStripMenuItem";
            this.removeEffectToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.removeEffectToolStripMenuItem.Text = "Remove effect";
            this.removeEffectToolStripMenuItem.Click += new System.EventHandler(this.removeEffectToolStripMenuItem_Click);
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
            this.descprev.Location = new System.Drawing.Point(350, 128);
            this.descprev.Name = "descprev";
            this.descprev.Size = new System.Drawing.Size(199, 181);
            this.descprev.TabIndex = 1;
            this.descprev.Text = "<>";
            this.descprev.DoubleClick += new System.EventHandler(this.descprev_DoubleClick);
            // 
            // lvlprev
            // 
            this.lvlprev.Location = new System.Drawing.Point(498, 97);
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
            this.iconcontainer.Location = new System.Drawing.Point(350, 36);
            this.iconcontainer.Margin = new System.Windows.Forms.Padding(0);
            this.iconcontainer.Name = "iconcontainer";
            this.iconcontainer.Size = new System.Drawing.Size(32, 32);
            this.iconcontainer.TabIndex = 4;
            // 
            // spellname
            // 
            this.spellname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spellname.Location = new System.Drawing.Point(385, 38);
            this.spellname.Name = "spellname";
            this.spellname.Size = new System.Drawing.Size(164, 46);
            this.spellname.TabIndex = 5;
            this.spellname.Text = "<>";
            this.spellname.DoubleClick += new System.EventHandler(this.spellname_DoubleClick);
            // 
            // castbase
            // 
            this.castbase.DecimalPlaces = 1;
            this.castbase.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.castbase.Location = new System.Drawing.Point(421, 320);
            this.castbase.Name = "castbase";
            this.castbase.Size = new System.Drawing.Size(61, 20);
            this.castbase.TabIndex = 6;
            this.castbase.ValueChanged += new System.EventHandler(this.cddelta_ValueChanged);
            // 
            // channelbase
            // 
            this.channelbase.DecimalPlaces = 1;
            this.channelbase.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.channelbase.Location = new System.Drawing.Point(421, 346);
            this.channelbase.Name = "channelbase";
            this.channelbase.Size = new System.Drawing.Size(61, 20);
            this.channelbase.TabIndex = 7;
            this.channelbase.ValueChanged += new System.EventHandler(this.cddelta_ValueChanged);
            // 
            // mpbase
            // 
            this.mpbase.DecimalPlaces = 1;
            this.mpbase.Location = new System.Drawing.Point(421, 372);
            this.mpbase.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.mpbase.Name = "mpbase";
            this.mpbase.Size = new System.Drawing.Size(61, 20);
            this.mpbase.TabIndex = 8;
            this.mpbase.ValueChanged += new System.EventHandler(this.cddelta_ValueChanged);
            // 
            // cdbase
            // 
            this.cdbase.DecimalPlaces = 1;
            this.cdbase.Location = new System.Drawing.Point(421, 398);
            this.cdbase.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.cdbase.Name = "cdbase";
            this.cdbase.Size = new System.Drawing.Size(61, 20);
            this.cdbase.TabIndex = 9;
            this.cdbase.ValueChanged += new System.EventHandler(this.cddelta_ValueChanged);
            // 
            // cddelta
            // 
            this.cddelta.DecimalPlaces = 1;
            this.cddelta.Location = new System.Drawing.Point(488, 398);
            this.cddelta.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.cddelta.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.cddelta.Name = "cddelta";
            this.cddelta.Size = new System.Drawing.Size(61, 20);
            this.cddelta.TabIndex = 13;
            this.cddelta.ValueChanged += new System.EventHandler(this.cddelta_ValueChanged);
            // 
            // mpdelta
            // 
            this.mpdelta.DecimalPlaces = 1;
            this.mpdelta.Location = new System.Drawing.Point(488, 372);
            this.mpdelta.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.mpdelta.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.mpdelta.Name = "mpdelta";
            this.mpdelta.Size = new System.Drawing.Size(61, 20);
            this.mpdelta.TabIndex = 12;
            this.mpdelta.ValueChanged += new System.EventHandler(this.cddelta_ValueChanged);
            // 
            // channeldelta
            // 
            this.channeldelta.DecimalPlaces = 1;
            this.channeldelta.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.channeldelta.Location = new System.Drawing.Point(488, 346);
            this.channeldelta.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.channeldelta.Name = "channeldelta";
            this.channeldelta.Size = new System.Drawing.Size(61, 20);
            this.channeldelta.TabIndex = 11;
            this.channeldelta.ValueChanged += new System.EventHandler(this.cddelta_ValueChanged);
            // 
            // castdelta
            // 
            this.castdelta.DecimalPlaces = 1;
            this.castdelta.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.castdelta.Location = new System.Drawing.Point(488, 320);
            this.castdelta.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.castdelta.Name = "castdelta";
            this.castdelta.Size = new System.Drawing.Size(61, 20);
            this.castdelta.TabIndex = 10;
            this.castdelta.ValueChanged += new System.EventHandler(this.cddelta_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(347, 322);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Cast time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(347, 348);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Channel time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(347, 374);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "MP cost";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(347, 400);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Cooldown";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(347, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Preview level:";
            // 
            // abilityselector
            // 
            this.abilityselector.ContextMenuStrip = this.abilitymenu;
            this.abilityselector.DisplayMember = "Name";
            this.abilityselector.Dock = System.Windows.Forms.DockStyle.Left;
            this.abilityselector.FormattingEnabled = true;
            this.abilityselector.HorizontalScrollbar = true;
            this.abilityselector.Location = new System.Drawing.Point(3, 3);
            this.abilityselector.Name = "abilityselector";
            this.abilityselector.ScrollAlwaysVisible = true;
            this.abilityselector.Size = new System.Drawing.Size(233, 562);
            this.abilityselector.TabIndex = 19;
            this.abilityselector.DoubleClick += new System.EventHandler(this.abilityselector_DoubleClick);
            // 
            // abilitymenu
            // 
            this.abilitymenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createAbilityToolStripMenuItem,
            this.deleteAbilityToolStripMenuItem});
            this.abilitymenu.Name = "abilitymenu";
            this.abilitymenu.Size = new System.Drawing.Size(144, 48);
            this.abilitymenu.Opening += new System.ComponentModel.CancelEventHandler(this.abilitymenu_Opening);
            // 
            // createAbilityToolStripMenuItem
            // 
            this.createAbilityToolStripMenuItem.Name = "createAbilityToolStripMenuItem";
            this.createAbilityToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.createAbilityToolStripMenuItem.Text = "Create ability";
            this.createAbilityToolStripMenuItem.Click += new System.EventHandler(this.createAbilityToolStripMenuItem_Click);
            // 
            // deleteAbilityToolStripMenuItem
            // 
            this.deleteAbilityToolStripMenuItem.Name = "deleteAbilityToolStripMenuItem";
            this.deleteAbilityToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.deleteAbilityToolStripMenuItem.Text = "Delete ability";
            this.deleteAbilityToolStripMenuItem.Click += new System.EventHandler(this.deleteAbilityToolStripMenuItem_Click);
            // 
            // saveabilities
            // 
            this.saveabilities.Location = new System.Drawing.Point(250, 6);
            this.saveabilities.Name = "saveabilities";
            this.saveabilities.Size = new System.Drawing.Size(75, 42);
            this.saveabilities.TabIndex = 21;
            this.saveabilities.Text = "Save abilities";
            this.saveabilities.UseVisualStyleBackColor = true;
            this.saveabilities.Click += new System.EventHandler(this.saveabilities_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.abilitylistpage);
            this.tabControl1.Controls.Add(this.classespage);
            this.tabControl1.Controls.Add(this.comppage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1185, 594);
            this.tabControl1.TabIndex = 22;
            // 
            // abilitylistpage
            // 
            this.abilitylistpage.Controls.Add(this.abilityselector);
            this.abilitylistpage.Controls.Add(this.EffectList);
            this.abilitylistpage.Controls.Add(this.descprev);
            this.abilitylistpage.Controls.Add(this.saveabilities);
            this.abilitylistpage.Controls.Add(this.lvlprev);
            this.abilitylistpage.Controls.Add(this.iconcontainer);
            this.abilitylistpage.Controls.Add(this.label5);
            this.abilitylistpage.Controls.Add(this.spellname);
            this.abilitylistpage.Controls.Add(this.label4);
            this.abilitylistpage.Controls.Add(this.castbase);
            this.abilitylistpage.Controls.Add(this.label3);
            this.abilitylistpage.Controls.Add(this.channelbase);
            this.abilitylistpage.Controls.Add(this.label2);
            this.abilitylistpage.Controls.Add(this.mpbase);
            this.abilitylistpage.Controls.Add(this.label1);
            this.abilitylistpage.Controls.Add(this.cdbase);
            this.abilitylistpage.Controls.Add(this.cddelta);
            this.abilitylistpage.Controls.Add(this.castdelta);
            this.abilitylistpage.Controls.Add(this.mpdelta);
            this.abilitylistpage.Controls.Add(this.channeldelta);
            this.abilitylistpage.Location = new System.Drawing.Point(4, 22);
            this.abilitylistpage.Name = "abilitylistpage";
            this.abilitylistpage.Padding = new System.Windows.Forms.Padding(3);
            this.abilitylistpage.Size = new System.Drawing.Size(1065, 568);
            this.abilitylistpage.TabIndex = 0;
            this.abilitylistpage.Text = "Abilities";
            this.abilitylistpage.UseVisualStyleBackColor = true;
            // 
            // classespage
            // 
            this.classespage.Controls.Add(this.panel1);
            this.classespage.Controls.Add(this.listBox1);
            this.classespage.Location = new System.Drawing.Point(4, 22);
            this.classespage.Name = "classespage";
            this.classespage.Padding = new System.Windows.Forms.Padding(3);
            this.classespage.Size = new System.Drawing.Size(1177, 568);
            this.classespage.TabIndex = 1;
            this.classespage.Text = "Classes";
            this.classespage.UseVisualStyleBackColor = true;
            // 
            // comppage
            // 
            this.comppage.Location = new System.Drawing.Point(4, 22);
            this.comppage.Name = "comppage";
            this.comppage.Padding = new System.Windows.Forms.Padding(3);
            this.comppage.Size = new System.Drawing.Size(1065, 568);
            this.comppage.TabIndex = 2;
            this.comppage.Text = "Components";
            this.comppage.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.ContextMenuStrip = this.abilitymenu;
            this.listBox1.DisplayMember = "Name";
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(233, 562);
            this.listBox1.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(767, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(407, 562);
            this.panel1.TabIndex = 21;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 594);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "Magic Editor v1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.effectmenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lvlprev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconimage)).EndInit();
            this.iconcontainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.castbase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.channelbase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mpbase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdbase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cddelta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mpdelta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.channeldelta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.castdelta)).EndInit();
            this.abilitymenu.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.abilitylistpage.ResumeLayout(false);
            this.abilitylistpage.PerformLayout();
            this.classespage.ResumeLayout(false);
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
        private System.Windows.Forms.NumericUpDown castbase;
        private System.Windows.Forms.NumericUpDown channelbase;
        private System.Windows.Forms.NumericUpDown mpbase;
        private System.Windows.Forms.NumericUpDown cdbase;
        private System.Windows.Forms.NumericUpDown cddelta;
        private System.Windows.Forms.NumericUpDown mpdelta;
        private System.Windows.Forms.NumericUpDown channeldelta;
        private System.Windows.Forms.NumericUpDown castdelta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox abilityselector;
        private System.Windows.Forms.ContextMenuStrip abilitymenu;
        private System.Windows.Forms.ToolStripMenuItem createAbilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAbilityToolStripMenuItem;
        private System.Windows.Forms.Button saveabilities;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage abilitylistpage;
        private System.Windows.Forms.TabPage classespage;
        private System.Windows.Forms.TabPage comppage;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel1;
    }
}

