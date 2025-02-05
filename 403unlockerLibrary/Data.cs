using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _403unlockerLibrary
{
    public class Data
    {
        public static List<Website> DefaultUrlList()
        {
            List<Website> urlDefault = new List<Website>
            {
                new Website{Name = "Nvidia", URL = "nvidia.com" },
                new Website{Name = "ASUS", URL = "asus.com" },
                new Website{Name = "Lenovo", URL = "lenovo.com" },
                new Website{Name = "YouTube", URL = "youtube.com" },
                new Website{Name = "ChatGPT", URL = "chatgpt.com" },
                new Website{Name = "X", URL = "x.com" },
                new Website{Name = "Google.Dev", URL = "developers.google.com" },
                new Website{Name = "Go", URL = "pkg.go.dev" },
                new Website{Name = "Lucid Chart", URL = "lucid.app/users/login#/login?" }
            };
            return urlDefault;
        }

        public static List<DnsProvider> DefaultDnsList()
        {
            List<DnsProvider> list = new List<DnsProvider>()
            {
                new DnsProvider{ Name = "shecan.ir", DNS = "178.22.122.100" },
                new DnsProvider{ Name = "shecan.ir", DNS = "185.51.200.2" },
                new DnsProvider{ Name = "server.ir/dns-proxy", DNS = "192.104.158.78" },
                new DnsProvider{ Name = "server.ir/dns-proxy", DNS = "194.104.158.48" },
                new DnsProvider{ Name = "hostiran.net/landing/proxy", DNS = "172.29.0.100" },
                new DnsProvider{ Name = "hostiran.net/landing/proxy", DNS = "172.29.2.100" },
                new DnsProvider{ Name = "electrotm.org", DNS = "78.157.42.101" },
                new DnsProvider{ Name = "electrotm.org", DNS = "78.157.42.100" },
                new DnsProvider{ Name = "403.online/download", DNS = "10.202.10.202" },
                new DnsProvider{ Name = "403.online/download", DNS = "10.202.10.102" },
                new DnsProvider{ Name = "begzar.ir", DNS = "185.55.226.26" },
                new DnsProvider{ Name = "begzar.ir", DNS = "185.55.225.25" },
                new DnsProvider{ Name = "radar.game/#/dns", DNS = "10.202.10.10" },
                new DnsProvider{ Name = "radar.game/#/dns", DNS = "10.202.10.11" },
                new DnsProvider{ Name = "dnspro.ir", DNS = "87.107.110.109" },
                new DnsProvider{ Name = "dnspro.ir", DNS = "87.107.110.110" },
                new DnsProvider{ Name = "LinkedIn Suggested", DNS = "87.107.52.11" },
                new DnsProvider{ Name = "LinkedIn Suggested", DNS = "87.107.52.13" },
                new DnsProvider{ Name = "pishgaman", DNS = "5.202.100.101" },
                new DnsProvider{ Name = "darzg.ir", DNS = "37.27.41.228" },
                new DnsProvider{ Name = "sheltertm.com", DNS = "94.103.125.157" },
                new DnsProvider{ Name = "sheltertm.com", DNS = "94.103.125.158" },
                new DnsProvider{ Name = "shatel.ir (rasana)", DNS = "85.15.1.15" },
                new DnsProvider{ Name = "shatel.ir (rasana)", DNS = "85.15.1.14" },
                new DnsProvider{ Name = "cleanbrowsing.org/filters", DNS = "185.228.168.168" },
                new DnsProvider{ Name = "cleanbrowsing.org/filters", DNS = "185.228.169.168" },
                new DnsProvider{ Name = "alternate-dns.com", DNS = "76.76.19.19" },
                new DnsProvider{ Name = "alternate-dns.com", DNS = "76.223.122.150" },
                new DnsProvider{ Name = "Unlocator", DNS = "185.37.37.37" },
                new DnsProvider{ Name = "Unlocator", DNS = "185.37.39.39" },
                new DnsProvider{ Name = "Yandex.DNS (Safe)", DNS = "77.88.8.88" },
                new DnsProvider{ Name = "Yandex.DNS (Safe)", DNS = "77.88.8.2" },
                new DnsProvider{ Name = "Yandex.DNS (Family)", DNS = "77.88.8.7" },
                new DnsProvider{ Name = "Yandex.DNS (Family)", DNS = "77.88.8.3" },
                new DnsProvider{ Name = "namecheap.com (SafeServe)", DNS = "198.54.117.10" },
                new DnsProvider{ Name = "namecheap.com (SafeServe)", DNS = "198.54.117.11" },
                new DnsProvider{ Name = "controld.com/free-dns (Unfiltered)", DNS = "76.76.2.0" },
                new DnsProvider{ Name = "controld.com/free-dns (Unfiltered)", DNS = "76.76.10.0" },
                new DnsProvider{ Name = "controld.com/free-dns (Malware)", DNS = "76.76.2.1" },
                new DnsProvider{ Name = "controld.com/free-dns (Malware)", DNS = "76.76.10.1" },
                new DnsProvider{ Name = "controld.com/free-dns (Ads & Tracking)", DNS = "76.76.2.2" },
                new DnsProvider{ Name = "controld.com/free-dns (Ads & Tracking)", DNS = "76.76.10.2" },
                new DnsProvider{ Name = "controld.com/free-dns (Social)", DNS = "76.76.2.3" },
                new DnsProvider{ Name = "controld.com/free-dns (Social)", DNS = "76.76.10.3" },
                new DnsProvider{ Name = "controld.com/free-dns (Family Friendly)", DNS = "76.76.2.4" },
                new DnsProvider{ Name = "controld.com/free-dns (Family Friendly)", DNS = "76.76.10.4" },
                new DnsProvider{ Name = "controld.com/free-dns (Uncensored)", DNS = "76.76.2.5" },
                new DnsProvider{ Name = "controld.com/free-dns (Uncensored)", DNS = "76.76.10.5" },
                new DnsProvider{ Name = "Private Internet Access (DNS)", DNS = "10.0.0.242" },
                new DnsProvider{ Name = "Private Internet Access (DNS+Stream)", DNS = "10.0.0.243" },
                new DnsProvider{ Name = "Private Internet Access (DNS+Mace)", DNS = "10.0.0.244" },
                new DnsProvider{ Name = "Private Internet Access (DNS+Strem+Mace)", DNS = "10.0.0.241" },
                new DnsProvider{ Name = "NordVPN", DNS = "103.86.96.100" },
                new DnsProvider{ Name = "NordVPN", DNS = "103.86.99.100" }
                // new DnsProvider{ Name = "", DNS = "" },

            };
            //server.ir/dns-proxy
            //vanillapp.ir
            //www.smartdnsproxy.com

            return list;
        }
    }
}
