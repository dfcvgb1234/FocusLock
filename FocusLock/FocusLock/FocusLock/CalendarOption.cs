using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace FocusLock
{
    class CalendarOption
    {
        // sørger for at de ting i Update metoden kun bliver kørt en gang
        bool onlyOnce = false;

        string currentOptionPath = @"C:\Windows\System32\drivers\etc\CurrentOption.begeba";

        // laver en ny timer
        Timer timer = new Timer();

        // variabel til en textbox, hvor at tiden kommer til at stå
        TextBox textbox;

        // metode som der viser de forskellige controls på skærmen
        public void Show(Form form)
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

            if (!File.Exists(currentOptionPath))
            {
                File.Create(currentOptionPath).Close();
            }
            File.WriteAllText(currentOptionPath, "CALENDAR");
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

            timer.Stop();

        }
        // metode slut

        // opdaterer så ofte som man har indstillet den til at gøre
        public void Update(Form form)
        {
            // tænder kun timeren en gang
            if (onlyOnce == false)
            {
                timer.Interval = 30000; // indstil til hvad der skal bruges
                timer.Tick += Timer_Tick;
                timer.Start();
            }
            onlyOnce = true;

            // handlinger der skal ske
            try
            {
                textbox.Text = string.Format("{0}:{1}:{2}", Program.GetNistTime().Hour, Program.GetNistTime().Minute, Program.GetNistTime().Second);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            RunCalendarCheck();
        }
        // metode slut

        // Hvad der sker hver gang timeren har klaret en cycle
        private void Timer_Tick(object sender, EventArgs e)
        {
            // kører update igen, så det kører i en loop
            Update(MainForm.ActiveForm);
        }
        // metode slut

        // checker om det er tid til at starte programmet
        private void RunCalendarCheck()
        {
            string day = "";
            string[] fileText = System.IO.File.ReadAllText(@"C:\Windows\System32\drivers\etc\DateInfo.begeba").Split(';');
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
                            MainForm.startProgram(true);
                            break;
                        }
                        else
                        {
                            // stopper programmet hvis ikke
                            MainForm.startProgram(false);
                        }
                    }
                    if(daytmp[1] == "2")
                    {
                        if (DateTime.Now.Hour == clock && DateTime.Now.DayOfWeek.ToString().ToLower() == day.ToLower() && DateTime.Now.Minute >= 30)
                        {
                            // starter programmet hvis det er
                            MainForm.startProgram(true);
                            break;
                        }
                        else
                        {
                            // stopper programmet hvis ikke
                            MainForm.startProgram(false);
                        }
                    }
                }
            }
        }
        // metode slut

    }
}
