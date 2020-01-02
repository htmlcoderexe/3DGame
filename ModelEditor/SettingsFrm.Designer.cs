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
            this.bgCR = new System.Windows.Forms.TrackBar();
            this.bgCG = new System.Windows.Forms.TrackBar();
            this.bgCB = new System.Windows.Forms.TrackBar();
            this.SettingsTabs.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bgCR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgCG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgCB)).BeginInit();
            this.SuspendLayout();
            // 
            // SettingsTabs
            // 
            this.SettingsTabs.Controls.Add(this.tabPage1);
            this.SettingsTabs.Controls.Add(this.tabPage2);
            this.SettingsTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsTabs.Location = new System.Drawing.Point(0, 0);
            this.SettingsTabs.Name = "SettingsTabs";
            this.SettingsTabs.SelectedIndex = 0;
            this.SettingsTabs.Size = new System.Drawing.Size(800, 450);
            this.SettingsTabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.bgCB);
            this.tabPage2.Controls.Add(this.bgCG);
            this.tabPage2.Controls.Add(this.bgCR);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Appearance";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // bgCR
            // 
            this.bgCR.LargeChange = 16;
            this.bgCR.Location = new System.Drawing.Point(81, 162);
            this.bgCR.Maximum = 255;
            this.bgCR.Name = "bgCR";
            this.bgCR.Size = new System.Drawing.Size(554, 45);
            this.bgCR.TabIndex = 0;
            this.bgCR.Scroll += new System.EventHandler(this.bgCR_Scroll);
            // 
            // bgCG
            // 
            this.bgCG.LargeChange = 16;
            this.bgCG.Location = new System.Drawing.Point(81, 213);
            this.bgCG.Maximum = 255;
            this.bgCG.Name = "bgCG";
            this.bgCG.Size = new System.Drawing.Size(554, 45);
            this.bgCG.TabIndex = 1;
            this.bgCG.Scroll += new System.EventHandler(this.bgCR_Scroll);
            // 
            // bgCB
            // 
            this.bgCB.LargeChange = 16;
            this.bgCB.Location = new System.Drawing.Point(81, 264);
            this.bgCB.Maximum = 255;
            this.bgCB.Name = "bgCB";
            this.bgCB.Size = new System.Drawing.Size(554, 45);
            this.bgCB.TabIndex = 2;
            this.bgCB.Scroll += new System.EventHandler(this.bgCR_Scroll);
            // 
            // SettingsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SettingsTabs);
            this.Name = "SettingsFrm";
            this.Text = "Settings";
            this.SettingsTabs.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bgCR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgCG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgCB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl SettingsTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TrackBar bgCB;
        private System.Windows.Forms.TrackBar bgCG;
        private System.Windows.Forms.TrackBar bgCR;
    }
}