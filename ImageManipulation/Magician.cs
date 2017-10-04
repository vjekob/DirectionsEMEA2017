using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ImageManipulation
{
    public static class Magician
    {
        public static Bitmap ScaleTo(Stream imageStream, int maxWidth, int maxHeight)
        {
            var sourceImage = Helper.GetImageFromStream(imageStream);
            var rect = Helper.ResizeToRect(sourceImage.Width, sourceImage.Height, maxWidth, maxHeight);
            var bitmap = new Bitmap(rect.Width, rect.Height);
            bitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var attr = new ImageAttributes())
                {
                    attr.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(sourceImage, rect, 0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel, attr);
                }
            }

            return bitmap;
        }

        public static Bitmap Crop(Stream imageStream, int newWidth, int newHeight)
        {
            var sourceImage = Helper.GetImageFromStream(imageStream);
            int offsetX, offsetY;
            if (sourceImage.Height > sourceImage.Width)
            {
                offsetX = 0;
                offsetY = sourceImage.Height / 2 - newHeight / 2;
            }
            else
            {
                offsetX = sourceImage.Width / 2 - newWidth / 2;
                offsetY = 0;
            }
            var rectCropped = new Rectangle(offsetX, offsetY, newWidth, newHeight);
            var bitmapCropped = new Bitmap(newWidth, newHeight);
            using (var graphicsCropped = Graphics.FromImage(bitmapCropped))
            {
                graphicsCropped.DrawImage(sourceImage, new Rectangle(0, 0, newWidth, newHeight), rectCropped, GraphicsUnit.Pixel);
            }

            return bitmapCropped;
        }
    }
}
