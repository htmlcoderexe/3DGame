namespace MagicEditor
{
    partial class EditAbilityComponent
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
            this.label1 = new System.Windows.Forms.Label();
            this.okSave = new System.Windows.Forms.Button();
            this.cancelbutt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(297, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "<ITEM_NAME>";
            // 
            // okSave
            // 
            this.okSave.Location = new System.Drawing.Point(632, 415);
            this.okSave.Name = "okSave";
            this.okSave.Size = new System.Drawing.Size(75, 23);
            this.okSave.TabIndex = 1;
            this.okSave.Text = "Save";
            this.okSave.UseVisualStyleBackColor = true;
            this.okSave.Click += new System.EventHandler(this.okSave_Click);
            // 
            // cancelbutt
            // 
            this.cancelbutt.Location = new System.Drawing.Point(713, 415);
            this.cancelbutt.Name = "cancelbutt";
            this.cancelbutt.Size = new System.Drawing.Size(75, 23);
            this.cancelbutt.TabIndex = 2;
            this.cancelbutt.Text = "Cancel";
            this.cancelbutt.UseVisualStyleBackColor = true;
            this.cancelbutt.Click += new System.EventHandler(this.cancelbutt_Click);
            // 
            // EditAbilityComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cancelbutt);
            this.Controls.Add(this.okSave);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditAbilityComponent";
            this.Text = "EditAbilityComponent";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okSave;
        private System.Windows.Forms.Button cancelbutt;
    }
}