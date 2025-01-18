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

namespace _403unlocker
{
    public partial class DnsCustomeAdderForm : Form
    {
        public bool isFormClosePressed = true, isAddButtonPressed = false;
        private Color themeColor = Color.FromArgb(0x2C, 0xD4, 0xBF);
        public DnsCustomeAdderForm()
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
            if ((string.IsNullOrEmpty(primaryDns) ^ DnsProvider.IsIPv4(primaryDns)) && primaryDns.Count(c => c == '.') < 4)
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
            if ((string.IsNullOrEmpty(secondaryDns) ^ DnsProvider.IsIPv4(secondaryDns)) && secondaryDns.Count(c => c == '.') < 4)
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
            // checks provider not empty => true
            if (!string.IsNullOrEmpty(providerTextBox.Text))
            {
                // checks one of DNSs empty => true (both empty => false, both not empty => true)
                if (!(string.IsNullOrEmpty(primaryDnsTextBox.Text) && string.IsNullOrEmpty(secondaryDnsTextBox.Text)))
                {
                    // checks one of DNSs valid => true (both valid => true, both not valid => fasle)
                    if (DnsProvider.IsIPv4(primaryDnsTextBox.Text) || DnsProvider.IsIPv4(secondaryDnsTextBox.Text))
                    {
                        // checks one of DNSs valid => true (both valid => false, both not valid => false)
                        if (DnsProvider.IsIPv4(primaryDnsTextBox.Text) ^ DnsProvider.IsIPv4(secondaryDnsTextBox.Text))
                        {
                            List<TextBox> textBox = new List<TextBox>() { primaryDnsTextBox, secondaryDnsTextBox };
                            // one of DNSs is valid, then empty one of DNSs which is not valid
                            textBox.Where(x => !DnsProvider.IsIPv4(x.Text)).First().Text = "";
                        }
                        isFormClosePressed = false;
                        isAddButtonPressed = true;
                        Close();
                    }
                    else
                    {
                        string text = "DNS value(s) are not valid!";
                        string caption = "Invalid Value!";
                        MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   
                }
                else
                {
                    string text = "Primary DNS & Secondry DNS can't be empty at same time!";
                    string caption = "Empty Values!";
                    MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Provider Can't Be Empty!", "Empty Values!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            isFormClosePressed = false;
            isAddButtonPressed = false;
            Close();
        }
    }
}
