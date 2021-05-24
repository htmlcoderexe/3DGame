namespace MagicEditor
{
    partial class ItemTypeSelector
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
            this.cancelbutt = new System.Windows.Forms.Button();
            this.okbutt = new System.Windows.Forms.Button();
            this.iddisplay = new System.Windows.Forms.Label();
            this.suggester = new System.Windows.Forms.TextBox();
            this.notselected = new System.Windows.Forms.ListBox();
            this.selected = new System.Windows.Forms.ListBox();
            this.rmbutt = new System.Windows.Forms.Button();
            this.addbutt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancelbutt
            // 
            this.cancelbutt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbutt.Location = new System.Drawing.Point(12, 197);
            this.cancelbutt.Name = "cancelbutt";
            this.cancelbutt.Size = new System.Drawing.Size(120, 23);
            this.cancelbutt.TabIndex = 11;
            this.cancelbutt.Text = "Cancel";
            this.cancelbutt.UseVisualStyleBackColor = true;
            // 
            // okbutt
            // 
            this.okbutt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okbutt.Location = new System.Drawing.Point(12, 168);
            this.okbutt.Name = "okbutt";
            this.okbutt.Size = new System.Drawing.Size(120, 23);
            this.okbutt.TabIndex = 10;
            this.okbutt.Text = "OK";
            this.okbutt.UseVisualStyleBackColor = true;
            this.okbutt.Click += new System.EventHandler(this.okbutt_Click);
            // 
            // iddisplay
            // 
            this.iddisplay.AutoSize = true;
            this.iddisplay.Location = new System.Drawing.Point(264, 12);
            this.iddisplay.Name = "iddisplay";
            this.iddisplay.Size = new System.Drawing.Size(0, 13);
            this.iddisplay.TabIndex = 8;
            // 
            // suggester
            // 
            this.suggester.Location = new System.Drawing.Point(198, 9);
            this.suggester.Name = "suggester";
            this.suggester.Size = new System.Drawing.Size(120, 20);
            this.suggester.TabIndex = 7;
            this.suggester.TextChanged += new System.EventHandler(this.suggester_TextChanged);
            // 
            // notselected
            // 
            this.notselected.DisplayMember = "Name";
            this.notselected.FormattingEnabled = true;
            this.notselected.HorizontalScrollbar = true;
            this.notselected.Location = new System.Drawing.Point(198, 41);
            this.notselected.Name = "notselected";
            this.notselected.Size = new System.Drawing.Size(120, 173);
            this.notselected.TabIndex = 6;
            // 
            // selected
            // 
            this.selected.DisplayMember = "Name";
            this.selected.FormattingEnabled = true;
            this.selected.HorizontalScrollbar = true;
            this.selected.Location = new System.Drawing.Point(12, 41);
            this.selected.Name = "selected";
            this.selected.Size = new System.Drawing.Size(120, 121);
            this.selected.TabIndex = 12;
            // 
            // rmbutt
            // 
            this.rmbutt.Location = new System.Drawing.Point(152, 63);
            this.rmbutt.Name = "rmbutt";
            this.rmbutt.Size = new System.Drawing.Size(28, 26);
            this.rmbutt.TabIndex = 13;
            this.rmbutt.Text = ">";
            this.rmbutt.UseVisualStyleBackColor = true;
            this.rmbutt.Click += new System.EventHandler(this.rmbutt_Click);
            // 
            // addbutt
            // 
            this.addbutt.Location = new System.Drawing.Point(152, 95);
            this.addbutt.Name = "addbutt";
            this.addbutt.Size = new System.Drawing.Size(28, 26);
            this.addbutt.TabIndex = 14;
            this.addbutt.Text = "<";
            this.addbutt.UseVisualStyleBackColor = true;
            this.addbutt.Click += new System.EventHandler(this.addbutt_Click);
            // 
            // ItemTypeSelector
            // 
            this.AcceptButton = this.okbutt;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbutt;
            this.ClientSize = new System.Drawing.Size(327, 229);
            this.Controls.Add(this.addbutt);
            this.Controls.Add(this.rmbutt);
            this.Controls.Add(this.selected);
            this.Controls.Add(this.cancelbutt);
            this.Controls.Add(this.okbutt);
            this.Controls.Add(this.iddisplay);
            this.Controls.Add(this.suggester);
            this.Controls.Add(this.notselected);
            this.Name = "ItemTypeSelector";
            this.Text = "ItemTypeSelector";
            this.Load += new System.EventHandler(this.ItemTypeSelector_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelbutt;
        private System.Windows.Forms.Button okbutt;
        private System.Windows.Forms.Label iddisplay;
        private System.Windows.Forms.TextBox suggester;
        private System.Windows.Forms.ListBox notselected;
        private System.Windows.Forms.ListBox selected;
        private System.Windows.Forms.Button rmbutt;
        private System.Windows.Forms.Button addbutt;
    }
}