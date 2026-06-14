using System;
using System.Collections.Generic;
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
        public enum BypassStatus
        {
            UnknownError = 0,

            Successful = 1,

            HttpConnectionError = 400,
            HttpConnectionFailed = 401,
            HttpConnectionTimedOut = 402,
            HttpConnectionForbidden = 403,
            HttpConnectionNotFound = 404,
            HttpConnectionClosedByServer = 405
        }

        public SslStream SslResponseMessage { get; internal set; }

        public double Latency { get; internal set; }
        public BypassStatus Status { get; internal set; }

        public async Task<string> ReadHtmlResponseAsync(SslStream ssl)
        {
            using (StreamReader reader = new StreamReader(ssl, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
        
    }
}
