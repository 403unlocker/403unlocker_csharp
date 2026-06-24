using _403Unlocker.Data_Models;
using Clipboard_Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace _403Unlocker.Find_DNS
{
    public partial class FindByIPv4Form : Form
    {
        private _403UnlockerForm mainForm;
        private DnsInfo[] foundList;
        private int currentIndex = 0;

        public FindByIPv4Form(_403UnlockerForm form)
        {
            InitializeComponent();

            AcceptButton = buttonFind;

            mainForm = form;
        }

        private TextBox[] Octets
        {
            get
            {
                return new TextBox[]
                {
                textBoxOctet1,
                textBoxOctet2,
                textBoxOctet3,
                textBoxOctet4
                };
            }
        }

        private bool IsAllOctetsValid()
        {
            return Octets.Any(textBox => !string.IsNullOrEmpty(textBox.Text)) && 
                Octets.Where(textBox => !string.IsNullOrEmpty(textBox.Text)).All(textBox => int.Parse(textBox.Text) < 255);
        }

        private void MoveToNextOctet(TextBox octet)
        {
            switch (octet.Name)
            {
                case "textBoxOctet1":
                    MoveToTextBox(textBoxOctet2, true);
                    break;
                case "textBoxOctet2":
                    MoveToTextBox(textBoxOctet3, true);
                    break;
                case "textBoxOctet3":
                    MoveToTextBox(textBoxOctet4, true);
                    break;
                case "textBoxOctet4":
                    SystemSounds.Exclamation.Play();
                    break;
            }
        }

        private void MoveToPreviousOctet(TextBox octet)
        {
            switch (octet.Name)
            {
                case "textBoxOctet1":
                    SystemSounds.Exclamation.Play();
                    break;
                case "textBoxOctet2":
                    MoveToTextBox(textBoxOctet1, false);
                    break;
                case "textBoxOctet3":
                    MoveToTextBox(textBoxOctet2, false);
                    break;
                case "textBoxOctet4":
                    MoveToTextBox(textBoxOctet3, false);
                    break;
            }
        }

        private void MoveToTextBox(TextBox textBox, bool selectAllText)
        {
            textBox.Focus();
            if (selectAllText) textBox.SelectAll();
        }

        private void SetFindAndClearButtonsEnable(bool visible)
        {
            buttonFind.Enabled = visible;
            buttonClear.Enabled = visible;
        }

        private void SetResultVisible(bool visible)
        {
            labelResult.Visible = visible;
        }

        private void SetPreviousAndNextVisible(bool visible)
        {
            buttonPrevious.Visible = visible;
            buttonNext.Visible = visible;
        }

        private void UpdateResultAndShow()
        {
            labelResult.Text = $"Result: {currentIndex + 1} of {foundList.Length}";
            mainForm.ShowFoundDns(foundList[currentIndex]);
        }

        private void ResetIndex()
        {
            currentIndex = 0;
        }

        private void ResultNotFound()
        {
            labelResult.Text = "DNS Not Found";
        }

        private void ResetFindResults()
        {
            SetResultVisible(false);
            SetPreviousAndNextVisible(false);
            ResetIndex();
        }

        private void Next()
        {
            currentIndex++;
            currentIndex %= foundList.Length;
        }

        private void Previous()
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex += foundList.Length;
            currentIndex %= foundList.Length;
        }

        private void textBoxOctets_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (e.KeyChar == '\b')
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    e.Handled = true;
                    MoveToPreviousOctet(textBox);
                }
            }
            else if (e.KeyChar == '.')
            {
                e.Handled = true;
                MoveToNextOctet(textBox);
            }
            else if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxOctets_TextChanged(object sender, EventArgs e)
        {
            TextBox octet = sender as TextBox;
            if (string.IsNullOrEmpty(octet.Text)) octet.BackColor = Color.White;
            else
            {
                int n = int.Parse(octet.Text);
                if (n > 254) octet.BackColor = Color.Red;
                else octet.BackColor = Color.White;
            }


            if (Octets.All(textBox => string.IsNullOrEmpty(textBox.Text)) || !IsAllOctetsValid()) SetFindAndClearButtonsEnable(false);
            else SetFindAndClearButtonsEnable(true);

            ResetFindResults();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            mainForm.isTableChangesAppliedToFind = false;

            foundList = mainForm.FindDnsByIPv4(Octets.Select(textBox => textBox.Text).ToArray());
            SetResultVisible(true);

            if (foundList.Length == 0)
            {
                ResultNotFound();
                SetPreviousAndNextVisible(false);
            }
            else // foundList.Length > 1
            {
                ResetIndex();
                UpdateResultAndShow();

                if (foundList.Length == 1) SetPreviousAndNextVisible(false);
                else SetPreviousAndNextVisible(true);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (TextBox octet in Octets)
            {
                octet.Clear();
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (mainForm.isTableChangesAppliedToFind)
            {
                ResetFindResults();
                return;
            }
            Next();
            UpdateResultAndShow();
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (mainForm.isTableChangesAppliedToFind)
            {
                ResetFindResults();
                return;
            }
            Previous();
            UpdateResultAndShow();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Octets.All(textBox => string.IsNullOrEmpty(textBox.Text))) return;
            ClipboardManager.CopyToClipboard(string.Join(".", Octets.Select(textBox => textBox.Text)));
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ipv4ToBePaste = ClipboardManager.PasteFromClipboard();
            string[] octets = ipv4ToBePaste.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (octets.Length < 4 ||
                octets.Any(octet => octet.Any(c => !char.IsDigit(c))) ||
                octets.Any(octet => int.Parse(octet) > 254))
            {
                MessageBox.Show("The clipboard does not contain a valid IPv4 address",
                               "Invalid IPv4 Address",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            textBoxOctet1.Text = octets[0];
            textBoxOctet2.Text = octets[1];
            textBoxOctet3.Text = octets[2];
            textBoxOctet4.Text = octets[3];
        }
    }
}
