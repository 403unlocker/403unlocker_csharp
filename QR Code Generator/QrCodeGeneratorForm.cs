using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace QR_Code_Generator
{
    public partial class QrCodeGeneratorForm : Form
    {
        private Dictionary<string, ImageFormat> imageFormat = new Dictionary<string, ImageFormat>
        {
            { ".bmp", ImageFormat.Bmp},
            { ".emf", ImageFormat.Emf},
            { ".exif", ImageFormat.Exif },
            { ".gif", ImageFormat.Gif },
            { ".icon", ImageFormat.Icon },
            { ".jpeg", ImageFormat.Jpeg },
            { ".memorybmp", ImageFormat.MemoryBmp },
            { ".png", ImageFormat.Png },
            { ".tiff", ImageFormat.Tiff },
            { ".wmf", ImageFormat.Wmf }
        };

        private string value;

        public QrCodeGeneratorForm(string value)
        {
            InitializeComponent();

            this.value = value;
        }

        private void QR_Code_Generator_Load(object sender, System.EventArgs e)
        {
            textBox1.Text = value;
            Bitmap qrCode = QrCodeGenerator.Generate(value);
            pictureBox1.Image = qrCode;
        }
        
        private void buttonClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void saveAsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (Bitmap bmp = new Bitmap(pictureBox1.Image))
                {
                    string fileExtension = Path.GetExtension(saveFileDialog1.FileName);
                    bmp.Save(saveFileDialog1.FileName, imageFormat[fileExtension]);
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
        }
    }
}
