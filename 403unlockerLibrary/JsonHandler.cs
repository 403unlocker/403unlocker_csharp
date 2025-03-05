using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace _403unlockerLibrary
{
    public class JsonHandler
    {
        public static async Task<List<T>> ReadJson<T>(string path)
        {
            if (File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Length != 0)
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string jsonText = await sr.ReadToEndAsync();

                        List<T> result = JsonConvert.DeserializeObject<List<T>>(jsonText);
                        return result;
                    }
                }
                else
                {
                    throw new FileLoadException($"Can't load file at {path}");
                }
            }
            else
            {
                throw new FileNotFoundException($"File dosen't exist at {path}");
            }
        }

        public static async Task WriteJson<T>(string path, List<T> data, bool append)
        {
            string text = "";

            string serializedData = data.Count == 0 ? "" : JsonConvert.SerializeObject(data, Formatting.Indented);
            text = serializedData;
            //File.WriteAllText(path, serializedData);


            using (StreamWriter sw = new StreamWriter(path, append))
            {
                await sw.WriteLineAsync(text);
            }
        }

        public static async Task WriteJson<T>(string path, T data, bool append)
        {
            List<T> l = new List<T> { data };
            await WriteJson(path, l, append);
        }

    }
}
