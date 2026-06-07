using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Network_Utilities.Http_Service
{
    public static class HttpService
    {
        private static readonly HttpClientHandler handler = new HttpClientHandler()
        {
            UseCookies = false,
            AllowAutoRedirect = true,
        };

        public async static Task<HttpResponseMessage> SendRequestAsync(Uri uri)
        {
            // url = https://103.10.4.227:443
            // hostname = asus.com
            using (HttpClient client = new HttpClient(handler))
            {
                client.Timeout = TimeSpan.FromMilliseconds(HttpSettings.HttpRequestTimeout);

                // content to accept in response
                client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

                // OS, browser version, html layout rendering engine
                client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
                client.DefaultRequestHeaders.Host = uri.Host;
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:138.0) Gecko/20100101 Firefox/138.0");
                return await client.GetAsync(uri);
            }
        }
    }
}
