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
using System.Xml.Linq;
using System.Runtime.Remoting.Messaging;
using _403unlockerLibrary;

namespace _403unlocker
{
    public partial class DnsCollectorForm : Form
    {
        private string pathDns = "dns";
        private BindingList<DnsProvider> dnsProviderBinding = new BindingList<DnsProvider> ();
        public DnsCollectorForm()
        {
            InitializeComponent();
            timerLabel.Text = "";
            dnsCountLabel.Text = "DNS Count: 0";
            dataGridView1.DataSource = dnsProviderBinding; // Links dataGridView to BindingList variable
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<DnsProvider> previousList = await JsonHandler.ReadJson<DnsProvider>(pathDns, true);
                AppendDataToDataGridView(previousList, false);
            }
            catch (Exception)
            {
                // When json text is not valid to json
                // Do Nothing
            }
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            await JsonHandler.WriteJson(pathDns, dnsProviderBinding.ToList(), true);
        }

        private void clearDnsButton_Click(object sender, EventArgs e)
        {
            dnsProviderBinding.Clear();
            dnsCountLabel.Text = "DNS Count: 0";
        }

        private void ScrollDownToEnd()
        {
            if (dataGridView1.RowCount > 0)
            {
                int lastRowIndex = dataGridView1.RowCount - 1;
                dataGridView1.FirstDisplayedScrollingRowIndex = lastRowIndex;
                dataGridView1.Rows[lastRowIndex].Selected = true;
            }
        }

        private void AppendDataToDataGridView(DnsProvider additionDns ,bool statusMessages = true)
        {
            AppendDataToDataGridView(new List<DnsProvider> { additionDns }, statusMessages);
        }

        private void AppendDataToDataGridView(List<DnsProvider> additionDnsList ,bool statusMessages = true)
        {
            // finds new DNSs
            List<DnsProvider> newDns = additionDnsList.Except(dnsProviderBinding).ToList();
            // counts new DNSs
            int newDnsCount = newDns.Count();
            // counts duplicate DNSs
            int existingDnsCount = additionDnsList.Count() - newDnsCount;

            foreach (DnsProvider dns in newDns)
            {
                dnsProviderBinding.Add(dns);
            }

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
                    MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void defaultDnsButton_Click(object sender, EventArgs e)
        {
            AppendDataToDataGridView(DnsProvider.DefaultDnsList);
        }

        private async void scrapDnsButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(timerLabel.Text))
            {
                dataGridView1.Cursor = Cursors.WaitCursor;

                var publicDnS = await DnsProvider.DnsScrapAsync();
                if (publicDnS == null)
                {
                    dataGridView1.Cursor = Cursors.Default;
                    return;
                }
                AppendDataToDataGridView(publicDnS);

                dataGridView1.Cursor = Cursors.Default;

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
            dnsCountLabel.Text = "DNS Count: " + dataGridView1.RowCount;
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
            using (DnsCustomAdderForm customeform = new DnsCustomAdderForm())
            {
                customeform.ShowDialog();
                
                if (!customeform.isFormClosePressed && customeform.isAddButtonPressed)
                {
                    List<DnsProvider> customeDnsList = new List<DnsProvider>
                    {
                        new DnsProvider
                        {
                            Name= customeform.providerTextBox.Text,
                            DNS = customeform.primaryDnsTextBox.Text,
                        },
                        new DnsProvider
                        {
                            Name = customeform.providerTextBox.Text,
                            DNS = customeform.secondaryDnsTextBox.Text
                        }
                    }
                    // removes null DNS
                    .Where(dns => !string.IsNullOrEmpty(dns.DNS)).ToList();

                    AppendDataToDataGridView(customeDnsList);
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) 
            {
                string selectedRowDns = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

                DialogResult confirmResult = MessageBox.Show($"Are you sure you want to delete \"{selectedRowDns}\" DNS?",
                                                             "Confirm Delete",
                                                             MessageBoxButtons.YesNo,
                                                             MessageBoxIcon.Question,
                                                             MessageBoxDefaultButton.Button2);

                if (confirmResult == DialogResult.Yes) 
                {
                    int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                    dnsProviderBinding.RemoveAt(selectedRowIndex); 
                } 
            }
            else
            {
                MessageBox.Show("Please select a DNS row before deleting it.", "Can't Delete!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void getPingButton_Click(object sender, EventArgs e)
        {
            using (DnsPingForm dnsPingForm = new DnsPingForm())
            {
                List<DnsProvider> dnsProviderList = dnsProviderBinding.ToList();
                List<NetworkUtility> dnsPingList = dnsProviderList.Select(dnsRecord => new NetworkUtility(dnsRecord)).ToList();
                dnsPingForm.dnsPingBinding = new BindingList<NetworkUtility>(dnsPingList);
                dnsPingForm.ShowDialog();
            }
        }
    }
}
