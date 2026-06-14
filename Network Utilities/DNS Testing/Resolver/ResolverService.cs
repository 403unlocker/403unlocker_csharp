using DnsClient;
using DnsClient.Protocol;
using Network_Utilities.DNS_Testing.ByPass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Network_Utilities.DNS_Testing.Resolver
{
    public static class ResolverService
    {
        private static LookupClientOptions CreateLookupOptions(IPAddress dns)
        {
            LookupClientOptions lookupClientOptions = new LookupClientOptions()
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

        public async static Task<ResolverResult> ResolveHostAsync(IPAddress dns, Uri uri)
        {
            LookupClientOptions options = CreateLookupOptions(dns);
            LookupClient lookup = new LookupClient(options);

            ResolverResult resolverResult = new ResolverResult();
            DateTime now = DateTime.Now;
            DateTime end;
            try
            {
                IDnsQueryResponse response = await lookup.QueryAsync(uri.Host, QueryType.A);
                end = DateTime.Now;

                resolverResult.IPv4 = response.Answers
                                  .OfType<ARecord>()
                                  .Select(x => x.Address)
                                  .ToArray();
            }
            catch (Exception error)
            {
                end = DateTime.Now;

                if (error is DnsResponseException)
                {
                    if (error.InnerException is OperationCanceledException)
                    {
                        resolverResult.Status = ResolverResult.ResolverStatus.TimedOut;
                    }
                    else
                    {
                        resolverResult.Status = ResolverResult.ResolverStatus.Failed;
                    }
                }
                resolverResult.Status = ResolverResult.ResolverStatus.TimedOut;
            }

            if (resolverResult.IPv4.Length == 0) resolverResult.Status = ResolverResult.ResolverStatus.NoIpReturned;

            resolverResult.Latency = (end - now).TotalMilliseconds;
            return resolverResult;
        }
    }
}
