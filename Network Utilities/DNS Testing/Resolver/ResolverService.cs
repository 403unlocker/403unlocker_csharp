using DnsClient;
using DnsClient.Protocol;
using Network_Utilities.DNS_Testing.ByPass;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Network_Utilities.DNS_Testing.Resolver
{
    public static class ResolverService
    {
        private static Stopwatch stopwatch = new Stopwatch();
        
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
            try
            {
                stopwatch.Restart();
                IDnsQueryResponse response = await lookup.QueryAsync(uri, QueryType.A);
                resolverResult.IPv4 = response.Answers
                                  .OfType<ARecord>()
                                  .Select(x => x.Address)
                                  .ToArray();

                if (resolverResult.IPv4.Length == 0) resolverResult.Status = ResolverResult.ResolverStatus.Resolved_but_no_IP_returned;

                stopwatch.Stop();
                resolverResult.Status = ResolverResult.ResolverStatus.Resolved_successfully;
            }
            catch (DnsResponseException error)
            {
                stopwatch.Stop();

                if (error.InnerException is OperationCanceledException) resolverResult.Status = ResolverResult.ResolverStatus.Resolution_timeout;
                else resolverResult.Status = ResolverResult.ResolverStatus.Resolution_failed;
            }

            resolverResult.Latency = stopwatch.ElapsedMilliseconds;
            return resolverResult;
        }
    }
}
