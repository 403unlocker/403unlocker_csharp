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
            string url = "";
            using (GetUrlForm form = new GetUrlForm())
            {
                form.ShowDialog();
                if (!form.isOk) return;
                url = form.textBoxUrl.Text;
            }

           
            var pingList = new List<DnsBenchmark>(dnsBinding);
            List<Task> tasks = pingList.Select(x => Task.Run(() => x.GetPing(url))).ToList();

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

        private void buttonAddDns_Click(object sender, EventArgs e)
        {
            var benchmarks = DnsBenchmark.ConvertToDnsConfig(dnsBinding.ToList());
            using (DnsConfigForm form = new DnsConfigForm(benchmarks))
            {
                form.ShowDialog();
                dnsBinding = new BindingList<DnsBenchmark>(DnsConfig.ConvertToDnsBenchmark(form.dnsBinding.ToList()));
                dataGridView1.DataSource = dnsBinding;
            }
        }

        private async void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var benchmarks = DnsBenchmark.ConvertToDnsConfig(dnsBinding.ToList());
                    var configs = await DnsConfig.ReadJson(openFileDialog1.FileName);
                    using (DnsConfigForm form = new DnsConfigForm(benchmarks, configs))
                    {
                        form.ShowDialog();
                        dnsBinding = new BindingList<DnsBenchmark>(DnsConfig.ConvertToDnsBenchmark(form.dnsBinding.ToList()));
                        dataGridView1.DataSource = dnsBinding;
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
                MessageBox.Show("Please select a row", "Can't Read DNS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            string selectedDns = dataGridView1.SelectedRows[0].Cells["DNS"].Value.ToString();
            string selectedInterface = Settings.NetworkAdaptor.SelectedNetworkInterface;

            DialogResult r;
            if (!selectedInterface.Contains("Ethernet") && !selectedInterface.ToLower().Contains("wifi"))
            {
                r = MessageBox.Show(
                    $"You are setting {selectedDns} to \"{selectedInterface}\" adaptor!\n" +
                    "Are you sure?\n\n" +
                    "(Go to the Settings and select your desire adaptor)",
                    "Unexpected Recognition",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No) return;
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
    }
}
