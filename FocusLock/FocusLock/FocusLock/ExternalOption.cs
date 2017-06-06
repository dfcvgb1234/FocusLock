using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System;
using System.IO;

namespace FocusLock
{
    class ExternalOption
    {
        string programPath = @"C:\Windows\System32\drivers\etc\External.begeba";

        bool fileFound;

        string currentOptionPath = @"C:\Windows\System32\drivers\etc\CurrentOption.begeba";

        Timer timer = new Timer();

        ComboBox cb;

        // metode som der viser de forskellige controls på skærmen
        public void Show(Form form)
        {
            if(File.Exists(programPath))
            {
                File.Delete(programPath);
            }

            // definerer nye controls
            Label lbl1 = new Label();
            ComboBox cb1 = new ComboBox();

            // control 1 : Label
            lbl1.Name = "_External_Hours_lbl";
            lbl1.Location = new Point(90, 74);
            lbl1.Size = new Size(200, 150);
            lbl1.Font = new Font("Arial", 18, FontStyle.Bold);
            lbl1.Text = "Venter på start";

            string[] collection1 = { "1.V", "1.T", "1.X", "1.Z" };
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

            if (!File.Exists(currentOptionPath))
            {
                File.Create(currentOptionPath).Close();
            }
            File.WriteAllText(currentOptionPath, "EXTERNAL");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Update();
        }

        // metode slut

        // metode til at slette de ting der ikke skal være på skærmen
        public void Hide(Form form)
        {
            Control[] rems =
            {
                form.Controls["_External_Hours_lbl"],
                form.Controls["_External_Combobox"]
            };
            foreach (Control rem in rems)
            {
                form.Controls.Remove(rem);
            }
            timer.Stop();
        }
        // metode slut

        public void Update()
        {
            string findingFile = "";

            switch(cb.SelectedIndex)
            {
                case 0:
                    findingFile = "V";
                    break;

                case 1:
                    findingFile = "T";
                    break;

                case 2:
                    findingFile = "X";
                    break;

                case 3:
                    findingFile = "Z";
                    break;
            }
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Credentials = new NetworkCredential("focuslock.dk", "bagebe");
                    webClient.DownloadFile("ftp://ftp.focuslock.dk/ftp/" + findingFile , programPath);
                }
                fileFound = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                fileFound = false;
            }

            if(fileFound)
            {
                MainForm.startProgram(true);
            }

            if(!fileFound)
            {
                MainForm.startProgram(false);
            }
        }
    }
}
