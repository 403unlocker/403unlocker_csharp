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
        private static bool isAuto = true;
        private static string selectedNetwork = "";

        public static bool IsAutoSelection
        {
            get => isAuto;
            set => isAuto = value;
        }

        public static string SelectedNetworkInterface
        {
            get
            {
                if (IsAutoSelection)
                {
                    var networks = NetworkSettingsManager.GetNetworkInterfaceName(true);
                    return networks.First().Name;
                }

                return selectedNetwork;
            }
            set
            {
                selectedNetwork = value;
            }
        }

    }
}
