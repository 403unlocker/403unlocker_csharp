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

            List<DnsInfo> dnsInfos = ExtractDnsInfos(rows);
            DnsConfig dnsConfig = new DnsConfig(dnsInfos);
            return dnsConfig;
        }

        private static HtmlNode ParseToTable(HtmlDocument htmlDocument)
        {
            var table = htmlDocument.DocumentNode.SelectSingleNode("//table");
            return table;
        }

        private static IEnumerable<IEnumerable<HtmlNode>> ExtractRows(HtmlNode htmlNode)
        {
            HtmlNodeCollection rows = htmlNode.SelectNodes(".//tr"); // get rows of table
            
            var customizedRows = rows.Select(row => row.ChildNodes.Where(cell => cell.Name != "#text")); // data preprocessing rows
            customizedRows = customizedRows.Where(x => x.Count() == 3); // removes second row (IPv6)
            customizedRows = customizedRows.Skip(1); // removes table title
            return customizedRows;
        }

        private static List<DnsInfo> ExtractDnsInfos(IEnumerable<IEnumerable<HtmlNode>> rows)
        {
            // removes non-letter in cells e.g. \n \t
            var minedDns = rows.Select(row => row.Select(
                                                     cell => string.Concat(
                                                                   cell.InnerText.Where(
                                                                       character => !char.IsControl(character)
                                                                       ))));

            // convert it to usable list for app
            var dnsInfosList = minedDns.SelectMany(dnsInfo => new DnsInfo[]
            {
                new DnsInfo(IPAddress.Parse(dnsInfo.ElementAt(1)), dnsInfo.ElementAt(0)),
                new DnsInfo(IPAddress.Parse(dnsInfo.ElementAt(2)), dnsInfo.ElementAt(0))
            })
            // removes empty DNS
            .Where(dnsInfo => !string.IsNullOrEmpty(dnsInfo.IPv4.ToString())).ToList();

            return dnsInfosList;
        }
    }
}
