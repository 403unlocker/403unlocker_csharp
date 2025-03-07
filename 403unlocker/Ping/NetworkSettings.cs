using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _403unlocker.Ping
{
    public class NetworkSettings
    {
        static string path = "cmd.log";
        public static string[] GetNetworkInterfaceName(bool selectActiveNetworkInterface)
        {
            // All Network Adaptors
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            // Shows only DNS allowed adaptors
            var netwrokFiltered = networkInterfaces.Where(x => x.GetIPProperties().GetIPv4Properties().IsDhcpEnabled);

            // shows Lan, Wi-Fi, VPN adaptors
            netwrokFiltered = netwrokFiltered.Where(x => x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                                                         x.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                                                         x.NetworkInterfaceType == (NetworkInterfaceType)53
                                                    );

            // shows usable ones
            netwrokFiltered = netwrokFiltered.Where(x => x.Speed > 0);

            // shows active one
            if (selectActiveNetworkInterface)
            {
                netwrokFiltered = netwrokFiltered.Where(a => a.GetIPProperties().GatewayAddresses.Any(g => g.Address.AddressFamily.ToString() == "InterNetwork"));
            }

            // just names
            return netwrokFiltered.Select(netwrok => netwrok.Name).ToArray();
        }

        internal class DnsSetting
        {
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

                    List<CommandConfig> notificationStates = new List<CommandConfig>()
                {
                    new CommandConfig()
                    {
                        Command = "command",
                        Message = command
                    },
                    new CommandConfig()
                    {
                        Command = "output",
                        Message = output
                    },
                    new CommandConfig()
                    {
                        Command = "error",
                        Message = error
                    }
                };

                    await WriteJson(notificationStates, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }


            public static void SetDnsAsPrimary(string adaptorName, string PrimaryDNS)
            {
                Run($"netsh interface ip add dns name=\"{adaptorName}\" {PrimaryDNS} index=1");
            }

            public static void SetDnsAsSecondary(string adaptorName, string SecondaryDns)
            {
                Run($"netsh interface ip add dns name=\"{adaptorName}\" {SecondaryDns} index=2");
            }

            public static void Reset(string adaptorName)
            {
                Run($"netsh interface ip set dns name=\"{adaptorName}\" source=dhcp");
            }

            public static async Task WriteJson<T>(List<T> data, bool append)
            {
                string text = "";

                string serializedData = data.Count == 0 ? "" : JsonConvert.SerializeObject(data, Formatting.Indented);
                text = serializedData;
                //File.WriteAllText(path, serializedData);


                using (StreamWriter sw = new StreamWriter(path, append))
                {
                    await sw.WriteLineAsync(text);
                }
            }
        }
    }

    class CommandConfig
    {
        public string Command { get; set; }
        public string Message { get; set; }
    }
}
