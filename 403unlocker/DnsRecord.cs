using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace _403unlocker
{
    internal class DnsRecord
    {
        private string provider = "", dns = "";
        public string Provider { get => provider; set => provider = value; }
        public string DNS { get => dns; set => dns = value; }

        public static bool IsValid(string dns)
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

        public static List<DnsRecord> DefaultDnsList
        {
            get
            {
                List<DnsRecord> list = new List<DnsRecord>
                {
                    new DnsRecord{
                        Provider = "shecan.ir",
                        DNS = "178.22.122.100",
                    },
                    new DnsRecord{
                        Provider = "shecan.ir",
                        DNS = "185.51.200.2"
                    },
                    new DnsRecord{
                        Provider = "server.ir/dns-proxy",
                        DNS = "192.104.158.78",
                    },
                    new DnsRecord{
                        Provider = "server.ir/dns-proxy",
                        DNS =  "194.104.158.48"
                    },
                    new DnsRecord{
                        Provider = "hostiran.net/landing/proxy",
                        DNS = "172.29.0.100",
                    },
                    new DnsRecord{
                        Provider = "hostiran.net/landing/proxy",
                        DNS = "172.29.2.100"
                    },
                    new DnsRecord{
                        Provider = "electrotm.org",
                        DNS = "78.157.42.101",
                    },
                    new DnsRecord{
                        Provider = "electrotm.org",
                        DNS = "78.157.42.100"
                    },
                    new DnsRecord{
                        Provider = "403.online/download",
                        DNS = "10.202.10.202",
                    },
                    new DnsRecord{
                        Provider = "403.online/download",
                        DNS = "10.202.10.102"
                    },
                    new DnsRecord{
                        Provider = "begzar.ir",
                        DNS = "185.55.226.26",
                    },
                    new DnsRecord{
                        Provider = "begzar.ir",
                        DNS = "185.55.225.25"
                    },
                    new DnsRecord{
                        Provider = "radar.game/#/dns",
                        DNS = "10.202.10.10",
                    },
                     new DnsRecord{
                        Provider = "radar.game/#/dns",
                        DNS = "10.202.10.11"
                    },
                    new DnsRecord{
                        Provider = "dnspro.ir",
                        DNS = "87.107.110.109",
                    },
                    new DnsRecord{
                        Provider = "dnspro.ir",
                        DNS = "87.107.110.110"
                    },
                    new DnsRecord{
                        Provider = "LinkedIn Suggested",
                        DNS = "87.107.52.11",
                    },
                    new DnsRecord{
                        Provider = "LinkedIn Suggested",
                        DNS = "87.107.52.13"
                    },
                    new DnsRecord{
                        Provider = "pishgaman",
                        DNS = "5.202.100.100",
                    },
                    new DnsRecord{
                        Provider = "pishgaman",
                        DNS = "5.202.100.101"
                    },
                    new DnsRecord{
                        Provider = "darzg.ir",
                        DNS = "37.27.41.228",
                    },
                    new DnsRecord{
                        Provider = "sheltertm.com",
                        DNS = "94.103.125.157",
                    },
                    new DnsRecord{
                        Provider = "sheltertm.com",
                        DNS = "94.103.125.158"
                    },
                    new DnsRecord{
                        Provider = "shatel.ir(rsana)",
                        DNS = "85.15.1.15"
                    },
                    new DnsRecord{
                        Provider = "shatel.ir(rsana)",
                        DNS = "85.15.1.14",
                    },
                    new DnsRecord{
                        Provider = "cleanbrowsing.org/filters",
                        DNS = "185.228.168.168",
                    },
                    new DnsRecord{
                        Provider = "cleanbrowsing.org/filters",
                        DNS = "185.228.169.168",
                    },
                    new DnsRecord{
                        Provider = "alternate-dns.com",
                        DNS = "76.76.19.19",
                    },
                    new DnsRecord{
                        Provider = "alternate-dns.com",
                        DNS = "76.223.122.150",
                    }
                };
                //server.ir/dns-proxy/
                //vanillapp.ir/

                return list;
            }
        }

        public async static Task<List<DnsRecord>> DnsScrapAsync()
        {
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    handler.UseCookies = true;
                    using (HttpClient client = new HttpClient(handler))
                    {
                        // content to accept in response
                        client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

                        // OS, browser version, html layout rendering engine
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:133.0) Gecko/20100101 Firefox/133.0");

                        // get html as string
                        string htmlString = await client.GetStringAsync("https://publicdns.xyz");

                        var htmlDocument = new HtmlAgilityPack.HtmlDocument();

                        // make html to tree
                        htmlDocument.LoadHtml(htmlString);

                        // get DNS table
                        var table = htmlDocument.DocumentNode.SelectSingleNode("//table");

                        // get rows of table
                        var rows = table.SelectNodes(".//tr");

                        // data preprocessing rows
                        var customizedRows = rows.Select(row => row.ChildNodes.Where(cell => cell.Name != "#text"));

                        // removes IPv6 DNSs
                        customizedRows = customizedRows.Where(x => x.Count() == 3);

                        // removes table title
                        customizedRows = customizedRows.Skip(1);

                        // removes non-letter in cells e.g. \n \t
                        var minedDns = customizedRows.Select(row => row
                                                   .Select(cell => string.Concat(
                                                           cell.InnerText.Where(character => !char.IsControl(character))
                                                                                 )
                                                          )
                                                            );

                        // convert it to usable list for app
                        var dnsList = minedDns.SelectMany(dnsConfig => new DnsRecord[]
                        {
                            new DnsRecord()
                            {
                                Provider = dnsConfig.ElementAt(0),
                                DNS = dnsConfig.ElementAt(1)
                            },
                            new DnsRecord()
                            {
                                Provider = dnsConfig.ElementAt(0),
                                DNS = dnsConfig.ElementAt(2)
                            }
                        })
                        // removes empty secondary DNS
                        .Where(dnsConfig => dnsConfig.DNS != "").ToList();

                        return dnsList;
                    }
                }
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

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Provider)) return "";
            return Provider;
        }

        public override bool Equals(object obj)
        {
            if (obj is DnsRecord)
            {
                return dns == (obj as DnsRecord).dns;
            }
            return false;
        }

        public override int GetHashCode()
        {
           return dns.GetHashCode();
        }
    }
}
