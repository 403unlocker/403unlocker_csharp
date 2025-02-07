using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _403unlockerLibrary;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _403unlocker
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            networkComboBox.AutoCompleteCustomSource.Clear();
            networkComboBox.Items.Clear();
            networkComboBox.Items.AddRange(NetworkSettingsManager.GetNetworkInterfaceName(false));
            networkComboBox.SelectedIndex = networkComboBox.Items.IndexOf(Setting.SelectedNetworkInterface);
            autoSelectionCheckBox.Checked = Setting.NetworkInterfaceAutoSelection;
        }

        private void getPingButton_Click(object sender, EventArgs e)
        {
            // Network Interface
            string selectedNetworkInterface = networkComboBox.SelectedItem as string;
            Setting.NetworkInterfaceAutoSelection = autoSelectionCheckBox.Checked;
            Setting.SelectedNetworkInterface = selectedNetworkInterface;
            Close();
        }

        private void autoSelectionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            networkComboBox.Enabled = !autoSelectionCheckBox.Checked;
        }
    }
}
