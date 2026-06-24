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
        private DnsInfo[] foundList;
        private int currentIndex = 0;

        public FindByProviderForm(_403UnlockerForm form)
        {
            InitializeComponent();

            AcceptButton = buttonFind;

            mainForm = form;
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

        private void textBoxProvider_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxProvider.Text)) SetFindAndClearButtonsEnable(false);
            else SetFindAndClearButtonsEnable(true);

            SetResultVisible(false);
            SetPreviousAndNextVisible(false);
            ResetIndex();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            mainForm.isTableChangesAppliedToFind = false;

            foundList = mainForm.FindDnsByProvider(textBoxProvider.Text);
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
            textBoxProvider.Clear();
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (mainForm.isTableChangesAppliedToFind)
            {
                SetResultVisible(false);
                SetPreviousAndNextVisible(false);
                ResetIndex();
                return;
            }
            currentIndex--;
            if (currentIndex < 0) currentIndex += foundList.Length;
            currentIndex %= foundList.Length;
            UpdateResultAndShow();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (mainForm.isTableChangesAppliedToFind)
            {
                SetResultVisible(false);
                SetPreviousAndNextVisible(false);
                ResetIndex();
                return;
            }
            currentIndex++;
            currentIndex %= foundList.Length;
            UpdateResultAndShow();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxProvider.Paste();
        }
        
    }
}
