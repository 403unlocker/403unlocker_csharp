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
using _403unlocker;
using DnsClient;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

namespace _403unlocker
{
    internal class DnsPing : DnsProvider
    {
        private string url = "";
        private string status = ""; // HttpStatusCode.NoContent.ToString();
        private long latency = 0;

        public string Name
        {
            get => base.Name;
        }

        public string DNS
        {
            get => base.DNS;
        }

        public string URL
        {
            get => url;
            set => url = value;
        }

        public string Status
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

        public async Task GetPing(int timeOutms)
        {
            using (Ping ping = new Ping())
            {
                try
                {
                    PingReply reply = await ping.SendPingAsync(DNS, timeOutms);
                    latency = reply.RoundtripTime;
                    status = reply.Status.ToString();
                }
                catch (TaskCanceledException)
                {
                    latency = 0;
                    status = "TimeOut";
                }
            }
        }

        public async Task GetPing(string url, TimeSpan timeOut)
        {
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(timeOut);
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                try
                {
                    // Manually resolve the DNS address for the target URL using the specified DNS server
                    string ip = "";

                    // Convert the string DNS server address to an IPAddress
                    IPAddress dnsServerIp = IPAddress.Parse(DNS);  // Convert the DNS server IP string to IPAddress

                    var lookupClient = new LookupClient(dnsServerIp);  // Create a new lookup client with the specified DNS server
                    var result = await lookupClient.QueryAsync(url, QueryType.A, cancellationToken: cancellationToken);  // Query for A record (IPv4 address)

                    // Assuming the result has a valid IP address
                    if (result.Answers.Count > 0)
                    {
                        ip = result.Answers[0].ToString();
                    }
                    else
                    {
                        status = "failed";
                        return;
                    }

                    // Now make the HTTP request using this resolved IP
                    using (var client = new HttpClient())
                    {
                        // content to accept in response
                        client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

                        // OS, browser version, html layout rendering engine
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:133.0) Gecko/20100101 Firefox/133.0");

                        client.DefaultRequestHeaders.Host = url;  // Important: Set Host header to the actual domain name
                        var response = await client.GetAsync($"http://{ip}", cancellationToken);

                        if (response.StatusCode == HttpStatusCode.Forbidden)
                        {
                            // If 403, the geo-blocking is still in effect
                            status = "403";
                        }

                        // If response is not 403, it means the request was successful (or bypassed geo-blocking)
                        status = "OK";
                    }

                }
                catch (HttpRequestException)
                {
                    latency = 0;
                    status = "Unreachable";
                }
                catch (Exception e) when (e is TaskCanceledException || e is DnsResponseException)
                {
                    latency = 0;
                    status = "TimeOut";
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
