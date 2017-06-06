using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FocusLock
{
    public partial class Websites : Form
    {

        string hostPath = @"C:\Windows\System32\drivers\etc\hosts";

        string hostValuesPath = @"C:\Windows\System32\drivers\etc\host.begeba";

        public Websites()
        {
            InitializeComponent();
        }

        private void Add_but_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(Item.Text))
            {
                list.Items.Add(Item.Text);
            }
            else
            {
                MessageBox.Show("Du skal skrive noget som du kan tilføje", "STOP!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void Delete_but_Click(object sender, EventArgs e)
        {
            if (list.SelectedItem != null)
            {
                list.Items.RemoveAt(list.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Du skal vælge noget du gerne vil fjerne først!", "STOP!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void Save_but_Click(object sender, EventArgs e)
        {
            if(File.Exists(hostValuesPath))
            {
                File.Delete(hostValuesPath);
            }

            foreach (string j in list.Items)
            {
                File.AppendAllText(hostValuesPath, "" + j + ";");
            }
            MessageBox.Show("Genstart programmet for at ændringerne træder i kraft", "HUSK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Websites_Load(object sender, EventArgs e)
        {
            string[] fileText = File.ReadAllText(hostValuesPath).Split(';');
            foreach(string j in fileText)
            {
                if (!String.IsNullOrWhiteSpace(j))
                {
                    list.Items.Add(j);
                }
            }
        }

        private void Search_tx_TextChanged(object sender, EventArgs e)
        {
            string searchString = Search_tx.Text;

            int index = list.FindString(searchString, -1);
            if (index != -1)
            {
                list.SetSelected(index, true);
            }
        }
    }
}
