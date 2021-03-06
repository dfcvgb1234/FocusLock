﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Cache;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Reflection;

namespace FocusLock
{
    static class Program
    {
        // En variabel som fortæller om programmet kører i admin
        public static bool isElevated;

        // Fortæller programmet om der skal kører eller om det var fordi at man lukkede login boksen
        public static bool shouldRun = false;

        public static bool isConnected;

        // Fortæller programmet hvilke elementer du har adgang til
        public static int permission = 0;

        public static string room;

        public static string ID;

        // Paths to important files
        static string hostsPath = @"C:\Windows\System32\drivers\etc\hosts";
        static string programPath = @"C:\Windows\System32\drivers\etc\Programs.begeba";
        static string hostPath = @"C:\Windows\System32\drivers\etc\Host.begeba";

        // laver en multidimmensionel array til når data hentes fra programfilen
        public static string[][] values = new string[5000][];

        // Genererer en ny array, med objecter til processlisten  
        // *husk at gøre arrayen større hvis der er brug for mere plads*
        public static object[] processList = new object[5000];

        // Genererer en ny array, med objecter til NavneListen  
        // *husk at gøre arrayen større hvis der er brug for mere plads*
        static public object[] gamesList = new object[5000];

        // Genererer en ny array, med objecter til CheckListen  
        // *husk at gøre arrayen større hvis der er brug for mere plads*
        static public object[] checkedState = new object[5000];

        // En array som der gemmer noget data fra program data filen, som senere splittes
        public static string[] netFileText = new string[5000];

        // Main startcc
        static void Main(string[] args)
        {
            // Checker om programmet bliver kørt i admin og bliver ved med at prompte om at det skal køres som admin
            if (!GetCheckForAdmin())
            {
                ForceAdmin();
            }

            // Sørger for at hosts filen den er der
            if (!File.Exists(hostsPath))
            {
                File.Create(hostsPath).Close();
            }

            // Sørger for at program key'en er i registry
            if (!MainForm.KeyExists("Programs"))
            {
                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("Programs", "");
            }

            // EventViewer der checker om et program er blevet åbnet
            ManagementEventWatcher startWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));

            // checker om computeren har forbindelse til internettet
            if (IsMachineUp("www.google.com"))
            {
                isConnected = true;
                CheckForUpdates();

                if (isConnected)
                {
                    if (!MainForm.KeyExists("ProgramsChanged"))
                    {
                        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("ProgramsChanged", "");
                    }

                    if (!MainForm.KeyExists("ChangedHost"))
                    {
                        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("ChangedHost", "");
                    }

                    if (Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").GetValue("ProgramsChanged").ToString() != "TRUE")
                    {
                        DownloadProgramList();
                    }

                    if (!MainForm.KeyExists("ChangedHost"))
                    {
                        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("ChangedHost", "");
                    }
                    else
                    {
                        if (Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").GetValue("ChangedHost").ToString() != "TRUE")
                        {
                            DownloadHostsFile();
                        }
                    }
                    isConnected = true;
                }
            }
            else
            {
                CheckProgramList();
                MessageBox.Show("Du har ikke noget internet, kører programmet i offline-tilstand", "FEJL!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isConnected = false;
            }

            // Laver vores array ud fra det data vi har i program nøglen
            CreateProgramArray();

            // Start startwatch, som der sørger for at finde de programmer som der åbnes
            startWatch.EventArrived += StartWatch_EventArrived;
            startWatch.Start();

            // Åbner login formen
            if (isConnected)
            {
                var login = new LoginForm();
                login.Show();
                login.FormClosing += Login_FormClosing;
            }
            else
            {
                OpenMainForm(1, "", "offline");
            }

            // Kører Programmet
            Application.Run();
        }

        private static void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Checker om programmet skal lukkes eller ej
            if (!MainForm.butPressed && !shouldRun)
            {
                Environment.Exit(0);
            }
            if(e.CloseReason == CloseReason.FormOwnerClosing)
            {
                Environment.Exit(0);
            }
        }

        // Start metoden slut

        // En metode som der laver vores array
        public static void CreateProgramArray()
        {
            gamesList = new object[5000];
            processList = new object[5000];
            checkedState = new object[5000];

            // Splitter hvad der står i vores program nøgle
            netFileText = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").GetValue("Programs").ToString().Split(';');

            // kører igennem alle værdierne og spliter dem
            for (int i = 0; i < netFileText.Length; i++)
            {
                values[i] = netFileText[i].Split(',');
            }

            // kører igennem alle værdierne og gemmer dem i de rigtige variabler
            for (int i = 0; i < netFileText.Length; i++)
            {

                int h = 0;
                foreach (string j in values[i])
                {
                    if (h == 0)
                    {
                        // gemmer GamesList
                        if (!String.IsNullOrEmpty(values[i][h]))
                        {
                            gamesList[i] = values[i][h];
                        }
                    }
                    if (h == 1)
                    {
                        // gemmer ProcessList
                        if (!String.IsNullOrEmpty(values[i][h]))
                        {
                            processList[i] = values[i][h];
                        }
                    }
                    if (h == 2)
                    {
                        // gemmer CheckedList
                        if (!String.IsNullOrEmpty(values[i][h]))
                        {
                            checkedState[i] = values[i][h];
                        }
                    }
                    h++;
                }
            }
        }
        // CreateProgramArray metode slut

        private static void CheckForUpdates()
        {
            var version = FileVersionInfo.GetVersionInfo(Environment.CurrentDirectory + @"\" + "FocusLock.exe");
            Console.WriteLine(version.FileVersion);
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential("focuslock.dk", "bagebe");
                    client.DownloadFile("ftp://ftp.focuslock.dk/FocusLock_updates/CurrentVersion.txt", Environment.CurrentDirectory + @"\CurrentVersion.txt");
                }
            }
            catch
            {
                MessageBox.Show("Du sidder bag en proxy, starter programmet i offline-tilstand", "FEJL!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isConnected = false;
                goto nointer;
            }
            string onlineversion = File.ReadAllText(Environment.CurrentDirectory + @"\CurrentVersion.txt");
            string[] nospace = onlineversion.Split('\n');
            Console.WriteLine(nospace[0]);
            if (version.FileVersion != nospace[0])
            {
                Console.WriteLine(Environment.CurrentDirectory + @"\FocusLock_updater.exe");
                Process.Start(Environment.CurrentDirectory + @"\FocusLock_updater.exe", onlineversion);
                Environment.Exit(1);
            }
            isConnected = true;
            nointer:;

        }

        // Checker om der  er en program fil, hvis der ikke er noget net
        private static void CheckProgramList()
        {
            // Advarer folk om at der skal være forbindelse til nettet første gang programmet åbnes
            if (!MainForm.KeyExists("Programs") || String.IsNullOrEmpty(Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").GetValue("Programs").ToString()))
            {
                MessageBox.Show("Du skal have forbindelse til nettet første gang du åbner programmet", "ADVARSEL!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Environment.Exit(1);
            }
        }
        // CheckProgramList metode slut

        // Metode der sørger for at programmet der kører i admin
        public static void ForceAdmin()
        {
            // Får den nuværende sti som programmet kører i
            string currentPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string openPath = currentPath + @"\FocusLock.exe";

            // Error code, til når brugeren siger nej
            const int ERROR_CANCELLED = 1223; //The operation was canceled by the user.

            //Får programmet til at starte som admin
            ProcessStartInfo info = new ProcessStartInfo(openPath)
            {
                UseShellExecute = true,
                Verb = "runas"
            };
            again:
            try
            {
                Process.Start(info);
            }
            // Prompter brugeren igen og igen indtil personen har sagt ja.
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode == ERROR_CANCELLED)
                {
                    goto again;
                }
            }
            Thread.Sleep(2000);
            Environment.Exit(1);
        }
        // ForceAdmin metode slut

        public static void OpenMainForm(int perm, string roomID, string id)
        {
            shouldRun = true;
            permission = perm;
            room = roomID;
            ID = id;
            var main = new MainForm();
            main.Show();
        }

        private static void DownloadProgramList()
        {
            // Skriver til konsolen hvad for en dato det er.
            Console.WriteLine(GetNistTime());
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Credentials = new NetworkCredential("focuslock.dk", "bagebe");
                    webClient.DownloadFile("ftp://ftp.focuslock.dk/ftp/moved/Programs.begeba", programPath);
                }

                string fileText = File.ReadAllText(programPath);

                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("Programs", fileText);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void DownloadHostsFile()
        {
            // Skriver til konsolen hvad for en dato det er.
            Console.WriteLine(GetNistTime());
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Credentials = new NetworkCredential("focuslock.dk", "bagebe");
                    webClient.DownloadFile("ftp://ftp.focuslock.dk/ftp/moved/Host.begeba", hostPath);
                }

                string fileText = File.ReadAllText(hostPath);
                string[] splitText = fileText.Split('\n');

                string text = "";

                foreach (string j in splitText)
                {
                    text += j + ";";
                }

                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("HOST", text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void StartWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            if (MainForm.butPressed)
            {
                string proc = e.NewEvent.Properties["ProcessName"].Value.ToString();
                if (proc == "regedit.exe")
                {
                    Process[] process = Process.GetProcessesByName("regedit");

                    foreach (Process j in process)
                    {
                        j.Kill();
                    }
                }
                else if (proc == "cmd.exe")
                {
                    Process[] process = Process.GetProcessesByName("cmd");

                    foreach (Process j in process)
                    {
                        j.Kill();
                    }
                }
            }
        }

        public static bool GetCheckForAdmin()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return isElevated;
        }

        // metode slut

        // En metode der checker om maskinen har forbindelse til nettet
        private static bool IsMachineUp(string hostName)
        {
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(hostName, 3000);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // metode slut

        // metode til at få internet tid
        public static DateTime GetNistTime()
        {
            try
            {
                DateTime dateTime = DateTime.MinValue;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://nist.time.gov/actualtime.cgi?lzbc=siqm9b");
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
                request.ContentType = "application/x-www-form-urlencoded";
                request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore); //No caching
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader stream = new StreamReader(response.GetResponseStream());
                    string html = stream.ReadToEnd();//<timestamp time=\"1395772696469995\" delay=\"1395772696469995\"/>
                    string time = Regex.Match(html, @"(?<=\btime="")[^""]*").Value;
                    double milliseconds = Convert.ToInt64(time) / 1000.0;
                    dateTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToLocalTime();
                }
                return dateTime;
            }
            catch
            {
                return DateTime.Now;
            }
        }
        // metode slut
    }
}
