using DnsClient.Protocol;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Network_Utilities.Http_Service
{
    public static class HttpService
    {
        private static HttpClientHandler CreateHttpHandler()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                UseCookies = false,
                AllowAutoRedirect = true,
            };
            return handler;
        }

        private async static Task SetValues(HttpResult httpResult, HttpResponseMessage httpRequestMessage)
        {
            httpResult.HttpResponseMessage = httpRequestMessage;
            httpResult.HttpResponseContent = await httpResult.ReadHtmlResponseAsync();
        }

        public async static Task<HttpResult> SendRequestAsync(Uri uri)
        {
            HttpResult httpResult = new HttpResult();

            Stopwatch stopwatch = new Stopwatch();

            using (HttpClient client = new HttpClient(CreateHttpHandler()))
            {
                client.Timeout = TimeSpan.FromMilliseconds(HttpSettings.TimeoutInMilliseconds);
                
                // content to accept in response
                client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

                // OS, browser version, html layout rendering engine
                client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
                client.DefaultRequestHeaders.Host = uri.Host;
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:151.0) Gecko/20100101 Firefox/151.0");

                stopwatch.Start();
                HttpResponseMessage httpResponseMessage = await client.GetAsync(uri);
                stopwatch.Stop();

                await SetValues(httpResult, httpResponseMessage);

                httpResult.Latency = stopwatch.ElapsedMilliseconds;
                return httpResult;
            }
        }
    }
}
