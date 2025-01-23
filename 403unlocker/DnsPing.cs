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
                    using (var handler = new HttpClientHandler())
                    {
                        handler.Proxy = new WebProxy(DNS);
                        handler.UseProxy = true;
                        using (var client = new HttpClient(handler))
                        {
                            var response = await client.GetAsync(url, cancellationTokenSource.Token);
                            if (response.IsSuccessStatusCode)
                            {
                                status = "OK";
                            }
                            else
                            {
                                status = "Unreachable";
                            }
                        }
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

        public override string ToString()
        {
            return Name;
        }
    }
}
