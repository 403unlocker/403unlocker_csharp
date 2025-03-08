using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using _403unlocker.Ping;

namespace _403unlocker.Config
{
    internal static class Settings
    {
        internal static class Ping
        {
            public static int PacketCount { get; set; } = 4;
            public static ushort PacketSize { get; set; } = 32;
            public static int TimeOutInMiliSeconds { get; set; } = 2000;
        }

        internal static class ByPass
        {
            public static int DnsResolveTimeOutInMiliSeconds { get; set; } = 2000;
            public static int HttpRequestTimeOutInMiliSeconds { get; set; } = 2000;
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
    }
}
