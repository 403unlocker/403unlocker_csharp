using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network_Utilities.Lookup.Reverse_Lookup
{
    public class ReverseLookupResult
    {
        public enum ReverseLookupStatus
        {
            Not_started,

            Successful,
            Failed,

            Unknown_error,
            Non_existent_domain,
            No_hostname_returned,

            Timeout,
        }

        public ReverseLookupResult()
        {
            Hostname = "";
            Status = ReverseLookupStatus.Not_started;
            Latency = 0;
        }

        public string Hostname { get; internal set; }
        public double Latency { get; internal set; }
        public ReverseLookupStatus Status { get; internal set; }
    }
}
