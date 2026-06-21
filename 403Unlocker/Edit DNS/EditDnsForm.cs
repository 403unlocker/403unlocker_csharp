using Clipboard_Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _403Unlocker.Edit_DNS
{
    public partial class EditDnsForm : Form
    {

        public EditDnsForm(IPAddress ipv4, string provider)
        {
            InitializeComponent();

            AcceptButton = buttonOK;

            string[] octets = ipv4.ToString().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            textBoxOctet1.Text = octets[0];
            textBoxOctet2.Text = octets[1];
            textBoxOctet3.Text = octets[2];
            textBoxOctet4.Text = octets[3];

            textBoxProvider.Text = provider;
        }

        public IPAddress IPv4 { get => IPAddress.Parse($"{textBoxOctet1.Text}.{textBoxOctet2.Text}.{textBoxOctet3.Text}.{textBoxOctet4.Text}"); }
        public string Provider { get => textBoxProvider.Text; }

        private TextBox[] Octets
        {
            get
            {
                return new TextBox[]
                {
                textBoxOctet1,
                textBoxOctet2,
                textBoxOctet3,
                textBoxOctet4
                };
            }
        }

        private bool IsAllOctetsValid()
        {
            return !Octets.Any(textBox => string.IsNullOrEmpty(textBox.Text)) && Octets.All(textBox => int.Parse(textBox.Text) < 255);
        }

        private void MoveToNextOctet(TextBox octet)
        {
            switch (octet.Name)
            {
                case "textBoxOctet1":
                    MoveToTextBox(textBoxOctet2, true);
                    break;
                case "textBoxOctet2":
                    MoveToTextBox(textBoxOctet3, true);
                    break;
                case "textBoxOctet3":
                    MoveToTextBox(textBoxOctet4, true);
                    break;
                case "textBoxOctet4":
                    SystemSounds.Exclamation.Play();
                    break;
            }
        }

        private void MoveToPreviousOctet(TextBox octet)
        {
            switch (octet.Name)
            {
                case "textBoxOctet1":
                    SystemSounds.Exclamation.Play();
                    break;
                case "textBoxOctet2":
                    MoveToTextBox(textBoxOctet1, false);
                    break;
                case "textBoxOctet3":
                    MoveToTextBox(textBoxOctet2, false);
                    break;
                case "textBoxOctet4":
                    MoveToTextBox(textBoxOctet3, false);
                    break;
            }
        }

        private void MoveToTextBox(TextBox Octet, bool selectAllText)
        {
            Octet.Focus();
            if (selectAllText) Octet.SelectAll();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void textBoxOctets_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (e.KeyChar == '\b')
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    e.Handled = true;
                    MoveToPreviousOctet(textBox);
                }
            }
            else if (e.KeyChar == '.')
            {
                e.Handled = true;
                MoveToNextOctet(textBox);
            }
            else if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxOctets_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox.Text)) textBox.BackColor = Color.White;
            else
            {
                int n = int.Parse(textBox.Text);
                if (n > 254) textBox.BackColor = Color.Red;
                else textBox.BackColor = Color.White;
            }

            buttonOK.Enabled = IsAllOctetsValid() ? true : false;
        }

        private void pasteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            textBoxProvider.Paste();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardManager.CopyToClipboard(string.Join(".", Octets.Select(textBox => textBox.Text)));
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ipv4ToBePaste = ClipboardManager.PasteFromClipboard();
            string[] octets = ipv4ToBePaste.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (!octets.All(octet => octet.All(c => char.IsDigit(c)) && int.Parse(octet) < 255))
            {
                MessageBox.Show("The clipboard does not contain a valid IPv4 address",
                               "Invalid IPv4 Address",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
            }

            textBoxOctet1.Text = octets[0];
            textBoxOctet2.Text = octets[1];
            textBoxOctet3.Text = octets[2];
            textBoxOctet4.Text = octets[3];
        }
    }
}
