using _403unlocker.Notification;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace _403unlocker.ByPass_Url
{
    public partial class GetUrlForm : Form
    {
        public bool isOkPressed = false;
        private bool needToWriteFile = false;
        private List<UrlConfig> urls;
        public GetUrlForm()
        {
            InitializeComponent();
        }

        private async void GetUrlForm_Load(object sender, EventArgs e)
        {
            try
            {
                //Set the properties for the TextBox
                urls = await UrlConfig.ReadJson();
            }
            catch (Exception)
            {
                urls = Data.Url.DefaultList();
                needToWriteFile = true;
            }
            comboBoxUrl.AddItemsAndAutoComplete(urls.Select(x => x.HostName).ToArray());
        }

        private void GetUrlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (needToWriteFile)
            {
                UrlConfig.WriteJson(urls);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!UrlConfig.IsValidHostName(comboBoxUrl.Text))
            {
                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.LabelText = "Please just type hostname\n" +
                                 "\n" +
                                 "For e.g.:\n" +
                                 "google.com";
                    form.Caption = "Hostname is wrong";
                    form.Buttons = MessageBoxButtons.OK;
                    form.Picture = MessageBoxIcon.Error;
                    form.ShowDialog();
                    return;
                }
            }

            if (!comboBoxUrl.Items.Contains(comboBoxUrl.Text))
            {
                urls.Add(new UrlConfig()
                {
                    Name = "custom",
                    HostName = comboBoxUrl.Text
                });
                comboBoxUrl.AddItemsAndAutoComplete(new string[] { comboBoxUrl.Text });
                needToWriteFile = true;
            }

            isOkPressed = true;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
