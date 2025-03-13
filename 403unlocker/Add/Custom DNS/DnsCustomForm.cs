using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _403unlocker.Add.Custom_DNS
{
    public partial class DnsCustomForm : Form
    {
        public bool isFormClosePressed = true, isAddButtonPressed = false;
        private Color themeColor = Color.FromArgb(0x2C, 0xD4, 0xBF);
        public DnsCustomForm()
        {
            InitializeComponent();
        }

        private static bool IsKeyValid(char key)
        {
            // logic compare
            // key is digit
            // key is not digit
            //// key is backspace => '\b'
            //// key is dot => '.'
            // let key in
           
            if (!char.IsDigit(key) && key != '\b' && key != '.') return false;
            return true;
        }

        private void providerTextBox_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(providerTextBox.Text))
            {
                providerTextBox.BackColor = Color.Red;
            }
            else
            {
                providerTextBox.BackColor = themeColor;
            }
        }

        private void primaryDnsTextBox_Validated(object sender, EventArgs e)
        {
            string primaryDns = primaryDnsTextBox.Text;
            if ((string.IsNullOrEmpty(primaryDns) ^ DnsConfig.IsIPv4(primaryDns)) && primaryDns.Count(c => c == '.') < 4)
            {
                primaryDnsTextBox.BackColor = themeColor;
            }
            else
            {
                primaryDnsTextBox.BackColor = Color.Red;
            }
        }

        private void secondaryDnsTextBox_Validated(object sender, EventArgs e)
        {
            string secondaryDns = secondaryDnsTextBox.Text;
            if ((string.IsNullOrEmpty(secondaryDns) ^ DnsConfig.IsIPv4(secondaryDns)) && secondaryDns.Count(c => c == '.') < 4)
            {
                secondaryDnsTextBox.BackColor = themeColor;
            }
            else
            {
                secondaryDnsTextBox.BackColor = Color.Red;
            }
        }

        private void primaryDnsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsKeyValid(e.KeyChar)) e.Handled = true;
        }

        private void secondaryDnsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsKeyValid(e.KeyChar)) e.Handled = true;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // checks provider empty
            if (string.IsNullOrEmpty(providerTextBox.Text))
            {
                MessageBox.Show("Provider Can't Be Empty!",
                                "Empty Values!", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Stop);
                return;
            }

            // checks both of DNSs empty
            if (string.IsNullOrEmpty(primaryDnsTextBox.Text) && string.IsNullOrEmpty(secondaryDnsTextBox.Text))
            {
                MessageBox.Show("Primary DNS & Secondry DNS can't be empty at same time!",
                                "Empty Values!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
                
            // checks both of DNSs invalid
            if (!DnsConfig.IsIPv4(primaryDnsTextBox.Text) && !DnsConfig.IsIPv4(secondaryDnsTextBox.Text))
            {
                MessageBox.Show("DNS value(s) are not valid!",
                                "Invalid Value!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
                    
            // checks which of DNSs is invalid
            if (!DnsConfig.IsIPv4(primaryDnsTextBox.Text))
            {
                primaryDnsTextBox.Text = "";
            }
            else if (!DnsConfig.IsIPv4(secondaryDnsTextBox.Text))
            {
                secondaryDnsTextBox.Text = "";
            }

            isFormClosePressed = false;
            isAddButtonPressed = true;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            isFormClosePressed = false;
            isAddButtonPressed = false;
            Close();
        }
    }
}
