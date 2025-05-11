using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using _403unlocker.Ping;
using _403unlocker.Notification;
using System.Drawing;
using System.Runtime.InteropServices;

namespace _403unlocker
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(true, "403unlocker", out bool firstInstance))
            {
                if (firstInstance)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new DnsBenchmarkForm());
                }
                else
                {
                    MessageBoxForm f = new MessageBoxForm(
                        "Another instance is already running!",
                        "Application is running...",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                    f.StartPosition = FormStartPosition.CenterScreen;
                    f.ShowDialog();
                }
            }
        }
    }
}
