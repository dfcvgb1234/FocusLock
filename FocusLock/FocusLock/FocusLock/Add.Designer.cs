namespace FocusLock
{
    partial class Add
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add));
            this.ProgramName_tx = new System.Windows.Forms.TextBox();
            this.ProcessName_tx = new System.Windows.Forms.TextBox();
            this.Add_but = new System.Windows.Forms.Button();
            this.ProcessName_lbl = new System.Windows.Forms.Label();
            this.ProgramName_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ProgramName_tx
            // 
            this.ProgramName_tx.Location = new System.Drawing.Point(24, 33);
            this.ProgramName_tx.Name = "ProgramName_tx";
            this.ProgramName_tx.Size = new System.Drawing.Size(100, 20);
            this.ProgramName_tx.TabIndex = 0;
            // 
            // ProcessName_tx
            // 
            this.ProcessName_tx.Location = new System.Drawing.Point(24, 93);
            this.ProcessName_tx.Name = "ProcessName_tx";
            this.ProcessName_tx.Size = new System.Drawing.Size(100, 20);
            this.ProcessName_tx.TabIndex = 1;
            // 
            // Add_but
            // 
            this.Add_but.Location = new System.Drawing.Point(37, 119);
            this.Add_but.Name = "Add_but";
            this.Add_but.Size = new System.Drawing.Size(75, 23);
            this.Add_but.TabIndex = 2;
            this.Add_but.Text = "Tilføj";
            this.Add_but.UseVisualStyleBackColor = true;
            this.Add_but.Click += new System.EventHandler(this.Add_but_Click);
            // 
            // ProcessName_lbl
            // 
            this.ProcessName_lbl.AutoSize = true;
            this.ProcessName_lbl.Location = new System.Drawing.Point(35, 75);
            this.ProcessName_lbl.Name = "ProcessName_lbl";
            this.ProcessName_lbl.Size = new System.Drawing.Size(72, 13);
            this.ProcessName_lbl.TabIndex = 3;
            this.ProcessName_lbl.Text = "Process navn";
            // 
            // ProgramName_lbl
            // 
            this.ProgramName_lbl.AutoSize = true;
            this.ProgramName_lbl.Location = new System.Drawing.Point(35, 15);
            this.ProgramName_lbl.Name = "ProgramName_lbl";
            this.ProgramName_lbl.Size = new System.Drawing.Size(73, 13);
            this.ProgramName_lbl.TabIndex = 4;
            this.ProgramName_lbl.Text = "Program navn";
            // 
            // Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(151, 151);
            this.Controls.Add(this.ProgramName_lbl);
            this.Controls.Add(this.ProcessName_lbl);
            this.Controls.Add(this.Add_but);
            this.Controls.Add(this.ProcessName_tx);
            this.Controls.Add(this.ProgramName_tx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Add";
            this.Text = "Tilføj program";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ProgramName_tx;
        private System.Windows.Forms.TextBox ProcessName_tx;
        private System.Windows.Forms.Button Add_but;
        private System.Windows.Forms.Label ProcessName_lbl;
        private System.Windows.Forms.Label ProgramName_lbl;
    }
}