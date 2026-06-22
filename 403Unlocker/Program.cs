using QR_Code_Generator;
using System;
using System.Threading;
using System.Windows.Forms;

namespace _403Unlocker
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(true, Application.ProductName, out bool firstInstance))
            {
                if (!firstInstance)
                {
                    MessageBox.Show("Another instance is already running!",
                                    "Application is running...",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop);
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new _403UnlockerForm());
            }
        }
    }
}
