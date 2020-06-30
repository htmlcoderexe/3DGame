namespace MagicEditor
{
    partial class SkillTreeEntryEditor
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
            this.SkillName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.learnlevel = new System.Windows.Forms.NumericUpDown();
            this.column = new System.Windows.Forms.NumericUpDown();
            this.expbase = new System.Windows.Forms.NumericUpDown();
            this.expdelta = new System.Windows.Forms.NumericUpDown();
            this.maxlvl = new System.Windows.Forms.NumericUpDown();
            this.traininglevel = new System.Windows.Forms.NumericUpDown();
            this.requireitemid = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.skill_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.req_level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.learnlevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.column)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expbase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expdelta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxlvl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.traininglevel)).BeginInit();
            this.SuspendLayout();
            // 
            // SkillName
            // 
            this.SkillName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SkillName.Location = new System.Drawing.Point(12, 9);
            this.SkillName.Name = "SkillName";
            this.SkillName.Size = new System.Drawing.Size(164, 46);
            this.SkillName.TabIndex = 24;
            this.SkillName.Text = "<>";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Learned at level:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Skill tree column:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Base EXP:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Delta EXP:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Level cap:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 217);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Training level:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 244);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "Requires item:";
            // 
            // learnlevel
            // 
            this.learnlevel.Location = new System.Drawing.Point(131, 84);
            this.learnlevel.Maximum = new decimal(new int[] {
            243,
            0,
            0,
            0});
            this.learnlevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.learnlevel.Name = "learnlevel";
            this.learnlevel.Size = new System.Drawing.Size(120, 20);
            this.learnlevel.TabIndex = 32;
            this.learnlevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // column
            // 
            this.column.Location = new System.Drawing.Point(131, 110);
            this.column.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.column.Name = "column";
            this.column.Size = new System.Drawing.Size(120, 20);
            this.column.TabIndex = 33;
            this.column.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // expbase
            // 
            this.expbase.Location = new System.Drawing.Point(131, 136);
            this.expbase.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.expbase.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.expbase.Name = "expbase";
            this.expbase.Size = new System.Drawing.Size(120, 20);
            this.expbase.TabIndex = 34;
            this.expbase.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // expdelta
            // 
            this.expdelta.Location = new System.Drawing.Point(131, 162);
            this.expdelta.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.expdelta.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.expdelta.Name = "expdelta";
            this.expdelta.Size = new System.Drawing.Size(120, 20);
            this.expdelta.TabIndex = 35;
            this.expdelta.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // maxlvl
            // 
            this.maxlvl.Location = new System.Drawing.Point(131, 188);
            this.maxlvl.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.maxlvl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxlvl.Name = "maxlvl";
            this.maxlvl.Size = new System.Drawing.Size(120, 20);
            this.maxlvl.TabIndex = 36;
            this.maxlvl.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // traininglevel
            // 
            this.traininglevel.Location = new System.Drawing.Point(131, 215);
            this.traininglevel.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.traininglevel.Name = "traininglevel";
            this.traininglevel.Size = new System.Drawing.Size(120, 20);
            this.traininglevel.TabIndex = 37;
            this.traininglevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // requireitemid
            // 
            this.requireitemid.Location = new System.Drawing.Point(131, 241);
            this.requireitemid.Name = "requireitemid";
            this.requireitemid.Size = new System.Drawing.Size(120, 20);
            this.requireitemid.TabIndex = 38;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.skill_name,
            this.req_level});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(325, 53);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(463, 208);
            this.listView1.TabIndex = 39;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // skill_name
            // 
            this.skill_name.Text = "Skill name";
            this.skill_name.Width = 219;
            // 
            // req_level
            // 
            this.req_level.Text = "Required level";
            this.req_level.Width = 97;
            // 
            // SkillTreeEntryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.requireitemid);
            this.Controls.Add(this.traininglevel);
            this.Controls.Add(this.maxlvl);
            this.Controls.Add(this.expdelta);
            this.Controls.Add(this.expbase);
            this.Controls.Add(this.column);
            this.Controls.Add(this.learnlevel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SkillName);
            this.Name = "SkillTreeEntryEditor";
            this.Text = "SkillTreeEntryEditor";
            ((System.ComponentModel.ISupportInitialize)(this.learnlevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.column)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.expbase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.expdelta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxlvl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.traininglevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SkillName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown learnlevel;
        private System.Windows.Forms.NumericUpDown column;
        private System.Windows.Forms.NumericUpDown expbase;
        private System.Windows.Forms.NumericUpDown expdelta;
        private System.Windows.Forms.NumericUpDown maxlvl;
        private System.Windows.Forms.NumericUpDown traininglevel;
        private System.Windows.Forms.TextBox requireitemid;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader skill_name;
        private System.Windows.Forms.ColumnHeader req_level;
    }
}