using _403Unlocker.Data_Models;
using HtmlAgilityPack;
using Network_Utilities.Http_Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace _403Unlocker.Add_DNS
{
    internal static class FetchDns
    {
        //https://www.getflix.com.au/setup/dns-servers/
        //https://www.publicdns.xyz

        private static readonly Uri url = new Uri("https://publicdns.xyz");

        public async static Task<DnsConfig> ScrapDnsServersAsync()
        {
            List<DnsInfo> dnsInfos = new List<DnsInfo>();

            HtmlDocument htmlDocument = new HtmlDocument();
            HttpResult response = await HttpService.SendRequestAsync(url);
            htmlDocument.LoadHtml(response.HttpResponseContent);

            HtmlNode table = htmlDocument.DocumentNode.SelectSingleNode("//table");

            HtmlNode tbody = table.SelectSingleNode("./tbody");

            HtmlNodeCollection rows = tbody.SelectNodes("./tr");

            foreach (HtmlNode row in rows)
            {
                if (row.SelectSingleNode("./th") != null)
                {
                    string provider = row.SelectSingleNode("./th").SelectSingleNode("./a").InnerText;
                    HtmlNodeCollection nodes = row.SelectNodes("./td");
                    if (nodes != null)
                    {
                        foreach (HtmlNode dns in nodes)
                        {
                            HtmlNode node = dns.SelectSingleNode("./span");
                            if (node != null)
                            {
                                string text = node.InnerText;
                                if (IPAddress.TryParse(text, out IPAddress ipv4) && ipv4.AddressFamily == AddressFamily.InterNetwork)
                                {
                                    dnsInfos.Add(new DnsInfo(ipv4, provider));
                                }
                            }
                        }
                    }
                }
            }

            DnsConfig dnsConfig = new DnsConfig(dnsInfos);
            return dnsConfig;
        }
    }
}
