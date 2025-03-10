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

namespace _403unlocker.Ping
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
        public async static Task<HtmlDocument> HttpRequest(string url, int timeOut_s = 5)
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

                    client.Timeout = TimeSpan.FromSeconds(timeOut_s);

                    // get html as string
                    string htmlString = await client.GetStringAsync(url);

                    var htmlDocument = new HtmlDocument();

                    // make html to tree
                    htmlDocument.LoadHtml(htmlString);
                    return htmlDocument;
                }
            }
        }
        public async static Task<HttpResponseMessage> HttpResponse(string url, int timeOut_s = 2)
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

                    client.Timeout = TimeSpan.FromSeconds(timeOut_s);

                    // get html response
                    HttpResponseMessage htmlResponse = await client.GetAsync(url);
                    return htmlResponse;
                }
            }
        }

        public async static Task<HtmlDocument> HttpRequestAsWeb(string url)
        {
            HtmlWeb web = new HtmlWeb();
            web.Timeout = Settings.ByPass.HttpRequestTimeOutInMiliSeconds;
            var htmlDoc = await web.LoadFromWebAsync(url);
            return htmlDoc;
        }


        public async static Task<string[]> ResolveDNS(string dns, string url)
        {
            // initialize settings
            var options = new LookupClientOptions(IPAddress.Parse(dns))
            {
                Timeout = TimeSpan.FromMilliseconds(Settings.ByPass.DnsResolveTimeOutInMiliSeconds),
                UseCache = false,
                ThrowDnsErrors = true,
                ContinueOnDnsError = false
            };
            // apply settings to query
            var lookup = new LookupClient(options);
            // query DNS server
            var result = await lookup.QueryAsync(url, QueryType.A);

            string[] resolvedIP = result.Answers.OfType<ARecord>().Select(x => $"http://{x.Address}").ToArray();
            return resolvedIP;
        }
    }

    
}
