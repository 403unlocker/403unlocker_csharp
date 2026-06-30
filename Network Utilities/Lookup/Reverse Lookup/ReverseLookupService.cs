using DnsClient;
using DnsClient.Protocol;
using Network_Utilities.Lookup.Forward_Lookup;
using Network_Utilities.Ping;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Network_Utilities.Lookup.Reverse_Lookup
{
    public class ReverseLookupService
    {
        private static LookupClientOptions CreateLookupOptions()
        {
            LookupClientOptions lookupClientOptions = new LookupClientOptions()
            {
                ContinueOnDnsError = false,
                UseCache = false,
                UseTcpOnly = false,
                Timeout = TimeSpan.FromMilliseconds(ReverseLookupSettings.TimeoutInMilliSeconds),
                ThrowDnsErrors = true,
                Retries = 0
            };
            return lookupClientOptions;
        }

        public async static Task<ReverseLookupResult> ReverseLookupHostAsync(IPAddress ip)
        {
            return await ReverseLookupHostAsync(ip, CancellationToken.None);
        }

        public async static Task<ReverseLookupResult> ReverseLookupHostAsync(IPAddress ip, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            LookupClientOptions options = CreateLookupOptions();
            LookupClient client = new LookupClient(options);

            ReverseLookupResult reverseLookupResult = new ReverseLookupResult();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                IDnsQueryResponse result = await client.QueryReverseAsync(ip);

                var ptr = result.Answers.PtrRecords().FirstOrDefault();

                if (!(ptr is null))
                {
                    reverseLookupResult.Hostname = ptr.PtrDomainName.Value;
                    reverseLookupResult.Status = ReverseLookupResult.ReverseLookupStatus.Successful;
                }

                reverseLookupResult.Status = ReverseLookupResult.ReverseLookupStatus.No_hostname_returned;
            }
            catch (DnsResponseException error)
            {
                if (error.InnerException is null)
                {
                    reverseLookupResult.Status = ReverseLookupResult.ReverseLookupStatus.Non_existent_domain;

                }
                else if (error.InnerException is OperationCanceledException)
                {
                    reverseLookupResult.Status = ReverseLookupResult.ReverseLookupStatus.Timeout;
                }
                else throw error;
            }
            stopwatch.Stop();

            reverseLookupResult.Latency = stopwatch.ElapsedMilliseconds;
            return reverseLookupResult;
        }
    }
}
