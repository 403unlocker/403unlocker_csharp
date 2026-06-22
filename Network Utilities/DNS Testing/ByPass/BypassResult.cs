using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Network_Utilities.DNS_Testing.ByPass
{
    public class BypassResult
    {
        public double Latency { get; internal set; }
        public HttpStatusCode Status { get; internal set; }
        public string[] HttpResponse { get; internal set; }
    }
}
