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
using Newtonsoft.Json.Bson;

namespace _403unlockerLibrary
{
    public class JsonHandler
    {
        public static async Task<List<T>> ReadJson<T>(string path, bool isEncryptToByte)
        {
            if (File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Length != 0)
                {
                    if (isEncryptToByte)
                    {
                        string text = "";
                        using (StreamReader sr = new StreamReader(path))
                        {
                            text = await sr.ReadToEndAsync();
                        }

                        List<T> result = new List<T>();
                        string[] lines = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string line in lines)
                        {
                            result.Add(DecryptByte<T>(line));
                        }
                        return result;
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(path))
                        {
                            string jsonText = await sr.ReadToEndAsync();

                            List<T> result = JsonConvert.DeserializeObject<List<T>>(jsonText);
                            return result;
                        }
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

        public static async Task WriteJson<T>(string path, List<T> data, bool append, bool encryptToByte)
        {
            string text = "";
            if (encryptToByte)
            {
                List<string> lines = new List<string>();
                foreach (T element in data)
                {
                    lines.Add(Encrypt(element));
                }
                text = string.Join("\r\n", lines);
                
            }
            else
            {
                string serializedData = data.Count == 0 ? "" : JsonConvert.SerializeObject(data, Formatting.Indented);
                text = serializedData;
                //File.WriteAllText(path, serializedData);
            }

            using (StreamWriter sw = new StreamWriter(path, append))
            {
                await sw.WriteLineAsync(text);
            }
        }

        public static async Task WriteJson<T>(string path, T data, bool append,bool encryptToByte)
        {
            List<T> l = new List<T> { data };
            await WriteJson(path, l, append, encryptToByte);
        }

        private static T DecryptByte<T>(string stringByte)
        {
            byte[] bytes = Convert.FromBase64String(stringByte);

            using (MemoryStream ms = new MemoryStream(bytes))
            using (BsonDataReader bdr = new BsonDataReader(ms))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<T>(bdr);
            }
        }

        private static string Encrypt<T>(T data)
        {
            using (MemoryStream ms = new MemoryStream())
            using (BsonDataWriter bdw = new BsonDataWriter(ms))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(bdw, data, typeof(T));

                byte[] bytes = ms.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
