using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _403unlocker.Notification
{
    public partial class MessageBoxForm : Form
    {
        private SystemSound sound;
        public string Title;
        public string Caption;
        public MessageBoxButtons Buttons;
        public MessageBoxIcon Picture;

        public MessageBoxForm()
        {
            InitializeComponent();
        }

        public MessageBoxForm(string title, string caption, MessageBoxButtons button, MessageBoxIcon icon) : this()
        {
            Title = title;
            Caption = caption;
            Buttons = button;
            Picture = icon;
        }

        private void MessageBoxForm_Load(object sender, EventArgs e)
        {
            Text = Caption;
            label1.Text = Title;

            switch (Picture)
            {
                case MessageBoxIcon.Asterisk: // Information
                    pictureBox1.Image = SystemIcons.Asterisk.ToBitmap();
                    sound = SystemSounds.Asterisk;
                    break;
                case MessageBoxIcon.Error: // Stop, Hand
                    pictureBox1.Image = SystemIcons.Error.ToBitmap();
                    sound = SystemSounds.Hand;
                    break;
                case MessageBoxIcon.Exclamation: // Warning
                    pictureBox1.Image = SystemIcons.Exclamation.ToBitmap();
                    sound = SystemSounds.Exclamation;
                    break;
                case MessageBoxIcon.Question:
                    pictureBox1.Image = SystemIcons.Question.ToBitmap();
                    sound = SystemSounds.Question;
                    break;
                default:
                    break;
            }

            switch (Buttons)
            {
                case MessageBoxButtons.OK:
                    buttonNo.Hide();
                    buttonYes.Hide();
                    break;
                case MessageBoxButtons.YesNo:
                    buttonOk.Hide();
                    break;
            }

            UpdateCoordinates();
            CenterToParent();
            sound.Play();
        }

        private void UpdateCoordinates()
        {
            if (label1.Text.Count(c => c == '\n') > 0)
            {
                label1.Location = new Point(label1.Location.X, pictureBox1.Location.Y);
            }

            Size = new Size(label1.Location.X + label1.Size.Width + 46, label1.Location.Y + label1.Size.Height + 111);

            int h = Size.Height - (buttonNo.Size.Height + 55);
            int w = Size.Width - (buttonOk.Size.Width + 32);
            buttonNo.Location = new Point(w, h);
            buttonOk.Location = new Point(w, h);

            buttonYes.Location = new Point(w - (buttonYes.Size.Width + 6), h);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }
    }
}
