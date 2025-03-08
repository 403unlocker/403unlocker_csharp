using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _403unlocker
{
    internal class DnsCommand
    {
        static string path = "cmd.log";
        private async static void Run(string command)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Verb = "runas",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = $"/c {command}",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                };

                string output, error;
                using (Process process = Process.Start(psi))
                {
                    using (StreamWriter sw = process.StandardInput)
                    {
                        if (sw.BaseStream.CanWrite)
                        {
                            sw.WriteLine(command);
                        }
                    }

                    output = await process.StandardOutput.ReadToEndAsync();
                    error = await process.StandardError.ReadToEndAsync();
                }

                List<CommandConfig> notificationStates = new List<CommandConfig>()
                {
                    new CommandConfig()
                    {
                        Command = "command",
                        Message = command
                    },
                    new CommandConfig()
                    {
                        Command = "output",
                        Message = output
                    },
                    new CommandConfig()
                    {
                        Command = "error",
                        Message = error
                    }
                };

                WriteJson(notificationStates, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }


        public static void SetDnsAsPrimary(string adaptorName, string PrimaryDNS)
        {
            Run($"netsh interface ip add dns name=\"{adaptorName}\" {PrimaryDNS} index=1");
        }

        public static void SetDnsAsSecondary(string adaptorName, string SecondaryDns)
        {
            Run($"netsh interface ip add dns name=\"{adaptorName}\" {SecondaryDns} index=2");
        }

        public static void Reset(string adaptorName)
        {
            Run($"netsh interface ip set dns name=\"{adaptorName}\" source=dhcp");
        }

        public static void WriteJson(List<CommandConfig> data, bool append)
        {
            string serializedData = data.Count == 0 ? "" : JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(path, serializedData);
        }
    }

    class CommandConfig
    {
        public string Command { get; set; }
        public string Message { get; set; }
    }
}
