using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace Network_Utilities.Connectivity
{
    public static class ConnectivityService
    {
        public async static Task<PingResult> PingHostAsync(IPAddress dns, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<PingReply> replyList = new List<PingReply>();
            for (int i = 0; i < ConnectivitySettings.PacketCount; i++)
            {
                using (Ping pingSender = new Ping())
                {
                    PingReply reply = await pingSender.SendPingAsync(dns, ConnectivitySettings.TimeoutInMiliSeconds, new byte[ConnectivitySettings.PacketSize]);
                    replyList.Add(reply);
                }
            }

            return SetValues(replyList);
        }

        private static PingResult SetValues(List<PingReply> replyList)
        {
            var successfulReplies = replyList.Where(reply => reply.Status == IPStatus.Success);
            PingResult pingResult = new PingResult()
            {
                Sent = ConnectivitySettings.PacketCount,
                Received = successfulReplies.Count(),
                Latency = successfulReplies.Count() > 0 ? successfulReplies.Average(reply => reply.RoundtripTime) : -1,
            };
            return pingResult;
        }
    }
}
