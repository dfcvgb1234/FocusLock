using System;
using System.IO;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Forms;

namespace FocusLock
{
    class CalendarOption
    {
        // sørger for at de ting i Update metoden kun bliver kørt en gang
        bool onlyOnce = false;

        RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock");

        bool isRunning = false;

        public bool updating = false;

        // laver en ny timer
        Timer timer = new Timer();

        // variabel til en textbox, hvor at tiden kommer til at stå
        TextBox textbox = new TextBox();

        // metode som der viser de forskellige controls på skærmen
        public void Show(Form form)
        {
            if (Program.permission >= 2)
            {
                // definerer nye controls
                TextBox tx1 = new TextBox();
                Label lbl1 = new Label();
                Button but1 = new Button();

                // control 1 : TextBox
                tx1.Location = new Point(63, 94);
                tx1.Size = new Size(150, 20);
                tx1.TextAlign = HorizontalAlignment.Center;
                tx1.Name = "_calendar_Hours";

                // control 2 : Label
                lbl1.Name = "_calendar_Hours_lbl";
                lbl1.Location = new Point(115, 74);
                lbl1.Size = new Size(46, 13);
                lbl1.Text = "Klokken";

                // control 3 : Button
                but1.Name = "_calendar_Start_but";
                but1.FlatStyle = FlatStyle.Flat;
                but1.FlatAppearance.BorderColor = Color.Black;
                but1.FlatAppearance.BorderSize = 2;
                but1.FlatAppearance.MouseDownBackColor = Color.Silver;
                but1.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
                but1.Location = new Point(358, 132);
                but1.Size = new Size(138, 48);
                but1.Click += But1_Click;
                but1.Text = "Tidsplan";

                // Tilføjer dem til programmet
                form.Controls.Add(tx1);
                form.Controls.Add(lbl1);
                form.Controls.Add(but1);
                textbox = tx1;

                if (key.GetValue("DateInfo") == null)
                {
                    key.SetValue("DateInfo", "");
                }

                updating = true;
                if (onlyOnce)
                {
                    timer.Start();
                }
            }
            else
            {
                Label elbl1 = new Label();
                Label elbl2 = new Label();

                elbl1.Text = "Du har ikke tiladelse til denne funktion";
                elbl1.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                elbl1.ForeColor = Color.Red;
                elbl1.Location = new Point(21, 50);
                elbl1.Size = new Size(316, 20);
                elbl1.Name = "error_lbl_1";

                elbl2.Text = "Opgrader din bruger for at få adgang";
                elbl2.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                elbl2.ForeColor = Color.Red;
                elbl2.Location = new Point(26, 79);
                elbl2.Size = new Size(304, 20);
                elbl2.Name = "error_lbl_2";

                form.Controls.Add(elbl1);
                form.Controls.Add(elbl2);
            }

        }
        // metode slut

        // metode der opfanger når knapen bliver trykket på
        private void But1_Click(object sender, System.EventArgs e)
        {
            // åbner Kalender formen
            Form calendar_form = new Calendar();
            calendar_form.Show();
        }
        //metode slut
        
        // metode til at fjerne de ting der ikke skal være på skærmen
        public void Hide(Form form)
        {
            timer.Stop();
            updating = false;
            if (Program.permission >= 2)
            {
                Control[] rems =
                {
                form.Controls["_calendar_Hours"],
                form.Controls["_calendar_Hours_lbl"],
                form.Controls["_calendar_Start_but"]
                };

                foreach (Control rem in rems)
                {
                    form.Controls.Remove(rem);
                }
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

        // opdaterer så ofte som man har indstillet den til at gøre
        public void Update(Form form)
        {
            if (updating)
            {
                // tænder kun timeren en gang
                if (onlyOnce == false)
                {
                    timer.Interval = 60000; // indstil til hvad der skal bruges
                    timer.Tick += Timer_Tick;
                    timer.Start();
                }
                onlyOnce = true;

                // handlinger der skal ske
                try
                {
                    textbox.Text = string.Format("{0}:{1}:{2}", Program.GetNistTime().Hour, Program.GetNistTime().Minute, Program.GetNistTime().Second);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                RunCalendarCheck();
            }
        }
        // metode slut

        public void OpenCalendar()
        {
            // åbner Kalender formen
            Form calendar_form = new Calendar();
            calendar_form.Show();
        }

        // Hvad der sker hver gang timeren har klaret en cycle
        private void Timer_Tick(object sender, EventArgs e)
        {
            // kører update igen, så det kører i en loop
            Update(MainForm.ActiveForm);
        }
        // metode slut

        // checker om det er tid til at starte programmet
        public void RunCalendarCheck()
        { 

            string day = "";
            string[] fileText = new string[key.GetValue("DateInfo").ToString().Split(';').Length];
            fileText = key.GetValue("DateInfo").ToString().Split(';');
            Console.WriteLine(DateTime.Now.Hour);
            foreach (string j in fileText)
            {
                if (!String.IsNullOrWhiteSpace(j))
                {
                    string[] tmp = j.Split(':');
                    string[] daytmp = tmp[1].Split(',');

                    // navngir varibalen "day" så den får den rigtige information
                    switch (daytmp[0])
                    {
                        case "0":
                            day = "Monday";
                            break;

                        case "1":
                            day = "Tuesday";
                            break;

                        case "2":
                            day = "Wedensday";
                            break;

                        case "3":
                            day = "Thursday";
                            break;

                        case "4":
                            day = "Friday";
                            break;

                        case "5":
                            day = "Saturday";
                            break;

                        case "6":
                            day = "Sunday";
                            break;
                    }
                    int clock = Int32.Parse(tmp[0]);
                    Console.WriteLine("{0}:{1}", clock, day);

                    // checker om det er tid
                    if (daytmp[1] == "1")
                    {
                        if (DateTime.Now.Hour == clock && DateTime.Now.DayOfWeek.ToString().ToLower() == day.ToLower())
                        {
                            // starter programmet hvis det er
                            MainForm.StartProgram(true);
                            if (!MainForm.KeyExists("CurrentOption"))
                            {
                                key.SetValue("CurrentOption", "");
                            }
                            key.SetValue("CurrentOption", "CALENDAR");
                            isRunning = true;
                            break;
                        }
                        else if (isRunning == true)
                        {
                            // stopper programmet hvis ikke
                            MainForm.StartProgram(false);
                            isRunning = false;
                        }
                    }
                    if(daytmp[1] == "2")
                    {
                        if (DateTime.Now.Hour == clock && DateTime.Now.DayOfWeek.ToString().ToLower() == day.ToLower() && DateTime.Now.Minute >= 30)
                        {
                            // starter programmet hvis det er
                            MainForm.StartProgram(true);
                            isRunning = true;
                            break;
                        }
                        else if (isRunning == true)
                        {
                            // stopper programmet hvis ikke
                            MainForm.StartProgram(false);
                            isRunning = false;
                        }
                    }
                }
            }
        }
        // metode slut

    }
}
