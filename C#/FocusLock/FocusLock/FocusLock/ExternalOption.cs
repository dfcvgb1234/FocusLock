using System.Drawing;
using System.Windows.Forms;
using System.Net;
using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;

namespace FocusLock
{
    class ExternalOption
    {
        RegistryKey key;

        bool fileFound;

        bool isRunning;

        Timer timer = new Timer();

        int tempPerm = Program.permission - 3;

        ComboBox cb;
        Label lbl;

        WebClient client = new WebClient();

        // metode som der viser de forskellige controls på skærmen
        public void Show(Form form)
        {
            if (Program.permission >= 3)
            {
                // definerer nye controls
                Label lbl1 = new Label();
                ComboBox cb1 = new ComboBox();

                // control 1 : Label
                lbl1.Name = "_External_Hours_lbl";
                lbl1.Location = new Point(90, 74);
                lbl1.Size = new Size(200, 150);
                lbl1.Font = new Font("Arial", 18, FontStyle.Bold);
                lbl1.Text = "Venter på start";

                cb1.Name = "_External_Combobox";
                cb1.Location = new Point(102, 39);
                foreach(string j in GetServerData())
                {
                    Console.WriteLine(j);
                    cb1.Items.Add(j);
                }
                cb1.SelectedIndex = 0;
                //cb.Text = Program.room;

                // Tilføjer dem til programmet
                form.Controls.Add(lbl1);
                form.Controls.Add(cb1);

                cb = cb1;
                lbl = lbl1;

                timer.Interval = 3000;
                timer.Tick += Timer_Tick;
                timer.Start();

                key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock");
                Update();
            }
            else
            {
                Label elbl1 = new Label();
                Label elbl2 = new Label();

                elbl1.Text = "Du har ikke tiladelse til denne funktion";
                elbl1.Font = new Font("Century Gothic", 12, FontStyle.Bold);
                elbl1.ForeColor = Color.Red;
                elbl1.Location = new Point(21, 50);
                elbl1.Size = new Size(316, 20);
                elbl1.Name = "error_lbl_1";

                elbl2.Text = "Klik her for at opgradere din bruger";
                elbl2.Font = new Font("Century Gothic", 11, FontStyle.Bold | FontStyle.Underline);
                elbl2.ForeColor = Color.Red;
                elbl2.Location = new Point(41, 79);
                elbl2.Size = new Size(315, 20);
                elbl2.Click += Elbl2_Click;
                elbl2.Name = "error_lbl_2";

                form.Controls.Add(elbl1);
                form.Controls.Add(elbl2);
            }

        }

        private void Elbl2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Update();
        }

        // metode slut

        // metode til at slette de ting der ikke skal være på skærmen
        public void Hide(Form form)
        {
            if (Program.permission >= 3)
            {
                Control[] rems =
                {
                form.Controls["_External_Hours_lbl"],
                form.Controls["_External_Combobox"],
                form.Controls["but1"]
                };

                foreach (Control rem in rems)
                {
                    form.Controls.Remove(rem);
                }
                timer.Stop();
            }
            else
            {
                Control[] rems =
                {
                form.Controls["error_lbl_1"],
                form.Controls["error_lbl_2"]
                };

                foreach (Control rem in rems)
                {
                    form.Controls.Remove(rem);
                }
            }
        }
        // metode slut

        public void Update()
        {
            fileFound = false;
            Console.WriteLine("LOOKING FOR FILE");

            try
            {
                Console.WriteLine("ftp://ftp.focuslock.dk/ftp/FocusLock/" + tempPerm + "/" + Program.ID + "/" + cb.Text + "/");

                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.focuslock.dk/ftp/FocusLock/" + tempPerm + "/" + Program.ID + "/" + cb.Text + "/");
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential("focuslock.dk", "bagebe");

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                

                if (reader.ReadToEnd().Contains(".txt"))
                {
                    fileFound = true;
                }
                else
                {
                    fileFound = false;
                }

                Console.WriteLine("Directory List Complete, status {0}", response.StatusDescription);

                reader.Close();
                response.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine(fileFound);
            if(fileFound && !isRunning)
            {
                FormCollection col = Application.OpenForms;
                foreach (Form form in col)
                {
                    form.Activate();
                }
                MainForm.StartProgram(true);

                if (!MainForm.KeyExists("CurrentOption"))
                {
                    key.SetValue("CurrentOption", "");
                }
                key.SetValue("CurrentOption", "EXTERNAL");
                isRunning = true;
                lbl.Text = "Programmet kører!";
            }

            if(!fileFound && isRunning == true)
            {   
                FormCollection col = Application.OpenForms;
                foreach (Form form in col)
                {
                    form.Activate();
                }
                MainForm.StartProgram(false);
                isRunning = false;
                lbl.Text = "Venter på start!";
            }
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            client.CancelAsync();
        }

        public List<string> GetServerData()
        {
            List<string> items = new List<string>();

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://ftp.focuslock.dk/ftp/FocusLock/" + tempPerm + "/" + Program.ID + "/");
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            Console.WriteLine("ftp://ftp.focuslock.dk/ftp/FocusLock/" + Program.permission + "/" + Program.ID + "/");

            //request.Proxy = System.Net.WebProxy.GetDefaultProxy();
            //request.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Credentials = new NetworkCredential("focuslock.dk", "bagebe");
            Stream rs = (Stream)request.GetResponse().GetResponseStream();

            StreamReader sr = new StreamReader(rs);
            string strList = sr.ReadToEnd();
            string[] lines = { null };

            if (strList.Contains("\r\n"))
            {
                lines = strList.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            }
            else if (strList.Contains("\n"))
            {
                lines = strList.Split(new string[] { "\n" }, StringSplitOptions.None);
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (i > 1)
                {
                    if (!String.IsNullOrWhiteSpace(lines[i]))
                    {
                        items.Add(lines[i]);
                    }
                }
            }

            rs.Close();
            sr.Close();
            return items;
        }
    }
}
