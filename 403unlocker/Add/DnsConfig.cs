using _403unlocker.Ping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace _403unlocker.Add
{
    public class DnsConfig
    {
        public virtual string Name { get; set; } = "";
        public virtual string DNS { get; set; } = "";

        public DnsConfig()
        {
        }

        public DnsConfig(DnsBenchmark dnsBenchmark)
        {
            Name = dnsBenchmark.Name;
            DNS = dnsBenchmark.DNS;
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
            if (string.IsNullOrWhiteSpace(dns)) return false;

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

        public static async Task<List<DnsConfig>> ReadJson(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"File dosen't exist");

            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Length == 0) throw new FileLoadException($"Can't load file");

            using (StreamReader sr = new StreamReader(path))
            {
                string jsonText = await sr.ReadToEndAsync();

                List<DnsConfig> result = JsonConvert.DeserializeObject<List<DnsConfig>>(jsonText);
                if (result is null) throw new NoNullAllowedException("Data is null");
                return result;
            }
        }

        public static void WriteJson(List<DnsConfig> data, string path)
        {
            string serializedData = data.Count == 0 ? "" : JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(path, serializedData);
        }

        public static List<DnsBenchmark> ConvertToDnsBenchmark(List<DnsConfig> dnsConfigs)
        {
            return dnsConfigs.Select(config => new DnsBenchmark(config)).ToList();
        }
    }
}
