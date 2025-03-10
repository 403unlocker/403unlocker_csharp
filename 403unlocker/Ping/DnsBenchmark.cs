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
using _403unlocker.Config;
using _403unlocker.Notification;
using System.Reflection;

namespace _403unlocker.Ping
{
    public class DnsBenchmark
    {
        private static string path = "DnsBenchmark.json";
        public string Name { get; set; } = "";
        public string DNS { get; set; } = "";
        public string Status { get; set; } = "";
        public long Latency { get; set; } = -1;

        private static ProgressReport progressReport = new ProgressReport();
        public static IProgress<ProgressReport> progress;

        public DnsBenchmark()
        {
        }

        public DnsBenchmark(DnsConfig dnsRecord)
        {
            Name = dnsRecord.Name;
            DNS = dnsRecord.DNS;
        }

        public async Task GetPing()
        {
            ProgressReset();
            using (System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping())
            {
                try
                {
                    long timeCount = 0;
                    int successCount = 0;
                    for (int i = 0; i < Settings.Ping.PacketCount; i++)
                    {
                        
                        byte[] buffer = new byte[Settings.Ping.PacketSize];

                        PingReply reply =  await pingSender.SendPingAsync(IPAddress.Parse(DNS),
                                                                            Settings.Ping.TimeOutInMiliSeconds,
                                                                            buffer
                                                                            );

                        

                        if (reply.Status == IPStatus.Success)
                        {
                            successCount++;
                            timeCount += reply.RoundtripTime;
                        }

                        ProgressIncreament();
                    }

                    if (successCount > 0)
                    {
                        // Average
                        Latency = timeCount / successCount;
                    }
                    else
                    {
                        Latency = -1;
                    }
                    int packetLossPercentage = (int)((Settings.Ping.PacketCount - successCount) / (double)Settings.Ping.PacketCount) * 100;
                    Status = $"{packetLossPercentage}% loss";
                }
                catch (TaskCanceledException)
                {
                    Latency = 0;
                    Status = HttpStatusCode.RequestTimeout.ToString();
                }
            }
        }

        public async Task GetPing(string url)
        {
            ProgressReset();
            try
            {
                // seeking for IPs
                string[] resolvedIP = await NetworkUtility.ResolveDNS(DNS, url);
                if (resolvedIP.Length == 0)
                {
                    throw new DnsResponseException();
                }

                var htmlreq = await NetworkUtility.HttpRequestAsWeb(resolvedIP.First());
                Status = HttpStatusCode.OK.ToString();

                ProgressIncreament();
            }
            catch (HttpRequestException)
            {
                Latency = -1;
                Status = HttpStatusCode.ServiceUnavailable.ToString();
            }
            catch (DnsResponseException)
            {
                Latency = -1;
                Status = HttpStatusCode.NotFound.ToString();
            }
            catch (TaskCanceledException)
            {
                Latency = -1;
                Status = HttpStatusCode.RequestTimeout.ToString();
            }
        }

        private void ProgressReset()
        {
            if (!(progress is null))
            {
                progressReport.CurrentValue = 0;
                progress.Report(progressReport);
            }
        }

        private void ProgressIncreament()
        {
            if (!(progress is null))
            {
                progressReport.CurrentValue++;
                progress.Report(progressReport);
            }
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
