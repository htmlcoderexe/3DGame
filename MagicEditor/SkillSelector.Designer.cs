namespace MagicEditor
{
    partial class SkillSelector
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
            this.skillist = new System.Windows.Forms.ListBox();
            this.suggester = new System.Windows.Forms.TextBox();
            this.iddisplay = new System.Windows.Forms.Label();
            this.levelselect = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.levelselect)).BeginInit();
            this.SuspendLayout();
            // 
            // skillist
            // 
            this.skillist.DisplayMember = "Name";
            this.skillist.FormattingEnabled = true;
            this.skillist.HorizontalScrollbar = true;
            this.skillist.Location = new System.Drawing.Point(12, 44);
            this.skillist.Name = "skillist";
            this.skillist.Size = new System.Drawing.Size(120, 82);
            this.skillist.TabIndex = 0;
            this.skillist.SelectedIndexChanged += new System.EventHandler(this.skillist_SelectedIndexChanged);
            // 
            // suggester
            // 
            this.suggester.Location = new System.Drawing.Point(12, 12);
            this.suggester.Name = "suggester";
            this.suggester.Size = new System.Drawing.Size(120, 20);
            this.suggester.TabIndex = 1;
            this.suggester.TextChanged += new System.EventHandler(this.suggester_TextChanged);
            // 
            // iddisplay
            // 
            this.iddisplay.AutoSize = true;
            this.iddisplay.Location = new System.Drawing.Point(138, 15);
            this.iddisplay.Name = "iddisplay";
            this.iddisplay.Size = new System.Drawing.Size(0, 13);
            this.iddisplay.TabIndex = 2;
            // 
            // levelselect
            // 
            this.levelselect.Location = new System.Drawing.Point(141, 44);
            this.levelselect.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.levelselect.Name = "levelselect";
            this.levelselect.Size = new System.Drawing.Size(123, 20);
            this.levelselect.TabIndex = 3;
            this.levelselect.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.levelselect.ValueChanged += new System.EventHandler(this.levelselect_ValueChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(141, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(141, 103);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // SkillSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 132);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.levelselect);
            this.Controls.Add(this.iddisplay);
            this.Controls.Add(this.suggester);
            this.Controls.Add(this.skillist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SkillSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SkillSelector";
            this.Load += new System.EventHandler(this.SkillSelector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.levelselect)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox skillist;
        private System.Windows.Forms.TextBox suggester;
        private System.Windows.Forms.Label iddisplay;
        private System.Windows.Forms.NumericUpDown levelselect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}