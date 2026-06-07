using _403Unlocker.Add_DNS;
using _403Unlocker.Data_Models;
using _403Unlocker.File;
using Clipboard_Manager;
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
using System.Windows.Forms;

namespace _403Unlocker
{
    public partial class _403UnlockerForm : Form
    {
        private string pathTable = "DnsTable.json";
        private BindingList<DnsInfo> dnsTable = new BindingList<DnsInfo>();
        
        public _403UnlockerForm()
        {
            InitializeComponent();

            LoadToTable();

            dataGridView1.Columns["IPv4"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Provider"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Latency"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["PacketLoss"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["ByPass"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        #region Message Boxes
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
                                    MessageBoxIcon.Question);
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
            var r = MessageBox.Show("",//Data.DnsScraper.Errors.Message,
                                    "Somthing went wrong",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            return r;
        }

        private static DialogResult MessageBoxDnsDeleteChoice(string selectedDns)
        {
            var r = MessageBox.Show($"Are you sure you want to delete {selectedDns} DNS?",
                                    "Confirm Delete",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);
            return r;
        }
        #endregion

        #region Form Events
        private void Form1_Load(object sender, EventArgs e)
        {
            RegistryForm formSettings = new RegistryForm(Application.ProductName);

            if (!formSettings.IsExisted)
            {
                formSettings.FormSize = new Size(686, 471);
                formSettings.FormLocation = new Point(457, 190);
                formSettings.FormWindowState = FormWindowState.Normal;
                formSettings.Write();
            }

            formSettings.Read();
            Size = formSettings.FormSize;
            Location = formSettings.FormLocation;
            WindowState = formSettings.FormWindowState;

            if (!System.IO.File.Exists(pathTable)) return;
            LoadToTable(pathTable);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
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

        #region Table Methods
        private async void ImportToTable(string path)
        {
            DnsConfig result = await FileManager.ReadJsonAsync<DnsConfig>(path);
            (int newCount, int duplicationCount) = AddListToTable(result.IPv4_Servers);
            MessageBoxShowAddToTableResult(newCount, duplicationCount);
            ShowLastRow();
        }

        private void LoadToTable()
        {
            dataGridView1.DataSource = dnsTable;
        }

        private async void LoadToTable(string path)
        {
            DnsConfig result = await FileManager.ReadJsonAsync<DnsConfig>(path);
            AddListToTable(result.IPv4_Servers);
        }

        private async void SaveAsJson(string path)
        {
            DnsConfig dnsConfig = new DnsConfig(dnsTable.ToList());
            await FileManager.WriteJsonAsync(path, dnsConfig);
        }

        private (int, int) AddCustomToTable(DnsInfo dnsInfo)
        {
            List<DnsInfo> dnsList = new List<DnsInfo>
            {
                dnsInfo,
            };
            return AddListToTable(dnsList);
        }

        private void RemoveDnsIndexFromTable(int index)
        {
            dnsTable.RemoveAt(index);
        }

        private (int,int) AddListToTable(List<DnsInfo> listToBeAdded)
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

        private void ClearTable()
        {
            dnsTable.Clear();
        }

        private void ShowLastRow()
        {
            if (dataGridView1.RowCount > 0)
            {
                int lastRowIndex = dataGridView1.RowCount - 1;
                dataGridView1.FirstDisplayedScrollingRowIndex = lastRowIndex;
                dataGridView1.ClearSelection();
            }
        }

        private void dataGridViewTotalDNSRecords_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            toolStripLabelTotalDNSRecords.Text = $"Total DNS Records: {dataGridView1.RowCount}";
        }
        #endregion

        #region File Tab
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileExtension = Path.GetExtension(openFileDialog1.SafeFileName);
                if (fileExtension == ".json")
                {
                    ImportToTable(openFileDialog1.FileName);
                }
                else if (fileExtension == ".txt")
                {
                }
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SaveAsJson(saveFileDialog1.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
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
        private void add403UnlockerDefaultDNSsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportToTable("DefaultDns.json");
        }

        private async void addPublicdnsxyzDNSsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DnsConfig dnsConfig = await FetchDns.ScrapDnsServersAsync();
            AddListToTable(dnsConfig.IPv4_Servers);
            ShowLastRow();
        }

        private void addCustomDNSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CustomDnsForm form = new CustomDnsForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    DnsInfo dnsInfo = new DnsInfo(IPAddress.Parse(form.IPv4), form.Provider);
                    AddCustomToTable(dnsInfo);
                    ShowLastRow();
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

            LoadToTable();
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

            LoadToTable();
        }

        private void sortLatencyAscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dnsTable = new BindingList<DnsInfo>
            (
                dnsTable.OrderBy(row => row.Latency == -1)
                        .ThenBy(row => row.Latency)
                        .ToList()
            );

            LoadToTable();
        }
        #endregion

        #region Clear All
        private void toolStripButtonClearAll_Click(object sender, EventArgs e)
        {
            if (MessageBoxDnsClearChoice() == DialogResult.Yes)
            {
                ClearTable();
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
                RemoveDnsIndexFromTable(selectedRowIndex);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void generateIPv4QRCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedDns = dataGridView1.SelectedRows[0].Cells["IPv4"].Value.ToString();
            QrCodeGeneratorForm form = new QrCodeGeneratorForm(selectedDns);
            form.ShowDialog();
        }

        private void findByIPv4ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void findByProviderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        #endregion


    }
}
