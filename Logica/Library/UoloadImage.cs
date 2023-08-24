using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica.Library
{
    public class UploadImage
    {
        private OpenFileDialog fd = new OpenFileDialog();

        public void cargarImagen(PictureBox pictureBox)
        {
            pictureBox.WaitOnLoad = true;
            fd.Filter = "Imagenes|*.jpg;*.png";
            fd.ShowDialog();
            if (fd.FileName != string.Empty)
            {
                pictureBox.ImageLocation = fd.FileName;
            }
        }

        public byte[] ImageToByte(Image Img)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(Img, typeof(byte[]));
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
