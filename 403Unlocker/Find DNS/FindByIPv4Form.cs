using _403Unlocker.Data_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private bool isIPv4ChangedFlag = true;
        private DnsInfo[] foundList;
        private int currentIndex = 0;

        public FindByIPv4Form(_403UnlockerForm form)
        {
            InitializeComponent();

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

        private void MoveToOctet(TextBox octet)
        {
            octet.Focus();
            octet.SelectAll();
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

        private void textBoxOctets_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b') return;

            TextBox textBox = sender as TextBox;
            if (e.KeyChar == '.')
            {
                e.Handled = true;
                switch (textBox.Name)
                {
                    case "textBoxOctet1":
                        MoveToOctet(textBoxOctet2);
                        break;
                    case "textBoxOctet2":
                        MoveToOctet(textBoxOctet3);
                        break;
                    case "textBoxOctet3":
                        MoveToOctet(textBoxOctet4);
                        break;
                    case "textBoxOctet4":
                        buttonFind.Focus();
                        break;
                }
            }
            else if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxOctets_Validated(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.BackColor = Color.White;
            }
            else
            {
                int n = int.Parse(textBox.Text);
                if (n > 254)
                {
                    textBox.BackColor = Color.Red;
                }
                else
                {
                    textBox.BackColor = Color.White;
                }
            }
        }


        private void textBoxOctet_TextChanged(object sender, EventArgs e)
        {
            isIPv4ChangedFlag = true;

            if (Octets.All(textBox => string.IsNullOrEmpty(textBox.Text)) || !IsAllOctetsValid()) SetFindAndClearButtonsEnable(false);
            else SetFindAndClearButtonsEnable(true);

            SetResultVisible(false);
            SetPreviousAndNextVisible(false);
            ResetIndex();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            isIPv4ChangedFlag = false;
            mainForm.isTabelChangedFlag = false;
            SetResultVisible(true);

            foundList = mainForm.FindDnsByIPv4(Octets.Select(textBox => textBox.Text).ToArray());
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

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (isIPv4ChangedFlag || mainForm.isTabelChangedFlag)
            {
                ResetIndex();
                SetResultVisible(false);
                return;
            }
            currentIndex--;
            if (currentIndex < 0) currentIndex += foundList.Length;
            currentIndex %= foundList.Length;
            UpdateResultAndShow();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (isIPv4ChangedFlag || mainForm.isTabelChangedFlag)
            {
                ResetIndex();
                SetResultVisible(false);
                return;
            }
            currentIndex++;
            currentIndex %= foundList.Length;
            UpdateResultAndShow();
        }

    }
}
