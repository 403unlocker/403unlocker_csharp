
namespace Network_Utilities.Connectivity
{
    internal class ConnectivitySettings
    {
        public static int PacketCount { get; set; } = 1;
        public static ushort PacketSize { get; set; } = 32;
        public static int TimeoutInMiliSeconds { get; set; } = 3000;
    }
}
