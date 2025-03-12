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
        List<UrlConfig> userUrls;
        public bool isOk = false;
        public GetUrlForm()
        {
            InitializeComponent();
        }

        private async void GetUrlForm_Load(object sender, EventArgs e)
        {
            try
            {
                //Set the properties for the TextBox
                userUrls = await UrlConfig.ReadJson();
                AppendToAutoComplete(userUrls);
            }
            catch (Exception)
            {
                AppendToAutoComplete(Data.Url.DefaultList());
            }
        }

        private void AppendToAutoComplete(UrlConfig url)
        {
            AppendToAutoComplete(new List<UrlConfig> { url });
        }

        private void AppendToAutoComplete(List<UrlConfig> additionUrlList)
        {
            // finds new Websites
            List<UrlConfig> newUrls = additionUrlList.Except(userUrls).ToList();
            userUrls.AddRange(newUrls);
            if (newUrls.Count > 0)
            {
                textBoxUrl.AutoCompleteCustomSource.AddRange(newUrls.Select(website => website.URL).ToArray());
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!UrlConfig.IsValidUrl(textBoxUrl.Text))
            {
                MessageBox.Show("Please type correct URL\n\nNot Passing:\nhttp://google.com\nhttps://google.com",
                                "URL is wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var website = new UrlConfig
            {
                Name = "custom",
                URL = textBoxUrl.Text
            };

            AppendToAutoComplete(website);

            isOk = true;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
