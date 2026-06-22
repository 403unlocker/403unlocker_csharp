
namespace Network_Utilities.Ping
{
    public static class PingSettings
    {
        public static int PacketCount { get; set; } = 1;
        public static ushort PacketSize { get; set; } = 32;
        public static int TimeoutInMiliSeconds { get; set; } = 1000;
    }
}
