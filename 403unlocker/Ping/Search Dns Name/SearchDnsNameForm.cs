using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _403unlocker.Ping.Search_Dns_Name
{
    public partial class SearchDnsNameForm : Form
    {
        public bool isOkPressed = false;
        public SearchDnsNameForm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            textBoxDns.Text = textBoxDns.Text.Trim();
            isOkPressed = true;
            Close();
        }

        private void textBoxDns_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
