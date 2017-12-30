using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace FocusLock_UI
{
    public partial class Main : Form
    {
        Timer time_but_timer = new Timer();
        Timer calendar_but_timer = new Timer();
        Timer external_but_timer = new Timer();

        float time_alpha;
        float calendar_alpha;
        float external_alpha;

        public Main()
        {
            InitializeComponent();

            FontAwesome.Sharp.IconBlock block = new IconBlock();
            block.Icon = IconChar.ClockO;

            // Event subscription of timers
            time_but_timer.Tick += Time_but_timer_Tick;
            calendar_but_timer.Tick += Calendar_but_timer_Tick;
            external_but_timer.Tick += External_but_timer_Tick;
        }

        /// <summary>
        /// Increments in the alpha value of "external button"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void External_but_timer_Tick(object sender, EventArgs e)
        {
            external_alpha += 17f;
            if (external_alpha >= 255) external_but_timer.Stop();
            if (Extern_but.BackColor.GetBrightness() < 0.3) Extern_but.ForeColor = Color.White;
            Extern_but.BackColor = Color.FromArgb((int)external_alpha, 252, 107, 10);
        }

        /// <summary>
        /// Increments in the alpha value of "calendar button"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calendar_but_timer_Tick(object sender, EventArgs e)
        {
            calendar_alpha += 17f;
            if (calendar_alpha >= 255) calendar_but_timer.Stop();
            if (Calendar_but.BackColor.GetBrightness() < 0.3) Calendar_but.ForeColor = Color.White;
            Calendar_but.BackColor = Color.FromArgb((int)calendar_alpha, 252, 107, 10);
        }

        /// <summary>
        /// Increments in the alpha value of "time button"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Time_but_timer_Tick(object sender, EventArgs e)
        {
            time_alpha += 17f;
            if (time_alpha >= 255) time_but_timer.Stop();
            if (Time_But.BackColor.GetBrightness() < 0.3) Time_But.ForeColor = Color.White;
            Time_But.BackColor = Color.FromArgb((int)time_alpha,252,107,10);
        }

        /// <summary>
        /// Paint a border around the interface
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Interface_Paint(object sender, PaintEventArgs e)
        {
                int thickness = 2;//it's up to you
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.Black, thickness))
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                              halfThickness,
                                                              Interface.ClientSize.Width - thickness,
                                                              Interface.ClientSize.Height - thickness));
                }
        }

        /// <summary>
        /// Checks when the mouse leaves the "time button" control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Time_But_MouseLeave(object sender, EventArgs e)
        {
            time_but_timer.Stop();
            Time_But.BackColor = Color.FromArgb(255,244,244,244);
            Time_But.ForeColor = Color.FromArgb(255,252,107,10);
        }

        /// <summary>
        /// Checks when the mouse enters the "time button" control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Time_But_MouseEnter(object sender, EventArgs e)
        {
            time_alpha = 0;
            time_but_timer.Interval = 15;
            Time_But.ForeColor = Color.Black;
            time_but_timer.Start();
        }

        /// <summary>
        /// Paint a border around the "time button"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Time_But_Paint(object sender, PaintEventArgs e)
        {
            int thickness = 2;//it's up to you
            int halfThickness = thickness / 2;
            using (Pen p = new Pen(Color.FromArgb(255,252,107,10), thickness))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                          halfThickness,
                                                          Time_But.ClientSize.Width - thickness,
                                                          Time_But.ClientSize.Height - thickness));
            }
        }

        /// <summary>
        /// Checks when the mouse enters the "calendar button" control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calendar_but_MouseEnter(object sender, EventArgs e)
        {
            calendar_alpha = 0;
            calendar_but_timer.Interval = 15;
            Calendar_but.ForeColor = Color.Black;
            calendar_but_timer.Start();
        }

        /// <summary>
        /// Paints a border around the "calendar button"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calendar_but_Paint(object sender, PaintEventArgs e)
        {
            int thickness = 2;//it's up to you
            int halfThickness = thickness / 2;
            using (Pen p = new Pen(Color.FromArgb(255, 252, 107, 10), thickness))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                          halfThickness,
                                                          Calendar_but.ClientSize.Width - thickness,
                                                          Calendar_but.ClientSize.Height - thickness));
            }
        }

        /// <summary>
        /// Checks when the mouse leaves the "calendar button" control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calendar_but_MouseLeave(object sender, EventArgs e)
        {
            calendar_but_timer.Stop();
            Calendar_but.BackColor = Color.FromArgb(255, 244, 244, 244);
            Calendar_but.ForeColor = Color.FromArgb(255, 252, 107, 10);
        }

        /// <summary>
        /// Checks when the mouse enters the "external button" control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Extern_but_MouseEnter(object sender, EventArgs e)
        {
            external_alpha = 0;
            external_but_timer.Interval = 15;
            Extern_but.ForeColor = Color.Black;
            external_but_timer.Start();
        }

        /// <summary>
        /// Checks when the mouse leaves the "external button" control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Extern_but_MouseLeave(object sender, EventArgs e)
        {
            external_but_timer.Stop();
            Extern_but.BackColor = Color.FromArgb(255, 244, 244, 244);
            Extern_but.ForeColor = Color.FromArgb(255, 252, 107, 10);
        }

        /// <summary>
        /// Paints a border around "extenal button"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Extern_but_Paint(object sender, PaintEventArgs e)
        {
            int thickness = 2;//it's up to you
            int halfThickness = thickness / 2;
            using (Pen p = new Pen(Color.FromArgb(255, 252, 107, 10), thickness))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                          halfThickness,
                                                          Extern_but.ClientSize.Width - thickness,
                                                          Extern_but.ClientSize.Height - thickness));
            }
        }

        /// <summary>
        /// Checks when the "time button" has been clicked by the mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Time_But_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Checks when the "calendar button" has been clicked by the mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calendar_but_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Checks when the "external button" has been clicked by the mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Extern_but_Click(object sender, EventArgs e)
        {

        }
    }
}
