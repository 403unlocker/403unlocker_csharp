using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Policy;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Diagnostics;
using DnsClient;
using System.Security.Cryptography;
using System.Threading;
using System.Text.RegularExpressions;
using DnsClient.Protocol;
using HtmlAgilityPack;

namespace _403unlockerLibrary
{
    public class DnsPing : DnsProvider
    {
        private int status;
        private long latency = 0;

        public string Name
        {
            get => base.Name;
        }

        public string DNS
        {
            get => base.DNS;
        }

        public int Status
        {
            get => status;
        }

        public long Latency
        {
            get => latency;
        }

        public DnsPing(string provider, string dns)
        {
            base.Name = provider;
            base.DNS = dns;
        }

        public DnsPing(DnsProvider dnsRecord)
        {
            base.Name = dnsRecord.Name;
            base.DNS = dnsRecord.DNS;
        }
        public static bool IsValidHostName(string hostName)
        {
            if (Regex.IsMatch(hostName, @"^(www\.)?[a-zA - Z0 - 9]+\.com$")) return true;
            return false;
        }
        public async Task GetPing(int timeOut_ms)
        {
            using (Ping ping = new Ping())
            {
                try
                {
                    PingReply reply = await ping.SendPingAsync(IPAddress.Parse(DNS), timeOut_ms);
                    latency = reply.RoundtripTime;
                    status = (int)HttpStatusCode.OK;
                }
                catch (TaskCanceledException)
                {
                    latency = 0;
                    status = (int)HttpStatusCode.RequestTimeout;
                }
            }
        }

        public async Task GetPing(string hostName, int timeOut_s)
        {
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(timeOut_s *1000);
                CancellationToken cancellationToken = cancellationTokenSource.Token;
                try
                {
                    // seeking for IPs
                    string[] resolvedIP = await ResolveDNS(DNS, hostName, timeOut_s * 1000);
                    if (resolvedIP.Length == 0)
                    {
                        status = (int)HttpStatusCode.NoContent;
                        return;
                    }

                    hostName = $"https://www.{hostName}";

                    // Now make the HTTP request using this resolved IP
                    var handler = new HttpClientHandler()
                    {
                        Proxy = new WebProxy(resolvedIP.First(), true),
                        UseProxy = true
                    };

                    HtmlWeb web = new HtmlWeb();
                    web.LoadFromBrowser(hostName);


                    using (var client = new HttpClient(handler))
                    {
                        // content to accept in response
                        client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

                        // OS, browser version, html layout rendering engine
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:133.0) Gecko/20100101 Firefox/133.0");

                        client.DefaultRequestHeaders.Host = hostName; // Replace with your domain name

                        var response = await client.GetAsync(hostName);

                        //if (response.StatusCode == HttpStatusCode.Forbidden)
                        //{
                        //    // If 403, the geo-blocking is still in effect
                        //    status = (int)HttpStatusCode.Forbidden;
                        //}
                    }
                }
                catch (HttpRequestException e)
                {
                    latency = 0;
                    status = (int)HttpStatusCode.BadRequest;
                    if (e.InnerException != null)
                    {
                        string s = string.Concat(e.InnerException.Message.Where(x => char.IsDigit(x)));
                        if (s.Length == 3)
                        {
                            status = int.Parse(s);
                        }
                    }
                }
                catch (Exception e) when (e is TaskCanceledException || e is DnsResponseException)
                {
                    latency = 0;
                    status = (int)HttpStatusCode.RequestTimeout;
                }
            }

        }

        private async static Task<string[]> ResolveDNS(string customeDNS, string hostName, int timeOut_s = 5)
        {
            // initialize settings
            var options = new LookupClientOptions(IPAddress.Parse(customeDNS))
            {
                Timeout = TimeSpan.FromSeconds(timeOut_s),
                UseCache = false,
                ThrowDnsErrors = true,
                ContinueOnDnsError = false
            };
            // apply settings to query
            var lookup = new LookupClient(options);
            // query DNS server
            var result = await lookup.QueryAsync(hostName, QueryType.A);

            string[] resolvedIP = result.Answers.OfType<ARecord>().Select(x => $"http://{x.Address}").ToArray();
            return resolvedIP;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
