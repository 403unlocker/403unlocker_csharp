using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Network_Utilities.Http_Service
{
    public class HttpResult
    {
        public HttpResponseMessage HttpResponseMessage { get; internal set; }
        public string HttpResponseContent { get; internal set; }

        public double Latency { get; internal set; }
        public bool IsSuccessful { get => HttpResponseMessage.IsSuccessStatusCode; }

        internal async Task<string> ReadHtmlResponseAsync()
        {
            using (Stream stream = await HttpResponseMessage.Content.ReadAsStreamAsync())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}