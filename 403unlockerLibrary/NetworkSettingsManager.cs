using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _403unlocker.Library.NotificationMessage;

namespace _403unlockerLibrary
{
    public class NetworkSettingsManager
    {
        static string path = "cmdLog";
        public static NetworkInterface[] GetNetworkInterfaceName(bool selectActiveNetworkInterface)
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            var netwrokFiltered = networkInterfaces.Where(x => x.GetIPProperties().GetIPv4Properties().IsDhcpEnabled);

            netwrokFiltered = netwrokFiltered.Where(x => x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                                                         x.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                                                         x.NetworkInterfaceType == (NetworkInterfaceType)53
                                                    );

            netwrokFiltered = netwrokFiltered.Where(x => x.Speed > 0);

            if (selectActiveNetworkInterface)
            {
                netwrokFiltered = netwrokFiltered.Where(a => a.GetIPProperties().GatewayAddresses.Any(g => g.Address.AddressFamily.ToString() == "InterNetwork"));
            }

            return netwrokFiltered.ToArray();
        }

        private async static void Run(string command)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Verb = "runas",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = $"/c {command}",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                };

                string output, error;
                using (Process process = Process.Start(psi))
                {
                    using (StreamWriter sw = process.StandardInput)
                    {
                        if (sw.BaseStream.CanWrite)
                        {
                            sw.WriteLine(command);
                        }
                    }

                    output = await process.StandardOutput.ReadToEndAsync();
                    error = await process.StandardError.ReadToEndAsync();
                }

                List<NotificationState> notificationStates = new List<NotificationState>()
                {
                    new NotificationState("command", command),
                    new NotificationState("output", output),
                    new NotificationState("error", error)
                };

                await JsonHandler.WriteJson(path, notificationStates, true, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }


        public static void SetAsPrimary(string adaptorName, string PrimaryDNS)
        {
            Run($"netsh interface ip add dns name=\"{adaptorName}\" {PrimaryDNS} index=1");
        }

        public static void SetAsSecondary(string adaptorName, string SecondaryDns)
        {
            Run($"netsh interface ip add dns name=\"{adaptorName}\" {SecondaryDns} index=2");
        }

        public static void Reset(string adaptorName)
        {
            Run($"netsh interface ip set dns name=\"{adaptorName}\" source=dhcp");
        }
    }
}
