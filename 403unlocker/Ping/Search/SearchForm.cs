using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _403unlocker.Ping.Search
{
    public partial class SearchForm : Form
    {
        public bool isOkPressed = false;
        private readonly bool SearchDns;

        public SearchForm(bool searchDns)
        {
            InitializeComponent();

            SearchDns = searchDns;
            string s = searchDns ? "DNS" : "Provider";
            Text += ' ' + s;
            label1.Text = s + " :";
            textBox1.Location = new Point(6 + label1.Location.X + label1.Size.Width, textBox1.Location.Y);
            Size = new Size(32 + textBox1.Location.X + textBox1.Size.Width, Size.Height);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
            isOkPressed = true;
            Close();
        }

        private void textBoxDns_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SearchDns)
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
            else
            {
                
            }
        }

    }
}
