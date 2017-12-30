using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FocusLock
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock");

            if(!MainForm.KeyExists("Creds"))
            {
                Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("Creds", "");
            }
            if(!MainForm.KeyExists("SaveCreds"))
            {
                Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("SaveCreds", "");
            }

            if(MainForm.KeyExists("SaveCreds"))
            {
                if(Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").GetValue("SaveCreds").ToString() == "TRUE")
                {
                    Save_cred.Checked = true;
                }
                else if(Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").GetValue("SaveCreds").ToString() == "FALSE")
                {
                    Save_cred.Checked = false;
                }
            }
            if(MainForm.KeyExists("Creds"))
            {
                if (!String.IsNullOrEmpty(Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").GetValue("Creds").ToString()))
                {
                    string[] creds = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").GetValue("Creds").ToString().Split(';');
                    User_txt.Text = creds[0];
                    Pass_txt.Text = creds[1];

                    Login_but.PerformClick();
                }
            }
        }

        private void Login_but_Click(object sender, EventArgs e)
        {
            string name = User_txt.Text;

            string pass = ComputeHash(Pass_txt.Text, "e0071fa9");

            Console.WriteLine(pass);

            if (name != "Admin" && pass != "Bagebe")
            {
                string response = SendRequest("https://www.focuslock.dk/php/permRequest.php?email=" + name + "&" + "pass=" + pass);

                Console.WriteLine(response);

                if (response != null)
                {
                    if (response.Contains("granted"))
                    {
                        string[] respsplit = response.Split(';');

                        Console.WriteLine("Your classroom is: {0}", respsplit[2]);
                        Console.WriteLine("You have the permission level of: {0}", respsplit[1]);

                        Program.OpenMainForm(int.Parse(respsplit[1]),respsplit[2], name);

                        if (Save_cred.Checked)
                        {
                            Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("Creds", name + ";" + Pass_txt.Text);
                            Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("SaveCreds", "TRUE");
                        }
                        else
                        {
                            Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("Creds", "");
                            Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("SaveCreds", "FALSE");
                        }

                        Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("LoginOffline", "FALSE");

                        this.FindForm().Close();
                    }
                    else
                    {
                        if(!MainForm.KeyExists("LoginOffline"))
                        {
                            Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("LoginOffline", "FALSE");
                        }

                        if (Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").GetValue("LoginOffline").ToString() == "FALSE")
                        {
                            Console.WriteLine("Username or Password is incorrect");
                            DialogResult result = MessageBox.Show("De indtastede oplysninger er forkerte, ellers har du ikke forbindelse til internetet. \n\nVil du starte i offline-tilstand?", "STOP!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                            if (result == DialogResult.Yes)
                            {
                                Program.OpenMainForm(1, "", name);
                                Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("LoginOffline", "TRUE");

                                if (Save_cred.Checked)
                                {
                                    Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("Creds", name + ";" + Pass_txt.Text);
                                    Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("SaveCreds", "TRUE");
                                }
                                else
                                {
                                    Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("Creds", "");
                                    Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("SaveCreds", "FALSE");
                                }

                                this.FindForm().Close();
                            }
                            else
                            {
                                Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("LoginOffline", "FALSE");
                            }
                        }
                        else
                        {
                            if (MainForm.KeyExists("Creds"))
                            {
                                if (!String.IsNullOrEmpty(Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").GetValue("Creds").ToString()))
                                {
                                    string[] creds = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").GetValue("Creds").ToString().Split(';');
                                    User_txt.Text = creds[0];
                                    Pass_txt.Text = creds[1];

                                    Program.OpenMainForm(1, "", name);
                                    this.FindForm().Close();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Program.OpenMainForm(3, "", name);

                if (Save_cred.Checked)
                {
                    Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("Creds", name + ";" + pass);
                    Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("SaveCreds", "TRUE");
                }
                else
                {
                    Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("Creds", "");
                    Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("SaveCreds", "FALSE");
                }

                this.FindForm().Close();
            }
        }

        public static string ComputeHash(string plainText, string salt)
        {
            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            SHA256Managed hash = new SHA256Managed();

            // Compute hash value of salt.
            byte[] plainHash = hash.ComputeHash(plainTextBytes);

            byte[] concat = new byte[plainHash.Length + saltBytes.Length];

            System.Buffer.BlockCopy(saltBytes, 0, concat, 0, saltBytes.Length);
            System.Buffer.BlockCopy(plainHash, 0, concat, saltBytes.Length, plainHash.Length);

            byte[] tHashBytes = hash.ComputeHash(concat);

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(tHashBytes);

            // Return the result.
            return hashValue;
        }

        private string SendRequest(string url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    return client.DownloadString(new Uri(url));
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Error while receiving data from the server:\n" + ex.Message + "Something broke.. :(");
                return null;
            }
        }

        private void User_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                Login_but.PerformClick();
            }
        }

        private void Pass_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Login_but.PerformClick();
            }
        }

        private void User_lbl_Click(object sender, EventArgs e)
        {

        }
    }
}
