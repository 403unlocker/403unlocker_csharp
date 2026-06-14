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
        public enum HttpStatus
        {
            UnknownError = 0,

            Successful = 1,

            HttpConnectionError = 400,
            HttpConnectionFailed = 401,
            HttpConnectionTimedOut = 402,
            HttpConnectionForbidden = 403,
            HttpConnectionNotFound = 404,
            HttpConnectionClosedByServer = 405
        }

        public HttpResponseMessage HttpResponseMessage { get; internal set; }

        public double Latency { get; internal set; }

        public HttpStatusCode Status { get => HttpResponseMessage.StatusCode; }
        public bool IsSuccessful { get => HttpResponseMessage.IsSuccessStatusCode; }

        public async Task<string> ReadHtmlResponseAsync()
        {
            using (Stream stream = await HttpResponseMessage.Content.ReadAsStreamAsync())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}