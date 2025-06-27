using System;
using System.Windows.Forms;

namespace _403unlocker.Config
{
    public partial class SettingsForm : Form
    {
        private bool wantToSave = false;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            checkBoxStartWithWindows.Checked = StartUp.isEnabled;

            checkBoxAutoSelection.Checked = Settings.NetworkAdaptor.AutoSelection;

            comboBoxNetworkInterfaces.AutoCompleteCustomSource.Clear();
            comboBoxNetworkInterfaces.Items.Clear();
            comboBoxNetworkInterfaces.Items.AddRange(Settings.NetworkAdaptor.AllNetworkInterfaces);
            comboBoxNetworkInterfaces.SelectedIndex = comboBoxNetworkInterfaces.Items.IndexOf(Settings.NetworkAdaptor.SelectedNetworkInterface);

            numericUpDownPacketCount.Value = Settings.Ping.PacketCount;
            numericUpDownPacketSize.Value = Settings.Ping.PacketSize;
            numericUpDownPingTimeOut.Value = Settings.Ping.TimeOutInMiliSeconds;

            numericUpDownDnsResolveTimeOut.Value = Settings.ByPass.DnsResolveTimeOutInMiliSeconds;
            numericUpDownHttpRequestTimeOut.Value = Settings.ByPass.HttpRequestTimeOutInMiliSeconds;
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (wantToSave)
            {
                StartUp.isEnabled = checkBoxStartWithWindows.Checked;

                Settings.NetworkAdaptor.AutoSelection = checkBoxAutoSelection.Checked;

                Settings.NetworkAdaptor.SelectedNetworkInterface = comboBoxNetworkInterfaces.SelectedItem as string;

                Settings.Ping.PacketCount = (int)numericUpDownPacketCount.Value;
                Settings.Ping.PacketSize = (ushort)numericUpDownPacketSize.Value;
                Settings.Ping.TimeOutInMiliSeconds = (int)numericUpDownPingTimeOut.Value;

                Settings.ByPass.DnsResolveTimeOutInMiliSeconds = (int)numericUpDownDnsResolveTimeOut.Value;
                Settings.ByPass.HttpRequestTimeOutInMiliSeconds = (int)numericUpDownHttpRequestTimeOut.Value;
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            wantToSave = true;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBoxAutoSelection_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxNetworkInterfaces.Enabled = !checkBoxAutoSelection.Checked;
        }
    }
}
