using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saobracaj.TerminalMap
{
    public static class ImageQuadrants
    {
        public static (Bitmap q1, Bitmap q2, Bitmap q3, Bitmap q4) Split4(Bitmap src)
        {
            int w2 = src.Width / 2;
            int h2 = src.Height / 2;

            Bitmap Crop(Rectangle r)
            {
                var bmp = new Bitmap(r.Width, r.Height, PixelFormat.Format32bppArgb);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.DrawImage(src, new Rectangle(0, 0, r.Width, r.Height), r, GraphicsUnit.Pixel);
                }
                return bmp;
            }

            var q1 = Crop(new Rectangle(0, 0, w2, h2));                       // top-left
            var q2 = Crop(new Rectangle(w2, 0, src.Width - w2, h2));           // top-right
            var q3 = Crop(new Rectangle(0, h2, w2, src.Height - h2));          // bottom-left
            var q4 = Crop(new Rectangle(w2, h2, src.Width - w2, src.Height - h2));// bottom-right

            return (q1, q2, q3, q4);
        }
    }
}
