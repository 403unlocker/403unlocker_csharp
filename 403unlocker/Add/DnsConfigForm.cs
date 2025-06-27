using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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
                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.LabelText = "Do you want to discard changes?";
                    form.Caption = "Closing";
                    form.Buttons = MessageBoxButtons.YesNo;
                    form.Picture = MessageBoxIcon.Question;
                    form.ShowDialog();
                    if (form.DialogResult == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
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
            using (MessageBoxForm form = new MessageBoxForm())
            {
                form.LabelText = "Are you sure about that?";
                form.Caption = "We are clearing";
                form.Buttons = MessageBoxButtons.YesNo;
                form.Picture = MessageBoxIcon.Question;
                form.ShowDialog();
                if (form.DialogResult == DialogResult.Yes)
                {
                    dnsBinding.Clear();
                    labelDnsCount.Text = "DNS Count: 0";
                }
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
                if (newDnsCount > 0)
                {
                    ScrollDownToEnd();
                    using (MessageBoxForm form = new MessageBoxForm())
                    {
                        form.LabelText = "New DNS(s) has been successfully added!\n" +
                                     "\n" +
                                     $"New DNSs: {newDnsCount}\n" +
                                     $"Duplicate DNSs: {duplicateDnsCount}";
                        form.Caption = "Successfully Updated 🎉";
                        form.Buttons = MessageBoxButtons.OK;
                        form.Picture = MessageBoxIcon.Information;
                        form.ShowDialog();
                    }
                }
                else
                {
                    using (MessageBoxForm form = new MessageBoxForm())
                    {
                        form.LabelText = "DNS(s) already exist";
                        form.Caption = "No Duplicates Allowed 🛑";
                        form.Buttons = MessageBoxButtons.OK;
                        form.Picture = MessageBoxIcon.Warning;
                        form.ShowDialog();
                    }
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
                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.LabelText = "You have time to wait, be patient";
                    form.Caption = "Hold Your Horses🐎!";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Hand;
                    form.ShowDialog();
                    return;
                }
            }

            using (var form = new MessageBoxWait(Data.DnsScraper.Get()))
            {
                form.ShowDialog();
            }

            if (Data.DnsScraper.Errors.Message != "")
            {
                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.LabelText = Data.DnsScraper.Errors.Message;
                    form.Caption = "Somthing went wrong";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Error;
                    form.ShowDialog();
                    return;
                }
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

                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.LabelText = $"Are you sure you want to delete \"{selectedRowDns}\" DNS?";
                    form.Caption = "Confirm Delete";
                    form.Buttons = MessageBoxButtons.YesNo;
                    form.Picture = MessageBoxIcon.Question;
                    form.ShowDialog();
                    if (form.DialogResult == DialogResult.Yes)
                    {
                        int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                        dnsBinding.RemoveAt(selectedRowIndex);
                    }
                }
            }
            else
            {
                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.LabelText = "Please select a DNS row before deleting it.";
                    form.Caption = "Can't Delete!";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Stop;
                    form.ShowDialog();
                }
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
