using DnsClient;
using DnsClient.Protocol;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Network_Utilities.Lookup.Forward_Lookup
{
    public static class ForwardLookupService
    {
        private static LookupClientOptions CreateLookupOptions(IPAddress dns)
        {
            LookupClientOptions lookupClientOptions = new LookupClientOptions(new IPEndPoint(dns, 53))
            {
                ContinueOnDnsError = false,
                UseCache = false,
                UseTcpOnly = false,
                Timeout = TimeSpan.FromMilliseconds(ForwardLookupSettings.TimeoutInMilliSeconds),
                ThrowDnsErrors = true,
                Retries = 0
            };
            return lookupClientOptions;
        }

        public async static Task<ForwardLookupResult> ForwardLookupHostAsync(IPAddress dns, string uri)
        {
            LookupClientOptions options = CreateLookupOptions(dns);
            LookupClient lookup = new LookupClient(options);

            ForwardLookupResult forwardLookupResult = new ForwardLookupResult();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                IDnsQueryResponse response = await lookup.QueryAsync(uri, QueryType.A);
                forwardLookupResult.IPv4 = response.Answers
                                  .OfType<ARecord>()
                                  .Select(x => x.Address)
                                  .ToArray();

                if (forwardLookupResult.IPv4.Length == 0) forwardLookupResult.Status = ForwardLookupResult.ForwardLookupStatus.Resolved_but_no_IP_returned;
                
                forwardLookupResult.Status = ForwardLookupResult.ForwardLookupStatus.Resolved_successfully;
            }
            catch (DnsResponseException error)
            {
                if (error.InnerException is OperationCanceledException) forwardLookupResult.Status = ForwardLookupResult.ForwardLookupStatus.Resolution_timeout;
                else forwardLookupResult.Status = ForwardLookupResult.ForwardLookupStatus.Resolution_failed;
            }
            stopwatch.Stop();

            forwardLookupResult.Latency = stopwatch.ElapsedMilliseconds;
            return forwardLookupResult;
        }
    }
}
