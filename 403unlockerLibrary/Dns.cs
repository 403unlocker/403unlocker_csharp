using _403unlocker.Library.NotificationMessage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _403unlockerLibrary
{
    public class Dns
    {
        private string name = "";
        private string dns = "";

        public string Name
        {
            get => name;
            set => name = value;
        }
        
        public string DNS
        {
            get => dns;
            set => dns = value;
        }

        public static bool IsIPv4(string dns)
        {
            var octets = dns.Split(new char[] { '.' });
            if (octets.Length == 4)
            {
                // converts octets string to int
                bool isOctetsValid = octets.Select(x => int.Parse(x))
                                     // checks are all between 0 to 255
                                     .All(x => 0 <= x && x <= 255); ;
                return isOctetsValid;
            }
            return false;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is Dns)
            {
                return dns == (obj as Dns).dns;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return dns.GetHashCode();
        }

        public async static Task<List<Dns>> Scrap()
        {
            //https://www.getflix.com.au/setup/dns-servers/
            try
            {
                var htmlDocument = await NetworkUtility.HttpRequest("https://publicdns.xyz");
                // get DNS table
                var table = htmlDocument.DocumentNode.SelectSingleNode("//table");

                // get rows of table
                var rows = table.SelectNodes(".//tr");

                // data preprocessing rows
                var customizedRows = rows.Select(row => row.ChildNodes.Where(cell => cell.Name != "#text"));

                // removes second row (IPv6)
                customizedRows = customizedRows.Where(x => x.Count() == 3);

                // removes table title
                customizedRows = customizedRows.Skip(1);

                // removes non-letter in cells e.g. \n \t
                var minedDns = customizedRows.Select(row =>
                                                     row.Select(
                                                         cell =>
                                                         string.Concat(
                                                                       cell.InnerText.Where(character => !char.IsControl(character))
                                                                       )
                                                               )
                                                     );

                // convert it to usable list for app
                var dnsList = minedDns.SelectMany(dns => (new Dns[]
                {
                    new Dns()
                    {
                        Name = dns.ElementAt(0),
                        // ensures IPv6 is removed
                        DNS = IsIPv4(dns.ElementAt(1)) ? dns.ElementAt(1) : ""
                    },
                    new _403unlockerLibrary.Dns()
                    {
                        Name = dns.ElementAt(0),
                        // ensures IPv6 is removed
                        DNS = IsIPv4(dns.ElementAt(2)) ? dns.ElementAt(2) : ""
                    }
                }))
                // removes empty DNS
                .Where((object dnsConfig) => !string.IsNullOrEmpty(dnsConfig.DNS)).ToList();

                return dnsList;
            }
            catch (HttpRequestException error)
            {
                string errorText = error.Message;
                string errorCaption = "Access Denied! - Server Blocked us";

                if (error.InnerException != null)
                {
                    errorText = error.InnerException.Message;
                    errorCaption = "Access Denied! - There is No Internet 🌐";
                }
                MessageBox.Show(errorText, errorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TaskCanceledException error)
            {
                MessageBox.Show(error.Message, "Request Timeout!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Something Went Wrong!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public static class NetworkSetting
        {
            static string path = "cmdLog.txt";
            public static string[] GetNames(bool selectActiveNetworkInterface)
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

                    await JsonHandler.WriteJson(path, notificationStates, true);
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
}
