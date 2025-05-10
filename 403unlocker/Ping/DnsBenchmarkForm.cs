using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using _403unlocker.Config;
using System.Security.Cryptography;
using _403unlocker.Add;
using System.Diagnostics;
using static _403unlocker.Data;
using System.IO;
using _403unlocker.Notification;
using _403unlocker.ByPass_Url;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Collections.Specialized.BitVector32;
using _403unlocker.Ping.Search_Dns_Name;
using System.Runtime.InteropServices;

namespace _403unlocker.Ping
{
    public partial class DnsBenchmarkForm : Form
    {
        private BindingList<DnsBenchmark> dnsBinding = new BindingList<DnsBenchmark>();

        public DnsBenchmarkForm()
        {
            InitializeComponent();
        }

        private async void DnsPingForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<DnsBenchmark> previousList = await DnsBenchmark.ReadJson();
                dnsBinding = new BindingList<DnsBenchmark>(previousList);
            }
            catch (Exception)
            {
                // When json text is not valid to json
                // Do Nothing
            }
            dataGridView1.DataSource = dnsBinding;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            

            try
            {
                bool isSuccesful = Settings.Read();
                if (!isSuccesful) throw new Exception("Reading config file, failed");
            }
            catch (Exception error)
            {
                if (error is FileNotFoundException)
                {
                    using (MessageBoxForm form = new MessageBoxForm())
                    {
                        form.Title = "Config File, Not Found";
                        form.Caption = error.Message;
                        form.Buttons = MessageBoxButtons.OK;
                        form.Picture = MessageBoxIcon.Error;
                        form.ShowDialog();
                    }
                }
                else
                {
                    using (MessageBoxForm form = new MessageBoxForm())
                    {
                        form.Title = error.Message;
                        form.Caption = "Error Occurred";
                        form.Buttons = MessageBoxButtons.OK;
                        form.Picture = MessageBoxIcon.Error;
                        form.ShowDialog();
                    }
                }

                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.Title = "App is using default Settings...";
                    form.Caption = "No Worries";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Information;
                    form.ShowDialog();
                }
            }
        }

        private void DnsPingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DnsBenchmark.WriteJson(dnsBinding.ToList());

            try
            {
                bool isSuccesful = Settings.Write();
                if (!isSuccesful) throw new Exception("App couldn't save config file!");
            }
            catch (Exception error)
            {
                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.Title = error.Message;
                    form.Caption = "Saving Config File, Failed";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Error;
                    form.ShowDialog();
                }
            }
        }

        private void pcPingButton_Click(object sender, EventArgs e)
        {
            var pingList = new List<DnsBenchmark>(dnsBinding);
            List<Task> tasks = pingList.Select(x => Task.Run(() => x.Ping())).ToList();

            using (MessageBoxProgress form = new MessageBoxProgress(tasks, Settings.Ping.PacketCount, Settings.Ping.TimeOutInMiliSeconds))
            {
                form.ShowDialog();
                dataGridView1.Invalidate();
            }
        }

        private async void sitePingButton_Click(object sender, EventArgs e)
        {
            string url = "";
            using (GetUrlForm form = new GetUrlForm())
            {
                form.ShowDialog();
                if (!form.isOkPressed)
                {
                    return;
                }
                url = form.comboBoxUrl.Text;
            }


            Uri uri = new Uri($"https://www.{url}");
            


            var pingList = new List<DnsBenchmark>()
            {
                new DnsBenchmark { Name = "8.8.8.8", DNS = "8.8.8.8" },
                new DnsBenchmark { Name = "8.8.4.4", DNS = "8.8.4.4" },
                new DnsBenchmark { Name = "185.55.226.26", DNS = "185.55.226.26" },
                new DnsBenchmark { Name = "178.22.122.100", DNS = "178.22.122.100" },
                new DnsBenchmark { Name = "9.9.9.9", DNS = "9.9.9.9" },
                new DnsBenchmark { Name = "5.202.100.100", DNS = "5.202.100.100" },
                new DnsBenchmark { Name = "5.202.100.101", DNS = "5.202.100.101" },
                new DnsBenchmark { Name = "185.51.200.2", DNS = "185.51.200.2" },
                new DnsBenchmark { Name = "1.1.1.1", DNS = "1.1.1.1" },
                new DnsBenchmark { Name = "1.0.0.1", DNS = "1.0.0.1" },
                new DnsBenchmark { Name = "10.202.10.11", DNS = "10.202.10.11" },
                new DnsBenchmark { Name = "10.202.10.10", DNS = "10.202.10.10" }
            };

            dnsBinding = new BindingList<DnsBenchmark>(pingList);
            dataGridView1.DataSource = dnsBinding;

            foreach (var item in dnsBinding)
            {
                await item.ByPass(uri);
            }

            //List<Task> tasks = dnsBinding.Select(x => Task.Run(() => x.ByPass(uri))).ToList();
            //using (MessageBoxProgress form = new MessageBoxProgress(tasks, 1, Settings.ByPass.DnsResolveTimeOutInMiliSeconds + Settings.ByPass.HttpRequestTimeOutInMiliSeconds))
            //{
            //    form.ShowDialog();
            //    dataGridView1.Invalidate();
            //}
        }

        private void buttonNsLookUp_Click(object sender, EventArgs e)
        {
            string url = "";
            using (GetUrlForm form = new GetUrlForm())
            {
                form.ShowDialog();
                if (!form.isOkPressed)
                {
                    return;
                }
                url = form.comboBoxUrl.Text;
            }

            Uri uri = new Uri($"https://www.{url}");

            List<Task> tasks = dnsBinding.Select(x => Task.Run(() => x.NsLookUp(uri))).ToList();
            using (MessageBoxProgress form = new MessageBoxProgress(tasks, 1, Settings.ByPass.DnsResolveTimeOutInMiliSeconds))
            {
                form.ShowDialog();
                dataGridView1.Invalidate();
            }
        }

        private void copyDnsCellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string selectedRowDns = dataGridView1.SelectedRows[0].Cells["DNS"].Value.ToString();
                try
                {
                    Clipboard.SetText(selectedRowDns);
                }
                catch (Exception)
                {
                    using (MessageBoxForm form = new MessageBoxForm())
                    {
                        form.Title = "Somthing went wrong!";
                        form.Caption = "Check your Clipboard\n" +
                                       "If it is not be copied, please try again";
                        form.Buttons = MessageBoxButtons.OK;
                        form.Picture = MessageBoxIcon.Error;
                        form.ShowDialog();
                    }
                }
            }
            else
            {
                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.Title = "Please select a row";
                    form.Caption = "Can't Get DNS Cell!";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Stop;
                    form.ShowDialog();
                }
            }
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            var valid = dnsBinding
                .Where(dns => dns.Latency >= 0)
                .OrderBy(dns => dns.Latency)
                .ThenBy(dns => dns.Status);

            var invalid = dnsBinding
                .Where(dns => dns.Latency == -1)
                .OrderBy(dns => dns.Status)
                .ThenBy(dns => dns.Name);

            List<DnsBenchmark> result = valid.Concat(invalid).ToList();

            dnsBinding = new BindingList<DnsBenchmark>(result);
            dataGridView1.DataSource = dnsBinding;
        }


        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm setting = new SettingsForm())
            {
                setting.ShowDialog();
            }
        }

        private void InvokeAddDns(List<DnsConfig> currentDns, List<DnsConfig> importedExternalDns)
        {
            using (DnsConfigForm form = new DnsConfigForm(currentDns, importedExternalDns))
            {
                Hide();
                form.ShowDialog();
                if (form.isApplyPressed && form.isTableChanged)
                {
                    List<DnsConfig> dnsEdited = form.dnsBinding.ToList();
                    List<DnsBenchmark> dnsConverted = DnsConfig.ConvertToDnsBenchmark(dnsEdited);
                    dnsBinding = new BindingList<DnsBenchmark>(dnsConverted);
                    dataGridView1.DataSource = dnsBinding;
                }
                Show();
            }
        }

        private void buttonAddDns_Click(object sender, EventArgs e)
        {
            List<DnsConfig> dns = DnsBenchmark.ConvertToDnsConfig(dnsBinding.ToList());
            InvokeAddDns(dns, null);
        }

        private async void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = openFileDialog1.ShowDialog();
            if (r == DialogResult.OK)
            {
                try
                {
                    List<DnsConfig> dnsCurrent = DnsBenchmark.ConvertToDnsConfig(dnsBinding.ToList());
                    List<DnsConfig> dnsSaved = await DnsConfig.ReadJson(openFileDialog1.FileName);
                    InvokeAddDns(dnsCurrent, dnsSaved);
                }
                catch (Exception)
                {
                    using (MessageBoxForm form = new MessageBoxForm())
                    {
                        form.Title = "Something went wrong!";
                        form.Caption = "Can't load file";
                        form.Buttons = MessageBoxButtons.OK;
                        form.Picture = MessageBoxIcon.Error;
                        form.ShowDialog();
                    }
                }
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = saveFileDialog1.ShowDialog();
            if (r == DialogResult.OK)
            {
                DnsConfig.WriteJson(DnsBenchmark.ConvertToDnsConfig(dnsBinding.ToList()), saveFileDialog1.FileName);
            }
        }

        private void codeSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string link = @"https://github.com/ALiMoradzade/403unlocker";
            Process.Start(link);
        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string link = @"https://www.403unlocker.ir";
            Process.Start(link);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDnsSet.Text = comboBoxDnsSet.SelectedItem as string;
        }

        private void buttonDnsSet_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 || string.IsNullOrEmpty(Settings.NetworkAdaptor.SelectedNetworkInterface))
            {
                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.Title = "Please select a row";
                    form.Caption = "Can't Read DNS";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Stop;
                    form.ShowDialog();
                }
            }

            string selectedDns = dataGridView1.SelectedRows[0].Cells["DNS"].Value.ToString();
            string selectedInterface = Settings.NetworkAdaptor.SelectedNetworkInterface;

            if (!selectedInterface.Contains("Ethernet") && !selectedInterface.ToLower().Contains("wifi"))
            {
                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.Title = $"You are setting {selectedDns} to \"{selectedInterface}\" adaptor!\n" +
                    "Are you sure?\n\n" +
                    "(Go to the Settings and select your desire adaptor)";
                    form.Caption = "Unexpected Recognition";
                    form.Buttons = MessageBoxButtons.YesNo;
                    form.Picture = MessageBoxIcon.Exclamation;
                    form.ShowDialog();
                    if (form.DialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            if (buttonDnsSet.Text == "Set as Primary")
            {
                DnsCommand.SetDnsAsPrimary(Settings.NetworkAdaptor.SelectedNetworkInterface, selectedDns);
            }
            else
            {
                DnsCommand.SetDnsAsSecondary(Settings.NetworkAdaptor.SelectedNetworkInterface, selectedDns);
            }
        }

        private void buttonResetDns_Click(object sender, EventArgs e)
        {
            DnsCommand.Reset(Settings.NetworkAdaptor.SelectedNetworkInterface);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            labelDnsCount.Text = "Count: " + dataGridView1.RowCount;
        }

        private void searchDNSNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SearchDnsNameForm searchDnsName = new SearchDnsNameForm())
            {
                searchDnsName.ShowDialog();
                if (searchDnsName.isOkPressed)
                {
                    DnsBenchmark dnsSearched = dnsBinding.First(x => x.DNS == searchDnsName.textBoxDns.Text);
                    int index = dnsBinding.IndexOf(dnsSearched);
                    if (index > 0)
                    {
                        dataGridView1.Rows[index].Selected = true;
                        if (!dataGridView1.Rows[index].Displayed)
                        {
                            dataGridView1.FirstDisplayedScrollingRowIndex = index;
                        }
                    }
                    else
                    {
                        using (MessageBoxForm messageBox = new MessageBoxForm())
                        {
                            messageBox.Title = $"\"{searchDnsName.textBoxDns.Text}\" is not in table";
                            messageBox.Caption = "DNS Not Found";
                            messageBox.Buttons = MessageBoxButtons.OK;
                            messageBox.Picture = MessageBoxIcon.Information;
                            messageBox.ShowDialog();
                        }
                    }
                }
            }
        }
    }
}
