using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _403unlocker.ByPass
{
    public class UrlConfig
    {
        private static string path = "Url.json";
        public string Name { get; set; } = "";

        private string url;
        public string URL
        {
            get => url;
            set
            {
                if (!IsValidUrl(value))
                {
                    throw new UriFormatException();
                }

                url = value;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is UrlConfig urlConfig)
            {
                return url == urlConfig.url;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return url.GetHashCode();
        }

        public static bool IsValidUrl(string hostname)
        {
            if (Regex.IsMatch(hostname, @"^(www.)?([^\W_]{1}[a-zA-Z\d\-]*){1}(\.[^\W_]{1}[a-zA-Z\d\-]*){0,60}(\.[a-z]+){1}(/[\w\W]*)*$")) return true;
            return false;
        }

        public static async Task<List<UrlConfig>> ReadJson()
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"File dosen't exist");

            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Length == 0) throw new FileLoadException($"Can't load file");

            using (StreamReader sr = new StreamReader(path))
            {
                string jsonText = await sr.ReadToEndAsync();

                List<UrlConfig> result = JsonConvert.DeserializeObject<List<UrlConfig>>(jsonText);
                return result;
            }
        }

        public static void WriteJson(List<UrlConfig> data)
        {
            string serializedData = data.Count == 0 ? "" : JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(path, serializedData);
        }
    }
}
