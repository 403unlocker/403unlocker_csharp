using Microsoft.Win32;
using Network_Utilities.Ping;
using Network_Utilities.DNS_Testing.Resolver;
using Network_Utilities.Http_Service;
using Registry_Manager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _403Unlocker.Configuration
{
    internal static class Settings
    {
        private static string path = RegistryPath.GetSettingsRegistryPath(Application.ProductName, "Settings");

        public static int PacketCount
        {
            get => PingSettings.PacketCount;
            set => PingSettings.PacketCount = value;
        }
        public static ushort PacketSize
        {
            get => PingSettings.PacketSize;
            set => PingSettings.PacketSize = value;
        }
        public static int PacketTimeoutInMiliSeconds
        {
            get => PingSettings.TimeoutInMiliSeconds;
            set => PingSettings.TimeoutInMiliSeconds = value;
        }
        public static double DnsResolveTimeoutInMilliSeconds
        {
            get => ResolverSettings.TimeoutInMilliSeconds;
            set => ResolverSettings.TimeoutInMilliSeconds = value;
        }
        public static double HttpRequestTimeoutInMiliSeconds
        {
            get => HttpSettings.TimeoutInMilliseconds;
            set => HttpSettings.TimeoutInMilliseconds = value;
        }

        public static bool NetworkAdapterAutoSelection { get; set; } = true;

        public static void Load()
        {
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(path, writable: false))
            {
                if (registryKey != null)
                {
                    PacketCount = (int)registryKey.GetValue("PacketCount");
                    PacketSize = Convert.ToUInt16(registryKey.GetValue("PacketSize"));
                    PacketTimeoutInMiliSeconds = (int)registryKey.GetValue("PacketTimeoutInMiliSeconds");

                    DnsResolveTimeoutInMilliSeconds = Convert.ToUInt16(registryKey.GetValue("DnsResolveTimeoutInMilliSeconds"));

                    HttpRequestTimeoutInMiliSeconds = Convert.ToUInt16(registryKey.GetValue("HttpRequestTimeoutInMiliSeconds"));
                    
                    NetworkAdapterAutoSelection = Convert.ToBoolean
                        (
                              (
                                    (byte[])registryKey.GetValue("NetworkAdapterAutoSelection")
                              ).First()
                        );
                }
            }
        }

        public static void Save()
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(path))
            {
                key.SetValue("PacketCount", PacketCount, RegistryValueKind.DWord);
                key.SetValue("PacketSize", PacketSize, RegistryValueKind.DWord);
                key.SetValue("PacketTimeoutInMiliSeconds", PacketTimeoutInMiliSeconds, RegistryValueKind.DWord);

                key.SetValue("DnsResolveTimeoutInMilliSeconds", DnsResolveTimeoutInMilliSeconds, RegistryValueKind.QWord);

                key.SetValue("HttpRequestTimeoutInMiliSeconds", HttpRequestTimeoutInMiliSeconds, RegistryValueKind.QWord);

                key.SetValue("NetworkAdapterAutoSelection", new byte[] { Convert.ToByte(NetworkAdapterAutoSelection) }, RegistryValueKind.Binary);
            }
        }
    }
}
