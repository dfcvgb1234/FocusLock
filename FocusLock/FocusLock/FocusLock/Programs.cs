using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FocusLock
{
    public partial class Programs : Form
    {
        bool[] checkedPrograms = new bool[Program.checkedState.Length];

        RegistryKey key;

        public static CheckedListBox itemlistbox;

        public static List<string> tmpprocesslist = new List<string>();

        public Programs()
        {
            InitializeComponent();
            itemlistbox = ItemBox;
            foreach (string j in Program.processList)
            {
                if (!string.IsNullOrWhiteSpace(j))
                {
                    tmpprocesslist.Add(j);
                }
            }
        }

        private void Programs_Load(object sender, EventArgs e)
        {
            key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock");

            int k = 0;
            foreach(string j in Program.checkedState)
            {
                if(j == "TRUE")
                {
                    checkedPrograms[k] = true;
                }
                if(j == "FALSE")
                {
                    checkedPrograms[k] = false;
                }
                k++;
            }
            for (int i = 0; i < Program.gamesList.Length; i++)
            {
                if (Program.gamesList[i] != null)
                {
                    ItemBox.Items.Add(Program.gamesList[i], checkedPrograms[i]);
                }
            }
        }

        private void Add_but_Click(object sender, EventArgs e)
        {
            var add_form = new Add();
            add_form.Show();
        }

        public static void AddToList(string ProgramName, string ProcessName, bool checkedState)
        {
            tmpprocesslist.Add(ProcessName);
            itemlistbox.Items.Add(ProgramName, true);
        }

        private void Save_but_Click(object sender, EventArgs e)
        {
            if (MainForm.KeyExists("Programs"))
            {
                key.SetValue("Programs", "");
            }

            for (int i = 0; i < ItemBox.Items.Count; i++)
            {
                string check;
                if(ItemBox.GetItemChecked(i))
                {
                    check = "TRUE";
                }
                else
                {
                    check = "FALSE";
                }

                key.SetValue("Programs", key.GetValue("Programs").ToString() + ItemBox.Items[i] + "," + tmpprocesslist[i] + "," + check + ";");
                
                //File.AppendAllText(@"C:\Windows\System32\drivers\etc\Programs.begeba", ItemBox.Items[i] + "," + tmpprocesslist[i] + "," + check + ";");
            }
            Program.CreateProgramArray();
            MainForm.CreateProgramArray(Program.checkedState, Program.processList);
            key.SetValue("ProgramsChanged", "TRUE");
            MessageBox.Show("Ændringerne er gemt", "ADVARSEL", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Search_tx_TextChanged(object sender, EventArgs e)
        {
            string searchString = Search_tx.Text;

            int index = itemlistbox.FindString(searchString, -1);
            if(index != -1)
            {
                itemlistbox.SetSelected(index, true);
            }
        }

        private void Searcj_lbl_Click(object sender, EventArgs e)
        {

        }

        private void Reset_Click(object sender, EventArgs e)
        {
            key.SetValue("ProgramsChanged", "FALSE");
            MessageBox.Show("Genstart programmet for at ændringerne træder i kraft", "HUSK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
