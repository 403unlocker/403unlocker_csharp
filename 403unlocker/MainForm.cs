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


namespace _403unlocker
{
    public partial class MainForm : Form
    {
        // Theme Color 0x2CD4BF
        public MainForm()
        {
            InitializeComponent();
            timerLabel.Text = "";
        }

        private void clearDnsButton_Click(object sender, EventArgs e)
        {
            dnsTable.DataSource = null;
        }

        private static void AppendDataTo(DataGridView dataGridView, DnsConfig additionDns)
        {
            AppendDataTo(dataGridView, new List<DnsConfig> { additionDns });
        }

        private static void AppendDataTo(DataGridView dataGridView, List<DnsConfig> additionDnsList)
        {
            // converts dnsTable to list
            List<DnsConfig> dnsTable = dataGridView.DataSource == null ? new List<DnsConfig>() : (dataGridView.DataSource as List<DnsConfig>).ToList();
            // finds new DNSs
            var newDns = additionDnsList.Except(dnsTable);
            // counts new DNSs
            int dnsAddedCount = newDns.Count();
            // add new DNSs to list
            dnsTable.AddRange(newDns);
            // import it to dnsTable
            dataGridView.DataSource = dnsTable;

            string text, caption;
            if (dnsAddedCount > 0)
            {
                text = $"New DNS(s) has been successfully added!\nAdded DNS Count: {dnsAddedCount}";
                caption = "Successfully Updated 🎉";
                MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                text = "DNSs already exists in table";
                caption = "No Duplicates Allowed 🛑";
                MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void defaultDnsButton_Click(object sender, EventArgs e)
        {
            AppendDataTo(dnsTable, Data.DefaultDnsList);
        }

        private async void scrapDnsButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(timerLabel.Text))
            {
                dnsTable.Cursor = Cursors.WaitCursor;

                var publicDnS = await Data.DnsScrapAsync();
                if (publicDnS == null)
                {
                    dnsTable.Cursor = Cursors.Default;
                    return;
                }
                AppendDataTo(dnsTable, publicDnS);

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
            if (dnsTable.RowCount > 0) dnsTable.FirstDisplayedScrollingRowIndex = dnsTable.RowCount - 1;
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
    }
}
