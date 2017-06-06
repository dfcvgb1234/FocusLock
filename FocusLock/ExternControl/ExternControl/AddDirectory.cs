using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExternControl
{
    public partial class AddDirectory : Form
    {
        string folders;
        bool isFirstDirectories;

        public AddDirectory(string folder, bool isFirstDirectory)
        {
            InitializeComponent();
            this.folders = folder;
            this.isFirstDirectories = isFirstDirectory;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBox1.Text))
            {
                CreateDirectoryFTP cdf = new CreateDirectoryFTP(folders, textBox1.Text, isFirstDirectories);
            }
            else
            {
                MessageBox.Show("Du skal skrive noget", "STOP!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
