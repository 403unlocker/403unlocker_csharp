using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using _403unlockerLibrary;
using System.Windows.Forms;

namespace _403unlocker
{
    internal static class Setting
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
                    var networks = NetworkSettingsManager.GetNetworkInterfaceName(true);
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
