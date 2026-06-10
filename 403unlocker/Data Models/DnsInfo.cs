using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _403Unlocker.Data_Models
{
    public class DnsInfo
    {
        private IPAddress ipv4;
        private string provider;

        [JsonConverter(typeof(IPAddressConverter))]
        [JsonProperty("IPv4")]
        public IPAddress IPv4 { get => ipv4; set => ipv4 = value; }

        [JsonProperty("Provider")]
        public string Provider { get => provider; set => provider = value; }

        [JsonProperty("Latency")]
        public string Latency { get; set; }

        [JsonProperty("PacketLoss")]
        public string PacketLoss { get; set; }

        [JsonProperty("ByPass")]
        public string ByPass { get; set; }

        [JsonConstructor]
        public DnsInfo(IPAddress ipv4, string provider)
        {
            this.ipv4 = ipv4;
            this.provider = provider;
        }


        public override string ToString()
        {
            return ipv4.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is DnsInfo dnsInfo)
            {
                return ipv4 == dnsInfo.ipv4;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ipv4.GetHashCode();
        }
    }
}
