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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.basetimevalue = new System.Windows.Forms.NumericUpDown();
            this.deltatimevalue = new System.Windows.Forms.NumericUpDown();
            this.basedurationvalue = new System.Windows.Forms.NumericUpDown();
            this.deltadurationvalue = new System.Windows.Forms.NumericUpDown();
            this.desclabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.basetimevalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deltatimevalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.basedurationvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deltadurationvalue)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(352, 9);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Start time:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(146, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Delta time:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(146, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Delta duration:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Duration:";
            // 
            // basetimevalue
            // 
            this.basetimevalue.DecimalPlaces = 1;
            this.basetimevalue.Location = new System.Drawing.Point(80, 17);
            this.basetimevalue.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.basetimevalue.Name = "basetimevalue";
            this.basetimevalue.Size = new System.Drawing.Size(60, 20);
            this.basetimevalue.TabIndex = 7;
            // 
            // deltatimevalue
            // 
            this.deltatimevalue.DecimalPlaces = 1;
            this.deltatimevalue.Location = new System.Drawing.Point(218, 17);
            this.deltatimevalue.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.deltatimevalue.Name = "deltatimevalue";
            this.deltatimevalue.Size = new System.Drawing.Size(60, 20);
            this.deltatimevalue.TabIndex = 8;
            // 
            // basedurationvalue
            // 
            this.basedurationvalue.DecimalPlaces = 1;
            this.basedurationvalue.Location = new System.Drawing.Point(80, 51);
            this.basedurationvalue.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.basedurationvalue.Name = "basedurationvalue";
            this.basedurationvalue.Size = new System.Drawing.Size(60, 20);
            this.basedurationvalue.TabIndex = 9;
            // 
            // deltadurationvalue
            // 
            this.deltadurationvalue.DecimalPlaces = 1;
            this.deltadurationvalue.Location = new System.Drawing.Point(218, 51);
            this.deltadurationvalue.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.deltadurationvalue.Name = "deltadurationvalue";
            this.deltadurationvalue.Size = new System.Drawing.Size(60, 20);
            this.deltadurationvalue.TabIndex = 10;
            // 
            // desclabel
            // 
            this.desclabel.AutoSize = true;
            this.desclabel.Location = new System.Drawing.Point(354, 53);
            this.desclabel.Name = "desclabel";
            this.desclabel.Size = new System.Drawing.Size(124, 13);
            this.desclabel.TabIndex = 11;
            this.desclabel.Text = "<ITEM_DESCRIPTION>";
            // 
            // EditAbilityComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.desclabel);
            this.Controls.Add(this.deltadurationvalue);
            this.Controls.Add(this.basedurationvalue);
            this.Controls.Add(this.deltatimevalue);
            this.Controls.Add(this.basetimevalue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelbutt);
            this.Controls.Add(this.okSave);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditAbilityComponent";
            this.Text = "EditAbilityComponent";
            ((System.ComponentModel.ISupportInitialize)(this.basetimevalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deltatimevalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.basedurationvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deltadurationvalue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okSave;
        private System.Windows.Forms.Button cancelbutt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown basetimevalue;
        private System.Windows.Forms.NumericUpDown deltatimevalue;
        private System.Windows.Forms.NumericUpDown basedurationvalue;
        private System.Windows.Forms.NumericUpDown deltadurationvalue;
        private System.Windows.Forms.Label desclabel;
    }
}