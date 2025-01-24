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

        public DnsPingForm()
        {
            InitializeComponent();
        }

        static void ChangeDnsSettings(string interfaceName, string primaryDns, string secondaryDns)
        {
            try
            {
                // Command to set DNS servers
                string command = $"interface ip set dns name=\"{interfaceName}\" static {primaryDns} primary";
                ExecuteCommand(command);

                // Command to set secondary DNS
                string secondaryCommand = $"interface ip add dns name=\"{interfaceName}\" {secondaryDns} index=2";
                ExecuteCommand(secondaryCommand);

                Console.WriteLine("DNS settings changed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ExecuteCommand(string command)
        {
            // Run the command using netsh via the command line
            ProcessStartInfo psi = new ProcessStartInfo("netsh", command)
            {
                Verb = "runas", // This ensures that the command runs with admin privileges
                CreateNoWindow = true,
                UseShellExecute = false
            };

            Process process = Process.Start(psi);
            process.WaitForExit();
        }

        private void PingDnsForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dnsPingBinding;
            dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["DNS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Latency"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            ChangeDnsSettings("Ethernet", "8.8.8.8", "8.8.4.4");
        }

        private async void pcPingButton_Click(object sender, EventArgs e)
        {
            foreach (DnsPing dnsPing in dnsPingBinding)
            {
                await dnsPing.GetPing(5000);
            }
            dataGridView1.Invalidate();
        }

        private void copyDnsCellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string selectedRowDns = dataGridView1.SelectedRows[0].Cells["DNS"].Value.ToString();
                Clipboard.SetText(selectedRowDns);
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
                DnsPing foundRecord = dnsPingBinding.First(dnsPing => dnsPing.DNS == selectedRowDns);
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
            List<DnsPing> sortedDnsPing = dnsPingBinding.OrderBy(dnsPing => dnsPing.Status)
                                                            // then sort by ping
                                                            .ThenBy(dnsPing => dnsPing.Latency)
                                                            .ToList();
            dnsPingBinding = new BindingList<DnsPing>(sortedDnsPing);
            dataGridView1.DataSource = dnsPingBinding;
        }

        private async void sitePingButton_Click(object sender, EventArgs e)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(5);
            foreach (DnsPing dnsPing in dnsPingBinding)
            {
                await dnsPing.GetPing(urlTextBox.Text, timeSpan);
            }
            dataGridView1.Invalidate();
        }

        private void urlTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') e.Handled = true;
        }

        
    }
}
