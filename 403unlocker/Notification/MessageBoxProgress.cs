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
        private int totalTimeSpan;
        private List<Task> tasks;

        public MessageBoxProgress(List<Task> tasks)
        {
            InitializeComponent();

            this.tasks = tasks;
        }

        private async void WaitingThreadForm_Load(object sender, EventArgs e)
        {
            progressBar1.Maximum = tasks.Count;
            Progress<ProgressReport> progress = new Progress<ProgressReport>();
            progress.ProgressChanged += (o, report) =>
            {
                int percentage = report.CurrentValue * 100 / progressBar1.Maximum;
                labelStatus.Text = $"{percentage}% Completed";
                progressBar1.Value = report.CurrentValue;
                progressBar1.Update();
            };
            DnsBenchmark.progress = progress;

            await Task.WhenAll(tasks);
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            isCanceled = true;
            labelStatus.Text = "Canceling - " + labelStatus.Text;
            Close();
        }
    }
}
