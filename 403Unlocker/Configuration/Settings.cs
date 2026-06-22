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
using Network_Utilities.DNS_Testing.ByPass;

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
        public static int BypassTcpConnectTimeoutInMilliSeconds
        {
            get => BypassSettings.TcpConnectTimeoutInMilliSeconds;
            set => BypassSettings.TcpConnectTimeoutInMilliSeconds = value;
        }
        public static int BypassTlsHandshakeTimeoutInMilliSeconds
        {
            get => BypassSettings.TlsHandshakeTimeoutInMilliSeconds;
            set => BypassSettings.TlsHandshakeTimeoutInMilliSeconds = value;
        }
        public static double ScraperHttpRequestTimeoutInMiliSeconds
        {
            get => HttpSettings.TimeoutInMilliseconds;
            set => HttpSettings.TimeoutInMilliseconds = value;
        }

        public static bool NetworkAdapterAutoSelection { get; set; } = true;

        public static int MaxParallelRequests { get; set; } = 5;

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

                    BypassTcpConnectTimeoutInMilliSeconds = Convert.ToInt32(registryKey.GetValue("BypassTcpConnectTimeoutInMilliSeconds"));
                    BypassTlsHandshakeTimeoutInMilliSeconds = Convert.ToInt32(registryKey.GetValue("BypassTlsHandshakeTimeoutInMilliSeconds"));

                    ScraperHttpRequestTimeoutInMiliSeconds = Convert.ToUInt16(registryKey.GetValue("ScraperHttpRequestTimeoutInMiliSeconds"));
                    
                    NetworkAdapterAutoSelection = Convert.ToBoolean
                        (
                              (
                                    (byte[])registryKey.GetValue("NetworkAdapterAutoSelection")
                              )
                              .First()
                        );

                    MaxParallelRequests = Convert.ToInt32(registryKey.GetValue("MaxParallelRequests"));
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

                key.SetValue("BypassTcpConnectTimeoutInMilliSeconds", BypassTcpConnectTimeoutInMilliSeconds, RegistryValueKind.DWord);
                key.SetValue("BypassTlsHandshakeTimeoutInMilliSeconds", BypassTlsHandshakeTimeoutInMilliSeconds, RegistryValueKind.DWord);

                key.SetValue("ScraperHttpRequestTimeoutInMiliSeconds", ScraperHttpRequestTimeoutInMiliSeconds, RegistryValueKind.QWord);

                key.SetValue("NetworkAdapterAutoSelection", new byte[] { Convert.ToByte(NetworkAdapterAutoSelection) }, RegistryValueKind.Binary);

                key.SetValue("MaxParallelRequests", MaxParallelRequests, RegistryValueKind.DWord);
            }
        }
    }
}
