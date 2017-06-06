namespace FocusLock
{
    partial class Websites
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
            this.list = new System.Windows.Forms.ListBox();
            this.Save_but = new System.Windows.Forms.Button();
            this.Item = new System.Windows.Forms.TextBox();
            this.Add_but = new System.Windows.Forms.Button();
            this.Delete_but = new System.Windows.Forms.Button();
            this.Searcj_lbl = new System.Windows.Forms.Label();
            this.Search_tx = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // list
            // 
            this.list.FormattingEnabled = true;
            this.list.Location = new System.Drawing.Point(12, 68);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(262, 303);
            this.list.TabIndex = 0;
            // 
            // Save_but
            // 
            this.Save_but.Location = new System.Drawing.Point(13, 377);
            this.Save_but.Name = "Save_but";
            this.Save_but.Size = new System.Drawing.Size(261, 24);
            this.Save_but.TabIndex = 1;
            this.Save_but.Text = "Gem ændringer";
            this.Save_but.UseVisualStyleBackColor = true;
            this.Save_but.Click += new System.EventHandler(this.Save_but_Click);
            // 
            // Item
            // 
            this.Item.Location = new System.Drawing.Point(13, 41);
            this.Item.Name = "Item";
            this.Item.Size = new System.Drawing.Size(163, 20);
            this.Item.TabIndex = 2;
            // 
            // Add_but
            // 
            this.Add_but.Location = new System.Drawing.Point(182, 39);
            this.Add_but.Name = "Add_but";
            this.Add_but.Size = new System.Drawing.Size(43, 23);
            this.Add_but.TabIndex = 3;
            this.Add_but.Text = "+";
            this.Add_but.UseVisualStyleBackColor = true;
            this.Add_but.Click += new System.EventHandler(this.Add_but_Click);
            // 
            // Delete_but
            // 
            this.Delete_but.Location = new System.Drawing.Point(234, 39);
            this.Delete_but.Name = "Delete_but";
            this.Delete_but.Size = new System.Drawing.Size(43, 23);
            this.Delete_but.TabIndex = 4;
            this.Delete_but.Text = "-";
            this.Delete_but.UseVisualStyleBackColor = true;
            this.Delete_but.Click += new System.EventHandler(this.Delete_but_Click);
            // 
            // Searcj_lbl
            // 
            this.Searcj_lbl.AutoSize = true;
            this.Searcj_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Searcj_lbl.Location = new System.Drawing.Point(246, 13);
            this.Searcj_lbl.Name = "Searcj_lbl";
            this.Searcj_lbl.Size = new System.Drawing.Size(33, 17);
            this.Searcj_lbl.TabIndex = 6;
            this.Searcj_lbl.Text = "Søg";
            // 
            // Search_tx
            // 
            this.Search_tx.Location = new System.Drawing.Point(12, 13);
            this.Search_tx.Name = "Search_tx";
            this.Search_tx.Size = new System.Drawing.Size(230, 20);
            this.Search_tx.TabIndex = 5;
            this.Search_tx.TextChanged += new System.EventHandler(this.Search_tx_TextChanged);
            // 
            // Websites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 410);
            this.Controls.Add(this.Searcj_lbl);
            this.Controls.Add(this.Search_tx);
            this.Controls.Add(this.Delete_but);
            this.Controls.Add(this.Add_but);
            this.Controls.Add(this.Item);
            this.Controls.Add(this.Save_but);
            this.Controls.Add(this.list);
            this.Name = "Websites";
            this.Text = "Websites";
            this.Load += new System.EventHandler(this.Websites_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox list;
        private System.Windows.Forms.Button Save_but;
        private System.Windows.Forms.TextBox Item;
        private System.Windows.Forms.Button Add_but;
        private System.Windows.Forms.Button Delete_but;
        private System.Windows.Forms.Label Searcj_lbl;
        private System.Windows.Forms.TextBox Search_tx;
    }
}