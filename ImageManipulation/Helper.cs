using System;
using System.Drawing;
using System.IO;

namespace ImageManipulation
{
    internal static class Helper
    {
        internal static Image GetImageFromStream(Stream stream)
        {
            return Image.FromStream(stream);
        }

        internal static Rectangle ResizeToRect(int fromWidth, int fromHeight, int toWidth, int toHeight)
        {
            if (fromWidth > fromHeight)
                toWidth = fromWidth;
            else
                toHeight = fromHeight;

            decimal ratioW = ((decimal)fromWidth / (decimal)toWidth),
                    ratioH = ((decimal)fromHeight / (decimal)toHeight),
                    ratio = fromWidth / ratioH > toWidth ? ratioW : ratioH;
            return new Rectangle(0, 0, Convert.ToInt32(fromWidth / ratio), Convert.ToInt32(fromHeight / ratio));
        }
    }
}
