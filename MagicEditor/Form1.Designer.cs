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
            this.EffectList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // EffectList
            // 
            this.EffectList.HideSelection = false;
            this.EffectList.Location = new System.Drawing.Point(290, 76);
            this.EffectList.Name = "EffectList";
            this.EffectList.Size = new System.Drawing.Size(484, 336);
            this.EffectList.TabIndex = 0;
            this.EffectList.UseCompatibleStateImageBehavior = false;
            this.EffectList.View = System.Windows.Forms.View.List;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 516);
            this.Controls.Add(this.EffectList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView EffectList;
    }
}

