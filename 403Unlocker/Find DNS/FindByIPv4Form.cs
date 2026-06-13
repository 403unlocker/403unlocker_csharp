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
        private bool isChangedFlag = true;
        private DnsInfo[] foundList;
        private int currentIndex = 0;

        public FindByIPv4Form(_403UnlockerForm form)
        {
            InitializeComponent();

            mainForm = form;
            labelResult.Visible = false;
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
            return !Octets.All(textBox => !string.IsNullOrEmpty(textBox.Text)) && 
                Octets.Where(textBox => !string.IsNullOrEmpty(textBox.Text)).All(textBox => int.Parse(textBox.Text) < 255);
        }

        private void MoveToOctet(TextBox octet)
        {
            octet.Focus();
            octet.SelectAll();
        }

        private void ShowCurrent()
        {
            labelResult.Text = $"Result: {currentIndex + 1} of {foundList.Length}";
            mainForm.ShowFoundDns(foundList[currentIndex]);
        }

        private void ShowNext()
        {
            labelResult.Text = $"Result: {currentIndex + 1} of {foundList.Length}";
            currentIndex = (currentIndex + 1) % foundList.Length;
            mainForm.ShowFoundDns(foundList[currentIndex]);
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

        private void buttonFind_Click(object sender, EventArgs e)
        {
            labelResult.Visible = true;

            if (isChangedFlag)
            {
                isChangedFlag = false;
                foundList = mainForm.FindDnsByIPv4(textBoxOctet1.Text, textBoxOctet2.Text, textBoxOctet3.Text, textBoxOctet4.Text);
                if (foundList.Length > 0)
                {

                    buttonFind.Text = "Find Next";
                    ShowCurrent();
                }
                else labelResult.Text = "Not Found";
            }
            else
            {
                if (foundList.Length > 0) ShowNext();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxOctet1_TextChanged(object sender, EventArgs e)
        {
            isChangedFlag = true;
            currentIndex = 0;

            buttonFind.Text = "Find";

            if (Octets.All(textBox=>string.IsNullOrEmpty(textBox.Text))|| !IsAllOctetsValid()) buttonFind.Enabled = false;
            else buttonFind.Enabled = true;
        }
    }
}
