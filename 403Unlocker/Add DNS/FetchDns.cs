using _403Unlocker.Data_Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Network_Utilities.Http_Service;

namespace _403Unlocker.Add_DNS
{
    internal static class FetchDns
    {
        private static readonly Uri url = new Uri("https://publicdns.xyz");

        public async static Task<DnsConfig> ScrapDnsServersAsync()
        {
            //https://www.getflix.com.au/setup/dns-servers/
            //https://www.publicdns.xyz

            HtmlDocument htmlDocument = new HtmlDocument();
            HttpResult response = await HttpService.SendRequestAsync(url);

            htmlDocument.LoadHtml(response.HttpResponseContent);

            HtmlNode table = ParseToTable(htmlDocument);

            var rows = ExtractRows(table);

            rows = rows.Skip(1).ToArray(); // removes table title

            rows = RemoveIPv6(rows);

            string[][] ipv4 = TrimIPv4(rows);

            DnsInfo[][] dnsInfos = ConvertToDnsInfo(ipv4);
            DnsConfig dnsConfig = new DnsConfig(dnsInfos.SelectMany(dns => dns).ToList());
            return dnsConfig;
        }

        private static HtmlNode ParseToTable(HtmlDocument htmlDocument)
        {
            var table = htmlDocument.DocumentNode.SelectSingleNode("//table");
            return table;
        }

        private static HtmlNode[][] ExtractRows(HtmlNode htmlNode)
        {
            HtmlNodeCollection rows = htmlNode.SelectNodes(".//tr");

            HtmlNode[][] customizedRows = rows.Select
            (
                row => row.ChildNodes.Where
                    (
                        cell => cell.Name != "#text"
                    ).ToArray()
            ).ToArray();

            return customizedRows;
        }

        private static HtmlNode[][] RemoveIPv6(HtmlNode[][] rows)
        {
            HtmlNode[][] removedIPv6 =  rows.Where(x => x.Count() == 3).ToArray();
            return removedIPv6;
        }

        private static string[][] TrimIPv4(HtmlNode[][] rows)
        {
            var minedDns = rows.Select
            (
                row => row.Select
                (
                    cell => string.Concat
                    (
                        cell.InnerText.Where
                        (
                            c => !char.IsControl(c)
                        )
                    )
                ).ToArray()
            ).ToArray();

            return minedDns;
        }

        private static DnsInfo[][] ConvertToDnsInfo(string[][] minedDns)
        {
            DnsInfo[][] dnsInfosList = minedDns.Select(dnsInfo =>
            {
                string provider = dnsInfo[0];
                string primary = dnsInfo[1];
                string secondary = dnsInfo[2];

                List<DnsInfo> dnsInfos = new List<DnsInfo>();
                if (!string.IsNullOrEmpty(primary))
                {
                    dnsInfos.Add(new DnsInfo(IPAddress.Parse(primary), provider));
                }
                if (!string.IsNullOrEmpty(secondary))
                {
                    dnsInfos.Add(new DnsInfo(IPAddress.Parse(secondary), provider));
                }
                return dnsInfos.ToArray();
            }).ToArray();

            return dnsInfosList;
        }
    }
}
