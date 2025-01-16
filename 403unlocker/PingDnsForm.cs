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

namespace _403unlocker
{
    public partial class PingDnsForm : Form
    {
        BindingList<DnsRecord> dnsRecordsBindingList = new BindingList<DnsRecord>();
        public PingDnsForm()
        {
            InitializeComponent();
            //dnsRecordsBindingList = new BindingList<DnsRecord>(dnsRecords);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string dnsServer = textBox2.Text; // Replace with the DNS server you want to ping
            //Ping pingSender = new Ping();
            //PingReply reply = pingSender.Send(dnsServer);

            //if (reply.Status == IPStatus.Success)
            //{
            //    textBox1.Text += $"Ping to {dnsServer} successful:\r\n";
            //    textBox1.Text += $"Address: {reply.Address}\r\n";
            //    textBox1.Text += $"RoundTrip time: {reply.RoundtripTime} ms\r\n"; // latency
            //    textBox1.Text += $"Time to live: {reply.Options.Ttl}\r\n"; // ttl
            //    textBox1.Text += $"Don't fragment: {reply.Options.DontFragment} \r\n";
            //    textBox1.Text += $"Buffer size: {reply.Buffer.Length} \r\n\r\n"; // bytes
            //}
            //else
            //{
            //    textBox1.Text += $"Ping to {dnsServer} failed. Status: {reply.Status}\r\n\r\n";
            //}
        }

        private void PingDnsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
