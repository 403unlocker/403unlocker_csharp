using _403Unlocker.Data_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _403Unlocker.Add_DNS.Online_Source
{
    public partial class ScraperForm : Form
    {
        public DnsConfig result;
        public Exception error;
        public enum Hostname
        {
            publicdns = 0,
        }

        private Hostname hostname;
        private Uri[] uris = new Uri[]
        {
            new Uri("https://publicdns.xyz"),
        };

        public ScraperForm(Hostname hostname)
        {
            InitializeComponent();

            this.hostname = hostname;
            labelHostname.Text = uris[(int)hostname].Host;
        }

        private void AddDot()
        {
            if (label1.Text.Count(c => c == '.') < 3) label1.Text += '.';
            else label1.Text = "Scraping";
        }

        private async void LoadingForm_Load(object sender, EventArgs e)
        {
            progressBar1.Size = new Size(labelHostname.Size.Width + labelHostname.Location.X - 12, progressBar1.Height);
            Width = progressBar1.Width + 40;

            timer1.Enabled = true;
            try
            {
                switch (hostname)
                {
                    case Hostname.publicdns:
                        result = await FetchDns.ScrapPublicDnsAsync();
                        break;
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception error)
            {
                DialogResult = DialogResult.Abort;
                this.error = error;
            }
            finally
            {
                Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            AddDot();
        }

        private void ScraperForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
