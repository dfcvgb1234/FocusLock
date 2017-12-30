namespace FocusLock
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Websites_but = new System.Windows.Forms.Button();
            this.Programs_but = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.åbenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startI1TimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startI2TimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kalenderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.åbenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.åbenPlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opdaterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eksternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opdaterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.opdaterToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.logUdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Tray_icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ServiceEventManager = new System.Diagnostics.EventLog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceEventManager)).BeginInit();
            this.SuspendLayout();
            // 
            // Websites_but
            // 
            this.Websites_but.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Websites_but.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Websites_but.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.Websites_but.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Websites_but.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Websites_but.Location = new System.Drawing.Point(358, 12);
            this.Websites_but.Name = "Websites_but";
            this.Websites_but.Size = new System.Drawing.Size(138, 48);
            this.Websites_but.TabIndex = 3;
            this.Websites_but.Text = "Ikke tiladte hjemmesider";
            this.Websites_but.UseVisualStyleBackColor = true;
            this.Websites_but.Click += new System.EventHandler(this.Websites_but_Click);
            // 
            // Programs_but
            // 
            this.Programs_but.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Programs_but.FlatAppearance.BorderSize = 2;
            this.Programs_but.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Programs_but.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.Programs_but.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Programs_but.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Programs_but.Location = new System.Drawing.Point(358, 66);
            this.Programs_but.Name = "Programs_but";
            this.Programs_but.Size = new System.Drawing.Size(138, 48);
            this.Programs_but.TabIndex = 4;
            this.Programs_but.Text = "Ikke tiladte programmer";
            this.Programs_but.UseVisualStyleBackColor = true;
            this.Programs_but.Click += new System.EventHandler(this.Programs_but_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tidToolStripMenuItem,
            this.kalenderToolStripMenuItem,
            this.eksternToolStripMenuItem,
            this.logUdToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(508, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tidToolStripMenuItem
            // 
            this.tidToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.åbenToolStripMenuItem,
            this.startI1TimeToolStripMenuItem,
            this.startI2TimerToolStripMenuItem});
            this.tidToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.tidToolStripMenuItem.Name = "tidToolStripMenuItem";
            this.tidToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.tidToolStripMenuItem.Text = "Tid";
            // 
            // åbenToolStripMenuItem
            // 
            this.åbenToolStripMenuItem.Name = "åbenToolStripMenuItem";
            this.åbenToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.åbenToolStripMenuItem.Text = "Åben";
            this.åbenToolStripMenuItem.Click += new System.EventHandler(this.åbenToolStripMenuItem_Click);
            // 
            // startI1TimeToolStripMenuItem
            // 
            this.startI1TimeToolStripMenuItem.Name = "startI1TimeToolStripMenuItem";
            this.startI1TimeToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.startI1TimeToolStripMenuItem.Text = "Start i 1 time";
            this.startI1TimeToolStripMenuItem.Click += new System.EventHandler(this.startI1TimeToolStripMenuItem_Click);
            // 
            // startI2TimerToolStripMenuItem
            // 
            this.startI2TimerToolStripMenuItem.Name = "startI2TimerToolStripMenuItem";
            this.startI2TimerToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.startI2TimerToolStripMenuItem.Text = "Start i 2 timer";
            this.startI2TimerToolStripMenuItem.Click += new System.EventHandler(this.startI2TimerToolStripMenuItem_Click);
            // 
            // kalenderToolStripMenuItem
            // 
            this.kalenderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.åbenToolStripMenuItem1,
            this.åbenPlanToolStripMenuItem,
            this.opdaterToolStripMenuItem});
            this.kalenderToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.kalenderToolStripMenuItem.Name = "kalenderToolStripMenuItem";
            this.kalenderToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.kalenderToolStripMenuItem.Text = "Kalender";
            // 
            // åbenToolStripMenuItem1
            // 
            this.åbenToolStripMenuItem1.Name = "åbenToolStripMenuItem1";
            this.åbenToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.åbenToolStripMenuItem1.Text = "Åben";
            this.åbenToolStripMenuItem1.Click += new System.EventHandler(this.åbenToolStripMenuItem1_Click);
            // 
            // åbenPlanToolStripMenuItem
            // 
            this.åbenPlanToolStripMenuItem.Name = "åbenPlanToolStripMenuItem";
            this.åbenPlanToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.åbenPlanToolStripMenuItem.Text = "Åben plan";
            this.åbenPlanToolStripMenuItem.Click += new System.EventHandler(this.åbenPlanToolStripMenuItem_Click);
            // 
            // opdaterToolStripMenuItem
            // 
            this.opdaterToolStripMenuItem.Name = "opdaterToolStripMenuItem";
            this.opdaterToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.opdaterToolStripMenuItem.Text = "Opdater";
            this.opdaterToolStripMenuItem.Click += new System.EventHandler(this.opdaterToolStripMenuItem_Click);
            // 
            // eksternToolStripMenuItem
            // 
            this.eksternToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opdaterToolStripMenuItem1,
            this.opdaterToolStripMenuItem2});
            this.eksternToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.eksternToolStripMenuItem.Name = "eksternToolStripMenuItem";
            this.eksternToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.eksternToolStripMenuItem.Text = "Ekstern kontrol";
            // 
            // opdaterToolStripMenuItem1
            // 
            this.opdaterToolStripMenuItem1.Name = "opdaterToolStripMenuItem1";
            this.opdaterToolStripMenuItem1.Size = new System.Drawing.Size(125, 22);
            this.opdaterToolStripMenuItem1.Text = "Åben";
            this.opdaterToolStripMenuItem1.Click += new System.EventHandler(this.opdaterToolStripMenuItem1_Click);
            // 
            // opdaterToolStripMenuItem2
            // 
            this.opdaterToolStripMenuItem2.Name = "opdaterToolStripMenuItem2";
            this.opdaterToolStripMenuItem2.Size = new System.Drawing.Size(125, 22);
            this.opdaterToolStripMenuItem2.Text = "Opdater";
            this.opdaterToolStripMenuItem2.Click += new System.EventHandler(this.opdaterToolStripMenuItem2_Click);
            // 
            // logUdToolStripMenuItem
            // 
            this.logUdToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.logUdToolStripMenuItem.Name = "logUdToolStripMenuItem";
            this.logUdToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.logUdToolStripMenuItem.Text = "Log ud";
            this.logUdToolStripMenuItem.Click += new System.EventHandler(this.logUdToolStripMenuItem_Click);
            // 
            // Tray_icon
            // 
            this.Tray_icon.Icon = ((System.Drawing.Icon)(resources.GetObject("Tray_icon.Icon")));
            this.Tray_icon.Text = "FocusLock";
            this.Tray_icon.Visible = true;
            this.Tray_icon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Tray_icon_MouseDoubleClick);
            // 
            // ServiceEventManager
            // 
            this.ServiceEventManager.SynchronizingObject = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(508, 192);
            this.Controls.Add(this.Programs_but);
            this.Controls.Add(this.Websites_but);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "FocusLock";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceEventManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Websites_but;
        private System.Windows.Forms.Button Programs_but;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem åbenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startI1TimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startI2TimerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kalenderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem åbenToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem åbenPlanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opdaterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eksternToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opdaterToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem opdaterToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem logUdToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon Tray_icon;
        private System.Diagnostics.EventLog ServiceEventManager;
    }
}

