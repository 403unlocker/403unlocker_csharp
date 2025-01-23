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
                    status = "Thread Timeout";
                }
            }
        }

        public async Task GetPing(string url, TimeSpan timeOut)
        {
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.CancelAfter(timeOut);
                try
                {

                    bool success = await TestDNSBypass(DNS);
                    if (success)
                    {
                        status = "OK";
                    }
                    else
                    {
                        status = "403";
                    }
                }
                catch (HttpRequestException)
                {
                    latency = 0;
                    status = "Unreachable";
                }
                catch (TaskCanceledException)
                {
                    latency = 0;
                    status = "Timeout";
                }
            }
        }

        // Method to test if a DNS server bypasses geo-restriction and returns no 403 error
        async Task<bool> TestDNSBypass(string dnsServer)
        {
            try
            {
                // Manually resolve the DNS address for the target URL using the specified DNS server
                var ip = ResolveDNS(url, dnsServer);

                // Now make the HTTP request using this resolved IP
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Host = url;  // Important: Set Host header to the actual domain name
                    var response = await client.GetAsync($"http://{ip}");

                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        // If 403, the geo-blocking is still in effect
                        return false;
                    }

                    // If response is not 403, it means the request was successful (or bypassed geo-blocking)
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error with DNS {dnsServer}: {ex.Message}");
                return false;
            }
        }

        // Method to resolve DNS using a custom DNS server
        static string ResolveDNS(string domain, string dnsServer)
        {
            var resolver = new DnsClient.DnsQueryClient(dnsServer); // You can use DnsClient or similar library
            var result = resolver.Query(domain, DnsClient.QueryType.A); // Query for A record (IPv4 address)

            // Assuming the result has valid IP address
            if (result.Answers.Count > 0)
            {
                return result.Answers[0].ToString();
            }
            else
            {
                throw new Exception("DNS resolution failed.");
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
