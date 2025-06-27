using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _403unlocker.Notification
{
    public partial class MessageBoxWait : Form
    {
        private Task task;
        public MessageBoxWait(Task task)
        {
            InitializeComponent();
            this.task = task;
            pictureBox1.Size = new Size(32, 32);
            pictureBox1.Image = SystemIcons.Information.ToBitmap();
        }

        private async void MessageBoxWait_Load(object sender, EventArgs e)
        {
            await task;
            Close();
        }
    }
}
