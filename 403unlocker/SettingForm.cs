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
            comboBox1.AutoCompleteCustomSource.Clear();
            comboBox1.Items.Clear();

            var networks = NetworkSettingsManager.GetNetworkInterfaceName(false);
            string[] networksNames = networks.Select(netwrok => netwrok.Name).ToArray();

            comboBox1.Items.AddRange(networksNames);
            comboBox1.Items.Add("Auto");
        }

        private void getPingButton_Click(object sender, EventArgs e)
        {
            // Network Interface
            string selectedNetworkInterface = comboBox1.SelectedItem as string;
            if (selectedNetworkInterface == "Auto")
            {
                Setting.IsAutoSelection = true;
            }
            else
            {
                Setting.IsAutoSelection = false;
                Setting.SelectedNetworkInterface = selectedNetworkInterface;
            }
        }
    }
}
