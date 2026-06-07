using DnsClient;
using Network_Utilities.Http_Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using Network_Utilities.DNS_Testing.Resolver;

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
                        result.Status = DnsBypassResult.BypassStatus.DnsResolveTimedOut;
                    }
                    else if (error.InnerException is SocketException)
                    {
                        result.Status = DnsBypassResult.BypassStatus.HttpConnectionClosedByServer;
                    }
                    else
                    {
                        result.Status = DnsBypassResult.BypassStatus.DnsResolveFailed;
                    }
                }
                else  if (error is TaskCanceledException)
                {
                    result.Status = DnsBypassResult.BypassStatus.HttpConnectionTimedOut;
                }
                else if (error is HttpRequestException)
                {
                    result.Status = DnsBypassResult.BypassStatus.HttpConnectionFailed;

                }
                result.Status = DnsBypassResult.BypassStatus.UnknownError;
            }

            return result;
        }
    }
}
