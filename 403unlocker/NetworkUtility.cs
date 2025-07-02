using DnsClient.Protocol;
using DnsClient;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using _403unlocker.Config;
using System.IO;
using System.Text;
using System.ComponentModel;

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

        public static async Task<HttpResponseMessage> HttpResponseMessage(string url, string hostnameHeader)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.UseCookies = false;
                handler.AllowAutoRedirect = true;
                using (HttpClient client = new HttpClient(handler))
                {
                    client.Timeout = TimeSpan.FromMilliseconds(Settings.ByPass.HttpRequestTimeOutInMiliSeconds);

                    // content to accept in response
                    client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

                    // OS, browser version, html layout rendering engine
                    client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
                    client.DefaultRequestHeaders.Host = hostnameHeader;
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:138.0) Gecko/20100101 Firefox/138.0");
                    return await client.GetAsync(url);
                }
            }
        }

        public async static Task<string[]> ResolveHostName(string Dns, string hostName)
        {
            // initialize settings
            var options = new LookupClientOptions(IPAddress.Parse(Dns))
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
                // example.com
                var response = await lookup.QueryAsync(hostName, QueryType.A);
                addresses.AddRange(response.Answers.OfType<ARecord>().Select(x => x.Address.ToString()));
            }
            catch (DnsResponseException)
            {
                // www.example.com
                var response = await lookup.QueryAsync($"www.{hostName}", QueryType.A);
                addresses.AddRange(response.Answers.OfType<ARecord>().Select(x => x.Address.ToString()));
            }

            return addresses.ToArray();
        }
    }
}
