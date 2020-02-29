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
            this.scrubber = new System.Windows.Forms.TrackBar();
            this.playpausebutt = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.next = new System.Windows.Forms.Button();
            this.prev = new System.Windows.Forms.Button();
            this.load = new System.Windows.Forms.Button();
            this.list = new System.Windows.Forms.ComboBox();
            this.time = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.scrubber)).BeginInit();
            this.SuspendLayout();
            // 
            // scrubber
            // 
            this.scrubber.Location = new System.Drawing.Point(113, 12);
            this.scrubber.Name = "scrubber";
            this.scrubber.Size = new System.Drawing.Size(604, 45);
            this.scrubber.TabIndex = 0;
            this.scrubber.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // playpausebutt
            // 
            this.playpausebutt.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playpausebutt.Location = new System.Drawing.Point(12, 57);
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
            this.stop.Location = new System.Drawing.Point(53, 57);
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
            this.next.Location = new System.Drawing.Point(135, 57);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(35, 24);
            this.next.TabIndex = 4;
            this.next.Text = "⏭";
            this.next.UseVisualStyleBackColor = true;
            // 
            // prev
            // 
            this.prev.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prev.Location = new System.Drawing.Point(94, 57);
            this.prev.Name = "prev";
            this.prev.Size = new System.Drawing.Size(35, 24);
            this.prev.TabIndex = 3;
            this.prev.Text = "⏮";
            this.prev.UseVisualStyleBackColor = true;
            // 
            // load
            // 
            this.load.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.load.Location = new System.Drawing.Point(502, 57);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(35, 24);
            this.load.TabIndex = 4;
            this.load.Text = "⏏";
            this.load.UseVisualStyleBackColor = true;
            // 
            // list
            // 
            this.list.FormattingEnabled = true;
            this.list.Location = new System.Drawing.Point(176, 59);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(320, 21);
            this.list.TabIndex = 5;
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time.Location = new System.Drawing.Point(12, 12);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(95, 18);
            this.time.TabIndex = 6;
            this.time.Text = "--:--/--:--";
            // 
            // ChoreoPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 90);
            this.Controls.Add(this.time);
            this.Controls.Add(this.list);
            this.Controls.Add(this.load);
            this.Controls.Add(this.next);
            this.Controls.Add(this.prev);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.playpausebutt);
            this.Controls.Add(this.scrubber);
            this.Name = "ChoreoPlayer";
            this.Text = "Player";
            ((System.ComponentModel.ISupportInitialize)(this.scrubber)).EndInit();
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
    }
}