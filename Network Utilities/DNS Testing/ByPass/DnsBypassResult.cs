using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_Utilities.DNS_Testing.ByPass
{
    public class DnsBypassResult
    {
        public enum BypassStatus
        {
            UnknownError = 0,

            BypassedSuccessful = 1,

            DnsResolveFailed = 100,
            DnsResolveTimedOut = 101,
            DnsResolvedNoIpAddressReturned = 102,

            HttpConnectionError = 400,
            HttpConnectionFailed = 401,
            HttpConnectionTimedOut = 402,
            HttpConnectionForbidden = 403,
            HttpConnectionNotFound = 404,
            HttpConnectionClosedByServer = 405
        }

        public double Latency { get; internal set; }
        public BypassStatus Status { get; internal set; }

    }
}
