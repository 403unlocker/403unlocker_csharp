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
using System.Net;

namespace _403unlocker.Ping
{
    public partial class DnsPingForm : Form
    {
        private BindingList<DnsBenchmark> dnsBinding = new BindingList<DnsBenchmark>();
        private List<UrlConfig> userUrls = new List<UrlConfig>();

        public DnsPingForm()
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
                //Set the properties for the TextBox
                userUrls = await UrlConfig.ReadJson();
                AppendToAutoComplete(userUrls);
            }
            catch (Exception)
            {
                AppendToAutoComplete(Data.Url.DefaultList());
            }

            try
            {
                bool isSuccesful = Settings.Read();
                if (!isSuccesful) throw new Exception("Reading config file, failed");
            }
            catch (Exception error)
            {
                if (error is FileNotFoundException)
                {
                    MessageBox.Show("Config File, Not Found", error.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(error.Message, "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                MessageBox.Show("App is using default Settings...", "No Worries", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DnsPingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DnsBenchmark.WriteJson(dnsBinding.ToList());
            UrlConfig.WriteJson(userUrls);

            try
            {
                bool isSuccesful = Settings.Write();
                if (!isSuccesful) throw new Exception("App couldn't save config file!");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Saving Config File, Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AppendToDataGridView(DnsCollectorForm form)
        {
            if (form.isApplied && form.isTableChanged)
            {
                foreach (DnsConfig dns in form.newDns)
                {
                    dnsBinding.Add(new DnsBenchmark(dns));
                }
            }
        }

        private void AppendToAutoComplete(UrlConfig url)
        {
            AppendToAutoComplete(new List<UrlConfig> { url });
        }

        private void AppendToAutoComplete(List<UrlConfig> additionUrlList)
        {
            // finds new Websites
            List<UrlConfig> newUrls = additionUrlList.Except(userUrls).ToList();
            userUrls.AddRange(newUrls);
            if (newUrls.Count > 0)
            {
                urlTextBox.AutoCompleteCustomSource.AddRange(newUrls.Select(website => website.URL).ToArray());
            }
        }

        private void pcPingButton_Click(object sender, EventArgs e)
        {
            var pingList = new List<DnsBenchmark>(dnsBinding);
            List<Task> tasks = pingList.Select(x => Task.Run(() => x.GetPing())).ToList();

            using (MessageBoxProgress form = new MessageBoxProgress(tasks, Settings.Ping.PacketCount, Settings.Ping.TimeOutInMiliSeconds))
            {
                form.ShowDialog();
                dataGridView1.Invalidate();
            }
        }

        private void sitePingButton_Click(object sender, EventArgs e)
        {
            if (!UrlConfig.IsValidUrl(urlTextBox.Text))
            {
                MessageBox.Show("Please type correct URL\n\nNot Passing:\nhttp://google.com\nhttps://google.com",
                                "URL is wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var website = new UrlConfig
            {
                Name = "custom",
                URL = urlTextBox.Text
            };

            AppendToAutoComplete(website);

            var pingList = new List<DnsBenchmark>(dnsBinding);
            List<Task> tasks = pingList.Select(x => Task.Run(() => x.GetPing(urlTextBox.Text))).ToList();

            using (MessageBoxProgress form = new MessageBoxProgress(tasks, 1, Settings.ByPass.DnsResolveTimeOutInMiliSeconds + Settings.ByPass.HttpRequestTimeOutInMiliSeconds))
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
                    MessageBox.Show("Somthing went wrong!", "Check your Clipboard\nIf it is not be copied, please try again", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a row", "Can't Get DNS Cell!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private async void getPingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string selectedRowDns = dataGridView1.SelectedRows[0].Cells["DNS"].Value.ToString();
                DnsBenchmark foundRecord = dnsBinding.First(dnsPing => dnsPing.DNS == selectedRowDns);
                await foundRecord.GetPing();
                dataGridView1.Invalidate();
            }
            else
            {
                MessageBox.Show("Please select a row", "Can't Get Ping!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            // 1 to 300 - smaller to bigger
            var valid = dnsBinding.Where(dns => dns.Latency >= 1 && dns.Latency <= 300).OrderBy(dns => dns.Latency);
            // -1
            var invalid = dnsBinding.Where(dns => dns.Latency >= -1);

            List<DnsBenchmark> result = valid.Concat(invalid).ToList();

            dnsBinding = new BindingList<DnsBenchmark>(result);
            dataGridView1.DataSource = dnsBinding;
        }

        

        private void asPrimaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && !string.IsNullOrEmpty(Settings.NetworkAdaptor.SelectedNetworkInterface))
            {
                string selectedRowDns = dataGridView1.SelectedRows[0].Cells["DNS"].Value.ToString();
                DnsCommand.SetDnsAsPrimary(Settings.NetworkAdaptor.SelectedNetworkInterface, selectedRowDns);
            }
            else
            {
                MessageBox.Show("Please select a row", "Can't Read DNS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void asSecondaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && !string.IsNullOrEmpty(Settings.NetworkAdaptor.SelectedNetworkInterface))
            {
                string selectedRowDns = dataGridView1.SelectedRows[0].Cells["DNS"].Value.ToString();
                DnsCommand.SetDnsAsPrimary(Settings.NetworkAdaptor.SelectedNetworkInterface, selectedRowDns);
            }
            else
            {
                MessageBox.Show("Please select a row", "Can't Read DNS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DnsCommand.Reset(Settings.NetworkAdaptor.SelectedNetworkInterface);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm setting = new SettingsForm())
            {
                setting.ShowDialog();
            }
        }

        private void buttonAddDns_Click(object sender, EventArgs e)
        {
            var benchmarks = DnsBenchmark.ConvertToDnsConfig(dnsBinding.ToList());
            using (DnsCollectorForm form = new DnsCollectorForm(benchmarks))
            {
                form.ShowDialog();
                AppendToDataGridView(form);
            }
        }

        private void about403ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string link = @"https://github.com/ALiMoradzade/403unlocker";
            Process.Start(link);
        }

        private async void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var benchmarks = DnsBenchmark.ConvertToDnsConfig(dnsBinding.ToList());
                    var configs = await DnsConfig.ReadJson(openFileDialog1.FileName);
                    using (DnsCollectorForm form = new DnsCollectorForm(benchmarks, configs))
                    {
                        form.ShowDialog();
                        AppendToDataGridView(form);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong!", "Can't load file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DnsConfig.WriteJson(DnsBenchmark.ConvertToDnsConfig(dnsBinding.ToList()), saveFileDialog1.FileName);
            }
        }
    }
}
