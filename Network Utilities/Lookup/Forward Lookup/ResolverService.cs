using DnsClient;
using DnsClient.Protocol;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Network_Utilities.Lookup.Forward_Lookup
{
    public static class ResolverService
    {
        private static LookupClientOptions CreateLookupOptions(IPAddress dns)
        {
            LookupClientOptions lookupClientOptions = new LookupClientOptions(new IPEndPoint(dns, 53))
            {
                ContinueOnDnsError = false,
                UseCache = false,
                UseTcpOnly = false,
                Timeout = TimeSpan.FromMilliseconds(ResolverSettings.TimeoutInMilliSeconds),
                ThrowDnsErrors = true,
                Retries = 0
            };
            return lookupClientOptions;
        }

        public async static Task<ResolverResult> ResolveHostAsync(IPAddress dns, string uri)
        {
            LookupClientOptions options = CreateLookupOptions(dns);
            LookupClient lookup = new LookupClient(options);

            ResolverResult resolverResult = new ResolverResult();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                IDnsQueryResponse response = await lookup.QueryAsync(uri, QueryType.A);
                resolverResult.IPv4 = response.Answers
                                  .OfType<ARecord>()
                                  .Select(x => x.Address)
                                  .ToArray();

                if (resolverResult.IPv4.Length == 0) resolverResult.Status = ResolverResult.ResolverStatus.Resolved_but_no_IP_returned;
                
                resolverResult.Status = ResolverResult.ResolverStatus.Resolved_successfully;
            }
            catch (DnsResponseException error)
            {
                if (error.InnerException is OperationCanceledException) resolverResult.Status = ResolverResult.ResolverStatus.Resolution_timeout;
                else resolverResult.Status = ResolverResult.ResolverStatus.Resolution_failed;
            }
            stopwatch.Stop();

            resolverResult.Latency = stopwatch.ElapsedMilliseconds;
            return resolverResult;
        }
    }
}
