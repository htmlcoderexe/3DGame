namespace ModelEditor
{
    partial class SettingsFrm
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
            this.SettingsTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.bgCB = new System.Windows.Forms.TrackBar();
            this.bgCG = new System.Windows.Forms.TrackBar();
            this.bgCR = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.okbutton = new System.Windows.Forms.Button();
            this.cancelbut = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.colprev = new System.Windows.Forms.Panel();
            this.coltxt = new System.Windows.Forms.TextBox();
            this.SettingsTabs.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bgCB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgCG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgCR)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SettingsTabs
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.SettingsTabs, 2);
            this.SettingsTabs.Controls.Add(this.tabPage1);
            this.SettingsTabs.Controls.Add(this.tabPage2);
            this.SettingsTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsTabs.Location = new System.Drawing.Point(3, 3);
            this.SettingsTabs.Name = "SettingsTabs";
            this.SettingsTabs.SelectedIndex = 0;
            this.SettingsTabs.Size = new System.Drawing.Size(663, 412);
            this.SettingsTabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(655, 386);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(655, 386);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Appearance";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // bgCB
            // 
            this.bgCB.LargeChange = 16;
            this.bgCB.Location = new System.Drawing.Point(136, 121);
            this.bgCB.Maximum = 255;
            this.bgCB.Name = "bgCB";
            this.bgCB.Size = new System.Drawing.Size(501, 45);
            this.bgCB.TabIndex = 2;
            this.bgCB.TickFrequency = 32;
            this.bgCB.Scroll += new System.EventHandler(this.bgCR_Scroll);
            // 
            // bgCG
            // 
            this.bgCG.LargeChange = 16;
            this.bgCG.Location = new System.Drawing.Point(136, 70);
            this.bgCG.Maximum = 255;
            this.bgCG.Name = "bgCG";
            this.bgCG.Size = new System.Drawing.Size(501, 45);
            this.bgCG.TabIndex = 1;
            this.bgCG.TickFrequency = 32;
            this.bgCG.Scroll += new System.EventHandler(this.bgCR_Scroll);
            // 
            // bgCR
            // 
            this.bgCR.LargeChange = 16;
            this.bgCR.Location = new System.Drawing.Point(136, 19);
            this.bgCR.Maximum = 255;
            this.bgCR.Name = "bgCR";
            this.bgCR.Size = new System.Drawing.Size(501, 45);
            this.bgCR.TabIndex = 0;
            this.bgCR.TickFrequency = 32;
            this.bgCR.Scroll += new System.EventHandler(this.bgCR_Scroll);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutPanel1.Controls.Add(this.SettingsTabs, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.okbutton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cancelbut, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(669, 466);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // okbutton
            // 
            this.okbutton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.okbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okbutton.Location = new System.Drawing.Point(494, 430);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(75, 23);
            this.okbutton.TabIndex = 1;
            this.okbutton.Text = "Save";
            this.okbutton.UseVisualStyleBackColor = true;
            // 
            // cancelbut
            // 
            this.cancelbut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cancelbut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbut.Location = new System.Drawing.Point(583, 430);
            this.cancelbut.Name = "cancelbut";
            this.cancelbut.Size = new System.Drawing.Size(75, 23);
            this.cancelbut.TabIndex = 2;
            this.cancelbut.Text = "Cancel";
            this.cancelbut.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.coltxt);
            this.groupBox1.Controls.Add(this.colprev);
            this.groupBox1.Controls.Add(this.bgCG);
            this.groupBox1.Controls.Add(this.bgCB);
            this.groupBox1.Controls.Add(this.bgCR);
            this.groupBox1.Location = new System.Drawing.Point(6, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(643, 176);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Model Viewer background";
            // 
            // colprev
            // 
            this.colprev.Location = new System.Drawing.Point(10, 20);
            this.colprev.Name = "colprev";
            this.colprev.Size = new System.Drawing.Size(120, 110);
            this.colprev.TabIndex = 3;
            // 
            // coltxt
            // 
            this.coltxt.Enabled = false;
            this.coltxt.Location = new System.Drawing.Point(10, 142);
            this.coltxt.Name = "coltxt";
            this.coltxt.ReadOnly = true;
            this.coltxt.Size = new System.Drawing.Size(119, 20);
            this.coltxt.TabIndex = 4;
            // 
            // SettingsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 466);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SettingsFrm";
            this.Text = "Settings";
            this.SettingsTabs.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bgCB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgCG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgCR)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl SettingsTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TrackBar bgCB;
        private System.Windows.Forms.TrackBar bgCG;
        private System.Windows.Forms.TrackBar bgCR;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button okbutton;
        private System.Windows.Forms.Button cancelbut;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox coltxt;
        private System.Windows.Forms.Panel colprev;
    }
}