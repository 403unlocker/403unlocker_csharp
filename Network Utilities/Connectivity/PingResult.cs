using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_Utilities.Connectivity
{
    public class PingResult
    {
        public int Sent { get; internal set; }
        public int Received { get; internal set; }
        public double Latency { get; internal set; }
        public double PacketLoss
        {
            get
            {
                double lossPercentage = (Sent - Received) / (double)Sent;
                return lossPercentage * 100;
            }
        }
    }
}
