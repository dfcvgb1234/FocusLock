using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FocusLock
{
    public partial class Calendar : Form
    {
        public Calendar()
        {
            InitializeComponent();
        }

        RegistryKey key;

        public string[][] labels;

        public List<string> whole = new List<string>();
        public List<string> half1 = new List<string>();
        public List<string> half2 = new List<string>();

        // definere hvor mange rows og columns der skal være
        const int RowCount = 24;
        const int ColumnCount = 7;

        public Byte index = 0;

        // en metode som der tæller hvor mange knapper der er af en bestemt farve
        private int CountCellsOfColor(Color color)
        {
            int count = 0;

            foreach (Label lbl in GridContainer.Controls.OfType<Label>())
            {
                if (lbl.BackColor == color) count += 1;
            }
            return count;
        }
        // metode slut

        // metode til at fortælle hvad der skal ske når denne form den åbnes
        private void Calendar_Load(object sender, EventArgs e)
        {

            key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock");

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    // generer knapperne
                    Label lbl = new Label()
                    {
                        Size = new Size(20, 20),
                        Name = i + ":" + j,
                        BackColor = Color.LightGray,
                        Location = new Point(i * 20, j * 20)
                    };
                    lbl.MouseClick += Lbl_MouseClick;
                    lbl.BorderStyle = BorderStyle.FixedSingle;
                    GridContainer.Controls.Add(lbl);
                }
            }

            if (MainForm.KeyExists("WHOLE"))
            {
                string[] filetext = key.GetValue("WHOLE").ToString().Split(';');

                foreach (string j in filetext)
                {
                    if (!String.IsNullOrWhiteSpace(j))
                    {
                        Label lbl = this.Controls.Find(j,true).FirstOrDefault() as Label;
                        lbl.BackColor = Color.Orange;
                        whole.Add(lbl.Name);
                    }
                }
            }
            if (MainForm.KeyExists("HALF1"))
            {
                string[] filetext = key.GetValue("HALF1").ToString().Split(';');

                foreach (string j in filetext)
                {
                    if (!String.IsNullOrWhiteSpace(j))
                    {
                        Label lbl = this.Controls.Find(j, true).FirstOrDefault() as Label;
                        lbl.BackColor = Color.Orange;
                        lbl.Size = new Size(12, 20);
                        half1.Add(lbl.Name);
                    }
                }
            }
            if (MainForm.KeyExists("HALF2"))
            {
                string[] filetext = key.GetValue("HALF2").ToString().Split(';');

                foreach (string j in filetext)
                {
                    if (!String.IsNullOrWhiteSpace(j))
                    {
                        Label lbl = this.Controls.Find(j, true).FirstOrDefault() as Label;
                        lbl.BackColor = Color.Orange;
                        lbl.Size = new Size(12, 20);
                        lbl.Location = new Point(lbl.Location.X + 9, lbl.Location.Y);
                        half2.Add(lbl.Name);
                    }
                }
            }
        }
        // metode slut

        // metode til at bestemme hvad der skal ske, når der trykkes på en knap
        private void Lbl_MouseClick(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;
            Color color = lbl.BackColor;
            Size size = new Size(12, 20);
            Size realSize = new Size(20, 20);
            Point originalPos = new Point(lbl.Location.X-9,lbl.Location.Y);
            Point newPos = new Point(lbl.Location.X + 9, lbl.Location.Y );

            if (e.Button == MouseButtons.Left)
            {
                if (color == Color.Orange && lbl.Size == realSize)
                {
                    whole.Remove(lbl.Name);
                    lbl.BackColor = Color.LightGray;
                }
                else if (color == Color.LightGray && lbl.Size == realSize)
                {
                    whole.Add(lbl.Name);
                    lbl.BackColor = Color.Orange;
                }
                else if (color == Color.Orange && lbl.Size == size)
                {
                    bool found = false;
                    foreach (string j in half1)
                    {
                        if(j == lbl.Name)
                        {
                            found = true;
                        }
                    }
                    if(found)
                    {
                        half1.Remove(lbl.Name);
                        lbl.Size = realSize;
                        whole.Add(lbl.Name);
                    }
                    else
                    {
                        bool find = false;
                        foreach (string j in half2)
                        {
                            if (j == lbl.Name)
                            {
                                find = true;
                            }
                        }
                        if(find)
                        {
                            lbl.Location = originalPos;
                            lbl.Size = realSize;
                            whole.Add(lbl.Name);
                            half2.Remove(lbl.Name);
                        }
                    }
                }
            }

            if(e.Button == MouseButtons.Right)
            {
                if (color == Color.Orange && lbl.Size == realSize)
                {
                    lbl.Size = size;
                    half1.Add(lbl.Name);
                    whole.Remove(lbl.Name);
                }
                else if (color == Color.LightGray && lbl.Size == realSize)
                {
                    lbl.Size = size;
                    lbl.BackColor = Color.Orange;
                    half1.Add(lbl.Name);
                }
                else if (color == Color.Orange && lbl.Size == size)
                {
                    bool found = false;
                    foreach (string j in half1)
                    {
                        if(j == lbl.Name)
                        {
                            lbl.Location = newPos;
                            found = true;
                        }
                    }
                    if(found)
                    {
                        half1.Remove(lbl.Name);
                        half2.Add(lbl.Name);
                    }
                    else
                    {
                        bool find = false;
                        foreach (string i in half2)
                        {
                            if (i == lbl.Name)
                            {
                                lbl.Location = originalPos;
                                find = true;
                            }
                        }
                        if(find)
                        {
                            half1.Add(lbl.Name);
                            half2.Remove(lbl.Name);
                        }
                    }
                }
            }

            RenameGrid(lbl);

            if (key.GetValue("WHOLE") == null)
            {
                key.SetValue("WHOLE", "");
            }
            if (key.GetValue("HALF1") == null)
            {
                key.SetValue("HALF1", "");
            }
            if (key.GetValue("HALF2") == null)
            {
                key.SetValue("HALF2", "");
            }

            key.SetValue("WHOLE", "");
            key.SetValue("HALF1", "");
            key.SetValue("HALF2", "");

            Console.WriteLine("WHOLE");
            foreach (string i in whole)
            {
                Console.WriteLine(i);
                key.SetValue("WHOLE", key.GetValue("WHOLE").ToString() + i + ";");
            }
            Console.WriteLine("HALF1");
            foreach (string i in half1)
            {
                Console.WriteLine(i);
                key.SetValue("HALF1", key.GetValue("HALF1").ToString() + i + ";");
            }
            Console.WriteLine("HALF2");
            foreach (string i in half2)
            {
                Console.WriteLine(i);
                key.SetValue("HALF2", key.GetValue("HALF2").ToString() + i + ";");
            }
        }
        // metode slut

        // metode til at navngive den variabel som der blev trykket på
        public void RenameGrid(Label lbl)
        {
            string clock = "";
            string day = "";
            string[] splitName = lbl.Name.Split(':');

            // første værdi
            switch (splitName[0])
            {
                case "0":
                    clock = "Midnight";
                    break;

                case "1":
                    clock = "1 klokken";
                    break;

                case "2":
                    clock = "2 klokken";
                    break;

                case "3":
                    clock = "3 klokken";
                    break;

                case "4":
                    clock = "4 klokken";
                    break;

                case "5":
                    clock = "5 klokken";
                    break;

                case "6":
                    clock = "6 klokken";
                    break;

                case "7":
                    clock = "7 klokken";
                    break;

                case "8":
                    clock = "8 klokken";
                    break;

                case "9":
                    clock = "9 klokken";
                    break;

                case "10":
                    clock = "10 klokken";
                    break;

                case "11":
                    clock = "11 klokken";
                    break;

                case "12":
                    clock = "Midday";
                    break;

                case "13":
                    clock = "13 klokken";
                    break;

                case "14":
                    clock = "14 klokken";
                    break;

                case "15":
                    clock = "15 klokken";
                    break;

                case "16":
                    clock = "16 klokken";
                    break;

                case "17":
                    clock = "17 klokken";
                    break;

                case "18":
                    clock = "18 klokken";
                    break;

                case "19":
                    clock = "19 klokken";
                    break;

                case "20":
                    clock = "20 klokken";
                    break;

                case "21":
                    clock = "21 klokken";
                    break;

                case "22":
                    clock = "22 klokken";
                    break;

                case "23":
                    clock = "23 klokken";
                    break;
            }

            // anden værdi
            switch (splitName[1])
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
            Console.WriteLine(clock + ":" + day);
        }
        // metode slut
    }
}
