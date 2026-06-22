using Network_Utilities.Ping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _403Unlocker.Configuration
{
    public partial class SettingsForm : Form
    {
        private bool isChanged = false;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            numericUpDownPingPacketCount.Value = Settings.PacketCount;
            numericUpDownPingPacketSize.Value = Settings.PacketSize;
            numericUpDownPingTimeout.Value = Settings.PacketTimeoutInMiliSeconds;

            numericUpDownDnsResolveTimeout.Value = (decimal)Settings.DnsResolveTimeoutInMilliSeconds;

            numericUpDownTcpConnectTimeout.Value = Settings.BypassTcpConnectTimeoutInMilliSeconds;
            numericUpDownTlsHandshakeTimeout.Value = Settings.BypassTlsHandshakeTimeoutInMilliSeconds;

            numericUpDownScraperHttpRequestTimeout.Value = (decimal)Settings.ScraperHttpRequestTimeoutInMiliSeconds;

            numericUpDownMaxParallelRequests.Value = Settings.MaxParallelRequests;
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isChanged)
            {
                Settings.PacketCount = Convert.ToInt32(numericUpDownPingPacketCount.Value);
                Settings.PacketSize = Convert.ToUInt16(numericUpDownPingPacketSize.Value);
                Settings.PacketTimeoutInMiliSeconds = Convert.ToInt32(numericUpDownPingTimeout.Value);

                Settings.DnsResolveTimeoutInMilliSeconds = Convert.ToDouble(numericUpDownDnsResolveTimeout.Value);

                Settings.BypassTcpConnectTimeoutInMilliSeconds = Convert.ToInt32(numericUpDownTcpConnectTimeout.Value);
                Settings.BypassTlsHandshakeTimeoutInMilliSeconds = Convert.ToInt32(numericUpDownTlsHandshakeTimeout.Value);

                Settings.ScraperHttpRequestTimeoutInMiliSeconds = Convert.ToDouble(numericUpDownScraperHttpRequestTimeout.Value);

                Settings.MaxParallelRequests = Convert.ToInt32(numericUpDownMaxParallelRequests.Value);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void numericUpDownPacketCount_ValueChanged(object sender, EventArgs e)
        {
            isChanged = true;
        }
    }
}
