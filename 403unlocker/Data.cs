using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _403unlocker.Add;
using _403unlocker.ByPass_Url;

namespace _403unlocker
{
    internal static class Data
    {
        internal static class DnsScraper
        {
            public static Exception Errors { get; set; } = new Exception("");
            public static List<DnsConfig> Values { get; set; }
            public static async Task Get()
            {
                //https://www.getflix.com.au/setup/dns-servers/
                try
                {
                    Uri uri = new Uri("https://www.publicdns.xyz");
                    var htmlDocument = await NetworkUtility.HttpResponseHtml(uri);
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
                    var minedDns = customizedRows.Select(row => row.Select(
                                                             cell => string.Concat(
                                                                           cell.InnerText.Where(
                                                                               character => !char.IsControl(character)
                                                                               ))));

                    // convert it to usable list for app
                    var dnsList = minedDns.SelectMany(dnsConfig => new DnsConfig[]
                    {
                    new DnsConfig()
                    {
                        Name = dnsConfig.ElementAt(0),
                        // ensures IPv6 is removed
                        DNS = DnsConfig.IsIPv4(dnsConfig.ElementAt(1)) ? dnsConfig.ElementAt(1) : ""
                    },
                    new DnsConfig()
                    {
                        Name = dnsConfig.ElementAt(0),
                        // ensures IPv6 is removed
                        DNS = DnsConfig.IsIPv4(dnsConfig.ElementAt(2)) ? dnsConfig.ElementAt(2) : ""
                    }
                    })
                    // removes empty DNS
                    .Where(dnsConfig => !string.IsNullOrEmpty(dnsConfig.DNS)).ToList();

                   Values = dnsList;
                }
                catch (Exception error)
                {
                    Errors = new Exception(error.GetMessages());
                }
            }

            public static List<DnsConfig> DefaultList()
            {
                List<DnsConfig> list = new List<DnsConfig>()
                {
                new DnsConfig{ Name = "shecan.ir", DNS = "178.22.122.100" },
                new DnsConfig{ Name = "shecan.ir", DNS = "185.51.200.2" },
                new DnsConfig{ Name = "server.ir/dns-proxy", DNS = "192.104.158.78" },
                new DnsConfig{ Name = "server.ir/dns-proxy", DNS = "194.104.158.48" },
                new DnsConfig{ Name = "hostiran.net/landing/proxy", DNS = "172.29.0.100" },
                new DnsConfig{ Name = "hostiran.net/landing/proxy", DNS = "172.29.2.100" },
                new DnsConfig{ Name = "electrotm.org", DNS = "78.157.42.101" },
                new DnsConfig{ Name = "electrotm.org", DNS = "78.157.42.100" },
                new DnsConfig{ Name = "403.online/download", DNS = "10.202.10.202" },
                new DnsConfig{ Name = "403.online/download", DNS = "10.202.10.102" },
                new DnsConfig{ Name = "begzar.ir", DNS = "185.55.226.26" },
                new DnsConfig{ Name = "begzar.ir", DNS = "185.55.225.25" },
                new DnsConfig{ Name = "radar.game/#/dns", DNS = "10.202.10.10" },
                new DnsConfig{ Name = "radar.game/#/dns", DNS = "10.202.10.11" },
                new DnsConfig{ Name = "dnspro.ir", DNS = "87.107.110.109" },
                new DnsConfig{ Name = "dnspro.ir", DNS = "87.107.110.110" },
                new DnsConfig{ Name = "LinkedIn Suggested", DNS = "87.107.52.11" },
                new DnsConfig{ Name = "LinkedIn Suggested", DNS = "87.107.52.13" },
                new DnsConfig{ Name = "pishgaman", DNS = "5.202.100.101" },
                // new DnsConfig{ Name = "darzg.ir", DNS = "37.27.41.228" },
                new DnsConfig{ Name = "sheltertm.com", DNS = "94.103.125.157" },
                new DnsConfig{ Name = "sheltertm.com", DNS = "94.103.125.158" },
                new DnsConfig{ Name = "shatel.ir (rasana)", DNS = "85.15.1.15" },
                new DnsConfig{ Name = "shatel.ir (rasana)", DNS = "85.15.1.14" },
                new DnsConfig{ Name = "cleanbrowsing.org/filters", DNS = "185.228.168.168" },
                new DnsConfig{ Name = "cleanbrowsing.org/filters", DNS = "185.228.169.168" },
                new DnsConfig{ Name = "alternate-dns.com", DNS = "76.76.19.19" },
                new DnsConfig{ Name = "alternate-dns.com", DNS = "76.223.122.150" },
                new DnsConfig{ Name = "Unlocator", DNS = "185.37.37.37" },
                new DnsConfig{ Name = "Unlocator", DNS = "185.37.39.39" },
                new DnsConfig{ Name = "Yandex.DNS (Safe)", DNS = "77.88.8.88" },
                new DnsConfig{ Name = "Yandex.DNS (Safe)", DNS = "77.88.8.2" },
                new DnsConfig{ Name = "Yandex.DNS (Family)", DNS = "77.88.8.7" },
                new DnsConfig{ Name = "Yandex.DNS (Family)", DNS = "77.88.8.3" },
                new DnsConfig{ Name = "namecheap.com (SafeServe)", DNS = "198.54.117.10" },
                new DnsConfig{ Name = "namecheap.com (SafeServe)", DNS = "198.54.117.11" },
                new DnsConfig{ Name = "controld.com/free-dns (Unfiltered)", DNS = "76.76.2.0" },
                new DnsConfig{ Name = "controld.com/free-dns (Unfiltered)", DNS = "76.76.10.0" },
                new DnsConfig{ Name = "controld.com/free-dns (Malware)", DNS = "76.76.2.1" },
                new DnsConfig{ Name = "controld.com/free-dns (Malware)", DNS = "76.76.10.1" },
                new DnsConfig{ Name = "controld.com/free-dns (Ads & Tracking)", DNS = "76.76.2.2" },
                new DnsConfig{ Name = "controld.com/free-dns (Ads & Tracking)", DNS = "76.76.10.2" },
                new DnsConfig{ Name = "controld.com/free-dns (Social)", DNS = "76.76.2.3" },
                new DnsConfig{ Name = "controld.com/free-dns (Social)", DNS = "76.76.10.3" },
                new DnsConfig{ Name = "controld.com/free-dns (Family Friendly)", DNS = "76.76.2.4" },
                new DnsConfig{ Name = "controld.com/free-dns (Family Friendly)", DNS = "76.76.10.4" },
                new DnsConfig{ Name = "controld.com/free-dns (Uncensored)", DNS = "76.76.2.5" },
                new DnsConfig{ Name = "controld.com/free-dns (Uncensored)", DNS = "76.76.10.5" },
                new DnsConfig{ Name = "Private Internet Access (DNS)", DNS = "10.0.0.242" },
                new DnsConfig{ Name = "Private Internet Access (DNS+Stream)", DNS = "10.0.0.243" },
                new DnsConfig{ Name = "Private Internet Access (DNS+Mace)", DNS = "10.0.0.244" },
                new DnsConfig{ Name = "Private Internet Access (DNS+Strem+Mace)", DNS = "10.0.0.241" },
                new DnsConfig{ Name = "NordVPN", DNS = "103.86.96.100" },
                new DnsConfig{ Name = "NordVPN", DNS = "103.86.99.100" }
                // new DnsProvider{ Name = "", DNS = "" },

                };
                //server.ir/dns-proxy
                //vanillapp.ir
                //www.smartdnsproxy.com

                return list;
            }
        }

        internal static class Url
        {
            public static List<UrlConfig> DefaultList()
            {
                List<UrlConfig> urlDefault = new List<UrlConfig>
                {
                new UrlConfig{Name = "Nvidia", HostName = "nvidia.com" },
                new UrlConfig{Name = "ASUS", HostName = "asus.com" },
                new UrlConfig{Name = "Lenovo", HostName = "lenovo.com" },
                new UrlConfig{Name = "YouTube", HostName = "youtube.com" },
                new UrlConfig{Name = "ChatGPT", HostName = "chatgpt.com" },
                new UrlConfig{Name = "X", HostName = "x.com" },
                new UrlConfig{Name = "Google.Dev", HostName = "developers.google.com" },
                new UrlConfig{Name = "Go", HostName = "pkg.go.dev" },
                new UrlConfig{Name = "Lucid Chart", HostName = "lucid.app" }
                };
                return urlDefault;
            }
        }
    }
}
