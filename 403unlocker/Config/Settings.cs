using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using _403unlocker.Ping;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Documents;

namespace _403unlocker.Config
{
    public class SettingsAttributes
    {
        public bool IconTray { get; set; }
        public bool StartUpIsEnabled { get; set; }

        public int PingPacketCount { get; set; }
        public ushort PingPacketSize { get; set; }
        public int PingTimeOutInMiliSeconds { get; set; }

        public int ByPassDnsResolveTimeOutInMiliSeconds { get; set; }
        public int ByPassHttpRequestTimeOutInMiliSeconds { get; set; }

        public bool NetworkAdaptorAutoSelection { get; set; }
        public string NetworkAdaptorSelectedNetworkInterface { get; set; }
    }

    internal static class Settings
    {
        private static string path = $"{Application.ProductName}.config";
        public static bool iconTray = false;

        internal static class Ping
        {
            public static int PacketCount { get; set; } = 4;
            public static ushort PacketSize { get; set; } = 32;
            public static int TimeOutInMiliSeconds { get; set; } = 2000;
        }

        internal static class ByPass
        {
            public static int DnsResolveTimeOutInMiliSeconds { get; set; } = 5000;
            public static int HttpRequestTimeOutInMiliSeconds { get; set; } = 10000;
        }

        internal static class NetworkAdaptor
        {
            public static bool AutoSelection = true;
            private static string selectedNetworkInterface = "Ethernet";
            public static string SelectedNetworkInterface
            {
                get
                {
                    if (AutoSelection) return ActiveNetworkInterface;
                    return selectedNetworkInterface;
                }
                set
                { 
                    selectedNetworkInterface = value; 
                }
            }
            public static string[] AllNetworkInterfaces
            {
                get
                {
                    return NetworkUtility.Adaptor.GetNetworkInterfaceName().Select(netwrok => netwrok.Name).ToArray();
                }
            }
            public static string ActiveNetworkInterface
            {
                get
                {
                    var b = NetworkUtility.Adaptor.GetNetworkInterfaceName().Where(a => a.GetIPProperties().GatewayAddresses.Any(g => g.Address.AddressFamily.ToString() == "InterNetwork"));
                    return b.ElementAt(0).Name;
                }
            }
        }

        public static bool Read()
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"File dosen't exist");

            var serializer = new XmlSerializer(typeof(SettingsAttributes));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                SettingsAttributes a = (SettingsAttributes)serializer.Deserialize(stream);

                Settings.iconTray = a.IconTray;
                
                Settings.Ping.PacketCount = a.PingPacketCount;
                Settings.Ping.PacketSize = a.PingPacketSize;
                Settings.Ping.TimeOutInMiliSeconds = a.PingTimeOutInMiliSeconds;

                Settings.ByPass.DnsResolveTimeOutInMiliSeconds = a.ByPassDnsResolveTimeOutInMiliSeconds;
                Settings.ByPass.HttpRequestTimeOutInMiliSeconds = a.ByPassHttpRequestTimeOutInMiliSeconds;

                Settings.NetworkAdaptor.AutoSelection = a.NetworkAdaptorAutoSelection;
                Settings.NetworkAdaptor.SelectedNetworkInterface = a.NetworkAdaptorSelectedNetworkInterface;
            }
            return true;
        }

        public static bool Write()
        {
            SettingsAttributes settings = new SettingsAttributes()
            {
                IconTray = Settings.iconTray,

                PingPacketCount = Settings.Ping.PacketCount,
                PingPacketSize = Settings.Ping.PacketSize,
                PingTimeOutInMiliSeconds = Settings.Ping.TimeOutInMiliSeconds,

                ByPassDnsResolveTimeOutInMiliSeconds = Settings.ByPass.DnsResolveTimeOutInMiliSeconds,
                ByPassHttpRequestTimeOutInMiliSeconds = Settings.ByPass.HttpRequestTimeOutInMiliSeconds,

                NetworkAdaptorAutoSelection = Settings.NetworkAdaptor.AutoSelection,
                NetworkAdaptorSelectedNetworkInterface = Settings.NetworkAdaptor.SelectedNetworkInterface
            };

            var serializer = new XmlSerializer(typeof(SettingsAttributes));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, settings);
            }
            return true;
        }
    }
}
