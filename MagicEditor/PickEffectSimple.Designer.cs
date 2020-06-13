namespace MagicEditor
{
    partial class PickEffectSimple
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
            this.options = new System.Windows.Forms.ComboBox();
            this.okbutt = new System.Windows.Forms.Button();
            this.cancelbut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // options
            // 
            this.options.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.options.FormattingEnabled = true;
            this.options.Location = new System.Drawing.Point(169, 59);
            this.options.Name = "options";
            this.options.Size = new System.Drawing.Size(121, 21);
            this.options.TabIndex = 0;
            // 
            // okbutt
            // 
            this.okbutt.Location = new System.Drawing.Point(103, 373);
            this.okbutt.Name = "okbutt";
            this.okbutt.Size = new System.Drawing.Size(75, 23);
            this.okbutt.TabIndex = 1;
            this.okbutt.Text = "OK";
            this.okbutt.UseVisualStyleBackColor = true;
            this.okbutt.Click += new System.EventHandler(this.okbutt_Click);
            // 
            // cancelbut
            // 
            this.cancelbut.Location = new System.Drawing.Point(184, 373);
            this.cancelbut.Name = "cancelbut";
            this.cancelbut.Size = new System.Drawing.Size(75, 23);
            this.cancelbut.TabIndex = 2;
            this.cancelbut.Text = "Cancel";
            this.cancelbut.UseVisualStyleBackColor = true;
            this.cancelbut.Click += new System.EventHandler(this.cancelbut_Click);
            // 
            // PickEffectSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cancelbut);
            this.Controls.Add(this.okbutt);
            this.Controls.Add(this.options);
            this.Name = "PickEffectSimple";
            this.Text = "PickEffectSimple";
            this.Load += new System.EventHandler(this.PickEffectSimple_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox options;
        private System.Windows.Forms.Button okbutt;
        private System.Windows.Forms.Button cancelbut;
    }
}