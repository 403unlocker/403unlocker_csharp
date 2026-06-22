using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_Utilities.DNS_Testing.ByPass
{
    public static class BypassSettings
    {
        public static int TcpConnectTimeoutInMilliSeconds { get; set; } = 5000;
        public static int TlsHandshakeTimeoutInMilliSeconds { get; set; } = 5000;
        public static int HttpWriteTimeoutInMilliSeconds { get; set; } = 2000;
        public static int HttpFlushTimeoutInMilliSeconds { get; set; } = 2000;
        public static int HttpReadTimeoutInMilliSeconds { get; set; } = 2000;
    }
}
