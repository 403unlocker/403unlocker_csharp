using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _403Unlocker.Data_Models
{
    public class DnsConfig
    {
        [JsonProperty("Version")]
        public string Version { get; } = "2.0";
        [JsonProperty("IPv4_Servers")]
        public List<DnsInfo> IPv4_Servers { get; set; }

        [JsonConstructor]
        public DnsConfig(List<DnsInfo> dnsInfos)
        {
            IPv4_Servers = dnsInfos;
        }
    }
}
