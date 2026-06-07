using DnsClient;
using Network_Utilities.Http_Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using static Network_Utilities.DNS_Testing.DnsBypassResult;

namespace Network_Utilities.DNS_Testing.ByPass
{
    public class DnsBypassService
    {
        public async Task<DnsBypassResult> TestAsync(IPAddress dns, Uri uri)
        {
            DnsBypassResult result = new DnsBypassResult();
            DateTime start = DateTime.Now;
            TimeSpan end = DateTime.Now - start;

            List<string> resolvedIP = await DnsResolverService.ResolveHostAsync(dns, uri);
            try
            {
                Uri newUri = new Uri($"https://{resolvedIP[0]}:443");
                HttpResponseMessage httpResponse = await HttpService.SendRequestAsync(uri);
            }
            catch (Exception error)
            {
                end = DateTime.Now - start;
                result.Latency = resolvedIP.Count > 0 ? end.Milliseconds : -1;

                if (error is DnsResponseException)
                {
                    if (error.InnerException is OperationCanceledException)
                    {
                        result.Status = BypassStatus.DnsResolveTimedOut;
                    }
                    else if (error.InnerException is SocketException)
                    {
                        result.Status = BypassStatus.HttpConnectionClosedByServer;
                    }
                    else
                    {
                        result.Status = BypassStatus.DnsResolveFailed;
                    }
                }
                else  if (error is TaskCanceledException)
                {
                    result.Status = BypassStatus.HttpConnectionTimedOut;
                }
                else if (error is HttpRequestException)
                {
                    result.Status = BypassStatus.HttpConnectionFailed;

                }
                result.Status = BypassStatus.UnknownError;
            }

            return result;
        }
    }
}
