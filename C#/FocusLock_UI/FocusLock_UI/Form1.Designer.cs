namespace FocusLock_UI
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Interface = new System.Windows.Forms.Panel();
            this.Hour_lbl = new System.Windows.Forms.Label();
            this.Min_lbl = new System.Windows.Forms.Label();
            this.Min_txt = new System.Windows.Forms.TextBox();
            this.Hour_txt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Time_But = new System.Windows.Forms.Button();
            this.Calendar_but = new System.Windows.Forms.Button();
            this.Extern_but = new System.Windows.Forms.Button();
            this.Web_lbl = new System.Windows.Forms.Label();
            this.Program_lbl = new System.Windows.Forms.Label();
            this.Web_but = new System.Windows.Forms.Button();
            this.Program_but = new System.Windows.Forms.Button();
            this.Interface.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Interface
            // 
            this.Interface.BackColor = System.Drawing.Color.White;
            this.Interface.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Interface.Controls.Add(this.Program_but);
            this.Interface.Controls.Add(this.Web_but);
            this.Interface.Controls.Add(this.Web_lbl);
            this.Interface.Controls.Add(this.Program_lbl);
            this.Interface.Controls.Add(this.Hour_lbl);
            this.Interface.Controls.Add(this.Min_lbl);
            this.Interface.Controls.Add(this.Min_txt);
            this.Interface.Controls.Add(this.Hour_txt);
            this.Interface.Controls.Add(this.button1);
            this.Interface.Controls.Add(this.pictureBox1);
            this.Interface.Location = new System.Drawing.Point(190, 35);
            this.Interface.Name = "Interface";
            this.Interface.Size = new System.Drawing.Size(519, 303);
            this.Interface.TabIndex = 0;
            this.Interface.Paint += new System.Windows.Forms.PaintEventHandler(this.Interface_Paint);
            // 
            // Hour_lbl
            // 
            this.Hour_lbl.AutoSize = true;
            this.Hour_lbl.Font = new System.Drawing.Font("Montserrat", 10F);
            this.Hour_lbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(107)))), ((int)(((byte)(10)))));
            this.Hour_lbl.Location = new System.Drawing.Point(61, 49);
            this.Hour_lbl.Name = "Hour_lbl";
            this.Hour_lbl.Size = new System.Drawing.Size(54, 23);
            this.Hour_lbl.TabIndex = 5;
            this.Hour_lbl.Text = "TIMER";
            this.Hour_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Min_lbl
            // 
            this.Min_lbl.AutoSize = true;
            this.Min_lbl.Font = new System.Drawing.Font("Montserrat", 10F);
            this.Min_lbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(107)))), ((int)(((byte)(10)))));
            this.Min_lbl.Location = new System.Drawing.Point(45, 110);
            this.Min_lbl.Name = "Min_lbl";
            this.Min_lbl.Size = new System.Drawing.Size(84, 23);
            this.Min_lbl.TabIndex = 4;
            this.Min_lbl.Text = "MINUTTER";
            this.Min_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Min_txt
            // 
            this.Min_txt.AcceptsTab = true;
            this.Min_txt.Font = new System.Drawing.Font("Montserrat", 10F);
            this.Min_txt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(107)))), ((int)(((byte)(10)))));
            this.Min_txt.Location = new System.Drawing.Point(37, 136);
            this.Min_txt.Name = "Min_txt";
            this.Min_txt.Size = new System.Drawing.Size(100, 24);
            this.Min_txt.TabIndex = 3;
            this.Min_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Hour_txt
            // 
            this.Hour_txt.AcceptsTab = true;
            this.Hour_txt.Font = new System.Drawing.Font("Montserrat", 10F);
            this.Hour_txt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(107)))), ((int)(((byte)(10)))));
            this.Hour_txt.Location = new System.Drawing.Point(37, 75);
            this.Hour_txt.Name = "Hour_txt";
            this.Hour_txt.Size = new System.Drawing.Size(100, 24);
            this.Hour_txt.TabIndex = 2;
            this.Hour_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Montserrat", 10F);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(107)))), ((int)(((byte)(10)))));
            this.button1.Location = new System.Drawing.Point(160, 228);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 55);
            this.button1.TabIndex = 1;
            this.button1.Text = "START";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(209, 167);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 55);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Time_But
            // 
            this.Time_But.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.Time_But.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Time_But.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(107)))), ((int)(((byte)(10)))));
            this.Time_But.FlatAppearance.BorderSize = 0;
            this.Time_But.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(107)))), ((int)(((byte)(10)))));
            this.Time_But.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(107)))), ((int)(((byte)(10)))));
            this.Time_But.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Time_But.Font = new System.Drawing.Font("Montserrat", 10F);
            this.Time_But.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(107)))), ((int)(((byte)(10)))));
            this.Time_But.Location = new System.Drawing.Point(19, 51);
            this.Time_But.Name = "Time_But";
            this.Time_But.Size = new System.Drawing.Size(151, 62);
            this.Time_But.TabIndex = 1;
            this.Time_But.Text = "TID";
            this.Time_But.UseVisualStyleBackColor = false;
            this.Time_But.Click += new System.EventHandler(this.Time_But_Click);
            this.Time_But.Paint += new System.Windows.Forms.PaintEventHandler(this.Time_But_Paint);
            this.Time_But.MouseEnter += new System.EventHandler(this.Time_But_MouseEnter);
            this.Time_But.MouseLeave += new System.EventHandler(this.Time_But_MouseLeave);
            // 
            // Calendar_but
            // 
            this.Calendar_but.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Calendar_but.Font = new System.Drawing.Font("Montserrat", 10F);
            this.Calendar_but.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(107)))), ((int)(((byte)(10)))));
            this.Calendar_but.Location = new System.Drawing.Point(19, 154);
            this.Calendar_but.Name = "Calendar_but";
            this.Calendar_but.Size = new System.Drawing.Size(151, 62);
            this.Calendar_but.TabIndex = 2;
            this.Calendar_but.Text = "KALENDER";
            this.Calendar_but.UseVisualStyleBackColor = false;
            this.Calendar_but.Click += new System.EventHandler(this.Calendar_but_Click);
            this.Calendar_but.Paint += new System.Windows.Forms.PaintEventHandler(this.Calendar_but_Paint);
            this.Calendar_but.MouseEnter += new System.EventHandler(this.Calendar_but_MouseEnter);
            this.Calendar_but.MouseLeave += new System.EventHandler(this.Calendar_but_MouseLeave);
            // 
            // Extern_but
            // 
            this.Extern_but.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Extern_but.Font = new System.Drawing.Font("Montserrat", 10F);
            this.Extern_but.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(107)))), ((int)(((byte)(10)))));
            this.Extern_but.Location = new System.Drawing.Point(19, 257);
            this.Extern_but.Name = "Extern_but";
            this.Extern_but.Size = new System.Drawing.Size(151, 62);
            this.Extern_but.TabIndex = 3;
            this.Extern_but.Text = "EKSTERN";
            this.Extern_but.UseVisualStyleBackColor = false;
            this.Extern_but.Click += new System.EventHandler(this.Extern_but_Click);
            this.Extern_but.Paint += new System.Windows.Forms.PaintEventHandler(this.Extern_but_Paint);
            this.Extern_but.MouseEnter += new System.EventHandler(this.Extern_but_MouseEnter);
            this.Extern_but.MouseLeave += new System.EventHandler(this.Extern_but_MouseLeave);
            // 
            // Web_lbl
            // 
            this.Web_lbl.AutoSize = true;
            this.Web_lbl.Font = new System.Drawing.Font("Montserrat", 10F);
            this.Web_lbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(107)))), ((int)(((byte)(10)))));
            this.Web_lbl.Location = new System.Drawing.Point(310, 49);
            this.Web_lbl.Name = "Web_lbl";
            this.Web_lbl.Size = new System.Drawing.Size(193, 23);
            this.Web_lbl.TabIndex = 7;
            this.Web_lbl.Text = "BLOKERET HJEMMESIDER";
            this.Web_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Program_lbl
            // 
            this.Program_lbl.AutoSize = true;
            this.Program_lbl.Font = new System.Drawing.Font("Montserrat", 10F);
            this.Program_lbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(107)))), ((int)(((byte)(10)))));
            this.Program_lbl.Location = new System.Drawing.Point(310, 127);
            this.Program_lbl.Name = "Program_lbl";
            this.Program_lbl.Size = new System.Drawing.Size(194, 23);
            this.Program_lbl.TabIndex = 6;
            this.Program_lbl.Text = "BLOKERET PROGRAMMER";
            this.Program_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Web_but
            // 
            this.Web_but.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Web_but.Font = new System.Drawing.Font("Montserrat", 10F);
            this.Web_but.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(107)))), ((int)(((byte)(10)))));
            this.Web_but.Location = new System.Drawing.Point(356, 75);
            this.Web_but.Name = "Web_but";
            this.Web_but.Size = new System.Drawing.Size(100, 37);
            this.Web_but.TabIndex = 8;
            this.Web_but.Text = "ÅBEN";
            this.Web_but.UseVisualStyleBackColor = true;
            // 
            // Program_but
            // 
            this.Program_but.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Program_but.Font = new System.Drawing.Font("Montserrat", 10F);
            this.Program_but.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(107)))), ((int)(((byte)(10)))));
            this.Program_but.Location = new System.Drawing.Point(356, 153);
            this.Program_but.Name = "Program_but";
            this.Program_but.Size = new System.Drawing.Size(100, 37);
            this.Program_but.TabIndex = 9;
            this.Program_but.Text = "ÅBEN";
            this.Program_but.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(817, 378);
            this.Controls.Add(this.Extern_but);
            this.Controls.Add(this.Calendar_but);
            this.Controls.Add(this.Time_But);
            this.Controls.Add(this.Interface);
            this.HelpButton = true;
            this.Name = "Main";
            this.Text = "FocusLock";
            this.Interface.ResumeLayout(false);
            this.Interface.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Interface;
        private System.Windows.Forms.Button Time_But;
        private System.Windows.Forms.Button Calendar_but;
        private System.Windows.Forms.Button Extern_but;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Hour_txt;
        private System.Windows.Forms.Label Min_lbl;
        private System.Windows.Forms.TextBox Min_txt;
        private System.Windows.Forms.Label Hour_lbl;
        private System.Windows.Forms.Button Web_but;
        private System.Windows.Forms.Label Web_lbl;
        private System.Windows.Forms.Label Program_lbl;
        private System.Windows.Forms.Button Program_but;
    }
}

