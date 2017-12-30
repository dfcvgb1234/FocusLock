using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ExternControl
{
    public partial class Form1 : Form
    {
        string programPath = @"C:\Windows\System32\drivers\etc\External.begeba";

        public Form1()
        {
            InitializeComponent();
        }

        private void Start_but_Click(object sender, EventArgs e)
        {
            if (isOn.Checked)
            {
                File.Create(programPath).Close();
                try
                {
                    using (WebClient webclient = new WebClient())
                    {
                        webclient.Credentials = new NetworkCredential("focuslock.dk", "bagebe");
                        webclient.UploadFile("ftp://ftp.focuslock.dk/ftp/" + comboBox2.SelectedItem.ToString() + "/" +comboBox1.SelectedItem.ToString() + "/" + "External.begeba", programPath);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Der opstod en fejl, sørg for at du har forbindelse til internettet", "Fejl:" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if(File.Exists(programPath))
                {
                    File.Delete(programPath);
                }
                try
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.focuslock.dk/ftp/" + comboBox2.SelectedItem.ToString() + "/" + comboBox1.SelectedItem.ToString() + "/" + "External.begeba");

                    //If you need to use network credentials
                    request.Credentials = new NetworkCredential("focuslock.dk", "bagebe");
                    //additionally, if you want to use the current user's network credentials, just use:
                    //System.Net.CredentialCache.DefaultNetworkCredentials

                    request.Method = WebRequestMethods.Ftp.DeleteFile;
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    Console.WriteLine("Delete status: {0}", response.StatusDescription);
                    response.Close();
                }
                catch(Exception ex)
                {
                    
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBox2.SelectedIndex == comboBox2.Items.Count-1)
            {
                var add = new AddDirectory("f", true);
                add.Show();
                comboBox2.SelectedIndex = 0;
            }
            

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://ftp.focuslock.dk/ftp/" + comboBox2.SelectedItem.ToString());
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            //request.Proxy = System.Net.WebProxy.GetDefaultProxy();
            //request.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Credentials = new NetworkCredential("focuslock.dk", "bagebe");
            Stream rs = (Stream)request.GetResponse().GetResponseStream();

            StreamReader sr = new StreamReader(rs);
            string strList = sr.ReadToEnd();
            string[] lines = { null };

            if (strList.Contains("\r\n"))
            {
                lines = strList.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            }
            else if (strList.Contains("\n"))
            {
                lines = strList.Split(new string[] { "\n" }, StringSplitOptions.None);
            }
            comboBox1.Items.Clear();
            for (int i = 0; i < lines.Length; i++)
            {
                if (i > 1)
                {
                    if (!String.IsNullOrWhiteSpace(lines[i]))
                    {
                        comboBox1.Items.Add(lines[i]);
                    }
                }
            }
            comboBox1.Items.Add("Tilføj +");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == comboBox1.Items.Count-1)
            {
                var add = new AddDirectory(comboBox2.SelectedItem.ToString(), false);
                add.Show();
                comboBox1.SelectedIndex = 0;
            }

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://ftp.focuslock.dk/ftp");
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            //request.Proxy = System.Net.WebProxy.GetDefaultProxy();
            //request.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Credentials = new NetworkCredential("focuslock.dk", "bagebe");
            Stream rs = (Stream)request.GetResponse().GetResponseStream();

            StreamReader sr = new StreamReader(rs);
            string strList = sr.ReadToEnd();
            string[] lines = { null };

            if (strList.Contains("\r\n"))
            {
                lines = strList.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            }
            else if (strList.Contains("\n"))
            {
                lines = strList.Split(new string[] { "\n" }, StringSplitOptions.None);
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (i > 1)
                {
                    if (!String.IsNullOrWhiteSpace(lines[i]))
                    {
                        if(lines[i] == "External.begeba")
                        {
                            isOn.Checked = true;
                        }
                    }
                }
            }

            rs.Close();
            sr.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://ftp.focuslock.dk/ftp");
            request.Method = WebRequestMethods.Ftp.ListDirectory;    

            //request.Proxy = System.Net.WebProxy.GetDefaultProxy();
            //request.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Credentials = new NetworkCredential("focuslock.dk", "bagebe");
            Stream rs = (Stream)request.GetResponse().GetResponseStream();

            StreamReader sr = new StreamReader(rs);
            string strList = sr.ReadToEnd();
            string[] lines = { null };

            if (strList.Contains("\r\n"))
            {
                lines = strList.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            }
            else if (strList.Contains("\n"))
            {
                lines = strList.Split(new string[] { "\n" }, StringSplitOptions.None);
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (i > 1)
                {
                    if (!String.IsNullOrWhiteSpace(lines[i]))
                    {
                        comboBox2.Items.Add(lines[i]);
                    }
                }
            }

            rs.Close();
            sr.Close();
            comboBox2.Items.Add("Tilføj +");
        }
    }
}
