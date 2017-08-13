using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace FocusLock
{
    class TimeOption
    {
        // Definerer den timer som der bruges
        Timer timer = new Timer();

        RegistryKey key = null;

        // Definerer minutter og timer som variabler
        int minutes;
        int hours;

        // Definerer de tre textboxes så de kan bruges uden for Show()
        TextBox text1;
        TextBox text2;
        TextBox text3;

        // sørger for at man kun kan trykke på knappen en gang
        bool onceBut = false;

        // metode som der viser de forskellige controls på skærmen
        public void Show(Form form)
        {
            // definerer nye controls
            TextBox tx1 = new TextBox();
            TextBox tx2 = new TextBox();
            TextBox tx3 = new TextBox();
            Label lbl1 = new Label();
            Label lbl2 = new Label();
            Label lbl3 = new Label();
            Button but1 = new Button();

            // control 1 : TextBox
            tx1.Location = new Point(27, 67);
            tx1.Size = new Size(107, 20);
            tx1.TextChanged += Tx1_TextChanged;
            tx1.Name = "_time_Hours";

            // control 2 : TextBox
            tx2.Location = new Point(181, 67);
            tx2.Size = new Size(107, 20);
            tx2.TextChanged += Tx2_TextChanged;
            tx2.Name = "_time_Minutes";

            // control 3 : TextBox
            tx3.Location = new Point(101, 133);
            tx3.Size = new Size(119, 20);
            tx3.Name = "_time_Time_left";
            tx3.Text = string.Format("{0} Timer : {1} Minutter",0,0);

            // control 4 : Label
            lbl1.Name = "_time_Hours_lbl";
            lbl1.Location = new Point(60, 48);
            lbl1.Size = new Size(35, 13);
            lbl1.Text = "Timer";

            // control 5 : Label
            lbl2.Name = "_time_Minutes_lbl";
            lbl2.Location = new Point(212, 48);
            lbl2.Size = new Size(45, 13);
            lbl2.Text = "Minutter";

            // control 6 : Label
            lbl3.Name = "_time_left_lbl";
            lbl3.Location = new Point(127, 113);
            lbl3.Size = new Size(127, 113);
            lbl3.Text = "Tid tilbage";

            // control 7 : Button
            but1.Name = "_time_Start_but";
            but1.FlatStyle = FlatStyle.Flat;
            but1.FlatAppearance.BorderColor = Color.Black;
            but1.FlatAppearance.BorderSize = 2;
            but1.FlatAppearance.MouseDownBackColor = Color.Silver;
            but1.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
            but1.Location = new Point(358, 132);
            but1.Size = new Size(138,48);
            but1.Click += But1_Click; ;
            but1.Text = "Start";

            // Tilføjer dem til programmet
            form.Controls.Add(tx1);
            form.Controls.Add(tx2);
            form.Controls.Add(tx3);
            form.Controls.Add(lbl1);
            form.Controls.Add(lbl2);
            form.Controls.Add(lbl3);
            form.Controls.Add(but1);

            text1 = tx1;
            text2 = tx2;
            text3 = tx3;

            key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock");

           if(!MainForm.KeyExists("TimeInfo"))
            {
                key.SetValue("TimeInfo", "0:0");
            }

            if (!string.IsNullOrWhiteSpace(key.GetValue("TimeInfo").ToString()))
            {
                Console.WriteLine(key.GetValue("TimeInfo").ToString());
                string[] splitText = key.GetValue("TimeInfo").ToString().Split(':');
                hours = int.Parse(splitText[0]);
                minutes = int.Parse(splitText[1]);
                if (hours <= 0 && minutes <= 0)
                {
                }
                else
                {
                    tx1.Text = ""+hours;
                    tx2.Text = ""+minutes;
                    but1.PerformClick();
                }
            }
        }

        // metode slut

        // metode der fortæller når man er igang med at skrive i textbox 2
        private void Tx2_TextChanged(object sender, System.EventArgs e)
        {
            // prøver at omdanne det der står i textboxen til en int
            try
            {
                minutes = int.Parse(text2.Text);
            }
            catch
            {
                // skriver en meddelse til brugeren hvis det ikke er et tal
                if (!string.IsNullOrWhiteSpace(text2.Text))
                {
                    MessageBox.Show("Der kan kun stå tal her", "ADVARSEL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            // sørger for at man ikke kan skrive over 60 i minut feltet
            if(minutes > 60)
            {
                minutes -= 60;
                hours++;
                text1.Text = "" + hours;
                text2.Text = "" + minutes;
            }
        }
        // metode slut

        // metoder der fortæller når man skriver i textbox 1
        private void Tx1_TextChanged(object sender, System.EventArgs e)
        {
            // prøver at omdanne det der står i textboxen til en int
            try
            {
                hours = int.Parse(text1.Text);
            }
            catch
            {
                // skriver en besked til brugeren hvis det ikke er tal
                if (!string.IsNullOrWhiteSpace(text1.Text))
                {
                    MessageBox.Show("Der kan kun stå tal her", "ADVARSEL", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
        // metode slut

        // metoder der fortæller hvad der skal gøres når der bliver trykket på start knappen
        private void But1_Click(object sender, System.EventArgs e)
        {
            text3.Text = string.Format("{0} Timer : {1} Minutter", hours, minutes);
            key.SetValue("CurrentOption", "TIME");

            Console.WriteLine("pressed");
            if (!onceBut)
            {
                try
                {
                    minutes = Int32.Parse(text2.Text);
                    hours = Int32.Parse(text1.Text);
                }
                catch
                {
                    MessageBox.Show("Du skal skrive noget i begge felter", "STOP!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

                // starter timeren
                timer.Interval = 1000; // ændre til hvor lang tid et minut det tager
                timer.Tick += Timer_Tick;
                timer.Start();

                // starter programmet
                MainForm.StartProgram(true);
            }
            onceBut = true;
        }
        // metode slut

        // metoder der fortæller hvad der sker hver gang der er gået et minut
        // eller hvor lang tid man har sat timeren til
        private void Timer_Tick(object sender, System.EventArgs e)
        {

            // stopper programmet når der ikke er mere tid tilbage
            if (hours <= 0 && minutes <= 0)
            {
                timer.Stop();
                //text3.Text = string.Format("{0} Timer : {1} Minutter", 0, 0);
                MainForm.StartProgram(false);
                onceBut = false;
                timer.Tick -= Timer_Tick;
                return;
            }

            // resetter minutter til 60 og trækker en fra timer
            if (minutes <= 0)
            {
                hours--;
                minutes = 60;
            }

            // opdaterer textboxen så man kan se hvor lang tid der er tilbage
            minutes--;
            text3.Text = string.Format("{0} Timer : {1} Minutter", hours, minutes);

            key.SetValue("TimeInfo", hours + ":" + minutes);

        }
        // metode slut

        // fjerner alle de controls som ikke skal være på formen
        public void Hide(Form form)
        {
            MainForm.Minutes = minutes;
            MainForm.Hours = hours;
            Control[] rems =
            {
                form.Controls["_time_Hours"],
                form.Controls["_time_Minutes"],
                form.Controls["_time_Time_left"],
                form.Controls["_time_Hours_lbl"],
                form.Controls["_time_Minutes_lbl"],
                form.Controls["_time_left_lbl"],
                form.Controls["_time_Start_but"]
            };

            foreach(Control rem in rems)
            {
                form.Controls.Remove(rem);
            }
        }
        // metode slut
    }
}
