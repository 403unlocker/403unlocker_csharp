using DnsClient;
using Network_Utilities.DNS_Testing.Resolver;
using Network_Utilities.Http_Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
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
        private static Stopwatch stopwatch = new Stopwatch();

        private static string HttpRequestHeaders(string uri)
        {
            List<string> httpRequestHeader = new List<string>();
            httpRequestHeader.Add("GET / HTTP/1.1");
            httpRequestHeader.Add($"Host: {uri}");
            httpRequestHeader.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:138.0) Gecko/20100101 Firefox/138.0");
            httpRequestHeader.Add("Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            httpRequestHeader.Add("Accept-Language: en-US,en;q=0.5");
            httpRequestHeader.Add("Connection: close");
            httpRequestHeader.Add("\r\n");

            return string.Join("\r\n", httpRequestHeader);
        }

        private async static Task<TcpClient> ConnectTcpAsync(TcpClient tcp, IPAddress[] resolvedIP, int port, CancellationToken cancellationToken)
        {
            Task tcpHandshake = tcp.ConnectAsync(resolvedIP, port);
            if (await Task.WhenAny(tcpHandshake, Task.Delay(5000, cancellationToken)) != tcpHandshake)
            {
                tcp.Close();
                throw new TimeoutException("TCP handshake timeout");
            }
            return tcp;
        }

        private async static Task AuthenticateTlsAsync(SslStream ssl, string uri, CancellationToken cancellationToken)
        {
            Task tlsHandshake = ssl.AuthenticateAsClientAsync(uri);
            if (await Task.WhenAny(tlsHandshake, Task.Delay(5000, cancellationToken)) != tlsHandshake)
            {
                ssl.Close();
                throw new TimeoutException("TLS handshaked timeout");
            }
        }

        private async static Task SendHttpRequestAsync(SslStream ssl, string uri, CancellationToken cancellationToken)
        {
            byte[] requestBytes = Encoding.ASCII.GetBytes(HttpRequestHeaders(uri));
            Task writeRequestTask = ssl.WriteAsync(requestBytes, 0, requestBytes.Length);
            if (await Task.WhenAny(writeRequestTask, Task.Delay(2000, cancellationToken)) != writeRequestTask)
            {
                ssl.Close();
                throw new TimeoutException("Timed out while sending the HTTP request");
            }

            Task flushStreamTask = ssl.FlushAsync();
            if (await Task.WhenAny(flushStreamTask, Task.Delay(2000, cancellationToken)) != flushStreamTask)
            {
                ssl.Close();
                throw new TimeoutException("Timedout while flushing the TLS stream");
            }
        }

        private async static Task<string> ReadHttpResponseAsync(SslStream sslStream, CancellationToken cancellationToken)
        {
            using (StreamReader reader = new StreamReader(sslStream, Encoding.UTF8))
            {
                Task<string> readResponseTask = reader.ReadToEndAsync();
                if (await Task.WhenAny(readResponseTask, Task.Delay(2000, cancellationToken)) != readResponseTask)
                {
                    reader.Close();
                    throw new TimeoutException("Timed out while reading the HTTP response");
                }
                return await readResponseTask;
            }
        }

        private async static Task<string> GetHttpResponseAsync(IPAddress[] resolvedIP, string uri, int port, CancellationToken cancellationToken)
        {
            BypassResult bypassResult = new BypassResult();
            
            using (TcpClient tcp = new TcpClient())
            {
                await ConnectTcpAsync(tcp, resolvedIP, port, cancellationToken); // TCP Connection

                using (SslStream ssl = new SslStream(tcp.GetStream(), false, (sender, cert, chain, errors) => true)) // Create TLS/SSL stream
                {
                    await AuthenticateTlsAsync(ssl, uri, cancellationToken); // TLS Handshake + SNI
                    await SendHttpRequestAsync(ssl, uri, cancellationToken);
                    string response = await ReadHttpResponseAsync(ssl, cancellationToken); // Read HTTP response

                    return response;
                }
            }
        }

        private static BypassResult SetValues(string httpResponse)
        {
            BypassResult bypassResult = new BypassResult();

            bypassResult.HttpResponse = httpResponse.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            bypassResult.Status = (HttpStatusCode)int.Parse(bypassResult.HttpResponse[0].Split(' ')[1]);
            return bypassResult;
        }

        private static async Task<IPAddress[]> ResolveOrThrow(IPAddress dns, string uri)
        {
            ResolverResult result = await ResolverService.ResolveHostAsync(dns, uri);

            if (result.IPv4.Length == 0) throw new InvalidDataException(result.Status.ToString().Replace('_', ' '));
            return result.IPv4;
        }

        public async static Task<BypassResult> BypassTestAsync(IPAddress dns, string uri, int port, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            stopwatch.Restart();

            IPAddress[] resolvedIPs = await ResolveOrThrow(dns, uri);
            string response = await GetHttpResponseAsync(resolvedIPs, uri, port, cancellationToken);
            BypassResult bypassResult = SetValues(response);

            while (300 < (int)bypassResult.Status && (int)bypassResult.Status < 399)
            {
                string location = bypassResult.HttpResponse.First(httpHeader => httpHeader.StartsWith("Location:", StringComparison.OrdinalIgnoreCase));
                string redirectionUri = location.Replace("Location: ", "");
                if (Uri.TryCreate(redirectionUri, UriKind.Absolute, out Uri result) &&
                   (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps)
                   )
                {
                    redirectionUri = result.Host;
                    IPAddress[] redirectionResolvedIPs = await ResolveOrThrow(dns, redirectionUri);
                    string redirectionResponse = await GetHttpResponseAsync(redirectionResolvedIPs, redirectionUri, port, cancellationToken);
                    bypassResult = SetValues(redirectionResponse);
                }
                else throw new UriFormatException("Invalid redirection URI format");
            }

            stopwatch.Stop();
            
            bypassResult.Latency = stopwatch.ElapsedMilliseconds;
            return bypassResult;
        }
    }
}
