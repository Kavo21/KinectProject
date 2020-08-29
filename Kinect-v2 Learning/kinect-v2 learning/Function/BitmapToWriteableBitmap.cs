using System.IO;
using System.Windows.Media.Imaging;

namespace Kinect_v2_Learning
{
    /// <summary>
    /// 把WriteableBitmap 轉成Bitmap因為QRCode格式需要
    /// </summary>
    public static class BitmapToWriteableBitmap
    {
        public static System.Drawing.Bitmap BitmapFromWriteableBitmap(WriteableBitmap writeBmp)
        {
            if (writeBmp == null) { return null; }
            System.Drawing.Bitmap bmp;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create((BitmapSource)writeBmp));
                enc.Save(outStream);
                bmp = new System.Drawing.Bitmap(outStream);
            }
            return bmp;
        }
    }
}
