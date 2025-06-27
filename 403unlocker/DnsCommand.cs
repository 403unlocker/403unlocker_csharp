using _403unlocker.Notification;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _403unlocker
{
    internal class DnsCommand
    {
        private async static Task<Dictionary<string, string>> Run(string command)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
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

                dict["output"] = output;
                dict["error"] = error;
                return dict;
            }
            catch (Exception ex)
            {
                dict["output"] = "";
                dict["error"] = ex.Message;
                return dict;
            }
        }

        public async static void SetDnsAsPrimary(string adaptorName, string PrimaryDNS)
        {
            Dictionary<string, string> response = await Run($"netsh interface ip add dns name=\"{adaptorName}\" {PrimaryDNS} index=1");
            Respond(response, $"{PrimaryDNS} has been set as primary\non \"{adaptorName}\" adaptor DNS setting");
        }

        public async static void SetDnsAsSecondary(string adaptorName, string SecondaryDns)
        {
            Dictionary<string, string> response = await Run($"netsh interface ip add dns name=\"{adaptorName}\" {SecondaryDns} index=2");
            Respond(response, $"{SecondaryDns} has been set secondary\non \"{adaptorName}\" adaptor DNS setting");
        }

        public async static void Reset(string adaptorName)
        {
            Dictionary<string, string> response = await Run($"netsh interface ip set dns name=\"{adaptorName}\" source=dhcp");
            Respond(response, $"\"{adaptorName}\" adaptor DNS setting has been reset");
        }

        private static void Respond(Dictionary<string, string> dict, string action)
        {
            string text, caption = "Successful";

            string output = dict["output"];
            string error = dict["error"];

            if (!string.IsNullOrEmpty(error))
            {
                text = error;
                caption = "Somthing went wrong";
            }
            else if (output != "\r\n" && !string.IsNullOrEmpty(output))
            {
                text = output;
                caption = "A Message form CMD";
            }
            else
            {
                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.LabelText = action;
                    form.Caption = caption;
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Information;
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }
                return;
            }
            using (MessageBoxForm form = new MessageBoxForm())
            {
                form.LabelText = text;
                form.Caption = caption;
                form.Buttons = MessageBoxButtons.OK;
                form.Picture = MessageBoxIcon.Error;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
            }
        }
    }
}
