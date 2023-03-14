namespace MagicEditor
{
    partial class ItemAndSetTypeSelector
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
            this.ItemTypes = new System.Windows.Forms.CheckedListBox();
            this.SetTypes = new System.Windows.Forms.CheckedListBox();
            this.okbutty = new System.Windows.Forms.Button();
            this.hellno = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ItemTypes
            // 
            this.ItemTypes.FormattingEnabled = true;
            this.ItemTypes.Location = new System.Drawing.Point(12, 12);
            this.ItemTypes.Name = "ItemTypes";
            this.ItemTypes.Size = new System.Drawing.Size(217, 319);
            this.ItemTypes.TabIndex = 0;
            // 
            // SetTypes
            // 
            this.SetTypes.FormattingEnabled = true;
            this.SetTypes.Location = new System.Drawing.Point(235, 12);
            this.SetTypes.Name = "SetTypes";
            this.SetTypes.Size = new System.Drawing.Size(217, 319);
            this.SetTypes.TabIndex = 1;
            // 
            // okbutty
            // 
            this.okbutty.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okbutty.Location = new System.Drawing.Point(296, 337);
            this.okbutty.Name = "okbutty";
            this.okbutty.Size = new System.Drawing.Size(75, 23);
            this.okbutty.TabIndex = 2;
            this.okbutty.Text = "Save";
            this.okbutty.UseVisualStyleBackColor = true;
            this.okbutty.Click += new System.EventHandler(this.okbutty_Click);
            // 
            // hellno
            // 
            this.hellno.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.hellno.Location = new System.Drawing.Point(377, 337);
            this.hellno.Name = "hellno";
            this.hellno.Size = new System.Drawing.Size(75, 23);
            this.hellno.TabIndex = 3;
            this.hellno.Text = "Cancel";
            this.hellno.UseVisualStyleBackColor = true;
            this.hellno.Click += new System.EventHandler(this.hellno_Click);
            // 
            // ItemAndSetTypeSelector
            // 
            this.AcceptButton = this.okbutty;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.hellno;
            this.ClientSize = new System.Drawing.Size(464, 370);
            this.Controls.Add(this.hellno);
            this.Controls.Add(this.okbutty);
            this.Controls.Add(this.SetTypes);
            this.Controls.Add(this.ItemTypes);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ItemAndSetTypeSelector";
            this.Text = "Select Item Types and Set Types";
            this.Load += new System.EventHandler(this.ItemAndSetTypeSelector_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox ItemTypes;
        private System.Windows.Forms.CheckedListBox SetTypes;
        private System.Windows.Forms.Button okbutty;
        private System.Windows.Forms.Button hellno;
    }
}