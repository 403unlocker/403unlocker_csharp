using _403unlocker.Ping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace _403unlocker.Add
{
    public class DnsConfig
    {
        public virtual string Name { get; set; } = "";
        public virtual string DNS { get; set; } = "";

        public DnsConfig()
        {
        }

        public DnsConfig(string name, string dns)
        {
            Name = name;
            DNS = dns;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is DnsConfig dnsConfig)
            {
                return DNS == dnsConfig.DNS;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return DNS.GetHashCode();
        }

        public static bool IsIPv4(string dns)
        {
            var octets = dns.Split(new char[] { '.' });
            if (octets.Length == 4)
            {
                // converts octets string to int
                bool isOctetsValid = octets.Select(x => int.Parse(x))
                                     // checks are all between 0 to 255
                                     .All(x => 0 <= x && x <= 255); ;
                return isOctetsValid;
            }
            return false;
        }

        public static List<DnsBenchmark> ConvertTo(List<DnsConfig> dnsConfigs)
        {
            return dnsConfigs.Select(x => new DnsBenchmark(x.Name, x.DNS)).ToList();
        }
    }
}
