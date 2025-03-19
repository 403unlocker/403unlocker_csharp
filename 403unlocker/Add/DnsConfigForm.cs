using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Windows.Forms.LinkLabel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Data.Common;
using System.Net;
using System.Xml.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using _403unlocker.Ping;
using _403unlocker.Add.Custom_DNS;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using _403unlocker.Notification;

namespace _403unlocker.Add
{
    public partial class DnsConfigForm : Form
    {
        public bool isApplyPressed = false, isTableChanged;
        public BindingList<DnsConfig> dnsBinding = new BindingList<DnsConfig>();
        private List<DnsConfig> dnsImported;

        private string pathTimer = "dnstimer.bson";
        private DateTime dateTime;
        private TimeSpan timeSpan, timeSpanLimitation = TimeSpan.FromMinutes(7);
        private bool needToBeWritten = false;
        public DnsConfigForm(List<DnsConfig> dnsCurrent, List<DnsConfig> dnsImported)
        {
            InitializeComponent();

            labelDnsCount.Text = "Count: 0";

            dnsBinding = new BindingList<DnsConfig>(dnsCurrent);
            this.dnsImported = dnsImported;

            dataGridView1.DataSource = dnsBinding; // Links dataGridView to BindingList variable

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void DnsCollectorForm_Load(object sender, EventArgs e)
        {
            isTableChanged = false;
            if (!(dnsImported is null))
            {
                AppendData(dnsImported, true);
            }

            try
            {
                dateTime = ReadBson(pathTimer);
                timeSpan = DateTime.Now - dateTime;
                if (timeSpanLimitation.TotalSeconds >= timeSpan.TotalSeconds) // 7 min
                {
                    timeSpan = timeSpanLimitation - timeSpan;
                    labelTimer.Text = $"Cooldown:\r\n{timeSpan:mm\\:ss}";
                    labelTimer.Show();
                    timer1.Start();
                }
            }
            catch (Exception error) when (error is FileNotFoundException || error is FileLoadException)
            {
                // Do Nothing
            }
        }

        private void DnsCollectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isApplyPressed && isTableChanged)
            {
                var r = MessageBox.Show("Do you want to discard changes?",
                                        "Closing",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No) e.Cancel = true;
            }
        }

        private void DnsConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (needToBeWritten)
            {
                WriteBson(dateTime, pathTimer);
            }
            timer1.Stop();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Are you sure about that?", "We are clearing", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (r == DialogResult.Yes)
            {
                dnsBinding.Clear();
                labelDnsCount.Text = "DNS Count: 0";
            }
        }

        private void ScrollDownToEnd()
        {
            if (dataGridView1.RowCount > 0)
            {
                int lastRowIndex = dataGridView1.RowCount - 1;
                dataGridView1.FirstDisplayedScrollingRowIndex = lastRowIndex;
                dataGridView1.Rows[lastRowIndex].Selected = true;
            }
        }

        private void AppendData(List<DnsConfig> additionDnsList ,bool statusMessages = true)
        {
            // finds new DNSs
            List<DnsConfig> newDns = additionDnsList.Except(dnsBinding).ToList();
            // counts new DNSs
            int newDnsCount = newDns.Count();
            // counts duplicate DNSs
            int duplicateDnsCount = additionDnsList.Count() - newDnsCount;

            foreach (DnsConfig dns in newDns)
            {
                dnsBinding.Add(dns);
            }

            if (statusMessages)
            {
                string text, caption;
                if (newDnsCount > 0)
                {
                    text = $"New DNS(s) has been successfully added!\n\nNew DNSs: {newDnsCount}\nDuplicate DNSs: {duplicateDnsCount}";
                    caption = "Successfully Updated 🎉";
                    ScrollDownToEnd();
                    MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    text = "DNS(s) already exist";
                    caption = "No Duplicates Allowed 🛑";
                    MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void buttonDefaultDns_Click(object sender, EventArgs e)
        {
            AppendData(Data.DnsScraper.DefaultList());
        }

        private void buttonPublicDns_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                MessageBox.Show("You have time to wait, be patient",
                    "Hold Your Horses🐎!", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Hand);
                return;
            }

            using (var form = new MessageBoxWait(Data.DnsScraper.Get()))
            {
                form.ShowDialog();
            }

            if (Data.DnsScraper.Errors.Message != "")
            {
                MessageBox.Show(Data.DnsScraper.Errors.Message,
                   "Somthing went wrong",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return;
            }

            AppendData(Data.DnsScraper.Values);

            dateTime = DateTime.Now;
            timeSpan = timeSpanLimitation;
            labelTimer.Text = $"Cooldown:\r\n{timeSpan:mm\\:ss}";
            labelTimer.Show();
            timer1.Start();
            needToBeWritten = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeSpan.TotalSeconds < 1)
            {
                timer1.Stop();
                labelTimer.Hide();
                needToBeWritten = false;
                return;
            }
            timeSpan -= TimeSpan.FromSeconds(1);
            labelTimer.Text = $"Cooldown:\r\n{timeSpan:mm\\:ss}";
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            labelDnsCount.Text = "Count: " + dataGridView1.RowCount;
            isTableChanged = true;
        }

        private void timerPublicDns_Tick(object sender, EventArgs e)
        {
            string s = labelTimer.Text;
            s = s.Replace("Seconds Left: ", "");
            ushort secondLeft = ushort.Parse(s.Remove(s.Length - 1));
            if (--secondLeft == 0)
            {
                labelTimer.Text = "";
                timer1.Enabled = false;
            }
            else
            {
                labelTimer.Text = $"Seconds Left: {secondLeft}s";
            }
        }

        private void buttonCustomeDns_Click(object sender, EventArgs e)
        {
            using (DnsCustomForm form = new DnsCustomForm())
            {
                form.ShowDialog();
                
                if (form.isAddPressed)
                {
                    AppendData(form.dns);
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) 
            {
                string selectedRowDns = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

                DialogResult confirmResult = MessageBox.Show($"Are you sure you want to delete \"{selectedRowDns}\" DNS?",
                                                             "Confirm Delete",
                                                             MessageBoxButtons.YesNo,
                                                             MessageBoxIcon.Question,
                                                             MessageBoxDefaultButton.Button2);

                if (confirmResult == DialogResult.Yes) 
                {
                    int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                    dnsBinding.RemoveAt(selectedRowIndex); 
                } 
            }
            else
            {
                MessageBox.Show("Please select a DNS row before deleting it.", "Can't Delete!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            isApplyPressed = true;
            Close();
        }

        private static DateTime ReadBson(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"File dosen't exist at {path}");


            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Length == 0) throw new FileLoadException($"Can't load file at {path}");

            // Read File
            string text = File.ReadAllText(path);
           
            // BSON
            byte[] bytes = Convert.FromBase64String(text);
            using (MemoryStream ms = new MemoryStream(bytes))
            using (BsonDataReader reader = new BsonDataReader(ms))
            {
                // JSON
                JsonSerializer serializer = new JsonSerializer();
                var deserializedObject = serializer.Deserialize<Dictionary<string, DateTime>>(reader);

                return deserializedObject["data"];
            }
        }

        private static void WriteBson(DateTime data ,string path)
        {
            Dictionary<string, DateTime> packedData = new Dictionary<string, DateTime>();
            packedData["data"] = data;

            // BSON
            using (MemoryStream ms = new MemoryStream())
            using (BsonDataWriter writer = new BsonDataWriter(ms))
            {
                // JSON
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, packedData);
                string serializedData = Convert.ToBase64String(ms.ToArray());

                // Write File
                File.WriteAllText(path, serializedData);
            }
        }
    }
}
