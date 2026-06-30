using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace Network_Utilities.Ping
{
    public static class PingService
    {
        public async static Task<PingResult> PingHostAsync(IPAddress dns)
        {
            return await PingHostAsync(dns, CancellationToken.None);
        }

        public async static Task<PingResult> PingHostAsync(IPAddress dns, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<PingReply> replyList = new List<PingReply>();
            for (int i = 0; i < PingSettings.PacketCount; i++)
            {
                using (System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping())
                {
                    PingReply reply = await pingSender.SendPingAsync(dns, PingSettings.TimeoutInMiliSeconds, new byte[PingSettings.PacketSize]);
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
                Sent = PingSettings.PacketCount,
                Received = successfulReplies.Count(),
                Latency = successfulReplies.Count() > 0 ? successfulReplies.Average(reply => reply.RoundtripTime) : -1,
            };
            return pingResult;
        }
    }
}
