using QRCoder;
using System.Drawing;

namespace QR_Code_Generator
{
    public static class QrCodeGenerator
    {
        public static Bitmap Generate(string value)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(value, QRCodeGenerator.ECCLevel.M))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, SystemColors.Control, false);
                return qrCodeImage;
            }
        }
    }
}
