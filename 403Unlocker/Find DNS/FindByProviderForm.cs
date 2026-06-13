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

namespace _403Unlocker.Find_DNS
{
    public partial class FindByProviderForm : Form
    {
        private _403UnlockerForm mainForm;
        private bool isChangedFlag = true;
        private DnsInfo[] foundList;
        private int currentIndex = 0;

        public FindByProviderForm(_403UnlockerForm form)
        {
            InitializeComponent();
            mainForm = form;
            labelResult.Visible = false;
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

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            labelResult.Visible = true;

            if (isChangedFlag)
            {
                isChangedFlag = false;
                foundList = mainForm.FindDnsByProvider(textBoxProvider.Text);
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

        private void textBoxProvider_TextChanged(object sender, EventArgs e)
        {
            isChangedFlag = true;
            currentIndex = 0;

            buttonFind.Text = "Find";

            if (string.IsNullOrEmpty(textBoxProvider.Text)) buttonFind.Enabled = false;
            else buttonFind.Enabled = true;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxProvider.Paste();
        }
    }
}
