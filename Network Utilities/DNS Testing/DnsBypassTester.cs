using DnsClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.Http;
using System.Threading.Tasks;
using Network_Utilities.Http_Services;

namespace Network_Utilities.DNS_Testing
{
    internal class DnsBypassTester
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

        public double Latency { get; set; }
        public BypassStatus Status { get; set; }

        public async Task<DnsBypassTester> TestBypassAsync(IPAddress dns, Uri uri)
        {
            DnsBypassTester bypassTester = new DnsBypassTester();

            DateTime now = DateTime.Now;
            List<string> resolvedIP = await DnsResolverService.ResolveHostAsync(dns, uri);
            
            if (resolvedIP.Count == 0)
            {
                bypassTester.Latency = -1;
                bypassTester.Status = BypassStatus.DnsResolvedNoIpAddressReturned;
                return bypassTester;
            }

            try
            {
                Uri newUri = new Uri($"https://{resolvedIP[0]}:443");
                HttpResponseMessage httpResponse = await HttpServices.SendRequestAsync(uri);
            }
            catch (DnsResponseException e)
            {
                if (e.InnerException is OperationCanceledException)
                {
                    bypassTester.Status = BypassStatus.DnsResolveTimedOut;
                }
                else if (e.InnerException is SocketException)
                {
                    bypassTester.Status = BypassStatus.HttpConnectionClosedByServer;
                }
                else
                {
                    bypassTester.Status = BypassStatus.DnsResolveFailed;
                }
            }
            catch (TaskCanceledException)
            {
                bypassTester.Status = BypassStatus.HttpConnectionTimedOut;
            }
            catch (Exception)
            {
                bypassTester.Status = BypassStatus.UnknownError;
            }

            TimeSpan latency = DateTime.Now - now;
            bypassTester.Latency = latency.TotalMilliseconds;

            return bypassTester;
        }
    }
}
