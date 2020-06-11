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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.EffectList = new System.Windows.Forms.ListView();
            this.typefriendly = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.icons = new System.Windows.Forms.ImageList(this.components);
            this.descprev = new System.Windows.Forms.Label();
            this.lvlprev = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.lvlprev)).BeginInit();
            this.SuspendLayout();
            // 
            // EffectList
            // 
            this.EffectList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.typefriendly,
            this.time,
            this.duration});
            this.EffectList.HideSelection = false;
            this.EffectList.Location = new System.Drawing.Point(290, 76);
            this.EffectList.Name = "EffectList";
            this.EffectList.Size = new System.Drawing.Size(484, 336);
            this.EffectList.SmallImageList = this.icons;
            this.EffectList.TabIndex = 0;
            this.EffectList.UseCompatibleStateImageBehavior = false;
            this.EffectList.View = System.Windows.Forms.View.Details;
            this.EffectList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.EffectList_MouseDoubleClick);
            // 
            // typefriendly
            // 
            this.typefriendly.Text = "Item";
            // 
            // time
            // 
            this.time.Text = "Time";
            // 
            // duration
            // 
            this.duration.Text = "Duration";
            // 
            // icons
            // 
            this.icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("icons.ImageStream")));
            this.icons.TransparentColor = System.Drawing.Color.Magenta;
            this.icons.Images.SetKeyName(0, "icon_vfx.bmp");
            this.icons.Images.SetKeyName(1, "icon_damage.bmp");
            // 
            // descprev
            // 
            this.descprev.Location = new System.Drawing.Point(12, 76);
            this.descprev.Name = "descprev";
            this.descprev.Size = new System.Drawing.Size(158, 152);
            this.descprev.TabIndex = 1;
            this.descprev.Text = "<>";
            // 
            // lvlprev
            // 
            this.lvlprev.Location = new System.Drawing.Point(25, 35);
            this.lvlprev.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.lvlprev.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lvlprev.Name = "lvlprev";
            this.lvlprev.Size = new System.Drawing.Size(120, 20);
            this.lvlprev.TabIndex = 2;
            this.lvlprev.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lvlprev.ValueChanged += new System.EventHandler(this.lvlprev_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 516);
            this.Controls.Add(this.lvlprev);
            this.Controls.Add(this.descprev);
            this.Controls.Add(this.EffectList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lvlprev)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView EffectList;
        private System.Windows.Forms.ImageList icons;
        private System.Windows.Forms.ColumnHeader time;
        private System.Windows.Forms.ColumnHeader duration;
        private System.Windows.Forms.ColumnHeader typefriendly;
        private System.Windows.Forms.Label descprev;
        private System.Windows.Forms.NumericUpDown lvlprev;
    }
}

