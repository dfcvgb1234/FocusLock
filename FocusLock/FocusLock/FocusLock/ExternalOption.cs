using System.Drawing;
using System.Windows.Forms;
using System.Net;
using Microsoft.Win32;
using System;
using System.IO;

namespace FocusLock
{
    class ExternalOption
    {
        string programPath = @"C:\Windows\System32\drivers\etc\External.begeba";

        RegistryKey key;

        bool fileFound;

        bool isRunning;

        Timer timer = new Timer();

        ComboBox cb;

        // metode som der viser de forskellige controls på skærmen
        public void Show(Form form)
        {
            if(File.Exists(programPath))
            {
                File.Delete(programPath);
            }

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

                string[] collection1 = { "Bruger1", "Bruger2", "Bruger3", "Bruger4", "Bruger5" };
                cb1.Name = "_External_Combobox";
                cb1.Location = new Point(102, 39);
                cb1.Items.AddRange(collection1);

                // Tilføjer dem til programmet
                form.Controls.Add(lbl1);
                form.Controls.Add(cb1);

                cb = cb1;

                timer.Interval = 30000;
                timer.Tick += Timer_Tick;
                timer.Start();

                key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock");
                cb.SelectedIndex = 0;
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
            Console.WriteLine("LOOKING FOR FILE");

            string findingFile = "";

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Credentials = new NetworkCredential("focuslock.dk", "bagebe");
                    webClient.DownloadFile("ftp://ftp.focuslock.dk/ftp/FocusLock" + "/" + cb.SelectedItem.ToString() + "/" + cb.SelectedItem.ToString() + ".txt", programPath);
                }
                fileFound = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                fileFound = false;
            }

            if(fileFound && isRunning == false)
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
            }
        }
    }
}
