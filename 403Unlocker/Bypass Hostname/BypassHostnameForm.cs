using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clipboard_Manager;

namespace _403Unlocker.Bypass_Hostname
{
    public partial class BypassHostnameForm : Form
    {
        public BypassHostnameForm()
        {
            InitializeComponent();

            AcceptButton = buttonOK;
        }

        public string Hostname { get => textBoxHostname.Text; }

        public static bool IsHostNameValid(string hostname)
        {
            if (Regex.IsMatch(hostname, @"^[A-Za-z0-9]{1}(?:[A-Za-z0-9-]*[A-Za-z0-9]{1})?(?:\.[A-Za-z0-9]{1}(?:[A-Za-z0-9-]*[A-Za-z0-9]{1})?)*\.[A-Za-z]{2,63}$"))
            {
                return true;
            }
            return false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxHostname.Text) || !IsHostNameValid(textBoxHostname.Text)) buttonOK.Enabled = false;
            else buttonOK.Enabled = true;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxHostname.Paste();
        }
    }
}
