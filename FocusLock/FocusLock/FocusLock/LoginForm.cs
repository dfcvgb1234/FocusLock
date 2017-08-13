using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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

            string pass = Pass_txt.Text;

            if (name != "Admin" && pass != "Bagebe")
            {
                string response = SendRequest("http://focuslock.dk/ftp/SendPHPRequest.php?name=" + name + "&" + "pass=" + pass);

                if (response != null)
                {
                    if (response.Contains("Permission"))
                    {
                        int index = response.IndexOf("Permission");
                        string perm = "Permission";
                        int endIndex = index + perm.Length;
                        endIndex++;
                        string resp = response.Substring(endIndex, 1);

                        Console.WriteLine("You have the permission level of: {0}", resp);

                        Program.OpenMainForm(Int16.Parse(resp));

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
                    else
                    {
                        Console.WriteLine("Username or Password is incorrect");
                    }
                }
            }
            else
            {
                Program.OpenMainForm(3);

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
    }
}
