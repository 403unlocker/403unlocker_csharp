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
using static _403unlocker.Data;
using System.IO;

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
        }

        private void DnsPingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DnsBenchmark.WriteJson(dnsBinding.ToList());
            UrlConfig.WriteJson(userUrls);
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
