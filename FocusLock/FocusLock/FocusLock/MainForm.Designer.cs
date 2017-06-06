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
            this.Selector = new System.Windows.Forms.ComboBox();
            this.InfoSelectorText = new System.Windows.Forms.Label();
            this.Websites_but = new System.Windows.Forms.Button();
            this.Programs_but = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Selector
            // 
            this.Selector.FormattingEnabled = true;
            this.Selector.Items.AddRange(new object[] {
            "Kalender",
            "Tid",
            "Ekstern kontrol"});
            this.Selector.Location = new System.Drawing.Point(12, 12);
            this.Selector.Name = "Selector";
            this.Selector.Size = new System.Drawing.Size(169, 21);
            this.Selector.TabIndex = 1;
            this.Selector.SelectedIndexChanged += new System.EventHandler(this.Selector_SelectedIndexChanged);
            // 
            // InfoSelectorText
            // 
            this.InfoSelectorText.AutoSize = true;
            this.InfoSelectorText.Location = new System.Drawing.Point(193, 14);
            this.InfoSelectorText.Name = "InfoSelectorText";
            this.InfoSelectorText.Size = new System.Drawing.Size(77, 13);
            this.InfoSelectorText.TabIndex = 2;
            this.InfoSelectorText.Text = "Vælg indstilling";
            // 
            // Websites_but
            // 
            this.Websites_but.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Websites_but.FlatAppearance.BorderSize = 2;
            this.Websites_but.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.Websites_but.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.Websites_but.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            this.Programs_but.Location = new System.Drawing.Point(358, 66);
            this.Programs_but.Name = "Programs_but";
            this.Programs_but.Size = new System.Drawing.Size(138, 48);
            this.Programs_but.TabIndex = 4;
            this.Programs_but.Text = "Ikke tiladte programmer";
            this.Programs_but.UseVisualStyleBackColor = true;
            this.Programs_but.Click += new System.EventHandler(this.Programs_but_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 192);
            this.Controls.Add(this.Programs_but);
            this.Controls.Add(this.Websites_but);
            this.Controls.Add(this.InfoSelectorText);
            this.Controls.Add(this.Selector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainForm";
            this.Text = "FocusLock";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox Selector;
        private System.Windows.Forms.Label InfoSelectorText;
        private System.Windows.Forms.Button Websites_but;
        private System.Windows.Forms.Button Programs_but;
    }
}

