namespace FocusLock
{
    partial class Programs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Programs));
            this.ItemBox = new System.Windows.Forms.CheckedListBox();
            this.Add_but = new System.Windows.Forms.Button();
            this.Save_but = new System.Windows.Forms.Button();
            this.Search_tx = new System.Windows.Forms.TextBox();
            this.Searcj_lbl = new System.Windows.Forms.Label();
            this.Reset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ItemBox
            // 
            this.ItemBox.FormattingEnabled = true;
            this.ItemBox.Location = new System.Drawing.Point(14, 34);
            this.ItemBox.Name = "ItemBox";
            this.ItemBox.Size = new System.Drawing.Size(291, 394);
            this.ItemBox.TabIndex = 0;
            // 
            // Add_but
            // 
            this.Add_but.Location = new System.Drawing.Point(14, 431);
            this.Add_but.Name = "Add_but";
            this.Add_but.Size = new System.Drawing.Size(88, 36);
            this.Add_but.TabIndex = 1;
            this.Add_but.Text = "Tilføj";
            this.Add_but.UseVisualStyleBackColor = true;
            this.Add_but.Click += new System.EventHandler(this.Add_but_Click);
            // 
            // Save_but
            // 
            this.Save_but.Location = new System.Drawing.Point(217, 431);
            this.Save_but.Name = "Save_but";
            this.Save_but.Size = new System.Drawing.Size(88, 36);
            this.Save_but.TabIndex = 2;
            this.Save_but.Text = "Gem ændringer";
            this.Save_but.UseVisualStyleBackColor = true;
            this.Save_but.Click += new System.EventHandler(this.Save_but_Click);
            // 
            // Search_tx
            // 
            this.Search_tx.Location = new System.Drawing.Point(14, 8);
            this.Search_tx.Name = "Search_tx";
            this.Search_tx.Size = new System.Drawing.Size(254, 20);
            this.Search_tx.TabIndex = 3;
            this.Search_tx.TextChanged += new System.EventHandler(this.Search_tx_TextChanged);
            // 
            // Searcj_lbl
            // 
            this.Searcj_lbl.AutoSize = true;
            this.Searcj_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Searcj_lbl.Location = new System.Drawing.Point(274, 8);
            this.Searcj_lbl.Name = "Searcj_lbl";
            this.Searcj_lbl.Size = new System.Drawing.Size(33, 17);
            this.Searcj_lbl.TabIndex = 4;
            this.Searcj_lbl.Text = "Søg";
            this.Searcj_lbl.Click += new System.EventHandler(this.Searcj_lbl_Click);
            // 
            // Reset
            // 
            this.Reset.Location = new System.Drawing.Point(116, 431);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(88, 36);
            this.Reset.TabIndex = 5;
            this.Reset.Text = "Gendan til standard";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // Programs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 470);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.Searcj_lbl);
            this.Controls.Add(this.Search_tx);
            this.Controls.Add(this.Save_but);
            this.Controls.Add(this.Add_but);
            this.Controls.Add(this.ItemBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Programs";
            this.Text = "Ikke tiladte programmer";
            this.Load += new System.EventHandler(this.Programs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox ItemBox;
        private System.Windows.Forms.Button Add_but;
        private System.Windows.Forms.Button Save_but;
        private System.Windows.Forms.TextBox Search_tx;
        private System.Windows.Forms.Label Searcj_lbl;
        private System.Windows.Forms.Button Reset;
    }
}