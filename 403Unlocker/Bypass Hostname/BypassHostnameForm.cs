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

namespace _403Unlocker.Bypass_Hostname
{
    public partial class BypassHostnameForm : Form
    {
        public BypassHostnameForm()
        {
            InitializeComponent();
        }

        public static bool IsHostNameValid(string hostname)
        {
            if (Regex.IsMatch(hostname, @"^(?!www\.)([^\W_]{1}[a-zA-Z\d\-]*){1}(\.[^\W_]{1}[a-zA-Z\d\-]*){0,60}(\.[a-z]+){1}$"))
            {
                return true;
            }
            return false;
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {


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
    }
}
