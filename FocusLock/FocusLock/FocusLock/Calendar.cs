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
            if (key.GetValue("DateInfo") != null)
            {
                string fileText = key.GetValue("DateInfo").ToString();
                string[] cube = fileText.Split(';');
                string[] splitIndex;
                Size size = new Size(12, 20);
                Size realSize = new Size(20, 20);

                foreach (string h in cube)
                {
                    splitIndex = h.Split(',');
                    try
                    {

                        Label lbl = this.Controls.Find(splitIndex[0], true).FirstOrDefault() as Label;
                        if (splitIndex[1] == "1")
                        {
                            Console.WriteLine("1");
                            lbl.BackColor = Color.CadetBlue;
                            lbl.Size = realSize;
                        }
                        else if (splitIndex[1] == "2")
                        {
                            Console.WriteLine("2");
                            lbl.BackColor = Color.CadetBlue;
                            lbl.Size = size;
                        }
                    }
                    catch { }
                }
            }

            else
            {
                key.SetValue("DateInfo", " ");
                string fileText = key.GetValue("DateInfo").ToString();
                string[] cube = fileText.Split(';');


                foreach (string h in cube)
                {
                    try
                    {
                        Label lbl = this.Controls.Find(h, true).FirstOrDefault() as Label;
                        lbl.BackColor = Color.CadetBlue;
                    }
                    catch { }
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
            Point originalPos = new Point(lbl.Location.X-2,lbl.Location.Y-2);
            Point newPos = new Point(lbl.Location.X + 2, lbl.Location.Y + 2);

            if (color == System.Drawing.Color.CadetBlue && e.Button == MouseButtons.Left)
            {
                color = System.Drawing.Color.LightGray;
                lbl.Size = new Size(20, 20);
            }
            else if (color == Color.LightGray && e.Button == MouseButtons.Left)
            {
                color = Color.CadetBlue;
                lbl.Size = realSize;
            }

            if(color == Color.CadetBlue && e.Button == MouseButtons.Right)
            {
                color = Color.LightGray;
                lbl.Size = realSize;
            }
            else if(color == Color.LightGray && e.Button == MouseButtons.Right)
            {
                color = Color.CadetBlue;
                lbl.Size = size;
            }

            lbl.BackColor = color;
            Console.WriteLine(GridContainer.Controls.OfType<Label>().Count());

            RenameGrid(lbl);

            if (key.GetValue("DateInfo") != null)
            {
                key.SetValue("DateInfo", "");
            }

            // gemmer det i en fil
            foreach (Label labl in GridContainer.Controls.OfType<Label>())
            {
                if (labl.BackColor == Color.CadetBlue && labl.Size == realSize)
                {
                    key.SetValue("DateInfo", key.GetValue("DateInfo").ToString() + labl.Name + ",1" + ";");
                    //File.AppendAllText(@"C:\Windows\System32\drivers\etc\DateInfo.begeba", labl.Name + ",1" + ";");
                }
                if (labl.BackColor == Color.CadetBlue && labl.Size == size)
                {
                    key.SetValue("DateInfo", key.GetValue("DateInfo").ToString() + labl.Name + ",2" + ";");
                    //File.AppendAllText(@"C:\Windows\System32\drivers\etc\DateInfo.begeba", labl.Name + ",2" + ";");
                }
                else if (labl.BackColor != Color.CadetBlue && key.GetValue("DateInfo") == null)
                {
                    key.SetValue("DateInfo", "");
                    //File.Create(@"C:\Windows\System32\drivers\etc\DateInfo.begeba").Close();
                }
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
