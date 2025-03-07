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
using System.IO;
using Newtonsoft.Json;
using _403unlocker.Add;
using System.Data;

namespace _403unlocker.Ping
{
    public class DnsBenchmark
    {
        private static string path = "DnsBenchmark.json";
        public string Name { get; set; } = "";
        public string DNS { get; set; } = "";
        public string Status { get; set; } = "";
        public long Latency { get; set; } = 0;

        public DnsBenchmark()
        {
        }

        public DnsBenchmark(DnsConfig dnsRecord)
        {
            Name = dnsRecord.Name;
            DNS = dnsRecord.DNS;
        }

        public async Task GetPing(int timeOutSecond = 2)
        {
            using (System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping())
            {
                try
                {
                    PingReply reply = await ping.SendPingAsync(IPAddress.Parse(DNS), timeOutSecond);
                    Latency = reply.RoundtripTime;
                    Status = reply.Status.ToString();
                }
                catch (TaskCanceledException)
                {
                    Latency = 0;
                    Status = HttpStatusCode.RequestTimeout.ToString();
                }
            }
        }

        public async Task GetPing(string hostName, int timeOut_s)
        {
            try
            {
                // seeking for IPs
                string[] resolvedIP = await ResolveDNS(DNS, hostName, timeOut_s);
                if (resolvedIP.Length == 0)
                {
                    throw new DnsResponseException();
                }

                var htmlreq = await HttpRequestAsWeb(resolvedIP.First(), timeOut_s);
                Status = HttpStatusCode.OK.ToString();
            }
            catch (HttpRequestException)
            {
                Latency = 0;
                Status = HttpStatusCode.ServiceUnavailable.ToString();
            }
            catch (DnsResponseException)
            {
                Latency = 0;
                Status = HttpStatusCode.NotFound.ToString();
            }
            catch (TaskCanceledException)
            {
                Latency = 0;
                Status = HttpStatusCode.RequestTimeout.ToString();
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

        public async static Task<HtmlDocument> HttpRequestAsWeb(string url, int timeOut_s)
        {
            HtmlWeb web = new HtmlWeb();
            web.Timeout = timeOut_s;
            var htmlDoc = await web.LoadFromWebAsync(url);
            return htmlDoc;
        }


        public async static Task<string[]> ResolveDNS(string customeDNS, string hostName, int timeOut_s = 2)
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

        public static async Task<List<DnsBenchmark>> ReadJson()
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"File dosen't exist");

            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Length == 0) throw new FileLoadException($"Can't load file");

            using (StreamReader sr = new StreamReader(path))
            {
                string jsonText = await sr.ReadToEndAsync();

                List<DnsBenchmark> result = JsonConvert.DeserializeObject<List<DnsBenchmark>>(jsonText);
                if (result is null) throw new NoNullAllowedException("Data is null");
                return result;
            }
        }

        public static void WriteJson(List<DnsBenchmark> data)
        {
            string serializedData = data.Count == 0 ? "" : JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(path, serializedData);
        }

        public static List<DnsConfig> ConvertToDnsConfig(List<DnsBenchmark> dnsBenchmark)
        {
            return dnsBenchmark.Select(benchmark => new DnsConfig(benchmark)).ToList();
        }
    }
}
