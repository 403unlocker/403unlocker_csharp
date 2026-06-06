using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Network_Utilities.Connectivity
{
    internal class ConnectivityService
    {
        private List<PingReply> pingReplies = new List<PingReply>();

        public ConnectivitySettings Settings { get; set; }
        public int SentPacketCount { get; } = ConnectivitySettings.PacketCount;
        public int ReceivedPacketCount { get => pingReplies.Count(reply => reply.Status == IPStatus.Success); }
        public int PacketSize { get; } = ConnectivitySettings.PacketSize;
        public int Timeout { get; } = ConnectivitySettings.TimeoutInMiliSeconds;
        public string Status
        {
            get
            {
                double lossPercentage = ReceivedPacketCount / (double)SentPacketCount;
                string status = $"{lossPercentage * 100}";
                return status;
            }
        }

        public int Latency
        {
            get
            {
                if (ReceivedPacketCount == 0) return -1;
                double avg = pingReplies.Average(reply => reply.RoundtripTime) * 100;
                return Convert.ToInt32(avg);
            }
        }

        public async Task PingHost(IPAddress dns)
        {
            pingReplies.Clear();
            for (int i = 1; i <= SentPacketCount; i++)
            {
                PingReply reply = await GetPing(dns);
                pingReplies.Add(reply);
            }
        }

        private async Task<PingReply> GetPing(IPAddress dns)
        {
            using (Ping pingSender = new Ping())
            {
                PingReply reply = await pingSender.SendPingAsync(dns, Timeout, new byte[PacketSize]);
                return reply;
            }
        }
    }
}
