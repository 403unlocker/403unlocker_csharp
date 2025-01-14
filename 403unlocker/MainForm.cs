using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Windows.Forms.LinkLabel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Data.Common;
using System.Net;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Runtime.Remoting.Messaging;


namespace _403unlocker
{
    public partial class MainForm : Form
    {
        private string jsonAddress = "DNSs.json";
        // Theme Color 0x2CD4BF
        public MainForm()
        {
            InitializeComponent();
            timerLabel.Text = "";
            dnsCountLabel.Text = "DNS Count: 0";
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(jsonAddress))
            {
                using (StreamReader streamReader = new StreamReader(jsonAddress))
                {
                    string jsonText = await streamReader.ReadToEndAsync();
                    if (!string.IsNullOrEmpty(jsonText))
                    {
                        try
                        {
                            List<DnsRecord> previousList = JsonConvert.DeserializeObject<List<DnsRecord>>(jsonText);
                            AppendDataToDnsTable(previousList, false);

                            dnsTable.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                            dnsTable.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }
                        catch (Exception)
                        {
                            // When json text is not valid to json
                            // Do Nothing
                        }
                    }
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string jsontext = dnsTable.DataSource == null ? "" : JsonConvert.SerializeObject(dnsTable.DataSource, Formatting.Indented);
            File.WriteAllText(jsonAddress, jsontext);
        }

        private void clearDnsButton_Click(object sender, EventArgs e)
        {
            dnsTable.DataSource = null;
            dnsCountLabel.Text = "DNS Count: 0";
        }

        private void ScrollDownToEnd()
        {
            if (dnsTable.RowCount > 0) dnsTable.FirstDisplayedScrollingRowIndex = dnsTable.RowCount - 1;
        }

        private void AppendDataToDnsTable(DnsRecord additionDns ,bool statusMessages = true)
        {
            List<DnsRecord> dnsList = new List<DnsRecord> { additionDns };
            AppendDataToDnsTable(dnsList, statusMessages);
        }

        private void AppendDataToDnsTable(List<DnsRecord> additionDnsList ,bool statusMessages = true)
        {
            // converts dnsTable to list
            List<DnsRecord> dnsList = dnsTable.DataSource == null ? new List<DnsRecord>() : (dnsTable.DataSource as List<DnsRecord>).ToList();
            // finds new DNSs
            var newDns = additionDnsList.Except(dnsList);
            // counts new DNSs
            int newDnsCount = newDns.Count();
            // counts duplicate DNSs
            int existingDnsCount = additionDnsList.Count() - newDnsCount;
            // add new DNSs to list
            dnsList.AddRange(newDns);
            // import it to dnsTable
            dnsTable.DataSource = dnsList;

            if (statusMessages)
            {
                string text, caption;
                if (newDnsCount > 0)
                {
                    text = $"New DNS(s) has been successfully added!\n\nNew DNSs: {newDnsCount}\nExisting DNSs: {existingDnsCount}";
                    caption = "Successfully Updated 🎉";
                    ScrollDownToEnd();
                    MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    text = "DNS(s) already exist in table";
                    caption = "No Duplicates Allowed 🛑";
                    MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void defaultDnsButton_Click(object sender, EventArgs e)
        {
            AppendDataToDnsTable(DnsRecord.DefaultDnsList);
        }

        private async void scrapDnsButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(timerLabel.Text))
            {
                dnsTable.Cursor = Cursors.WaitCursor;

                var publicDnS = await DnsRecord.DnsScrapAsync();
                if (publicDnS == null)
                {
                    dnsTable.Cursor = Cursors.Default;
                    return;
                }
                AppendDataToDnsTable(publicDnS);

                dnsTable.Cursor = Cursors.Default;

                timerLabel.Text = "Seconds Left: 60s";
                publicDnsTimer.Enabled = true;
            }
            else
            {
                MessageBox.Show("You have time to wait, be patient", "Hold Your Horses🐎!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void dnsTable_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dnsCountLabel.Text = "DNS Count: " + dnsTable.RowCount;
        }

        private void publicDnsTimer_Tick(object sender, EventArgs e)
        {
            string s = timerLabel.Text;
            s = s.Replace("Seconds Left: ", "");
            ushort secondLeft = ushort.Parse(s.Remove(s.Length - 1));
            if (--secondLeft == 0)
            {
                timerLabel.Text = "";
                publicDnsTimer.Enabled = false;
            }
            else
            {
                timerLabel.Text = $"Seconds Left: {secondLeft}s";
            }
        }

        private void customeDnsButton_Click(object sender, EventArgs e)
        {
            using (CustomeDNSForm form = new CustomeDNSForm())
            {
                form.ShowDialog();
                
                if (!form.isFormClosePressed && form.isAddButtonPressed)
                {
                    List<DnsRecord> customeDnsList = new List<DnsRecord>
                    {
                        new DnsRecord
                        {
                            Provider= form.providerTextBox.Text,
                            DNS = form.primaryDnsTextBox.Text,
                        },
                        new DnsRecord
                        {
                            Provider = form.providerTextBox.Text,
                            DNS = form.secondaryDnsTextBox.Text
                        }
                    }
                    // Removes null DNSs
                    .Where(dns => !string.IsNullOrEmpty(dns.DNS)).ToList();

                    AppendDataToDnsTable(customeDnsList);
                }
            }
        }
    }
}
