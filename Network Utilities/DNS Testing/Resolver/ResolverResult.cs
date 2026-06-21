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
            Resolution_not_started,
            Resolved_successfully,
            Resolve_unknown_error,

            Resolution_failed,
            Resolution_timeout,
            Resolved_but_no_IP_returned,
        }

        public ResolverResult()
        {
            IPv4 = Array.Empty<IPAddress>();
            Status = ResolverStatus.Resolution_not_started;
            Latency = 0;
        }

        public IPAddress[] IPv4 { get; internal set; }
        public double Latency { get; internal set; }
        public ResolverStatus Status { get; internal set; }
    }
}
