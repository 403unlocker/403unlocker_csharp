using System;
using System.Net;

namespace Network_Utilities.Lookup.Forward_Lookup
{
    public class ForwardLookupResult
    {
        public enum ForwardLookupStatus
        {
            Resolution_not_started,
            Resolved_successfully,
            Resolve_unknown_error,

            Resolution_failed,
            Resolution_timeout,
            Resolved_but_no_IP_returned,
        }

        public ForwardLookupResult()
        {
            IPv4 = Array.Empty<IPAddress>();
            Status = ForwardLookupStatus.Resolution_not_started;
            Latency = 0;
        }

        public IPAddress[] IPv4 { get; internal set; }
        public double Latency { get; internal set; }
        public ForwardLookupStatus Status { get; internal set; }
    }
}
