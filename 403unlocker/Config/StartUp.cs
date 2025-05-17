using Microsoft.Win32;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _403unlocker.Config
{
    internal static class StartUp
    {

        private static string windowsRegisteryPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private static string appName = Application.ProductName;

        public static bool isEnabled
        {
            get
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(windowsRegisteryPath, false);
                return key?.GetValue(appName) != null;
            }
            set
            {
                string exePath = Application.ExecutablePath;
                RegistryKey key = Registry.CurrentUser.OpenSubKey(windowsRegisteryPath, true);

                if (value)
                {
                    key.SetValue(appName, exePath);
                }
                else
                {
                    key.DeleteValue(appName, false);
                }
            }
        }
    }
}
