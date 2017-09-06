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

        Button but;

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
                but = but1;

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
                elbl1.Font = new Font("Century Gothic", 12, FontStyle.Bold);
                elbl1.ForeColor = Color.Red;
                elbl1.Location = new Point(21, 50);
                elbl1.Size = new Size(316, 20);
                elbl1.Name = "error_lbl_1";

                elbl2.Text = "Klik her for at opgradere din bruger";
                elbl2.Font = new Font("Century Gothic", 11, FontStyle.Bold |FontStyle.Underline);
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

            int day = 0;
            DateTime time = Program.GetNistTime();
            string[] whole = key.GetValue("WHOLE").ToString().Split(';');
            string[] half1 = key.GetValue("HALF1").ToString().Split(';');
            string[] half2 = key.GetValue("HALF2").ToString().Split(';');

            switch (time.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    day = 0;
                    break;

                case DayOfWeek.Tuesday:
                    day = 1;
                    break;

                case DayOfWeek.Wednesday:
                    day = 2;
                    break;

                case DayOfWeek.Thursday:
                    day = 3;
                    break;

                case DayOfWeek.Friday:
                    day = 4;
                    break;

                case DayOfWeek.Saturday:
                    day = 5;
                    break;

                case DayOfWeek.Sunday:
                    day = 6;
                    break;
            }

            foreach (string j in whole)
            {
                bool failure = true;
                if (!String.IsNullOrWhiteSpace(j))
                {
                    string[] split = j.Split(':');
                    if (day == int.Parse(split[1]))
                    {
                        if (int.Parse(split[0]) == time.Hour)
                        {
                            if (isRunning == false)
                            {
                                FormCollection col = Application.OpenForms;
                                foreach (Form form in col)
                                {
                                    form.Activate();
                                }
                                but.Visible = false;
                                MainForm.StartProgram(true);
                                isRunning = true;
                                failure = false;
                            }
                            break;
                        }
                        else
                        {
                            failure = true;
                        }
                    }
                    else
                    {
                        failure = true;
                    }

                    if (failure)
                    {
                        if (isRunning == true)
                        {
                            FormCollection col = Application.OpenForms;
                            foreach (Form form in col)
                            {
                                form.Activate();
                            }
                            but.Visible = true;
                            MainForm.StartProgram(false);
                        }
                    }
                }
            }

            foreach (string j in half1)
            {
                bool faliure = true;
                if (!String.IsNullOrWhiteSpace(j))
                {
                    string[] split = j.Split(':');
                    if (day == int.Parse(split[1]))
                    {
                        if (int.Parse(split[0]) == time.Hour)
                        {
                            if (time.Minute < 30)
                            {
                                if (isRunning == false)
                                {
                                    FormCollection col = Application.OpenForms;
                                    foreach (Form form in col)
                                    {
                                        form.Activate();
                                    }
                                    but.Visible = false;
                                    MainForm.StartProgram(true);
                                    isRunning = true;
                                    faliure = false;
                                }
                                break;
                            }
                            else
                            {
                                faliure = true;
                            }
                        }
                        else
                        {
                            faliure = true;
                        }
                    }
                    else
                    {
                        faliure = true;
                    }
                    
                    if(faliure)
                    {
                        if (isRunning == true)
                        {
                            FormCollection col = Application.OpenForms;
                            foreach (Form form in col)
                            {
                                form.Activate();
                            }
                            but.Visible = true;
                            MainForm.StartProgram(false);
                        }
                    }
                }
            }

            foreach (string j in half2)
            {
                bool faliure = true;
                if (!String.IsNullOrWhiteSpace(j))
                {
                    string[] split = j.Split(':');
                    if (day == int.Parse(split[1]))
                    {
                        if (int.Parse(split[0]) == time.Hour)
                        {
                            if (time.Minute > 30)
                            {
                                if (isRunning == false)
                                {
                                    FormCollection col = Application.OpenForms;
                                    foreach (Form form in col)
                                    {
                                        form.Activate();
                                    }
                                    but.Visible = false;
                                    MainForm.StartProgram(true);
                                    isRunning = true;
                                    faliure = false;
                                }
                                break;
                            }
                            else
                            {
                                faliure = true;
                            }
                        }
                        else
                        {
                            faliure = true;
                        }
                    }
                    else
                    {
                        faliure = true;
                    }

                    if (faliure)
                    {
                        if (isRunning == true)
                        {
                            FormCollection col = Application.OpenForms;
                            foreach (Form form in col)
                            {
                                form.Activate();
                            }
                            but.Visible = true;
                            MainForm.StartProgram(false);
                        }
                    }
                }
            }
        }
        // metode slut

    }
}
