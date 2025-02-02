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
using _403unlockerLibrary;

namespace _403unlocker
{
    public partial class DnsPingForm : Form
    {
        internal BindingList<NetworkUtility> dnsPingBinding;
        AutoCompleteStringCollection suggestions = new AutoCompleteStringCollection();

        string pathUrl = "url";
        string[] urlDefault = new string[]
        {
            "nvidia.com",
            "asus.com",
            "lenovo.com",
            "youtube.com",
            "chatgpt.com",
            "x.com",
            "developers.google.com",
            "pkg.go.dev"
        };

        public DnsPingForm(List<DnsProvider> dnsProviders)
        {
            InitializeComponent();

            List<NetworkUtility> dnsPings = dnsProviders.Select(dnsRecord => new NetworkUtility(dnsRecord)).ToList();
            dnsPingBinding = new BindingList<NetworkUtility>(dnsPings);

            dataGridView1.DataSource = dnsPingBinding;
            dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["DNS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Latency"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //Set the properties for the TextBox
            suggestions.AddRange(urlDefault);
            urlTextBox.AutoCompleteCustomSource = suggestions;
        }

        private async void pcPingButton_Click(object sender, EventArgs e)
        {
            foreach (NetworkUtility dnsPing in dnsPingBinding)
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

        private async void getPingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string selectedRowDns = dataGridView1.SelectedRows[0].Cells["DNS"].Value.ToString();
                NetworkUtility foundRecord = dnsPingBinding.First(dnsPing => dnsPing.DNS == selectedRowDns);
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
            List<NetworkUtility> sortedDnsPing = dnsPingBinding.OrderBy(dnsPing => dnsPing.Status)
                                                            // then sort by ping
                                                            .ThenBy(dnsPing => dnsPing.Latency)
                                                            .ToList();
            dnsPingBinding = new BindingList<NetworkUtility>(sortedDnsPing);
            dataGridView1.DataSource = dnsPingBinding;
        }

        private async void sitePingButton_Click(object sender, EventArgs e)
        {
            if (!NetworkUtility.IsValidHostname(urlTextBox.Text))
            {
                MessageBox.Show("Please type correct Hostname\n\nLike this:\nwww.google.com\ngoogle.com",
                                "Hostname is wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (NetworkUtility dnsPing in dnsPingBinding)
            {
                await dnsPing.GetPing(urlTextBox.Text, 5000);
            }
            dataGridView1.Invalidate();
        }
    }
}
