using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _403unlocker
{
    public partial class PingDnsForm : Form
    {
        internal BindingList<DnsPing> dnsPingBinding = new BindingList<DnsPing>();

        public PingDnsForm()
        {
            InitializeComponent();
        }

        private void PingDnsForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dnsPingBinding;
            dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["DNS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns["URL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Latency"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
    }
}
