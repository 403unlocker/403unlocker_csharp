using DnsClient;
using Network_Utilities.DNS_Testing.Resolver;
using Network_Utilities.Http_Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Network_Utilities.DNS_Testing.ByPass
{
    public static class BypassService
    {
        private static string HttpRequestHeader(Uri uri)
        {
            return $"GET / HTTP/1.1\r\n" +
                   $"Host: {uri.Host}\r\n" +
                   "User-Agent: Mozilla/5.0\r\n" +
                   "Accept: text/html\r\n" +
                   "Connection: close\r\n\r\n";
        }

        public async static Task<BypassResult> SendRequestAsync(IPAddress dns, Uri uri, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            BypassResult bypassResult = new BypassResult();
            DateTime now;
            DateTime end;

            using (TcpClient tcp = new TcpClient())
            {
                now = DateTime.Now;

                ResolverResult resolverResult = await ResolverService.ResolveHostAsync(dns, uri);
                await tcp.ConnectAsync(resolverResult.IPv4.First(), uri.Port); // TCP Handshake

                using (SslStream ssl = new SslStream(tcp.GetStream(), false, (sender, cert, chain, errors) => true)) // Create TLS/SSL stream
                {
                    
                    await ssl.AuthenticateAsClientAsync(uri.Host); // TLS Handshake + SNI

                    byte[] requestBytes = Encoding.ASCII.GetBytes(HttpRequestHeader(uri));

                    await ssl.WriteAsync(requestBytes, 0, requestBytes.Length); // HTTP request goes over TLS
                    await ssl.FlushAsync();
                    end = DateTime.Now;
                    bypassResult.Latency = (end - now).TotalMilliseconds;
                    bypassResult.SslResponseMessage = ssl;
                    bypassResult.Status = BypassResult.BypassStatus.Successful;
                }
            }
            return bypassResult;
        }
    }
}
