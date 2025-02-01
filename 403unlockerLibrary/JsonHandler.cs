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

        public static async Task WriteJson<T>(string path, List<T> data, bool encryptToByte)
        {
            if (encryptToByte)
            {
                List<string> texts = new List<string>();
                foreach (T element in data)
                {
                    texts.Add(Encrypt(element));
                }

                using (StreamWriter sw = new StreamWriter(path))
                {
                    await sw.WriteLineAsync(string.Join("\r\n", texts));
                }
            }
            else
            {
                string serializedData = data.Count == 0 ? "" : JsonConvert.SerializeObject(data, Formatting.Indented);
                using (StreamWriter sw = new StreamWriter(path))
                {
                    await sw.WriteLineAsync(serializedData);
                }
                //File.WriteAllText(path, serializedData);
            }
        }

        private static T DecryptByte<T>(string stringByte)
        {
            byte[] bytes = Convert.FromBase64String(stringByte);

            MemoryStream ms = new MemoryStream(bytes);
            using (BsonDataReader bsonDataReader = new BsonDataReader(ms))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<T>(bsonDataReader);
            }
        }

        private static string Encrypt<T>(T data)
        {
            MemoryStream ms = new MemoryStream();
            using (BsonDataWriter bsonDataWriter = new BsonDataWriter(ms))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(bsonDataWriter, data);
            }
            string serializedData = Convert.ToBase64String(ms.ToArray());
            return serializedData;
        }
    }
}
