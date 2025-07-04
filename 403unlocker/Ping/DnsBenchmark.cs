using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using DnsClient;
using System.IO;
using Newtonsoft.Json;
using _403unlocker.Add;
using System.Data;
using _403unlocker.Config;
using _403unlocker.Notification;
using System.Net.Sockets;

namespace _403unlocker.Ping
{
    public class DnsBenchmark
    {
        private static string path = "dns.json";
        public string Provider { get; set; } = "";
        public string DNS { get; set; } = "";
        public string Status { get; set; } = "";
        public int Latency { get; set; } = -1;

        private static ProgressReport progressReport = new ProgressReport();
        public static IProgress<ProgressReport> progress;

        public DnsBenchmark()
        {
        }

        public DnsBenchmark(DnsConfig dnsRecord)
        {
            Provider = dnsRecord.Provider;
            DNS = dnsRecord.DNS;
        }

        public async Task Ping()
        {
            ProgressReset();

            try
            {
                int timeCount = 0;
                int successCount = 0;
                for (int i = 0; i < Settings.Ping.PacketCount; i++)
                {
                    using (System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping())
                    {

                        byte[] buffer = new byte[Settings.Ping.PacketSize];

                        PingReply reply = await pingSender.SendPingAsync(IPAddress.Parse(DNS),
                                                                            Settings.Ping.TimeOutInMiliSeconds,
                                                                            buffer
                                                                            );

                        if (reply.Status == IPStatus.Success)
                        {
                            successCount++;
                            timeCount += (int)reply.RoundtripTime;
                        }
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
                Latency = -1;
                Status = "Ping Timeout";
            }
        }

        public async Task ByPass(string hostName)
        {
            ProgressReset();
            DateTime now = DateTime.Now;
            try
            {
                string[] resolvedIP = await NetworkUtility.ResolveHostName(DNS, hostName);
                if (resolvedIP.Length == 0)
                {
                    Status = "Get No IP";
                    Latency = -1;
                    return;
                }
                else
                {
                    var httpResponse = await NetworkUtility.HttpResponseMessage($"https://{resolvedIP[0]}:443", hostName);
                    Status = $"{(int)httpResponse.StatusCode} - {httpResponse.StatusCode}";
                }
            }
            catch (DnsResponseException e)
            {
                if (e.InnerException is OperationCanceledException)
                {
                    Status = "Resolve TimeOut";
                }
                else if (e.InnerException is SocketException)
                {
                    Status = "Host Closed Socket";
                }
                else
                {
                    Status = "Not Existent Domain";
                }
            }
            catch (TaskCanceledException)
            {
                Status = "HTTP TimeOut";
            }
            catch (Exception)
            {
                Status = "Unknown Error";
            }
            Latency = (int)Math.Round((DateTime.Now - now).TotalMilliseconds);
            ProgressIncreament();
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
            return Provider;
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
