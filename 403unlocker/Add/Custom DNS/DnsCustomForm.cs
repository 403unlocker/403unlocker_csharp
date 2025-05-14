using _403unlocker.Notification;
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

        private void textBoxName_Validated(object sender, EventArgs e)
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

        private void TextBoxDnsValidated(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox.Text) || DnsConfig.IsIPv4(textBox.Text))
            {
                textBox.BackColor = colorTheme;
            }
            else textBox.BackColor = Color.Red;
        }

        private void TextBoxDnsKeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar)) return;
            else
            {
                if (e.KeyChar == '.')
                {
                    TextBox textBox = (sender as TextBox);
                    int n = textBox.Text.Count(x => x == '.');
                    if (n < 3) return;
                }
                else if (e.KeyChar == '\b') return;
            }
            e.Handled = true;
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // checks provider empty
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.Title = "Provider Can't Be Empty!";
                    form.Caption = "Empty Values!";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Stop;
                    form.ShowDialog();
                    return;
                }
            }
            // checks both of DNSs empty
            else if (string.IsNullOrEmpty(textBoxPrimaryDns.Text) && string.IsNullOrEmpty(textBoxSecondaryDns.Text))
            {
                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.Title = "Primary DNS & Secondry DNS can't be empty at same time!";
                    form.Caption = "Empty Values!";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Error;
                    form.ShowDialog();
                    return;
                }
            }
            // checks both of DNSs invalid
            else if (!DnsConfig.IsIPv4(textBoxPrimaryDns.Text) && !DnsConfig.IsIPv4(textBoxSecondaryDns.Text))
            {
                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.Title = "DNS value(s) are not valid!";
                    form.Caption = "Invalid Value!";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Error;
                    form.ShowDialog();
                    return;
                }
            }

            // checks which of DNSs is valid
            if (DnsConfig.IsIPv4(textBoxPrimaryDns.Text))
            {
                dns.Add(new DnsConfig()
                {
                    Provider = textBoxPrimaryDns.Text,
                    DNS = textBoxPrimaryDns.Text,
                });
            }
            if (DnsConfig.IsIPv4(textBoxSecondaryDns.Text))
            {
                dns.Add(new DnsConfig()
                {
                    Provider = textBoxPrimaryDns.Text,
                    DNS = textBoxSecondaryDns.Text,
                });
            }

            isAddPressed = true;
            Close();
        }
    }
}
