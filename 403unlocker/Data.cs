using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace _403unlocker
{
    internal class Data
    {
        public static List<DnsConfig> DefaultDnsList
        {
            get
            {
                List<DnsConfig> list = new List<DnsConfig>
                {
                    new DnsConfig{
                        Provider = "shecan.ir",
                        DNS = "178.22.122.100",
                    },
                    new DnsConfig{
                        Provider = "shecan.ir",
                        DNS = "185.51.200.2"
                    },
                    new DnsConfig{
                        Provider = "server.ir/dns-proxy",
                        DNS = "192.104.158.78",
                    },
                    new DnsConfig{
                        Provider = "server.ir/dns-proxy",
                        DNS =  "194.104.158.48"
                    },
                    new DnsConfig{
                        Provider = "hostiran.net/landing/proxy",
                        DNS = "172.29.0.100",
                    },
                    new DnsConfig{
                        Provider = "hostiran.net/landing/proxy",
                        DNS = "172.29.2.100"
                    },
                    new DnsConfig{
                        Provider = "electrotm.org",
                        DNS = "78.157.42.101",
                    },
                    new DnsConfig{
                        Provider = "electrotm.org",
                        DNS = "78.157.42.100"
                    },
                    new DnsConfig{
                        Provider = "403.online/download",
                        DNS = "10.202.10.202",
                    },
                    new DnsConfig{
                        Provider = "403.online/download",
                        DNS = "10.202.10.102"
                    },
                    new DnsConfig{
                        Provider = "begzar.ir",
                        DNS = "185.55.226.26",
                    },
                    new DnsConfig{
                        Provider = "begzar.ir",
                        DNS = "185.55.225.25"
                    },
                    new DnsConfig{
                        Provider = "radar.game/#/dns",
                        DNS = "10.202.10.10",
                    },
                     new DnsConfig{
                        Provider = "radar.game/#/dns",
                        DNS = "10.202.10.11"
                    },
                    new DnsConfig{
                        Provider = "dnspro.ir",
                        DNS = "87.107.110.109",
                    },
                    new DnsConfig{
                        Provider = "dnspro.ir",
                        DNS = "87.107.110.110"
                    },
                    new DnsConfig{
                        Provider = "LinkedIn Suggested",
                        DNS = "87.107.52.11",
                    },
                    new DnsConfig{
                        Provider = "LinkedIn Suggested",
                        DNS = "87.107.52.13"
                    },
                    new DnsConfig{
                        Provider = "pishgaman",
                        DNS = "5.202.100.100",
                    },
                    new DnsConfig{
                        Provider = "pishgaman",
                        DNS = "5.202.100.101"
                    },
                    new DnsConfig{
                        Provider = "darzg.ir",
                        DNS = "37.27.41.228",
                    },
                    new DnsConfig{
                        Provider = "sheltertm.com",
                        DNS = "94.103.125.157",
                    },
                    new DnsConfig{
                        Provider = "sheltertm.com",
                        DNS = "94.103.125.158"
                    },
                    new DnsConfig{
                        Provider = "shatel.ir",
                        DNS = "85.15.1.15"
                    },
                    new DnsConfig{
                        Provider = "shatel.ir",
                        DNS = "85.15.1.14",
                    }
                };
                //server.ir/dns-proxy/
                //vanillapp.ir/

                return list;
            }
        }
    }
}
