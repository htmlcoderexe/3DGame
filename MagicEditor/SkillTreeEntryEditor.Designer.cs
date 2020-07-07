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
            this.components = new System.ComponentModel.Container();
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
            this.requisitelist = new System.Windows.Forms.ListView();
            this.skill_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.req_level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.requisitelistmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addRequisiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeRequisiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.learnlevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.column)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expbase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expdelta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxlvl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.traininglevel)).BeginInit();
            this.requisitelistmenu.SuspendLayout();
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
            // requisitelist
            // 
            this.requisitelist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.skill_name,
            this.req_level});
            this.requisitelist.ContextMenuStrip = this.requisitelistmenu;
            this.requisitelist.HideSelection = false;
            this.requisitelist.Location = new System.Drawing.Point(325, 53);
            this.requisitelist.Name = "requisitelist";
            this.requisitelist.Size = new System.Drawing.Size(463, 208);
            this.requisitelist.TabIndex = 39;
            this.requisitelist.UseCompatibleStateImageBehavior = false;
            this.requisitelist.View = System.Windows.Forms.View.Details;
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
            // requisitelistmenu
            // 
            this.requisitelistmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRequisiteToolStripMenuItem,
            this.removeRequisiteToolStripMenuItem});
            this.requisitelistmenu.Name = "requisitelistmenu";
            this.requisitelistmenu.Size = new System.Drawing.Size(166, 48);
            this.requisitelistmenu.Opening += new System.ComponentModel.CancelEventHandler(this.requisitelistmenu_Opening);
            // 
            // addRequisiteToolStripMenuItem
            // 
            this.addRequisiteToolStripMenuItem.Name = "addRequisiteToolStripMenuItem";
            this.addRequisiteToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.addRequisiteToolStripMenuItem.Text = "Add requisite";
            this.addRequisiteToolStripMenuItem.Click += new System.EventHandler(this.addRequisiteToolStripMenuItem_Click);
            // 
            // removeRequisiteToolStripMenuItem
            // 
            this.removeRequisiteToolStripMenuItem.Name = "removeRequisiteToolStripMenuItem";
            this.removeRequisiteToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.removeRequisiteToolStripMenuItem.Text = "Remove requisite";
            this.removeRequisiteToolStripMenuItem.Click += new System.EventHandler(this.removeRequisiteToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 271);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 40;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(101, 271);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 41;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // SkillTreeEntryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 306);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.requisitelist);
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
            this.requisitelistmenu.ResumeLayout(false);
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
        private System.Windows.Forms.ListView requisitelist;
        private System.Windows.Forms.ColumnHeader skill_name;
        private System.Windows.Forms.ColumnHeader req_level;
        private System.Windows.Forms.ContextMenuStrip requisitelistmenu;
        private System.Windows.Forms.ToolStripMenuItem addRequisiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeRequisiteToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}