using DnsClient.Protocol;
using DnsClient;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using _403unlocker.Config;

namespace _403unlocker
{
    internal class NetworkUtility
    {
        internal class Adaptor
        {
            public static NetworkInterface[] GetNetworkInterfaceName()
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
                return netwrokFiltered.Where(x => x.Speed > 0).ToArray();
            }
        }

        private static HttpClient HttpRequestHeaders(Uri uri)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseCookies = true;

            // content to accept in response
            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

            // OS, browser version, html layout rendering engine
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
            client.DefaultRequestHeaders.Host = uri.Host;
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:138.0) Gecko/20100101 Firefox/138.0");

            client.Timeout = TimeSpan.FromMilliseconds(Settings.ByPass.HttpRequestTimeOutInMiliSeconds);

            return client;
        }

        public async static Task<HtmlDocument> HttpResponseHtml(Uri uri)
        {
            HttpClient client = HttpRequestHeaders(uri);

            // get html as string
            string htmlString = await client.GetStringAsync(uri);

            // convert html to tree
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);
            return htmlDocument;
        }

        public async static Task<HttpResponseMessage> HttpResponseHeader(string url, Uri uri)
        {
            HttpClient client = HttpRequestHeaders(uri);

            // get html response
            HttpResponseMessage htmlResponse = await client.GetAsync(url);
            return htmlResponse;
        }

        public async static Task<string[]> ResolveDNS(string dns, Uri uri)
        {
            // initialize settings
            var options = new LookupClientOptions(IPAddress.Parse(dns))
            {
                ContinueOnDnsError = false,
                UseCache = false,
                UseTcpOnly = false,
                Timeout = TimeSpan.FromMilliseconds(Settings.ByPass.DnsResolveTimeOutInMiliSeconds),
                ThrowDnsErrors = true,
                Retries = 0
            };
            // apply settings to query
            var lookup = new LookupClient(options);

            // query DNS server
            List<string> addresses = new List<string>();
            try
            {
                // without www.
                var response = await lookup.QueryAsync(uri.Host.Replace("www.", ""), QueryType.A);
                addresses.AddRange(response.Answers.OfType<ARecord>().Select(x => x.Address.ToString()));
            }
            catch (DnsResponseException)
            {
                // hostname: www.example.com
                var response = await lookup.QueryAsync(uri.Host, QueryType.A);
                addresses.AddRange(response.Answers.OfType<ARecord>().Select(x => x.Address.ToString()));
            }

            return addresses.ToArray();
        }
    }
}
