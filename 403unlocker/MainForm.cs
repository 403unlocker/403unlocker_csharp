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
        // 0x2CD4BF
        public MainForm()
        {
            InitializeComponent();

        }

        private void clearDnsButton_Click(object sender, EventArgs e)
        {
            dnsTable.DataSource = null;
        }

        private static void AppendDataTo(DataGridView dataGridView, List<DnsConfig> dnsConfigs)
        {
            if (dataGridView.DataSource != null)
            {
                var dnsTable = new List<DnsConfig>((IEnumerable<DnsConfig>)dataGridView.DataSource);
                var newDns = dnsConfigs.Where(dns => !dnsTable.Contains(dns)).ToList();
                dnsTable.AddRange(newDns);
                dataGridView.DataSource = dnsTable;
            }
            else
            {
                dataGridView.DataSource = dnsConfigs;
            }
        }

        private void defaultDnsButton_Click(object sender, EventArgs e)
        {
            AppendDataTo(dnsTable, Data.DefaultDnsList);
        }

        private async void scrapDnsButton_Click(object sender, EventArgs e)
        {
            AppendDataTo(dnsTable, await Data.DnsScrapAsync());
        }

        private void dnsTable_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dnsTable.FirstDisplayedScrollingRowIndex = dnsTable.RowCount - 1;
        }
    }
}
