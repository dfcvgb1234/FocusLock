using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Threading;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using System.Net;

namespace FocusLock
{
    public partial class MainForm : Form
    {
        public static int Minutes { get; set; }
        public static int Hours { get; set; }

        public static bool calendarOpen = false;

        static Form form_main;

        static Process[] chrome;

        NotifyIcon ni;

        static MenuStrip menu;

        public static ComboBox cb = new ComboBox();

        static Button focusBut;

        public static bool logout = false;

        TimeOption to = new TimeOption();
        CalendarOption co = new CalendarOption();
        ExternalOption eo = new ExternalOption();

        RegistryKey key;

        // Timer der kører hvert minut
        public static System.Windows.Forms.Timer externTimer = new System.Windows.Forms.Timer();


        // Sørger for at der kun er visse ting der bliver gjort en gang
        static bool onceBut = new bool();

        // en variabel som fortæller om programmet kører i admin
        public bool isElevated;

        // laver en object array til vores checkedPrograms
        static List<object> checkedPrograms = new List<object>();

        // en array som indeholder de websites der bliver blokeret
        public static string[] websites;

        // fortæller om programmet er blevet startet
        public static bool butPressed = false;

        public MainForm()
        {
            // fortæller når programmet er bliver lukket
            FormClosing += MainForm_FormClosing;
            InitializeComponent();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // bruger lukning
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // lukker kun hvis programmet ikke kører
                if (!butPressed && !logout)
                {
                    Environment.Exit(1);
                }
                else if (!butPressed && logout)
                {

                }
                else
                {
                    e.Cancel = true;
                    WindowState = FormWindowState.Minimized;
                }
            }
            // windows lukning
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                Environment.Exit(1);
            }
        }

        // genererer vores fulde liste af de programmer der skal lukkes
        public static void CreateProgramArray(object[] checkedArray, object[] programArray)
        {
            checkedPrograms.Clear();
            // omdanner arrayen til en string array
            string[] check = checkedArray.Where(x => x != null)
           .Select(x => x.ToString())
           .ToArray();

            // genererer listen
            for (int i = 0; i < check.Length; i++)
            {
                if (check[i] == "TRUE")
                {
                    checkedPrograms.Add(programArray[i]);
                }
            }
        }
        // metode slut

        // metoden der sørger for at processerne bliver checket og lukket hvis de skal lukkes
        public static void StopProcesses(List<object> Processes)
        {
            RegistryKey task;

            //cmd = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\cmd.exe");
            task = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\taskmgr.exe");

            //cmd.SetValue("Debugger", "cmd.exe /k " + Environment.CurrentDirectory + "\\\"Acess Denied\"\\\"Acess Denied.html\"" + "& exit");
            task.SetValue("Debugger", "cmd.exe /k " + Environment.CurrentDirectory + "\\\"Acess Denied\"\\\"Acess Denied.html\"" + "& exit");

            //cmd.Close();
            task.Close();

            // omdanner listen til en string array
            string[] proc = Processes.Where(x => x != null)
                       .Select(x => x.ToString())
                       .ToArray();
            foreach (string n in proc)
            {
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\" + n + ".exe");
                key.SetValue("Debugger", "cmd.exe /k " + Environment.CurrentDirectory + "\\\"Acess Denied\"\\\"Acess Denied.html\"" + "& exit");
                key.Close();
                // finder processen og dræber den
                Console.WriteLine("Kill: " + n);
                if (n == "spotify")
                {
                    Console.WriteLine("SPOTIFY FOUND");
                }
                Process[] running = Process.GetProcessesByName(n.ToLower());
                foreach (Process j in running)
                {
                    Console.WriteLine("Kill: " + j);
                    try
                    {
                        j.Kill();
                    }
                    catch (Exception c)
                    {
                        Console.WriteLine(c.Message);
                    }
                }
            }
        }
        // metode slut

        // Hvad der skal ske når programmet åbnes
        private void Main_Load(object sender, EventArgs e)
        {
            menu = menuStrip1;

            focusBut = Programs_but;

            ni = Tray_icon;

            form_main = ActiveForm;

            logout = false;

            key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock");

            if (key.GetValue("CurrentOption") != null)
            {
                string fileText = key.GetValue("CurrentOption").ToString();
                if (!string.IsNullOrWhiteSpace(fileText))
                {
                    if(fileText == "CALENDAR")
                    {
                        CalendarOption co = new CalendarOption();
                        co.Show(this);
                        co.Update(this);
                    }

                    if(fileText == "TIME")
                    {
                        TimeOption to = new TimeOption();
                        to.Show(this);
                    }

                    if(fileText == "EXTERNAL")
                    {
                        ExternalOption eo = new ExternalOption();
                        eo.Show(this);
                    }
                }
            }

            // finder ud af om programmet blev lukket imens det var tændt
            if (System.IO.File.Exists(@"C:\Windows\System32\drivers\etc\DateInfo.begeba"))
            {
                if (!String.IsNullOrWhiteSpace(System.IO.File.ReadAllText(@"C:\Windows\System32\drivers\etc\DateInfo.begeba")))
                {
                    string[] splitText = System.IO.File.ReadAllText(@"C:\Windows\System32\drivers\etc\DateInfo.begeba").Split(';');
                    
                }
            }

            // laver arrayen af programmer der skal lukkes
            CreateProgramArray(Program.checkedState, Program.processList);

            // omdanner arrayen til en string array
            string[] check = checkedPrograms.Where(x => x != null)
                       .Select(x => x.ToString())
                       .ToArray();
            
            // checker om en vigtig fil eksisterer
            if (!System.IO.File.Exists(@"C:\Windows\System32\drivers\etc\host.begeba"))
            {
                System.IO.File.Create(@"C:\Windows\System32\drivers\etc\host.begeba").Close();
            }
        }
        // metode slut

        // metode til at opdatere Host filen
        public static void UpdateHostFile(string updateText)
        {
            string path = @"C:\Windows\System32\drivers\etc\hosts";
            using (StreamWriter w = System.IO.File.AppendText(path))
            {
                if (!updateText.Contains("www."))
                {
                    w.WriteLine("127.0.0.1 " + "www." + updateText);
                }
                else
                {
                    w.WriteLine("127.0.0.1 " + updateText);
                }
            }
        }
        // metode slut

        // En metode til at lave et shorcut når programmet bliver tændt
        public static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        {
            RegistryKey key;

            key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");

            key.SetValue("FocusLock", Environment.CurrentDirectory + "\\" + "FocusLock.exe");

            //string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
            //WshShell shell = new WshShell();
            //IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            //shortcut.TargetPath = targetFileLocation;           // den fil genvejen skal have
            //shortcut.Save();                                    // gem genvejen
        }
        // metode slut

        // metode til at tjekke om der er nogle vigtige filer der er blevet omdøbt
        private void Host_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine(string.Format("Renamed: {0} {1}", e.OldName, e.ChangeType));
            if (e.OldName == "host.begeba")
            {
                System.IO.File.Move(e.FullPath, e.OldFullPath);
            }
            if (e.OldName == "hosts")
            {
                System.IO.File.Move(e.FullPath, e.OldFullPath);
            }
            if (e.OldName == "Info.begeba")
            {
                System.IO.File.Move(e.FullPath, e.OldFullPath);
            }
            if (e.OldName == "Programs.begeba")
            {
                System.IO.File.Move(e.FullPath, e.OldFullPath);
            }
        }
        // metode slut

        // metode til at tjekke om der er vigitge filer der bliver slettet
        private void Host_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(string.Format("Deleted: {0} {1}", e.Name, e.ChangeType));
        }
        // metode slut

        // metode til at tjekke om nogle vigtige filer er blevet ændret
        private void Host_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(string.Format("Changed: {0} {1}", e.Name, e.ChangeType));
        }
        // metode slut

        // metode til at tjekke om der er vigtige filer der bliver skabt
        private void Host_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(string.Format("Created: {0} {1}", e.Name, e.ChangeType));
        }
        // metode slut

        // metode der checker kalenderen
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

                    // navngir varibalen "day" så den får den rigtige information
                    switch (tmp[1])
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
                    if (DateTime.Now.Hour == clock && DateTime.Now.DayOfWeek.ToString().ToLower() == day.ToLower())
                    {
                        // starter programmet hvis det er
                        StartProgram(true);
                        break;
                    }
                    else
                    {
                        // stopper programmet hvis ikke
                        StartProgram(false);
                    }
                }
            }
        }
        // metode slut

        // metode der starter programmet og stopper det
        public static void StartProgram(bool isTime = false)
        {
            if (isTime)
            {
                Console.WriteLine("Program has been started");
                onceBut = new bool();
                if (!onceBut)
                {
                    if (Process.GetProcessesByName("chrome") != null || Process.GetProcessesByName("chrome").Length != 0)
                    {
                        chrome = new Process[Process.GetProcessesByName("chrome").Length];
                        chrome = Process.GetProcessesByName("chrome");
                        foreach (Process p in chrome)
                        {
                            p.Kill();
                        }
                    }
                    if (GetCurrentForm().Controls["_time_Start_but"] != null)
                    {
                        Button but = (Button)GetCurrentForm().Controls["_time_Start_but"];

                        but.Image = Image.FromFile(Environment.CurrentDirectory + @"\Rescources\Lock.png");
                    }
                        onceBut = true;
                    StopProcesses(checkedPrograms);

                    GetCurrentForm().MainMenuStrip.Visible = false;

                    butPressed = true;
                    CreateShortcut("FocusLock", Environment.GetFolderPath(Environment.SpecialFolder.Startup), AppDomain.CurrentDomain.BaseDirectory + "FocusLock.exe");

                    if (!KeyExists("HOST"))
                    {
                        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").SetValue("HOST", "");
                    }

                    foreach (string j in Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").GetValue("HOST").ToString().Split(';'))
                    {
                        if (!String.IsNullOrWhiteSpace(j))
                        {
                            UpdateHostFile(j);
                        }
                    }

                    GetCurrentForm().Controls["Programs_but"].Visible = false;
                    GetCurrentForm().Controls["Websites_but"].Visible = false;

                    Console.WriteLine("Program has been run");
                }
                //MessageBox.Show("Programmet kører nu", "running");
            }
            if (!isTime)
            {
                if (GetCurrentForm().Controls["_time_Start_but"] != null)
                {
                    Button but = (Button)GetCurrentForm().Controls["_time_Start_but"];

                    but.Image = Image.FromFile(Environment.CurrentDirectory + @"\Rescources\Lock_open.png");
                }

                GetCurrentForm().Controls["Programs_but"].Visible = true;
                GetCurrentForm().Controls["Websites_but"].Visible = true;

                GetCurrentForm().MainMenuStrip.Visible = true;

                butPressed = false;
                System.IO.File.Delete(@"C:\Windows\System32\drivers\etc\hosts");
                System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\FocusLock.lnk");
                //Registry.LocalMachine.DeleteSubKey(@"SOFTWARE\Begeba\FocusLock");
                Console.WriteLine("Program has been stopped");
                //MessageBox.Show("Programmet kører ikke længere", "Stopped");
                onceBut = false;
                DeleteRegistryData(checkedPrograms);
            }
        }
        // metode slut

        public static Form GetCurrentForm()
        {
            return ActiveForm;
        }

        public static void ChangeWindowSize()
        {
            focusBut.Focus();
        }

        // metode der sletter det data som er blevet lagt ind i registry
        public static void DeleteRegistryData(List<object> Processes)
        {
            //Microsoft.Win32.Registry.LocalMachine.DeleteSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\cmd.exe");
            Microsoft.Win32.Registry.LocalMachine.DeleteSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\taskmgr.exe");

            string[] proc = Processes.Where(x => x != null)
                       .Select(x => x.ToString())
                       .ToArray();
            Console.WriteLine(proc.Length);
            Console.WriteLine(checkedPrograms.Count);
            try
            {
                foreach (string n in proc)
                {
                    Console.WriteLine(n=="spotify");
                    if (Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\" + n + ".exe") != null)
                    {
                        Microsoft.Win32.Registry.LocalMachine.DeleteSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\" + n + ".exe");
                    }
                }
            }
            catch
            {
            }

            if (KeyExists("CurrentOption"))
            {
                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").DeleteValue("CurrentOption");
            }

            if (KeyExists("TimeInfo"))
            {
                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock").DeleteValue("TimeInfo");
            }

            RegistryKey key;

            key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");

            key.DeleteValue("FocusLock");
        }

        public static bool KeyExists(string value)
        {
            RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Begeba\FocusLock");

            try
            {
                if (key.GetValue(value) == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // metode der fortæller hvad der sker når der bliver trykket på program knappen
        private void Programs_but_Click(object sender, EventArgs e)
        {
            var Programs_form = new Programs();
            Programs_form.Show();
        }

        private void Websites_but_Click(object sender, EventArgs e)
        {
            var Websites_form = new Websites();
            Websites_form.Show();
        }

        private void åbenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!onceBut)
            {
                to.Show(this);
                co.Hide(this);
                eo.Hide(this);

                co.updating = false;

                calendarOpen = false;
            }
        }

        private void åbenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!onceBut)
            {
                if (!calendarOpen)
                {
                    to.Hide(this);
                    eo.Hide(this);
                    co.Show(this);
                    co.Update(this);
                    calendarOpen = true;
                    co.updating = true;
                }
            }
        }

        private void opdaterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!onceBut)
            {
                to.Hide(this);
                co.Hide(this);
                eo.Show(this);

                co.updating = false;

                calendarOpen = false;
            }
        }

        private void startI1TimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!onceBut)
            {
                to.Hide(this);

                key.SetValue("TimeInfo", "0:20");

                to.Show(this);
                co.Hide(this);
                eo.Hide(this);

                co.updating = false;

                calendarOpen = false;
            }
        }

        private void startI2TimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!onceBut)
            {
                to.Hide(this);

                key.SetValue("TimeInfo", "0:10");

                to.Show(this);
                co.Hide(this);
                eo.Hide(this);

                co.updating = false;

                calendarOpen = false;
            }
        }

        private void åbenPlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!calendarOpen)
            {
                to.Hide(this);
                co.Show(this);
                eo.Hide(this);
                co.Update(this);
                calendarOpen = true;
                co.updating = true;
            }

            co.OpenCalendar();
        }

        private void opdaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!calendarOpen)
            {
                to.Hide(this);
                co.Show(this);
                eo.Hide(this);
                co.Update(this);
                calendarOpen = true;
                co.updating = true;
            }

            co.Update(this);
        }

        private void opdaterToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            eo.Update();
        }

        private void logUdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!onceBut)
            {
                key.SetValue("LoginOffline", "FALSE");
                key.SetValue("Creds", "");
                logout = true;
                var login = new LoginForm();
                login.Show();
                this.Close();
            }
        }

        private void Tray_icon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }
        // metode slut
    }
}
