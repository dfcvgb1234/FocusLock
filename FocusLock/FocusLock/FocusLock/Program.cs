using System;
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
using System.Windows.Forms;

namespace FocusLock
{
    static class Program
    {
        public static int i = 0;
        // En variabel som fortæller om programmet kører i admin
        public static bool isElevated;

        // Paths to important files
        static string path = @"C:\Windows\System32\drivers\etc\hosts";
        static string programPath = @"C:\Windows\System32\drivers\etc\Programs.begeba";

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

        public static string[] netFileText = new string[5000];

        // Main startcc
        static void Main(string[] args)
        {
            // Checker om programmet bliver kørt i admin og bliver ved med at prompte om at det skal køres som admin
            if (!checkForAdmin())
            {
                // Får den nuværende sti som programmet kører i
                string currentPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                string openPath = currentPath + @"\FocusLock.exe";

                // Error code, til når brugeren siger nej
                const int ERROR_CANCELLED = 1223; //The operation was canceled by the user.

                //Får programmet til at starte som admin
                ProcessStartInfo info = new ProcessStartInfo(openPath);
                info.UseShellExecute = true;
                info.Verb = "runas";
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

            // Sørger for at de nødvændige filer er der.
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

            // Sørger for at de nødvændige filer er der.
            if (!File.Exists(programPath))
            {
                File.Create(programPath).Close();
            }

            // EventViewer der checker om et program er blevet åbnet
            ManagementEventWatcher startWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));

            // checker om computeren har forbindelse til internettet
            if (IsMachineUp("www.google.com"))
            {
                // Skriver til konsolen hvad for en dato det er.
                Console.WriteLine(GetNistTime());
                if (!File.Exists(@"C:\Windows\System32\drivers\etc\Changed.begeba"))
                {
                    try
                    {
                        using (WebClient webClient = new WebClient())
                        {
                            webClient.Credentials = new NetworkCredential("focuslock.dk", "bagebe");
                            webClient.DownloadFile("ftp://ftp.focuslock.dk/ftp/Programs.begeba", programPath);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                netFileText = File.ReadAllText(programPath).Split(';');
                for (int i = 0; i < netFileText.Length; i++)
                {
                    values[i] = netFileText[i].Split(',');
                }
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
            else
            {
                // Advarer folk om at der skal være forbindelse til nettet første gang programmet åbnes
                if (String.IsNullOrEmpty(File.ReadAllText(programPath)))
                {
                    MessageBox.Show("Du skal have forbindelse til nettet første gang du åber programmet", "ADVARSEL!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Environment.Exit(1);
                }
                else
                {
                    // gemmer hvad der står i program filen i en 2d array
                    netFileText = File.ReadAllText(programPath).Split(';');
                    for (int i = 0; i < netFileText.Length; i++)
                    {
                        values[i] = netFileText[i].Split(',');
                    }
                    for (int i = 0; i < netFileText.Length; i++)
                    {
                        
                        int h = 0;
                        foreach (string j in values[i])
                        {
                            if (h == 0)
                            {
                                // gemmer GamesList
                                if(!String.IsNullOrEmpty(values[i][h]))
                                {
                                    gamesList[i] = values[i][h];
                                }
                            }
                            if(h == 1)
                            {
                                // gemmer ProcessList
                                if (!String.IsNullOrEmpty(values[i][h]))
                                {
                                    processList[i] = values[i][h];
                                }
                            }
                            if(h == 2)
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
            }
            // subscriber startwatch til StartWatch_EventArrived metoden.
            startWatch.EventArrived += new EventArrivedEventHandler(StartWatch_EventArrived);
            // Starter startwatch
            startWatch.Start();

            var main_form = new MainForm();
            main_form.Show();
            Application.Run();
        }

        // StartWatch metoden, som der lukket programmet hvis det skal lukkes
        private static void StartWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            if (MainForm.butPressed == true)
            {
                // Finder hvilken process der er blevet åbnet.
                string process = e.NewEvent.Properties["ProcessName"].Value.ToString();

                // splitter process navnet fra .exe så vi ikke får det med i navnet.
                string[] rProcess = Regex.Split(process, ".exe");

                // for loop der sørger for at vi får det rigtige navn ud af det.
                for (int i = 0; i < rProcess.Length; i++)
                {
                    if (rProcess[i].ToLower() != ".exe" && string.IsNullOrWhiteSpace(rProcess[i]) == false)
                    {
                        Console.WriteLine(rProcess[i]);
                        Thread.Sleep(1000);
                        // kører overload metoden af stopProcesses.
                        MainForm.StopProcesses(Program.processList, rProcess[i]);
                    }
                }
            }
        }
        // metode slut

        // En metode der checker om programmet bliver kørt i admin
        static public bool checkForAdmin()
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
        // metode slut
    }
}
