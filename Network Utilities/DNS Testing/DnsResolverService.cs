using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using DnsClient;
using DnsClient.Protocol;

namespace Network_Utilities.DNS_Testing
{
    internal class DnsResolverService
    {
        private static LookupClientOptions CreateLookupOptions(IPAddress dns)
        {
            LookupClientOptions lookupClientOptions = new LookupClientOptions()
            {
                ContinueOnDnsError = false,
                UseCache = false,
                UseTcpOnly = false,
                Timeout = TimeSpan.FromMilliseconds(DnsResolverSettings.TimeoutInMilliSeconds),
                ThrowDnsErrors = true,
                Retries = 0
            };
            return lookupClientOptions;
        }

        public async static Task<List<string>> ResolveHostAsync(IPAddress dns, Uri uri)
        {
            LookupClientOptions options = CreateLookupOptions(dns);
            LookupClient lookup = new LookupClient(options);

            List<string> addresses = new List<string>();
            string hostname = uri.Host;
            try
            {
                // example.com
                var response = await lookup.QueryAsync(hostname.Substring(hostname.IndexOf("www.")), QueryType.A);
                addresses.AddRange(response.Answers.OfType<ARecord>().Select(x => x.Address.ToString()));
            }
            catch (DnsResponseException)
            {
                // www.example.com
                var response = await lookup.QueryAsync(hostname, QueryType.A);
                addresses.AddRange(response.Answers.OfType<ARecord>().Select(x => x.Address.ToString()));
            }

            return addresses;
        }
    }
}
