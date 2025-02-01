using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _403unlockerLibrary
{
    public class JsonHandler
    {
        public static async Task<List<T>> ReadJson<T>(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    string jsonText = await streamReader.ReadToEndAsync();
                    if (!string.IsNullOrEmpty(jsonText))
                    {
                        List<T> previousList = JsonConvert.DeserializeObject<List<T>>(jsonText);
                        return previousList;
                    }
                    else
                    {
                        throw new FileLoadException($"Can't load file at {path}");
                    }
                }
            }
            else
            {
                throw new FileNotFoundException($"File dosen't exist at {path}");
            }
        }

        public static void WriteJson<T>(string path, List<T> data)
        {
            string jsontext = data.Count == 0 ? "" : JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(path, jsontext);
        }
    }
}
