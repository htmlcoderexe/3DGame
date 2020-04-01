namespace ModelEditor
{
    partial class ChoreoPlayer
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
            this.scrubber = new System.Windows.Forms.TrackBar();
            this.playpausebutt = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.next = new System.Windows.Forms.Button();
            this.prev = new System.Windows.Forms.Button();
            this.load = new System.Windows.Forms.Button();
            this.list = new System.Windows.Forms.ComboBox();
            this.time = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.choreocode = new System.Windows.Forms.TextBox();
            this.barupdater = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.scrubber)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrubber
            // 
            this.scrubber.Location = new System.Drawing.Point(129, 14);
            this.scrubber.Name = "scrubber";
            this.scrubber.Size = new System.Drawing.Size(604, 45);
            this.scrubber.TabIndex = 0;
            this.scrubber.TickStyle = System.Windows.Forms.TickStyle.None;
            this.scrubber.Scroll += new System.EventHandler(this.scrubber_Scroll);
            this.scrubber.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scrubber_MouseDown);
            this.scrubber.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scrubber_MouseUp);
            // 
            // playpausebutt
            // 
            this.playpausebutt.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playpausebutt.Location = new System.Drawing.Point(28, 59);
            this.playpausebutt.Name = "playpausebutt";
            this.playpausebutt.Size = new System.Drawing.Size(35, 24);
            this.playpausebutt.TabIndex = 1;
            this.playpausebutt.Text = "▶️";
            this.playpausebutt.UseVisualStyleBackColor = true;
            this.playpausebutt.Click += new System.EventHandler(this.playpausebutt_Click);
            // 
            // stop
            // 
            this.stop.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stop.Location = new System.Drawing.Point(69, 59);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(35, 24);
            this.stop.TabIndex = 2;
            this.stop.Text = "■";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // next
            // 
            this.next.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.next.Location = new System.Drawing.Point(151, 59);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(35, 24);
            this.next.TabIndex = 4;
            this.next.Text = "⏭";
            this.next.UseVisualStyleBackColor = true;
            // 
            // prev
            // 
            this.prev.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prev.Location = new System.Drawing.Point(110, 59);
            this.prev.Name = "prev";
            this.prev.Size = new System.Drawing.Size(35, 24);
            this.prev.TabIndex = 3;
            this.prev.Text = "⏮";
            this.prev.UseVisualStyleBackColor = true;
            // 
            // load
            // 
            this.load.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.load.Location = new System.Drawing.Point(518, 59);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(35, 24);
            this.load.TabIndex = 4;
            this.load.Text = "⏏";
            this.load.UseVisualStyleBackColor = true;
            // 
            // list
            // 
            this.list.FormattingEnabled = true;
            this.list.Location = new System.Drawing.Point(192, 61);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(320, 21);
            this.list.TabIndex = 5;
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time.Location = new System.Drawing.Point(28, 14);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(95, 18);
            this.time.TabIndex = 6;
            this.time.Text = "--:--/--:--";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.playpausebutt);
            this.panel1.Controls.Add(this.time);
            this.panel1.Controls.Add(this.scrubber);
            this.panel1.Controls.Add(this.list);
            this.panel1.Controls.Add(this.stop);
            this.panel1.Controls.Add(this.load);
            this.panel1.Controls.Add(this.prev);
            this.panel1.Controls.Add(this.next);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1114, 100);
            this.panel1.TabIndex = 7;
            // 
            // choreocode
            // 
            this.choreocode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.choreocode.Location = new System.Drawing.Point(0, 100);
            this.choreocode.Multiline = true;
            this.choreocode.Name = "choreocode";
            this.choreocode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.choreocode.Size = new System.Drawing.Size(1114, 433);
            this.choreocode.TabIndex = 8;
            // 
            // barupdater
            // 
            this.barupdater.Interval = 50;
            this.barupdater.Tick += new System.EventHandler(this.barupdater_Tick);
            // 
            // ChoreoPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 533);
            this.Controls.Add(this.choreocode);
            this.Controls.Add(this.panel1);
            this.Name = "ChoreoPlayer";
            this.Text = "Player";
            ((System.ComponentModel.ISupportInitialize)(this.scrubber)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar scrubber;
        private System.Windows.Forms.Button playpausebutt;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button prev;
        private System.Windows.Forms.Button load;
        private System.Windows.Forms.ComboBox list;
        private System.Windows.Forms.Label time;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox choreocode;
        private System.Windows.Forms.Timer barupdater;
    }
}