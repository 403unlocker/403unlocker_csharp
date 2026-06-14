using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace Network_Utilities.Connectivity
{
    public class ConnectivityService
    {
        public int SentPacketCount { get; } 
        public int PacketSize { get; } 
        public int Timeout { get; } 


        public ConnectivityService()
        {
            SentPacketCount = ConnectivitySettings.PacketCount;
            PacketSize = ConnectivitySettings.PacketSize;
            Timeout = ConnectivitySettings.TimeoutInMiliSeconds;
        }

        public async Task<PingResult> PingHostAsync(IPAddress dns, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<PingReply> replyList = new List<PingReply>();
            for (int i = 0; i < SentPacketCount; i++)
            {
                PingReply reply = await GetPing(dns);
                replyList.Add(reply);
            }

            return SetValues(replyList);
        }

        private PingResult SetValues(List<PingReply> replyList)
        {
            var successfulReplies = replyList.Where(reply => reply.Status == IPStatus.Success);
            PingResult pingResult = new PingResult()
            {
                Sent = SentPacketCount,
                Received = successfulReplies.Count(),
                Latency = successfulReplies.Count() > 0 ? successfulReplies.Average(reply => reply.RoundtripTime) : -1,
            };
            return pingResult;
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
