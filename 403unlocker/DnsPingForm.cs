using DnsClient;
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
using System.Management;
using System.Diagnostics;

namespace _403unlocker
{
    public partial class DnsPingForm : Form
    {
        internal BindingList<DnsPing> dnsPingBinding = new BindingList<DnsPing>();
        private List<string> urlList = new List<string>();
        public DnsPingForm()
        {
            InitializeComponent();
        }

        private void PingDnsForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dnsPingBinding;
            dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["DNS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Latency"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }

        private async void pcPingButton_Click(object sender, EventArgs e)
        {
            foreach (DnsPing dnsPing in dnsPingBinding)
            {
                await dnsPing.GetPing(5000);
            }
            dataGridView1.Invalidate();
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            // sort by status
            List<DnsPing> sortedDnsPing = dnsPingBinding.OrderBy(dnsPing => dnsPing.Status)
                                                            // then sort by ping
                                                            .ThenBy(dnsPing => dnsPing.Latency)
                                                            .ToList();
            dnsPingBinding = new BindingList<DnsPing>(sortedDnsPing);
            dataGridView1.DataSource = dnsPingBinding;
        }

        private async void sitePingButton_Click(object sender, EventArgs e)
        {
            foreach (DnsPing dnsPing in dnsPingBinding)
            {
                await dnsPing.GetPing(urlTextBox.Text, 5000);
            }
            dataGridView1.Invalidate();
        }

        private void urlTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') e.Handled = true;
        }

        
    }
}
