using Microsoft.Win32;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _403unlocker.QR_Code
{
    public partial class QrCodeForm : Form
    {
        private readonly string Dns;

        private Dictionary<string, ImageFormat> imageFormtaMapping = new Dictionary<string, ImageFormat>();

        public QrCodeForm(string Dns)
        {
            InitializeComponent();

            this.Dns = Dns;
            label1.Text = Dns;
            int x = pictureBox1.Location.X + (pictureBox1.Size.Width - label1.Size.Width) / 2;
            label1.Location = new Point(x, label1.Location.Y);

            saveFileDialog1.Filter = "Bitmap File|*.bmp" +
                         "|Enhanced MetaFile|*.emf" +
                         "|Exchangeable Image File Format|*.exif" +
                         "|Graphics Interchange Format|*.gif" +
                         "|Icon File|*.icon" +
                         "|Joint Photographic Experts Group|*.jpeg" +
                         "|memorybmp File|*.memorybmp" +
                         "|Portable Network Graphic|*.png" +
                         "|Tag Image File Format|*.tiff" +
                         "|Windows Metafile|*.wmf";

            imageFormtaMapping = new Dictionary<string, ImageFormat>
            {
                { ".bmp", ImageFormat.Bmp },
                { ".emf", ImageFormat.Emf },
                { ".exif", ImageFormat.Exif },
                { ".gif", ImageFormat.Gif },
                { ".icon", ImageFormat.Icon },
                { ".jpeg", ImageFormat.Jpeg },
                { ".memorybmp", ImageFormat.MemoryBmp },
                { ".png", ImageFormat.Png },
                { ".tiff", ImageFormat.Tiff },
                { ".wmf", ImageFormat.Wmf }
            };

            pictureBox1.Image = QrCodeGenerator(Dns);
        }

        private static Bitmap QrCodeGenerator(string data)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.M))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.FromArgb(44, 212, 191), true);
                return qrCodeImage;
            }
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (Bitmap bmp = new Bitmap(pictureBox1.Image))
                {
                    bmp.Save(saveFileDialog1.FileName, imageFormtaMapping[Path.GetExtension(saveFileDialog1.FileName)]);
                }
            }
        }
    }
}
