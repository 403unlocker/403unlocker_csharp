using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace _403Unlocker.Add_DNS
{
    public partial class CustomDnsForm : Form
    {
        public CustomDnsForm()
        {
            InitializeComponent();
        }

        public string IPv4 { get => $"{textBoxOctet1.Text}.{textBoxOctet2.Text}.{textBoxOctet3.Text}.{textBoxOctet4.Text}"; }
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
            return Octets.All(textBox => textBox.BackColor == Color.White && !string.IsNullOrEmpty(textBox.Text) );
        }

        private void MoveToOctet(TextBox octet)
        {
            octet.Focus();
            octet.SelectAll();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxOctets_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b') return;

            TextBox textBox = sender as TextBox;
            if (e.KeyChar == '.')
            {
                e.Handled = true;
                switch (textBox.Name)
                {
                    case "textBoxOctet1":
                        MoveToOctet(textBoxOctet2);
                        break;
                    case "textBoxOctet2":
                        MoveToOctet(textBoxOctet3);
                        break;
                    case "textBoxOctet3":
                        MoveToOctet(textBoxOctet4);
                        break;
                    case "textBoxOctet4":
                        textBoxProvider.Focus();
                        break;
                }
            }
            else if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxOctets_Validated(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.BackColor = Color.White;
            }
            else
            {
                int n = int.Parse(textBox.Text);
                if (n > 254)
                {
                    textBox.BackColor = Color.Red;
                }
                else
                {
                    textBox.BackColor = Color.White;
                }
            }

            buttonOk.Enabled = IsAllOctetsValid() ? true : false;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxProvider.Paste();
        }
    }
}
