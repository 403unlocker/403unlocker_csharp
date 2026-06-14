using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network_Utilities.DNS_Testing.Resolver
{
    public class ResolverResult
    {
        public enum ResolverStatus
        {
            UnknownError = 0,

            ResolvedSuccessful = 1,

            Failed = 100,
            TimedOut = 101,
            NoIpReturned = 102,
        }

        public IPAddress[] IPv4 { get; internal set; } = Array.Empty<IPAddress>();
        public double Latency { get; internal set; }
        public ResolverStatus Status { get; internal set; }
    }
}
