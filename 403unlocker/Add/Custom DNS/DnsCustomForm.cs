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
        public bool isAddPressed = false;
        public List<DnsConfig> dns = new List<DnsConfig>();
        private Color colorTheme = Color.FromArgb(0x2C, 0xD4, 0xBF);
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
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                textBoxName.BackColor = Color.Red;
            }
            else
            {
                textBoxName.BackColor = colorTheme;
            }
        }

        private void primaryDnsTextBox_Validated(object sender, EventArgs e)
        {
            string primaryDns = primaryDnsTextBox.Text;
            if ((string.IsNullOrEmpty(primaryDns) ^ DnsConfig.IsIPv4(primaryDns)) && primaryDns.Count(c => c == '.') < 4)
            {
                primaryDnsTextBox.BackColor = colorTheme;
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
                secondaryDnsTextBox.BackColor = colorTheme;
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
            if (string.IsNullOrEmpty(textBoxName.Text))
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
                    
            // checks which of DNSs is valid
            if (DnsConfig.IsIPv4(primaryDnsTextBox.Text))
            {
                dns.Add(new DnsConfig()
                {
                    Name = primaryDnsTextBox.Text,
                    DNS = primaryDnsTextBox.Text,
                });
            }
            else if (DnsConfig.IsIPv4(secondaryDnsTextBox.Text))
            {
                dns.Add(new DnsConfig()
                {
                    Name = primaryDnsTextBox.Text,
                    DNS = secondaryDnsTextBox.Text,
                });
            }

            isAddPressed = true;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox textBox = contextMenuStrip1.SourceControl as TextBox;
            textBox.Undo();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox textBox = contextMenuStrip1.SourceControl as TextBox;
            if (textBox.SelectionLength > 0) textBox.Copy();
            else Clipboard.SetText(textBox.Text);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox textBox = contextMenuStrip1.SourceControl as TextBox;
            textBox.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox textBox = contextMenuStrip1.SourceControl as TextBox;
            textBox.SelectedText = "";
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox textBox = contextMenuStrip1.SourceControl as TextBox;
            textBox.SelectAll();
        }
    }
}
