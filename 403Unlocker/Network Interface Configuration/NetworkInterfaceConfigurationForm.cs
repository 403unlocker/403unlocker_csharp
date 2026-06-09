using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows_Services;
using Windows_Services.Netwrok_Interface_Service;
using static System.Net.Mime.MediaTypeNames;

namespace _403Unlocker.Network_Interface_Configuration
{
    public partial class NetworkInterfaceConfigurationForm : Form
    {
        private IPAddress selectedDns;

        public NetworkInterfaceConfigurationForm(IPAddress dns)
        {
            InitializeComponent();

            selectedDns = dns;

            comboBox1.DataSource = NetworkInterfaceService.GetAllNetworkInterfaces()
                                                          .Where(x => x.GetIPProperties().IsDynamicDnsEnabled && x.GetIPProperties().GetIPv4Properties().IsDhcpEnabled)
                                                          .Select(networkInterface => networkInterface.Name)
                                                          .ToArray();
            comboBox1.SelectedIndex = -1;

            labelSelectedDns.Text = $"Selected DNS: {dns}";
        }

        private NetworkInterface Interface
        {
            get
            {
                return NetworkInterfaceService.GetAllNetworkInterfaces()
                                              .First(x => x.Name == comboBox1.SelectedItem.ToString());
            }
        }

        private NetworkInterface ActiveInterface 
        {
            get
            {
                return NetworkInterfaceService.GetAllNetworkInterfaces()
                                              .First(@interface => @interface.GetIPProperties().GatewayAddresses
                                              .Any(getaway => getaway.Address.AddressFamily == AddressFamily.InterNetwork));
            }
        }

        private IPAddress[] InterfaceDnsServers { get => NetworkInterfaceService.GetIPv4DnsServersConfiguration(Interface).Take(2).ToArray(); }
        private IPAddress[] InterfaceDhcpServers { get => NetworkInterfaceService.GetIPv4DhcpServersConfiguration(Interface).Take(2).ToArray(); }

        private static DialogResult MessageBoxAlreadySet(IPAddress dns)
        {
            var r = MessageBox.Show($"DNS server {dns} is already configured on the selected network adapter",
                                    "DNS Already Configured",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
            return r;
        }

        private static DialogResult MessageBoxPrimarySuccess(NetworkInterface @interface)
        {
            var r = MessageBox.Show($"The primary DNS server has been configured successfully on {@interface.Name}",
                                    "Primary DNS Updated",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            return r;
        }

        private static DialogResult MessageBoxSecondarySuccess(NetworkInterface @interface)
        {
            var r = MessageBox.Show($"The secondary DNS server has been configured successfully on {@interface.Name}",
                                    "Secondary DNS Updated",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            return r;
        }

        private static DialogResult MessageBoxResetSuccess(NetworkInterface @interface)
        {
            var r = MessageBox.Show($"DNS settings for {@interface.Name} have been restored to automatic configuration.",
                                    "DNS Settings Reset",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            return r;
        }

        private void SetButtonsEnabilityState(bool state)
        {
            buttonSetAsPrimary.Enabled = state;
            buttonSetAsSecondary.Enabled = state;
            buttonResetDNS.Enabled = state;
        }

        private void DisplayDnsConfiguration()
        {
            IPAddress[] dns = InterfaceDnsServers.Except(InterfaceDhcpServers).ToArray();

            labelDns.ResetText();

            if (dns.Length == 0)
            {
                labelDns.Text = "There is no DNS";
            }
            else if (dns.Length == 1)
            {
                labelDns.Text = $"Primary DNS: {dns[0]}";
            }
            else if (dns.Length == 2)
            {
                labelDns.Text = $"Primary DNS: {dns[0]}";
                labelDns.Text += $"\r\nSecondary DNS: {dns[1]}";
            }
        }

        private async Task SetPrimaryDnsAsync(IPAddress primary)
        {
            IPAddress[] dns = InterfaceDnsServers.Except(InterfaceDhcpServers).ToArray();
            await NetworkInterfaceService.ResetDnsConfigurationAsync(Interface.Name);
            await NetworkInterfaceService.SetInterfaceDnsAsync(Interface.Name, primary, 1, false);

            if (dns.Length > 1)
            {
                IPAddress secondary = dns[1];
                await NetworkInterfaceService.SetInterfaceDnsAsync(Interface.Name, secondary, 2, false);
            }
        }

        private async Task SetSecondaryDnsAsync(IPAddress secondary)
        {
            IPAddress[] dns = InterfaceDnsServers.Except(InterfaceDhcpServers).ToArray();
            await NetworkInterfaceService.ResetDnsConfigurationAsync(Interface.Name);
            await NetworkInterfaceService.SetInterfaceDnsAsync(Interface.Name, dns[0], 1, false);
            await NetworkInterfaceService.SetInterfaceDnsAsync(Interface.Name, secondary, 2, false);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                SetButtonsEnabilityState(false);
                labelDns.Text = "Please, select a network interface";
                return;
            }
            SetButtonsEnabilityState(true);

            DisplayDnsConfiguration();
        }

        private async void buttonSetAsPrimary_Click(object sender, EventArgs e)
        {
            if (InterfaceDnsServers.Contains(selectedDns))
            {
                MessageBoxAlreadySet(selectedDns);
                return;
            }

            await SetPrimaryDnsAsync(selectedDns);

            DisplayDnsConfiguration();
            MessageBoxPrimarySuccess(Interface);
        }

        private async void buttonSetAsSecondary_Click(object sender, EventArgs e)
        {
            if (InterfaceDnsServers.Contains(selectedDns))
            {
                MessageBoxAlreadySet(selectedDns);
                return;
            }

            await SetSecondaryDnsAsync(selectedDns);

            DisplayDnsConfiguration();
            MessageBoxSecondarySuccess(Interface);
        }

        private async void buttonResetDNS_Click(object sender, EventArgs e)
        {
            await NetworkInterfaceService.ResetDnsConfigurationAsync(Interface.Name);
            DisplayDnsConfiguration();
            MessageBoxResetSuccess(Interface);
        }

        private void labelDns_TextChanged(object sender, EventArgs e)
        {
            if (!labelDns.Text.Contains("Primary"))
            {
                buttonSetAsSecondary.Enabled = false;
            }
            else
            {
                buttonSetAsSecondary.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboBox1.Enabled = false;
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf(ActiveInterface.Name);
            }
            else
            {
                comboBox1.Enabled = true;
            }
        }
        
    }
}
