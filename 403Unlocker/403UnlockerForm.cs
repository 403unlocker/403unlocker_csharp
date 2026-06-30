using _403Unlocker.Add_DNS;
using _403Unlocker.Add_DNS.Online_Source;
using _403Unlocker.Bypass_Hostname;
using _403Unlocker.Data_Models;
using _403Unlocker.Edit_DNS;
using _403Unlocker.File;
using _403Unlocker.Find_DNS;
using _403Unlocker.Network_Interface_Configuration;
using Clipboard_Manager;
using Network_Utilities.Bypass_Testing;
using Network_Utilities.Http_Service;
using Network_Utilities.Ping;
using Newtonsoft.Json;
using QR_Code_Generator;
using Registry_Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _403Unlocker
{
    public partial class _403UnlockerForm : Form
    {
        private string pathTable = "DnsTable.json";
        private BindingList<DnsInfo> dnsTable = new BindingList<DnsInfo>();
        private CancellationTokenSource cancellationToken;
        private bool sortBindingFlag = false;
        private bool tableBindingFlag = false;
        public bool isTableChangesAppliedToFind = false;

        public _403UnlockerForm()
        {
            InitializeComponent();

            tableBindingFlag = true;
            ReloadTable();
            tableBindingFlag = false;

            dataGridView1.Columns["IPv4"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Provider"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Latency"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["PingPacketLoss"].HeaderText = "Ping Packet Loss";
            dataGridView1.Columns["PingPacketLoss"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Bypass"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["NsLookup"].HeaderText = "NS Lookup";
            dataGridView1.Columns["NsLookup"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        #region Message Boxes
        private static DialogResult MessageBoxIPv4ImportFailed(Exception error)
        {
            var r = MessageBox.Show(error.Message,
                                    "IPv4 Import Failed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            return r;
        }

        private static DialogResult MessageBoxShowAddToTableResult(int newCount, int duplicationCount)
        {
            if (newCount > 0) return MessageBoxDnsAddedSuccessful(newCount, duplicationCount);
            else return MessageBoxDnsAlreadyExists();
        }

        private static DialogResult MessageBoxDnsAddedSuccessful(int newCount, int duplicateCount)
        {
            var r = MessageBox.Show("New DNS(s) has been successfully added!\n\n" +
                                    $"New DNSs: {newCount}\n" +
                                    $"Duplicate DNSs: {duplicateCount}",
                                    "Successfully Updated 🎉",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);

            return r;
        }

        private static DialogResult MessageBoxDnsAlreadyExists()
        {
            var r = MessageBox.Show("DNS(s) already exist",
                                    "No Duplicates Allowed 🛑",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
            return r;
        }

        private static DialogResult MessageBoxDnsClearChoice()
        {
            var r = MessageBox.Show("Are you sure about that?",
                                    "We are clearing",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button2);
            return r;
        }

        private static DialogResult MessageBoxDnsAddTimer()
        {
            var r = MessageBox.Show("You have time to wait, be patient",
                                    "Hold Your Horses🐎!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Hand);
            return r;
        }

        private static DialogResult MessageBoxDnsAddScraperError()
        {
            var r = MessageBox.Show("Please, try again later",
                                    "Somthing went wrong",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            return r;
        }

        private static DialogResult MessageBoxDnsAddScraperHttpRequestTimeout()
        {
            var r = MessageBox.Show("The DNS list could not be downloaded because the request timed out\nPlease try again later",
                                    "Request Timed Out",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            return r;
        }

        private static DialogResult MessageBoxDnsAddScraperNoInternet()
        {
            var r = MessageBox.Show("Unable to connect to the Internet\nPlease, check your network connection and try again",
                                    "No Internet Connection",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            return r;
        }

        private static DialogResult MessageBoxDnsDeleteChoice(string selectedDns)
        {
            var r = MessageBox.Show($"Are you sure you want to delete {selectedDns} DNS?",
                                    "Confirm Delete",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button2);
            return r;
        }
        
        private static DialogResult MessageBoxDnsIPv4NotFound()
        {
            var r = MessageBox.Show("The specified IPv4 address was not found in the DNS list",
                                    "DNS Server Not Found",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            return r;
        }

        private static DialogResult MessageBoxDnsProviderNotFound()
        {
            var r = MessageBox.Show("The specified DNS provider was not found in the DNS list",
                                    "DNS Provider Not Found",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            return r;
        }

        private static DialogResult MessageBoxDnsFound(int foundDns)
        {
            var r = MessageBox.Show($"{foundDns} DNS servers match your search criteria",
                                    "Multiple DNS Servers Found",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            return r;
        }

        private static DialogResult MessageBoxIsReachableWithoutDns(string uri)
        {
            var r = MessageBox.Show($"This hostname \'{uri}\' can be reached without resolving it through the selected DNS server",
                                     "Hostname Accessible",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Warning);
            return r;
        }
        #endregion

        #region Form Events
        private async void Form1_Load(object sender, EventArgs e)
        {
            Configuration.Settings.Load();

            RegistryForm formSettings = new RegistryForm(Application.ProductName);

            if (!formSettings.Exists)
            {
                formSettings.FormSize = new Size(825, 470);
                formSettings.FormLocation = new Point(387, 191);
                formSettings.FormWindowState = FormWindowState.Normal;
                formSettings.Write();
            }

            formSettings.Read();
            Size = formSettings.FormSize;
            Location = formSettings.FormLocation;
            WindowState = formSettings.FormWindowState;

            tableBindingFlag = true;
            if (System.IO.File.Exists(pathTable)) await LoadToTable(pathTable);
            tableBindingFlag = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Configuration.Settings.Save();

            SaveAsJson(pathTable);

            if (WindowState == FormWindowState.Normal)
            {
                RegistryForm formSettings = new RegistryForm(Application.ProductName);
                formSettings.FormSize = Size;
                formSettings.FormLocation = Location;
                formSettings.FormWindowState = WindowState;
                formSettings.Write();
            }
        }
        #endregion

        #region Progress Bar Methods
        private void IncrementProgressBar()
        {
            toolStripProgressBarDns.Value++;
            RefreshProgressBarLabel();
        }

        private void BeginCheck()
        {
            cancellationToken = new CancellationTokenSource();
            SetCancelEnabled(true);
            ResetProgressBar(dnsTable.Count);

            SetTestingState(true);
        }

        private void EndCheck()
        {
            SetTestingState(false);

            if (cancellationToken.IsCancellationRequested)
            {
                SetCheckStatisticsVisible(false);
                ResetCheckStatistics();
                return;
            }
            SetCheckStatisticsVisible(true);
            RefreshCheckStatistics();
        }

        private void SetTestingState(bool isChecking)
        {
            toolStripProgressBarDns.Visible = isChecking;
            toolStripButtonCancelTask.Visible = isChecking;
            toolStripLabelProgressBar.Visible = isChecking;

            menuStrip1.Enabled = !isChecking;
            
            contextMenuStrip1.Enabled = !isChecking;

            toolStripDropDownButton1.Enabled = !isChecking;
            toolStripDropDownButton2.Enabled = !isChecking;
            toolStripButtonPing.Enabled = !isChecking;
            toolStripButtonNslookup.Enabled = !isChecking;
            toolStripButtonBypass.Enabled = !isChecking;
            toolStripButtonApplyDns.Enabled = !isChecking;

            toolStrip3.Enabled = !isChecking;
        }

        private void RefreshCheckStatistics()
        {
            int failedCount = dnsTable.Count(dns => int.Parse(dns.Latency.Replace("ms", "")) == -1);
            int successCount = dnsTable.Count(dns => int.Parse(dns.Latency.Replace("ms", "")) != -1);
            toolStripLabel2.Text = $"Success: {successCount}";
            toolStripLabel3.Text = $"Failed: {failedCount}";
        }

        private void ResetCheckStatistics()
        {
            toolStripLabel2.Text = "";
            toolStripLabel3.Text = "";
        }

        private void SetCheckStatisticsVisible(bool state)
        {
            toolStripSeparator4.Visible = state;
            toolStripLabel2.Visible = state;
            toolStripSeparator5.Visible = state;
            toolStripLabel3.Visible = state;
        }

        private void SetTargetHostname(string hostname)
        {
            toolStripLabelTargetHost.Text = $"Target Host: {hostname}";
        }

        private void SetTargetHostnameVisible(bool visible)
        {
            toolStripLabelTargetHost.Visible = visible;
        }

        private void SetCancelEnabled(bool state)
        {
            toolStripButtonCancelTask.Text = state ? "Cancel" : "Canceling";
            toolStripButtonCancelTask.Enabled = state;
        }

        private void toolStripButtonCancelTask_Click(object sender, EventArgs e)
        {
            cancellationToken.Cancel();
            SetCancelEnabled(false);
        }

        private void ResetProgressBar(int maxValue)
        {
            toolStripProgressBarDns.Maximum = maxValue;
            toolStripProgressBarDns.Value = 0;
        }

        private void RefreshProgressBarLabel()
        {
            toolStripLabelProgressBar.Text = $"{toolStripProgressBarDns.Value}/{toolStripProgressBarDns.Maximum} DNS tested";
        }
        #endregion

        #region Table Methods
        private async Task<(int, int)> ImportJsonToTable(string path)
        {
            DnsConfig result = await FileManager.ReadJsonAsync<DnsConfig>(path);
            return AddToTable(result.IPv4_Servers);
        }

        private async Task<(int, int)> ImportTextToTable(string path)
        {
            string text = await FileManager.ReadTextAsync(path);
            string[] ipList = text.Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            List<DnsInfo> dnsList = new List<DnsInfo>();
            foreach (string ip in ipList)
            {
                if (IPAddress.TryParse(text, out IPAddress ipv4) && ipv4.AddressFamily == AddressFamily.InterNetwork)
                {
                    dnsList.Add(new DnsInfo(ipv4, ""));
                }
                else if (IPAddress.TryParse(text, out IPAddress ipv6) && ipv6.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    throw new FormatException($"The selected file contains an IPv6 address ({ip})\nOnly IPv4 addresses can be imported");
                }
                else
                {
                    throw new FormatException("The selected file contains invalid entries\nOnly files containing IPv4 addresses can be imported");
                }
            }

           return AddToTable(dnsList);
        }

        private void SaveAsJson(string path)
        {
            DnsConfig dnsConfig = new DnsConfig(dnsTable.ToList());
            FileManager.WriteJsonAsync(path, dnsConfig);
        }

        private void SaveAsText(string path)
        {
            DnsConfig dnsConfig = new DnsConfig(dnsTable.ToList());
            string[] ipv4List = dnsConfig.IPv4_Servers.Select(dns => dns.IPv4.ToString()).ToArray();
            string text = string.Join("\r\n", ipv4List);
            FileManager.WriteTextAsync(path, text);
        }

        private void ReloadTable()
        {
            dataGridView1.DataSource = dnsTable;
        }

        private void RefreshTable()
        {
            dataGridView1.Invalidate();
        }

        private async Task LoadToTable(string path)
        {
            DnsConfig result = await FileManager.ReadJsonAsync<DnsConfig>(path);
            AddToTable(result.IPv4_Servers);
        }

        private (int, int) AddToTable(DnsInfo dnsInfo)
        {
            List<DnsInfo> dnsList = new List<DnsInfo>
            {
                dnsInfo,
            };
            return AddToTable(dnsList);
        }

        private (int,int) AddToTable(List<DnsInfo> listToBeAdded)
        {
            List<DnsInfo> newDns = listToBeAdded.Except(dnsTable).ToList();
            
            int newCount = newDns.Count();
            int duplicationCount = listToBeAdded.Count() - newCount;

            foreach (DnsInfo dns in newDns)
            {
                dnsTable.Add(dns);
            }

            return (newCount, duplicationCount);
        }

        private void RemoveAtTable(int index)
        {
            dnsTable.RemoveAt(index);
        }

        private void ClearTable()
        {
            dnsTable.Clear();
        }

        private void ResetDnsResultsForPing()
        {
            foreach (var dns in dnsTable)
            {
                dns.Latency = "";
                dns.PingPacketLoss = "";
            }
            RefreshTable();
        }

        private void ResetDnsResultsForBypass()
        {
            foreach (var dns in dnsTable)
            {
                dns.Latency = "";
                dns.Bypass = "";
            }
            RefreshTable();
        }

        private void ResetDnsResultsForNsLookup()
        {
            foreach (var dns in dnsTable)
            {
                dns.Latency = "";
                dns.NsLookup = "";
            }
            RefreshTable();
        }

        private void ShowRow(int index)
        {
            dataGridView1.Rows[index].Selected = true;
            if (!dataGridView1.Rows[index].Displayed)
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = index;
            }
        }

        private void dataGridViewTotalDNSRecords_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (sortBindingFlag || tableBindingFlag) isTableChangesAppliedToFind = false;
            else isTableChangesAppliedToFind = true;

            if (sortBindingFlag) return;

            SetCheckStatisticsVisible(false);
            SetTargetHostnameVisible(false);
            toolStripLabelTargetHost.Visible = false;
            toolStripLabelTotalDNSRecords.Text = $"Total DNS Records: {dataGridView1.RowCount}";
        }
        #endregion

        #region File Tab
        private async void importIPv4AddressesListTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogText.ShowDialog() == DialogResult.OK)
            {
                int newCount = 0;
                int duplicationCount = 0;
                try
                {
                    (newCount, duplicationCount) = await ImportTextToTable(openFileDialogText.FileName);
                    MessageBoxShowAddToTableResult(newCount, duplicationCount);
                }
                catch (Exception error)
                {
                    if (error is FormatException)
                    {
                        MessageBoxIPv4ImportFailed(error);
                    }
                }
            }
        }

        private async void importDNSListJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogJson.ShowDialog() == DialogResult.OK)
            {
                int newCount = 0;
                int duplicationCount = 0;
                try
                {
                    (newCount, duplicationCount) = await ImportJsonToTable(openFileDialogJson.FileName);
                    MessageBoxShowAddToTableResult(newCount, duplicationCount);
                }
                catch (Exception error)
                {
                    if (error is JsonSerializationException)
                    {
                        MessageBoxIPv4ImportFailed(error);
                    }
                }
                
            }
        }

        private void exportIPv4AddressesListTextToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (saveFileDialogText.ShowDialog() == DialogResult.OK)
            {
                SaveAsText(saveFileDialogText.FileName);
            }
        }

        private void exportDNSListJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialogJson.ShowDialog() == DialogResult.OK)
            {
                SaveAsJson(saveFileDialogJson.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Settings Tab
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration.SettingsForm form = new Configuration.SettingsForm();
            form.ShowDialog();
        }
        #endregion

        #region Help tab
        private void sourceCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string link = @"https://github.com/ALiMoradzade/403unlocker";
            Process.Start(link);
        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string link = @"https://403unlocker.netlify.app/";
            Process.Start(link);
        }
        #endregion

        #region Add DNS
        private async void add403UnlockerDefaultDNSsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int newCount = 0;
            int duplicationCount = 0;
            (newCount, duplicationCount) = await ImportJsonToTable("DefaultDns.json");
            MessageBoxShowAddToTableResult(newCount, duplicationCount);
        }

        private void publicDNSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ScraperForm form = new ScraperForm(ScraperForm.Hostname.publicdns))
            {
                var r = form.ShowDialog();
                if (r == DialogResult.OK)
                {
                    DnsConfig dnsConfig = form.result;

                    int newCount = 0;
                    int duplicationCount = 0;
                    (newCount, duplicationCount) = AddToTable(dnsConfig.IPv4_Servers);
                    MessageBoxShowAddToTableResult(newCount, duplicationCount);
                }
                else if (r == DialogResult.Abort)
                {
                    if (form.error is TaskCanceledException)
                    {
                        MessageBoxDnsAddScraperHttpRequestTimeout();
                    }
                    else if (form.error is HttpRequestException exception)
                    {
                        if (exception.InnerException is WebException)
                        {
                            MessageBoxDnsAddScraperNoInternet();
                        }
                    }
                    else
                    {
                        MessageBoxDnsAddScraperError();
                    }
                }
            }
        }

        private void addCustomDNSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CustomDnsForm form = new CustomDnsForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    DnsInfo dnsInfo = new DnsInfo(form.IPv4, form.Provider);

                    int newCount = 0;
                    int duplicationCount = 0;
                    (newCount, duplicationCount) = AddToTable(dnsInfo);
                    MessageBoxShowAddToTableResult(newCount, duplicationCount);

                    int lastIndex = dnsTable.Count - 1;
                    ShowRow(lastIndex);
                }
            }
        }
        #endregion

        #region Sort DNS
        private void sortIPv4AscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dnsTable = new BindingList<DnsInfo>
            (
                dnsTable.OrderBy(row =>
                {
                    byte[] bytes = row.IPv4.GetAddressBytes();
                    return BitConverter.ToUInt32(bytes.Reverse().ToArray(), 0);
                }).ToList()
            );

            sortBindingFlag = true;
            ReloadTable();
            sortBindingFlag = false;
        }

        private void sortIPv4DescToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dnsTable = new BindingList<DnsInfo>
            (
                dnsTable.OrderByDescending(row =>
                {
                    byte[] bytes = row.IPv4.GetAddressBytes();
                    return BitConverter.ToUInt32(bytes.Reverse().ToArray(), 0);
                }).ToList()
            );

            sortBindingFlag = true;
            ReloadTable();
            sortBindingFlag = false;
        }

        private void sortLatencyAscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dnsTable = new BindingList<DnsInfo>
            (
                dnsTable.OrderBy(row => row.Latency == "-1ms" || row.Latency.Contains("Canceled"))
                        .ThenBy(row => row.Latency.Contains("Canceled")? 0 : int.Parse(row.Latency.Replace("ms", "")))
                        .ToList()
            );

            sortBindingFlag = true;
            ReloadTable();
            sortBindingFlag = false;
        }
        #endregion

        #region Clear All
        private void toolStripButtonClearAll_Click(object sender, EventArgs e)
        {
            if (MessageBoxDnsClearChoice() == DialogResult.Yes)
            {
                ClearTable();
                toolStripLabel3.Visible = false;
                toolStripLabel2.Visible = false;
            }
        }
        #endregion

        #region Right Click
        private void iPv4AddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedDns = dataGridView1.SelectedRows[0].Cells["IPv4"].Value.ToString();
            ClipboardManager.CopyToClipboard(selectedDns);
        }

        private void providerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedProvider = dataGridView1.SelectedRows[0].Cells["Provider"].Value.ToString();
            ClipboardManager.CopyToClipboard(selectedProvider);
        }

        private void allFieldsCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridView1.SelectedRows[0];
            var values = selectedRow.Cells.Cast<DataGridViewCell>()
                                          .Select(cell => cell.Value.ToString());

            string csv = string.Join(",", values);
            ClipboardManager.CopyToClipboard(csv);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedDns = dataGridView1.SelectedRows[0].Cells["IPv4"].Value.ToString();

            if (MessageBoxDnsDeleteChoice(selectedDns) == DialogResult.Yes)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                RemoveAtTable(selectedRowIndex);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedDns = dataGridView1.SelectedRows[0];

            IPAddress ipv4 = IPAddress.Parse(selectedDns.Cells["IPv4"].Value.ToString());
            string provider = selectedDns.Cells["Provider"].Value.ToString();

            EditDnsForm form = new EditDnsForm(ipv4, provider);
            if (form.ShowDialog() == DialogResult.OK)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                dnsTable[selectedRowIndex].IPv4 = form.IPv4;
                dnsTable[selectedRowIndex].Provider = form.Provider;
                RefreshTable();
            }
        }

        private void generateIPv4QRCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedDns = dataGridView1.SelectedRows[0].Cells["IPv4"].Value.ToString();
            QrCodeGeneratorForm form = new QrCodeGeneratorForm(selectedDns);
            form.ShowDialog();
        }


        public void ShowFoundDns(DnsInfo foundDns)
        {
            int index = dnsTable.IndexOf(foundDns);
            ShowRow(index);
        }

        public DnsInfo[] FindDnsByIPv4(string[] octecsToBeFound)
        {
            DnsInfo[] foundDns = dnsTable.ToList().FindAll(row =>
            {
                string currenctRowIPv4 = row.IPv4.ToString();
                int[] octets = currenctRowIPv4.Split('.').Select(int.Parse).ToArray();
                bool isMatch = false;

                for (int i = 0; i < 4; i++)
                {
                    if (string.IsNullOrEmpty(octecsToBeFound[i])) continue;
                    else
                    {
                        if (int.Parse(octecsToBeFound[i]) == octets[i])
                        {
                            isMatch = true;
                        }
                        else
                        {
                            isMatch = false;
                            break;
                        }
                    }
                }
                return isMatch;
            }).ToArray();

            if (foundDns.Length > 0) MessageBoxDnsFound(foundDns.Length);
            else MessageBoxDnsIPv4NotFound();
         
            return foundDns;
        }

        public DnsInfo[] FindDnsByProvider(string Provider)
        {
            DnsInfo[] foundDns = dnsTable.ToList().FindAll(row =>
            {
                int index = row.Provider.IndexOf(Provider, StringComparison.OrdinalIgnoreCase);
                return index >= 0;
            }).ToArray();

            if (foundDns.Length > 0) MessageBoxDnsFound(foundDns.Length);
            else MessageBoxDnsProviderNotFound();

            return foundDns;
        }

        private void findByIPv4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindByIPv4Form form = new FindByIPv4Form(this);
            form.Show(this);
        }

        private void findByProviderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindByProviderForm form = new FindByProviderForm(this);
            form.Show(this);
        }
        #endregion

        #region Bypass
        private async void toolStripBypass_Click(object sender, EventArgs e)
        {
            string uri;
            using (BypassHostnameForm form = new BypassHostnameForm())
            {
                if (form.ShowDialog() != DialogResult.OK) return;

                uri = form.Hostname;
            }

            try
            {
                toolStripLabelProgressBar.Visible = true;
                toolStripLabelProgressBar.Text = "Checking direct access to hostname...";

                HttpResult httpResult = await HttpService.SendRequestAsync(new Uri($"https://{uri}:443"));
                if (httpResult.IsSuccessful)
                {
                    MessageBoxIsReachableWithoutDns(uri);
                    toolStripLabelProgressBar.Visible = false;
                    return;
                }
            }
            catch (Exception)
            {
            }

            BeginCheck();

            ResetDnsResultsForBypass();

            SetTargetHostname(uri);
            SetTargetHostnameVisible(true);

            SemaphoreSlim semaphore = new SemaphoreSlim(Configuration.Settings.MaxParallelRequests);
            await Task.WhenAll(
                dnsTable.Select(async dns =>
                {
                    await semaphore.WaitAsync();

                    try
                    {
                        BypassResult bypassResult = await BypassService.BypassTestAsync(dns.IPv4, uri,443, cancellationToken.Token);
                        dns.Latency = $"{bypassResult.Latency:F0}ms";
                        dns.Bypass = $"{(int)bypassResult.Status} – {bypassResult.Status}";
                    }
                    catch (Exception error)
                    {
                        if (error is OperationCanceledException)
                        {
                            if (error.Message == "The operation was canceled.") dns.Bypass = "Canceled by user";
                        }
                        else if (error is TimeoutException) dns.Bypass = error.Message;
                        else if (error is UriFormatException) dns.Bypass = error.Message;
                        else if (error is InvalidDataException) dns.Bypass = error.Message;
                        else if (error is IOException exception)
                        {
                            if (exception.InnerException is SocketException) dns.Bypass = "Socket Closed";
                        }
                        else if (error is InvalidOperationException) dns.Bypass = "Invalid Operation";
                        else throw error;

                        dns.Latency = "-1ms";
                    }
                    finally
                    {
                        semaphore.Release();
                        RefreshTable();
                        IncrementProgressBar();
                    }
                })
            );

            EndCheck();
        }
        #endregion

        #region Ping
        private async void toolStripPing_Click(object sender, EventArgs e)
        {
            BeginCheck();

            ResetDnsResultsForPing();

            SetTargetHostnameVisible(false);

            SemaphoreSlim semaphore = new SemaphoreSlim(Configuration.Settings.MaxParallelRequests);
            await Task.WhenAll(
                dnsTable.Select(async dns =>
                {
                    await semaphore.WaitAsync();

                    try
                    {
                        PingResult pingResult = await PingService.PingHostAsync(dns.IPv4, cancellationToken.Token);
                        dns.Latency = $"{pingResult.Latency:F0}ms";
                        dns.PingPacketLoss = $"{pingResult.PacketLoss:F0}%";
                    }
                    catch (Exception error)
                    {
                        if (error is OperationCanceledException)
                        {
                            if (error.Message == "The operation was canceled.") dns.PingPacketLoss = "Canceled by user";
                        }
                        dns.Latency = "-1ms";
                    }
                    finally
                    {
                        semaphore.Release();
                        RefreshTable();
                        IncrementProgressBar();
                    }
                })
            );

            EndCheck();
        }
        #endregion

        #region NS Lookup
        private async void toolStripButtonNslookup_Click(object sender, EventArgs e)
        {
            BeginCheck();

            ResetDnsResultsForNsLookup();

            SetTargetHostnameVisible(false);

            SemaphoreSlim semaphore = new SemaphoreSlim(Configuration.Settings.MaxParallelRequests);
            await Task.WhenAll(
                dnsTable.Select(async dns =>
                {
                    await semaphore.WaitAsync();

                    try
                    {
                        ReverseLookupResult reverseLookupResult = await ReverseLookupService.ReverseLookupHostAsync(dns.IPv4, cancellationToken.Token);
                        
                        dns.Latency = $"{reverseLookupResult.Latency:F0}ms";
                        dns.NsLookup = string.IsNullOrEmpty(reverseLookupResult.Hostname) ?
                                       $"{reverseLookupResult.Status.ToString().Replace('_', ' ')}" :
                                       reverseLookupResult.Hostname;
                    }
                    catch (Exception error)
                    {
                        if (error is OperationCanceledException)
                        {
                            if (error.Message == "The operation was canceled.") dns.NsLookup = "Canceled by user";
                        }
                        dns.Latency = "-1ms";
                    }
                    finally
                    {
                        semaphore.Release();
                        RefreshTable();
                        IncrementProgressBar();
                    }
                })
            );

            EndCheck();
        }
        #endregion

        #region Apply DNS
        private void toolStripButtonApplyDns_Click(object sender, EventArgs e)
        {
            string selectedDns = dataGridView1.SelectedRows[0].Cells["IPv4"].Value.ToString();
            NetworkInterfaceConfigurationForm form = new NetworkInterfaceConfigurationForm(IPAddress.Parse(selectedDns));
            form.ShowDialog();
        }
        #endregion


    }
}
