using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using _403unlocker.Config;
using _403unlocker.Ping;

namespace _403unlocker.Config
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            networkComboBox.AutoCompleteCustomSource.Clear();
            networkComboBox.Items.Clear();
            networkComboBox.Items.AddRange(NetworkSettings.GetNetworkInterfaceName(false));
            networkComboBox.SelectedIndex = networkComboBox.Items.IndexOf(Settings.SelectedNetworkInterface);
            autoSelectionCheckBox.Checked = Settings.NetworkInterfaceAutoSelection;
        }

        private void getPingButton_Click(object sender, EventArgs e)
        {
            // Network Interface
            string selectedNetworkInterface = networkComboBox.SelectedItem as string;
            Settings.NetworkInterfaceAutoSelection = autoSelectionCheckBox.Checked;
            Settings.SelectedNetworkInterface = selectedNetworkInterface;
            Close();
        }

        private void autoSelectionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            networkComboBox.Enabled = !autoSelectionCheckBox.Checked;
        }
    }
}
