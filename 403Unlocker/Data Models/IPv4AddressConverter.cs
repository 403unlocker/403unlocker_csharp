using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _403Unlocker.Data_Models
{
    public class IPv4AddressConverter : JsonConverter<IPAddress>
    {
        public override IPAddress ReadJson(JsonReader reader, Type objectType, IPAddress existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (!IPAddress.TryParse((string)reader.Value, out IPAddress ip) || ip.AddressFamily != AddressFamily.InterNetwork)
            {
                throw new JsonSerializationException($"The IP address ({ip}) is invalid or unsupported.\nOnly IPv4 addresses can be imported");
            }
            return ip;
        }

        public override void WriteJson(JsonWriter writer, IPAddress value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
