namespace FocusLock
{
    partial class LoginForm
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
            this.Save_cred = new System.Windows.Forms.CheckBox();
            this.User_txt = new System.Windows.Forms.TextBox();
            this.Pass_txt = new System.Windows.Forms.TextBox();
            this.Login_but = new System.Windows.Forms.Button();
            this.User_lbl = new System.Windows.Forms.Label();
            this.Pass_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Save_cred
            // 
            this.Save_cred.AutoSize = true;
            this.Save_cred.Location = new System.Drawing.Point(12, 97);
            this.Save_cred.Name = "Save_cred";
            this.Save_cred.Size = new System.Drawing.Size(135, 17);
            this.Save_cred.TabIndex = 0;
            this.Save_cred.Text = " Husk login oplysninger";
            this.Save_cred.UseVisualStyleBackColor = true;
            // 
            // User_txt
            // 
            this.User_txt.Location = new System.Drawing.Point(11, 23);
            this.User_txt.Name = "User_txt";
            this.User_txt.Size = new System.Drawing.Size(134, 20);
            this.User_txt.TabIndex = 1;
            // 
            // Pass_txt
            // 
            this.Pass_txt.Location = new System.Drawing.Point(10, 65);
            this.Pass_txt.Name = "Pass_txt";
            this.Pass_txt.PasswordChar = '=';
            this.Pass_txt.Size = new System.Drawing.Size(134, 20);
            this.Pass_txt.TabIndex = 2;
            // 
            // Login_but
            // 
            this.Login_but.Location = new System.Drawing.Point(12, 123);
            this.Login_but.Name = "Login_but";
            this.Login_but.Size = new System.Drawing.Size(128, 20);
            this.Login_but.TabIndex = 3;
            this.Login_but.Text = "Login";
            this.Login_but.UseVisualStyleBackColor = true;
            this.Login_but.Click += new System.EventHandler(this.Login_but_Click);
            // 
            // User_lbl
            // 
            this.User_lbl.AutoSize = true;
            this.User_lbl.Location = new System.Drawing.Point(44, 6);
            this.User_lbl.Name = "User_lbl";
            this.User_lbl.Size = new System.Drawing.Size(62, 13);
            this.User_lbl.TabIndex = 4;
            this.User_lbl.Text = "Brugernavn";
            // 
            // Pass_lbl
            // 
            this.Pass_lbl.AutoSize = true;
            this.Pass_lbl.Location = new System.Drawing.Point(40, 48);
            this.Pass_lbl.Name = "Pass_lbl";
            this.Pass_lbl.Size = new System.Drawing.Size(73, 13);
            this.Pass_lbl.TabIndex = 5;
            this.Pass_lbl.Text = "Adgangskode";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(154, 151);
            this.Controls.Add(this.Pass_lbl);
            this.Controls.Add(this.User_lbl);
            this.Controls.Add(this.Login_but);
            this.Controls.Add(this.Pass_txt);
            this.Controls.Add(this.User_txt);
            this.Controls.Add(this.Save_cred);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox Save_cred;
        private System.Windows.Forms.TextBox User_txt;
        private System.Windows.Forms.TextBox Pass_txt;
        private System.Windows.Forms.Button Login_but;
        private System.Windows.Forms.Label User_lbl;
        private System.Windows.Forms.Label Pass_lbl;
    }
}