using DnsClient;
using Network_Utilities.Http_Service;
using Network_Utilities.Lookup.Forward_Lookup;
using Network_Utilities.Ping;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Network_Utilities.Bypass_Testing
{
    public static class BypassService
    {
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
            if (await Task.WhenAny(tcpHandshake, Task.Delay(BypassSettings.TcpConnectTimeoutInMilliSeconds, cancellationToken)) != tcpHandshake)
            {
                tcp.Close();
                throw new TimeoutException("TCP handshake timeout");
            }
            return tcp;
        }

        private async static Task AuthenticateTlsAsync(SslStream ssl, string uri, CancellationToken cancellationToken)
        {
            Task tlsHandshake = ssl.AuthenticateAsClientAsync(uri);
            if (await Task.WhenAny(tlsHandshake, Task.Delay(BypassSettings.TlsHandshakeTimeoutInMilliSeconds, cancellationToken)) != tlsHandshake)
            {
                ssl.Close();
                throw new TimeoutException("TLS handshaked timeout");
            }
        }

        private async static Task SendHttpRequestAsync(SslStream ssl, string uri, CancellationToken cancellationToken)
        {
            byte[] requestBytes = Encoding.ASCII.GetBytes(HttpRequestHeaders(uri));
            Task writeRequestTask = ssl.WriteAsync(requestBytes, 0, requestBytes.Length);
            if (await Task.WhenAny(writeRequestTask, Task.Delay(BypassSettings.HttpWriteTimeoutInMilliSeconds, cancellationToken)) != writeRequestTask)
            {
                ssl.Close();
                throw new TimeoutException("Timed out while sending the HTTP request");
            }

            Task flushStreamTask = ssl.FlushAsync();
            if (await Task.WhenAny(flushStreamTask, Task.Delay(BypassSettings.HttpFlushTimeoutInMilliSeconds, cancellationToken)) != flushStreamTask)
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
                if (await Task.WhenAny(readResponseTask, Task.Delay(BypassSettings.HttpReadTimeoutInMilliSeconds, cancellationToken)) != readResponseTask)
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

        private static void SetValues(BypassResult bypassResult, string httpResponse)
        {
            bypassResult.HttpResponse = httpResponse.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            bypassResult.Status = (HttpStatusCode)int.Parse(bypassResult.HttpResponse[0].Split(' ')[1]);
        }

        private static async Task<IPAddress[]> ResolveOrThrow(IPAddress dns, string uri, CancellationToken cancellationToken)
        {
            ForwardLookupResult forwardLookupResult = await ForwardLookupService.ForwardLookupHostAsync(dns, uri, cancellationToken);

            if (forwardLookupResult.IPv4.Length == 0) throw new InvalidDataException(forwardLookupResult.Status.ToString().Replace('_', ' '));
            return forwardLookupResult.IPv4;
        }

        public async static Task<BypassResult> BypassTestAsync(IPAddress dns, string uri, int port)
        {
            return await BypassTestAsync(dns, uri, port, CancellationToken.None);
        }

        public async static Task<BypassResult> BypassTestAsync(IPAddress dns, string uri, int port, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            BypassResult bypassResult = new BypassResult();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            IPAddress[] resolvedIPs = await ResolveOrThrow(dns, uri, cancellationToken);
            string response = await GetHttpResponseAsync(resolvedIPs, uri, port, cancellationToken);
            SetValues(bypassResult, response);

            while (300 <= (int)bypassResult.Status && (int)bypassResult.Status < 400)
            {
                string location = bypassResult.HttpResponse.First(httpHeader => httpHeader.StartsWith("Location:", StringComparison.OrdinalIgnoreCase));
                string redirectionUri = location.Replace("Location: ", "");
                if (Uri.TryCreate(redirectionUri, UriKind.Absolute, out Uri result) &&
                   (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps)
                   )
                {
                    redirectionUri = result.Host;

                    IPAddress[] redirectionResolvedIPs = await ResolveOrThrow(dns, redirectionUri, cancellationToken);
                    string redirectionResponse = await GetHttpResponseAsync(redirectionResolvedIPs, redirectionUri, port, cancellationToken);
                    SetValues(bypassResult, redirectionResponse);
                }
                else throw new UriFormatException("Invalid redirection URI format");
            }
            stopwatch.Stop();

            bypassResult.Latency = stopwatch.ElapsedMilliseconds;
            return bypassResult;
        }
    }
}
