using System.Net;

namespace Network_Utilities.Bypass_Testing
{
    public class BypassResult
    {
        public double Latency { get; internal set; }
        public HttpStatusCode Status { get; internal set; }
        public string[] HttpResponse { get; internal set; }
    }
}
