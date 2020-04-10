namespace ModelEditor
{
    partial class MainAppFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainAppFrm));
            this.mm = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.choreoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discardAndReloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.modelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.choreoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.playerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maintoolbar = new System.Windows.Forms.ToolStrip();
            this.wireframetoggle = new System.Windows.Forms.ToolStripButton();
            this.fliptrianglebutton = new System.Windows.Forms.ToolStripButton();
            this.modelcode = new System.Windows.Forms.TextBox();
            this.statusbar = new System.Windows.Forms.StatusStrip();
            this.mm.SuspendLayout();
            this.maintoolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // mm
            // 
            this.mm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripMenuItem4,
            this.modelToolStripMenuItem1,
            this.choreoToolStripMenuItem1,
            this.toolsToolStripMenuItem});
            this.mm.Location = new System.Drawing.Point(0, 0);
            this.mm.Name = "mm";
            this.mm.Size = new System.Drawing.Size(862, 24);
            this.mm.TabIndex = 0;
            this.mm.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.discardAndReloadToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelToolStripMenuItem,
            this.choreoToolStripMenuItem,
            this.otherToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.newToolStripMenuItem.Text = "New...";
            // 
            // modelToolStripMenuItem
            // 
            this.modelToolStripMenuItem.Name = "modelToolStripMenuItem";
            this.modelToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.modelToolStripMenuItem.Text = "Model";
            this.modelToolStripMenuItem.Click += new System.EventHandler(this.modelToolStripMenuItem_Click);
            // 
            // choreoToolStripMenuItem
            // 
            this.choreoToolStripMenuItem.Name = "choreoToolStripMenuItem";
            this.choreoToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.choreoToolStripMenuItem.Text = "Choreo";
            // 
            // otherToolStripMenuItem
            // 
            this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            this.otherToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.otherToolStripMenuItem.Text = "Other...";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // discardAndReloadToolStripMenuItem
            // 
            this.discardAndReloadToolStripMenuItem.Name = "discardAndReloadToolStripMenuItem";
            this.discardAndReloadToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.discardAndReloadToolStripMenuItem.Text = "Discard and reload";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(39, 20);
            this.toolStripMenuItem2.Text = "Edit";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem4.Text = "View";
            // 
            // modelToolStripMenuItem1
            // 
            this.modelToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compileToolStripMenuItem});
            this.modelToolStripMenuItem1.Name = "modelToolStripMenuItem1";
            this.modelToolStripMenuItem1.Size = new System.Drawing.Size(53, 20);
            this.modelToolStripMenuItem1.Text = "Model";
            // 
            // compileToolStripMenuItem
            // 
            this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
            this.compileToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.compileToolStripMenuItem.Text = "Compile";
            this.compileToolStripMenuItem.Click += new System.EventHandler(this.compileToolStripMenuItem_Click);
            // 
            // choreoToolStripMenuItem1
            // 
            this.choreoToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playerToolStripMenuItem});
            this.choreoToolStripMenuItem1.Name = "choreoToolStripMenuItem1";
            this.choreoToolStripMenuItem1.Size = new System.Drawing.Size(58, 20);
            this.choreoToolStripMenuItem1.Text = "Choreo";
            // 
            // playerToolStripMenuItem
            // 
            this.playerToolStripMenuItem.Name = "playerToolStripMenuItem";
            this.playerToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.playerToolStripMenuItem.Text = "Player";
            this.playerToolStripMenuItem.Click += new System.EventHandler(this.playerToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            this.toolsToolStripMenuItem.Click += new System.EventHandler(this.toolsToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // maintoolbar
            // 
            this.maintoolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wireframetoggle,
            this.fliptrianglebutton});
            this.maintoolbar.Location = new System.Drawing.Point(0, 24);
            this.maintoolbar.Name = "maintoolbar";
            this.maintoolbar.Size = new System.Drawing.Size(862, 25);
            this.maintoolbar.TabIndex = 1;
            this.maintoolbar.Text = "toolStrip1";
            // 
            // wireframetoggle
            // 
            this.wireframetoggle.CheckOnClick = true;
            this.wireframetoggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.wireframetoggle.Image = global::ModelEditor.Properties.Resources.Wfmodebutton;
            this.wireframetoggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wireframetoggle.Name = "wireframetoggle";
            this.wireframetoggle.Size = new System.Drawing.Size(23, 22);
            this.wireframetoggle.Text = "W";
            this.wireframetoggle.Click += new System.EventHandler(this.wireframetoggle_Click);
            // 
            // fliptrianglebutton
            // 
            this.fliptrianglebutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fliptrianglebutton.Image = ((System.Drawing.Image)(resources.GetObject("fliptrianglebutton.Image")));
            this.fliptrianglebutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fliptrianglebutton.Name = "fliptrianglebutton";
            this.fliptrianglebutton.Size = new System.Drawing.Size(23, 22);
            this.fliptrianglebutton.Text = "Flip triangle(s)";
            this.fliptrianglebutton.Click += new System.EventHandler(this.fliptrianglebutton_Click);
            // 
            // modelcode
            // 
            this.modelcode.AcceptsReturn = true;
            this.modelcode.AcceptsTab = true;
            this.modelcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelcode.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modelcode.Location = new System.Drawing.Point(0, 49);
            this.modelcode.Multiline = true;
            this.modelcode.Name = "modelcode";
            this.modelcode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.modelcode.Size = new System.Drawing.Size(862, 476);
            this.modelcode.TabIndex = 2;
            this.modelcode.TextChanged += new System.EventHandler(this.modelcode_TextChanged);
            // 
            // statusbar
            // 
            this.statusbar.Location = new System.Drawing.Point(0, 525);
            this.statusbar.Name = "statusbar";
            this.statusbar.Size = new System.Drawing.Size(862, 22);
            this.statusbar.TabIndex = 3;
            this.statusbar.Text = "statusStrip1";
            // 
            // MainAppFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 547);
            this.Controls.Add(this.modelcode);
            this.Controls.Add(this.statusbar);
            this.Controls.Add(this.maintoolbar);
            this.Controls.Add(this.mm);
            this.MainMenuStrip = this.mm;
            this.Name = "MainAppFrm";
            this.Text = "MainAppFrm";
            this.Load += new System.EventHandler(this.MainAppFrm_Load);
            this.mm.ResumeLayout(false);
            this.mm.PerformLayout();
            this.maintoolbar.ResumeLayout(false);
            this.maintoolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mm;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem choreoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discardAndReloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem modelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem choreoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStrip maintoolbar;
        private System.Windows.Forms.StatusStrip statusbar;
        private System.Windows.Forms.ToolStripMenuItem playerToolStripMenuItem;
        public System.Windows.Forms.TextBox modelcode;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton wireframetoggle;
        private System.Windows.Forms.ToolStripButton fliptrianglebutton;
    }
}