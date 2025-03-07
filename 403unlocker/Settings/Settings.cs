using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using _403unlocker.Ping;

namespace _403unlocker.Settings
{
    internal static class Settings
    {
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
