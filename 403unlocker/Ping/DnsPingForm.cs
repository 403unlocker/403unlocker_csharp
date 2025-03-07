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
using _403unlocker.Settings;
using System.Security.Cryptography;
using _403unlocker.Add;
using System.Diagnostics;

namespace _403unlocker.Ping
{
    public partial class DnsPingForm : Form
    {
        internal BindingList<DnsBenchmark> dnsBinding = new BindingList<DnsBenchmark>();
        private List<UrlConfig> Websites = new List<UrlConfig>();

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
                dataGridView1.DataSource = dnsBinding;

                dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView1.Columns["DNS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView1.Columns["Latency"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                //Set the properties for the TextBox
                Websites = await UrlConfig.ReadJson();
                AppendToAutoComplete(Websites);
            }
            catch (Exception)
            {
                AppendToAutoComplete(Data.Url.DefaultList());
                SaveNewUrlToBson(Data.Url.DefaultList());
            }
        }

        private async void DnsPingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await DnsBenchmark.WriteJson(dnsBinding.ToList(), false);
        }

        private void SaveNewUrlToBson(UrlConfig website)
        {
            SaveNewUrlToBson(new List<UrlConfig> { website });
        }

        private async void SaveNewUrlToBson(List<UrlConfig> toAddList)
        {
            // finds new Websites
            List<UrlConfig> newWebsites = toAddList.Except(Websites).ToList();
            Websites.AddRange(newWebsites);
            if (newWebsites.Count > 0)
            {
                await UrlConfig.WriteJson(newWebsites, true);
            }
        }

        private void AppendToAutoComplete(UrlConfig website)
        {
            AppendToAutoComplete(new List<UrlConfig> { website });
        }

        private void AppendToAutoComplete(List<UrlConfig> websites)
        {
            urlTextBox.AutoCompleteCustomSource.AddRange(websites.Select(website => website.URL).ToArray());
        }

        private async void pcPingButton_Click(object sender, EventArgs e)
        {
            var pingList = new List<DnsBenchmark>(dnsBinding);
            List<Task> tasks = pingList.Select(x => Task.Run(() => x.GetPing(5))).ToList();
            await Task.WhenAll(tasks);
            dataGridView1.Invalidate();
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
                await foundRecord.GetPing(5000);
                dataGridView1.Invalidate();
            }
            else
            {
                MessageBox.Show("Please select a row", "Can't Get Ping!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            // sort by status
            List<DnsBenchmark> sortedDnsPing = dnsBinding.OrderBy(dnsPing => dnsPing.Status)
                                                            // then sort by ping
                                                            .ThenBy(dnsPing => dnsPing.Latency)
                                                            .ToList();
            dnsBinding = new BindingList<DnsBenchmark>(sortedDnsPing);
            dataGridView1.DataSource = dnsBinding;
        }

        private async void sitePingButton_Click(object sender, EventArgs e)
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

            SaveNewUrlToBson(website);
            AppendToAutoComplete(website);

            var pingList = new List<DnsBenchmark>(dnsBinding);
            List<Task> tasks = pingList.Select(x => Task.Run(() => x.GetPing(urlTextBox.Text, 5))).ToList();
            await Task.WhenAll(tasks);
            dataGridView1.Invalidate();
        }

        private void asPrimaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && !string.IsNullOrEmpty(Settings.Settings.SelectedNetworkInterface))
            {
                string selectedRowDns = dataGridView1.SelectedRows[0].Cells["DNS"].Value.ToString();
                NetworkSettings.DnsSetting.SetDnsAsPrimary(Settings.Settings.SelectedNetworkInterface, selectedRowDns);
            }
            else
            {
                MessageBox.Show("Please select a row", "Can't Read DNS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void asSecondaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && !string.IsNullOrEmpty(Settings.Settings.SelectedNetworkInterface))
            {
                string selectedRowDns = dataGridView1.SelectedRows[0].Cells["DNS"].Value.ToString();
                NetworkSettings.DnsSetting.SetDnsAsPrimary(Settings.Settings.SelectedNetworkInterface, selectedRowDns);
            }
            else
            {
                MessageBox.Show("Please select a row", "Can't Read DNS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NetworkSettings.DnsSetting.Reset(Settings.Settings.SelectedNetworkInterface);
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
            List<DnsConfig> dns = new List<DnsConfig>();
            dns.AddRange(DnsBenchmark.ConvertTo(dnsBinding.ToList()));
           
            using (DnsCollectorForm form = new DnsCollectorForm(dns))
            {
                form.ShowDialog();
            }
        }

        private void about403ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string link = @"https://github.com/ALiMoradzade/403unlocker";
            Process.Start(link);
        }
    }
}
