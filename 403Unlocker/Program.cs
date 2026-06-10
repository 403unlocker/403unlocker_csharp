using QR_Code_Generator;
using System;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new _403UnlockerForm());
        }
    }
}
