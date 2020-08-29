using System;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;

namespace Kinect_v2_Learning
{
    /// <summary>
    /// 儲存照片
    /// </summary>
    public static class Capture_Method
    {
        public static void Capture(WriteableBitmap wbData) {
            if (wbData != null)
            {
                // create a png bitmap encoder which knows how to save a .png file
                BitmapEncoder encoder = new PngBitmapEncoder();

                // create frame from the writable bitmap and add to encoder
                encoder.Frames.Add(BitmapFrame.Create(wbData));

                string time = System.DateTime.Now.ToString("hh'-'mm'-'ss", CultureInfo.CurrentUICulture.DateTimeFormat);

                string myPhotos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                string path = System.IO.Path.Combine(myPhotos, "KinectScreenshot-Color-" + time + ".png");

                // write the new file to disk
                try
                {
                    // FileStream is IDisposable
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        encoder.Save(fs);
                    }

                }
                catch (IOException)
                {

                }
            }
        }

    }
}
