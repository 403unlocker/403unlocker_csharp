using _403Unlocker.Data_Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Resources.ResXFileRef;

namespace _403Unlocker.File
{
    internal static class FileManager
    {
        private static readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            Converters =
            {
                new IPAddressConverter()
            }
        };

        public async static Task<string> ReadTextAsync(string path)
        {
            string text;
            using (StreamReader streamReader = new StreamReader(path))
            {
                text = await streamReader.ReadToEndAsync();
            }
            return text;
        }

        public async static Task WriteTextAsync(string path, string text)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                await streamWriter.WriteAsync(text);
            }
        }

        public async static Task<T> ReadJsonAsync<T>(string path)
        {
            string json = await ReadTextAsync(path);
            T result = JsonConvert.DeserializeObject<T>(json, jsonSettings);
            return result;
        }

        public async static Task WriteJsonAsync(string path, object value)
        {
            string json = JsonConvert.SerializeObject(value, Formatting.Indented, jsonSettings);
            await WriteTextAsync(path, json);
        }
    }
}
