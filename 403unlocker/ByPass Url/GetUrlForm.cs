using _403unlocker.Notification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            comboBoxUrl.AddItemsAndAutoComplete(urls.Select(x => x.URL).ToArray());
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
            if (!UrlConfig.IsValidUrl(comboBoxUrl.Text))
            {
                using (MessageBoxForm form = new MessageBoxForm())
                {
                    form.Title = "Please type correct URL\n" +
                                 "\n" +
                                 "Not Passing:\n" +
                                 "http://google.com\n" +
                                 "https://google.com\n" +
                                 "www.google.com/search?q=cat";
                    form.Caption = "URL is wrong";
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
                    URL = comboBoxUrl.Text
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
