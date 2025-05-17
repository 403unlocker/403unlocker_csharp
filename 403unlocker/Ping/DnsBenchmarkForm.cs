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
using _403unlocker.Ping.Search;
using System.Runtime.InteropServices;
using _403unlocker.QR_Code;

namespace _403unlocker.Ping
{
    public partial class DnsBenchmarkForm : Form
    {
        private BindingList<DnsBenchmark> dnsBinding = new BindingList<DnsBenchmark>();
        private bool doesNotifyClose = false;

        public DnsBenchmarkForm()
        {
            InitializeComponent();
        }

        private async void DnsBenchmarkForm_Load(object sender, EventArgs e)
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
                        form.LabelText = "Config File, Not Found";
                        form.Caption = error.Message;
                        form.Buttons = MessageBoxButtons.OK;
                        form.Picture = MessageBoxIcon.Error;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.ShowDialog();
                    }
                }
                else
                {
                    using (MessageBoxForm form = new MessageBoxForm())
                    {
                        form.LabelText = error.Message;
                        form.Caption = "Error Occurred";
                        form.Buttons = MessageBoxButtons.OK;
                        form.Picture = MessageBoxIcon.Error;
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.ShowDialog();
                    }
                }

                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.LabelText = "App is using default Settings...";
                    form.Caption = "No Worries";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Information;
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }
            }

            showIconOnTaskTrayToolStripMenuItem.Checked = Settings.iconTray;
        }

        private void DnsBenchmarkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!doesNotifyClose && showIconOnTaskTrayToolStripMenuItem.Checked)
            {
                notifyIcon1_DoubleClick(sender, EventArgs.Empty);
                e.Cancel = true;
            }
        }

        private void DnsBenchmarkForm_FormClosed(object sender, FormClosedEventArgs e)
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
                    form.LabelText = error.Message;
                    form.Caption = "Saving Config File, Failed";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Error;
                    form.ShowDialog();
                }
            }
        }

        #region Ping
        private void buttonPing_Click(object sender, EventArgs e)
        {
            var pingList = new List<DnsBenchmark>(dnsBinding);
            List<Task> tasks = pingList.Select(x => Task.Run(async() => await x.Ping())).ToList();

            using (MessageBoxProgress form = new MessageBoxProgress(tasks, Settings.Ping.PacketCount, Settings.Ping.TimeOutInMiliSeconds))
            {
                form.ShowDialog();
                dataGridView1.Invalidate();
            }
        }
        #endregion

        #region ByPass
        private void buttonBypass_Click(object sender, EventArgs e)
        {
            string hostName = "";
            using (GetUrlForm form = new GetUrlForm())
            {
                form.ShowDialog();
                if (!form.isOkPressed)
                {
                    return;
                }
                hostName = form.comboBoxUrl.Text;
            }

            List<Task> tasks = dnsBinding.Select(x => Task.Run(async () => await x.ByPass(hostName))).ToList();
            using (MessageBoxProgress form = new MessageBoxProgress(tasks, 1, Settings.ByPass.DnsResolveTimeOutInMiliSeconds + Settings.ByPass.HttpRequestTimeOutInMiliSeconds))
            {
                form.ShowDialog();
                dataGridView1.Invalidate();
            }
        }
        #endregion

        #region Sort
        private void buttonSort_Click(object sender, EventArgs e)
        {
            var valid = dnsBinding
                .Where(dns => dns.Latency >= 0)
                .OrderBy(dns => dns.Status)
                .ThenBy(dns => dns.Latency);

            var invalid = dnsBinding
                .Where(dns => dns.Latency == -1)
                .OrderBy(dns => dns.Provider)
                .ThenBy(dns => dns.Status);

            List<DnsBenchmark> result = valid.Concat(invalid).ToList();

            dnsBinding = new BindingList<DnsBenchmark>(result);
            dataGridView1.DataSource = dnsBinding;
        }
        #endregion

        #region Setting Form
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm setting = new SettingsForm())
            {
                setting.ShowDialog();
            }
        }
        #endregion

        #region Add DNS
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
        #endregion

        #region File

        #region Import
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
                        form.LabelText = "Something went wrong!";
                        form.Caption = "Can't load file";
                        form.Buttons = MessageBoxButtons.OK;
                        form.Picture = MessageBoxIcon.Error;
                        form.ShowDialog();
                    }
                }
            }
        }
        #endregion

        #region Export
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult r = saveFileDialog1.ShowDialog();
            if (r == DialogResult.OK)
            {
                DnsConfig.WriteJson(DnsBenchmark.ConvertToDnsConfig(dnsBinding.ToList()), saveFileDialog1.FileName);
            }
        }
        #endregion

        #endregion

        #region Help

        #region Code Source
        private void codeSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string link = @"https://github.com/ALiMoradzade/403unlocker";
            Process.Start(link);
        }
        #endregion

        #region Website
        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string link = @"https://www.403unlocker.ir";
            Process.Start(link);
        }
        #endregion

        #endregion

        #region Set DNS
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
                    form.LabelText = "Please select a row";
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
                    form.LabelText = $"You are setting {selectedDns} to \"{selectedInterface}\" adaptor!\n" +
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
        #endregion

        #region Reset DNS
        private void buttonResetDns_Click(object sender, EventArgs e)
        {
            DnsCommand.Reset(Settings.NetworkAdaptor.SelectedNetworkInterface);
        }
        #endregion

        #region View
        private void showIconOnTaskTrayToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.iconTray = showIconOnTaskTrayToolStripMenuItem.Checked;
            notifyIcon1.Visible = showIconOnTaskTrayToolStripMenuItem.Checked;
        }
        #endregion

        #region Copy

        #region Provider
        private void providerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                string selectedCell = selectedRow.Cells["Provider"].Value.ToString();
                try
                {
                    Clipboard.SetText(selectedCell);
                }
                catch (Exception)
                {
                    using (MessageBoxForm form = new MessageBoxForm())
                    {
                        form.LabelText = "Check your Clipboard\n" +
                                       "If it is not be copied, please try again";
                        form.Caption = "Unknown Error!";
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
                    form.LabelText = "Please select a row";
                    form.Caption = "Can't Get Provider Cell!";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Stop;
                    form.ShowDialog();
                }
            }
        }
        #endregion

        #region DNS
        private void dNSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                string selectedCell = selectedRow.Cells["Provider"].Value.ToString();
                try
                {
                    Clipboard.SetText(selectedCell);
                }
                catch (Exception)
                {
                    using (MessageBoxForm form = new MessageBoxForm())
                    {
                        form.LabelText = "Check your Clipboard\n" +
                                       "If it is not be copied, please try again";
                        form.Caption = "Unknown Error!";
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
                    form.LabelText = "Please select a row";
                    form.Caption = "Can't Get DNS Cell!";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Stop;
                    form.ShowDialog();
                }
            }
        }
        #endregion

        #region Status
        private void statusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                string selectedCell = selectedRow.Cells["Status"].Value.ToString();
                try
                {
                    Clipboard.SetText(selectedCell);
                }
                catch (Exception)
                {
                    using (MessageBoxForm form = new MessageBoxForm())
                    {
                        form.LabelText = "Check your Clipboard\n" +
                                       "If it is not be copied, please try again";
                        form.Caption = "Unknown Error!";
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
                    form.LabelText = "Please select a row";
                    form.Caption = "Can't Get Status Cell!";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Stop;
                    form.ShowDialog();
                }
            }
        }
        #endregion

        #region Latency
        private void latencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                string selectedCell = selectedRow.Cells["Latency"].Value.ToString();
                try
                {
                    Clipboard.SetText(selectedCell);
                }
                catch (Exception)
                {
                    using (MessageBoxForm form = new MessageBoxForm())
                    {
                        form.LabelText = "Check your Clipboard\n" +
                                       "If it is not be copied, please try again";
                        form.Caption = "Unknown Error!";
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
                    form.LabelText = "Please select a row";
                    form.Caption = "Can't Get Latency Cell!";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Stop;
                    form.ShowDialog();
                }
            }
        }
        #endregion

        #region as CSV
        private void asCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                string selectedCell = $"\"{selectedRow.Cells["Provider"].Value}\", " +
                                      $"\"{selectedRow.Cells["DNS"].Value}\", " +
                                      $"\"{selectedRow.Cells["Status"].Value}\", " +
                                      $"\"{selectedRow.Cells["Latency"].Value}\" ";
                try
                {
                    Clipboard.SetText(selectedCell);
                }
                catch (Exception)
                {
                    using (MessageBoxForm form = new MessageBoxForm())
                    {
                        form.LabelText = "Check your Clipboard\n" +
                                       "If it is not be copied, please try again";
                        form.Caption = "Unknown Error!";
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
                    form.LabelText = "Please select a row";
                    form.Caption = "Can't Get CSV!";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Stop;
                    form.ShowDialog();
                }
            }
        }
        #endregion

        #endregion

        #region Search 

        #region by Provider
        private void byProviderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SearchForm form = new SearchForm(false))
            {
                form.ShowDialog();
                if (form.isOkPressed)
                {
                    List<string> searchedList = dnsBinding.ToList().FindAll(row =>
                    {
                        // IndexOf returns an int indicating the position of searchText
                        int index = row.Provider.IndexOf(form.textBox1.Text, StringComparison.OrdinalIgnoreCase);

                        // We use the returned int to decide if the substring is neither at the start nor the end.
                        return index >= 0 && index <= row.Provider.Length;
                    })
                        .Select(row => row.Provider).ToList();

                    if (searchedList.Count > 0)
                    {
                        DnsBenchmark searchedValue = dnsBinding.First(x => x.Provider == searchedList.First());
                        int index = dnsBinding.IndexOf(searchedValue);
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
                            messageBox.LabelText = $"\"{form.textBox1.Text}\" is not in table";
                            messageBox.Caption = "Provider Not Found";
                            messageBox.Buttons = MessageBoxButtons.OK;
                            messageBox.Picture = MessageBoxIcon.Information;
                            messageBox.ShowDialog();
                        }
                    }
                }
            }
        }
        #endregion

        #region by DNS
        private void byDNSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SearchForm form = new SearchForm(true))
            {
                form.ShowDialog();
                if (form.isOkPressed)
                {
                    DnsBenchmark searchedValue = dnsBinding.First(x => x.DNS == form.textBox1.Text);
                    int index = dnsBinding.IndexOf(searchedValue);
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
                            messageBox.LabelText = $"\"{form.textBox1.Text}\" is not in table";
                            messageBox.Caption = "DNS Not Found";
                            messageBox.Buttons = MessageBoxButtons.OK;
                            messageBox.Picture = MessageBoxIcon.Information;
                            messageBox.ShowDialog();
                        }
                    }
                }
            }
        }
        #endregion

        #endregion

        #region QR Code Gen
        private void generateQRCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string selectedRowDns = dataGridView1.SelectedRows[0].Cells["DNS"].Value.ToString();

                using (QrCodeForm f = new QrCodeForm(selectedRowDns))
                {
                    f.ShowDialog();
                }
            }
            else
            {
                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.LabelText = "Please select a row";
                    form.Caption = "Can't Create";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Stop;
                    form.ShowDialog();
                }
            }
        }
        #endregion

        #region DNS Counter
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            labelDnsCount.Text = "Count: " + dataGridView1.RowCount;
        }
        #endregion

        #region Task Tray

        #region ِDouble Click
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (ShowInTaskbar)
            {
                Hide();
                ShowInTaskbar = false;
            }
            else
            {
                Show();
                Activate();
                ShowInTaskbar = true;
            }
        }
        #endregion

        #region Reset DNS
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            buttonResetDns.PerformClick();
        }
        #endregion

        #region Exit
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            doesNotifyClose = true;
            Close();
        }




        #endregion

        #endregion

    }
}
