using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public async static Task<List<DnsConfig>> DnsScrapAsync()
        {
            try
            {
                List<DnsConfig> dnsFound = new List<DnsConfig>();

                using (var handler = new HttpClientHandler())
                {
                    handler.UseCookies = true;
                    using (HttpClient client = new HttpClient(handler))
                    {
                        client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:133.0) Gecko/20100101 Firefox/133.0");

                        string htmlString = await client.GetStringAsync("https://publicdns.xyz");

                        var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                        htmlDocument.LoadHtml(htmlString);

                        var table = htmlDocument.DocumentNode.SelectSingleNode("//table");

                        var rows = table.SelectNodes(".//tr").ToList();
                        var values = rows.Select(row =>
                        {
                            return row.ChildNodes.Where(value => value.Name == "td" || value.Name == "th");
                        }).Where(row => row.Count() == 3).ToList();

                        values = values.Where(value => value.ElementAt(0).Name == "th" &&
                                                value.ElementAt(1).Name == "td" &&
                                                value.ElementAt(2).Name == "td").ToList();

                        dnsFound = values.SelectMany(x => new DnsConfig[]
                        {
                            new DnsConfig()
                            {
                                Provider = Regex.Replace(x.ElementAt(0).InnerText, @"\\[nt]", ""),
                                DNS = Regex.Replace(x.ElementAt(1).InnerText, @"\\[nt]", "")
                            },
                            new DnsConfig()
                            {
                                Provider = Regex.Replace(x.ElementAt(0).InnerText, @"\\[nt]", ""),
                                DNS = Regex.Replace(x.ElementAt(2).InnerText, @"\\[nt]", "")
                            }
                        }
                        ).ToList();
                    }
                }

                return dnsFound;
            }
            catch (HttpRequestException error)
            {
                MessageBox.Show(error.Message, "Access Denied!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
