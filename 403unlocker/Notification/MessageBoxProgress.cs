using _403unlocker.Config;
using _403unlocker.Ping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _403unlocker.Notification
{
    public partial class MessageBoxProgress : Form
    {
        internal bool isCanceled;
        private TimeSpan timeSpan;
        private List<Task> tasks;

        public MessageBoxProgress(List<Task> tasks, int eachThreadTaskCount, int eachThreadTime)
        {
            InitializeComponent();

            this.tasks = tasks;
            progressBar1.Maximum = tasks.Count * eachThreadTaskCount;

            Progress<ProgressReport> progress = new Progress<ProgressReport>();
            progress.ProgressChanged += (o, report) =>
            {
                int percentage = report.CurrentValue * 100 / progressBar1.Maximum;
                labelProgressBarStatus.Text = $"{percentage}% Completed";

                progressBar1.Value = report.CurrentValue;
                progressBar1.Update();
            };
            DnsBenchmark.progress = progress;

            timeSpan = TimeSpan.FromMilliseconds(eachThreadTaskCount * eachThreadTime);
            labelTimeStatus.Text = $"Estimated Time:    {timeSpan:mm\\:ss}";
        }

        private async void WaitingThreadForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
            await Task.WhenAll(tasks);
            timer1.Stop();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            isCanceled = true;
            labelProgressBarStatus.Text = "Canceling - " + labelProgressBarStatus.Text;
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeSpan -= TimeSpan.FromSeconds(1);
            labelTimeStatus.Text = $"Estimated Time:    {timeSpan:mm\\:ss}";
        }
    }
}
