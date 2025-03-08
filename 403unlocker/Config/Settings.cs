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
            public static int EchoRequestCount { get; set; } = 4;
            public static int TimeOutInMiliSeconds { get; set; } = 2000;
            public static ushort BufferSize { get; set; } = 32;
        }

        private static bool networkInterfaceAutoSelection = true;
        private static string selectedNetworkInterface = "Auto";

        public static bool NetworkInterfaceAutoSelection
        {
            get => networkInterfaceAutoSelection;
            set => networkInterfaceAutoSelection = value;
        }

        public static string SelectedNetworkInterface
        {
            get
            {
                if (networkInterfaceAutoSelection)
                {
                    var networks = NetworkSettings.GetNetworkInterfaceName(true);
                    return networks[0];
                }

                return selectedNetworkInterface;
            }
            set
            {
                selectedNetworkInterface = value;
            }
        }
    }
}
