namespace MagicEditor
{
    partial class TextPrompt
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.inputbox = new System.Windows.Forms.TextBox();
            this.okbutt = new System.Windows.Forms.Button();
            this.cancelbutt = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.inputbox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.okbutt, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cancelbutt, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(529, 310);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // inputbox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.inputbox, 2);
            this.inputbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputbox.Location = new System.Drawing.Point(3, 3);
            this.inputbox.Multiline = true;
            this.inputbox.Name = "inputbox";
            this.inputbox.Size = new System.Drawing.Size(523, 272);
            this.inputbox.TabIndex = 0;
            // 
            // okbutt
            // 
            this.okbutt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.okbutt.Location = new System.Drawing.Point(3, 281);
            this.okbutt.Name = "okbutt";
            this.okbutt.Size = new System.Drawing.Size(258, 26);
            this.okbutt.TabIndex = 1;
            this.okbutt.Text = "OK";
            this.okbutt.UseVisualStyleBackColor = true;
            this.okbutt.Click += new System.EventHandler(this.okbutt_Click);
            // 
            // cancelbutt
            // 
            this.cancelbutt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cancelbutt.Location = new System.Drawing.Point(267, 281);
            this.cancelbutt.Name = "cancelbutt";
            this.cancelbutt.Size = new System.Drawing.Size(259, 26);
            this.cancelbutt.TabIndex = 2;
            this.cancelbutt.Text = "Cancel";
            this.cancelbutt.UseVisualStyleBackColor = true;
            this.cancelbutt.Click += new System.EventHandler(this.cancelbutt_Click);
            // 
            // TextPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 310);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextPrompt";
            this.Text = "TextPrompt";
            this.Load += new System.EventHandler(this.TextPrompt_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox inputbox;
        private System.Windows.Forms.Button okbutt;
        private System.Windows.Forms.Button cancelbutt;
    }
}