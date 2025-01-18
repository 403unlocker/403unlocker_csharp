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

        public async Task GetPing()
        {
            Ping ping = new Ping();
            PingReply reply = await ping.SendPingAsync(DNS);
            latency = reply.RoundtripTime;
            status = reply.Status.ToString();
        }

        public async Task GetPing(string url)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                var stopwatch = Stopwatch.StartNew();
                HttpResponseMessage response = await httpClient.GetAsync(url);
                stopwatch.Stop();
                latency = stopwatch.ElapsedMilliseconds;
                status = response.StatusCode.ToString();
            }
            catch (HttpRequestException)
            {
                latency = 0;
                status = "Unreachable";
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
