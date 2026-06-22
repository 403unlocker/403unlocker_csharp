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
            numericUpDownPacketCount.Value = Settings.PacketCount;
            numericUpDownPacketSize.Value = Settings.PacketSize;
            numericUpDownPacketTimeout.Value = Settings.PacketTimeoutInMiliSeconds;

            numericUpDownDnsResolveTimeout.Value = (decimal)Settings.DnsResolveTimeoutInMilliSeconds;

            numericUpDownHttpRequestTimeout.Value = (decimal)Settings.HttpRequestTimeoutInMiliSeconds;
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isChanged)
            {
                Settings.PacketCount = Convert.ToInt32(numericUpDownPacketCount.Value);
                Settings.PacketSize = Convert.ToUInt16(numericUpDownPacketSize.Value);
                Settings.PacketTimeoutInMiliSeconds = Convert.ToInt32(numericUpDownPacketTimeout.Value);

                Settings.DnsResolveTimeoutInMilliSeconds = Convert.ToDouble(numericUpDownDnsResolveTimeout.Value);

                Settings.HttpRequestTimeoutInMiliSeconds = Convert.ToDouble(numericUpDownHttpRequestTimeout.Value);
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
