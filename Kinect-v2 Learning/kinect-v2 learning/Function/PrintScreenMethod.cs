using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Kinect_v2_Learning
{
    /// <summary>
    /// 螢幕截圖，並存檔
    /// </summary>
    public static class PrintScreenMethod
    {      
        public static String PrintScreen() {

            string time = System.DateTime.Now.ToString("hh'-'mm'-'ss", CultureInfo.CurrentUICulture.DateTimeFormat);

            string myPhotos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            string path = System.IO.Path.Combine(myPhotos, "KinectScreenshot-" + time + ".png");

            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            /// copy screen through .net form api
            using (Graphics grapics = Graphics.FromImage(bitmap))
            {
                grapics.CopyFromScreen(0, 0, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
            }
            try
            {
                bitmap.Save(path);
            }
            catch (IOException)
            {

            }
            return path;
        }
    }
}
