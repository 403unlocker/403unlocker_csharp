
namespace Network_Utilities.Bypass_Testing
{
    public static class BypassSettings
    {
        public static int TcpConnectTimeoutInMilliSeconds { get; set; } = 5000;
        public static int TlsHandshakeTimeoutInMilliSeconds { get; set; } = 5000;
        public static int HttpWriteTimeoutInMilliSeconds { get; } = 2000;
        public static int HttpFlushTimeoutInMilliSeconds { get; } = 2000;
        public static int HttpReadTimeoutInMilliSeconds { get; } = 2000;
    }
}
