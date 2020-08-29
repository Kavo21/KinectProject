using Microsoft.Kinect;
using Microsoft.Kinect.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Kinect_v2_Learning
{
    /// <summary>
    /// Kinect_Capture_Picture.xaml 的互動邏輯
    /// </summary>
    public partial class Kinect_Capture_Picture : Window
    {
        public KinectSensor sensor;

        public ColorFrameReader colorFrameReader = null;

        public FrameDescription frameDescription = null;

        private BodyFrameReader bodyFrameReader = null;

        public byte[] byteData = null;

        /// <summary>
        /// 骨架陣列，陣列每一個元素代表一個人的骨架
        /// <summary>
        private Body[] bodies = null;

        /// <summary>
        /// 影像存在記憶體
        /// </summary>
        public WriteableBitmap wbData = null;
  
        /// <summary>
        /// 宣告情感辨識的頁面，方便呼叫
        /// </summary>
        public Api api;

        /// <summary>
        /// QRcode readner
        /// </summary>
        public ZXing.IBarcodeReader reader = new ZXing.BarcodeReader();

        /// <summary>
        /// Timer
        /// </summary>
        public DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public Kinect_Capture_Picture()
        {    
            InitializeComponent();
            InitializeKinect();
            InitializeColorFrame();
            InitalizeEvent();

        }

        private void InitializeKinect() {

            sensor = KinectSensor.GetDefault();

            if (sensor != null)
            {
                sensor.Open();
            }
        }

        private void InitializeColorFrame() {
  
            // 從感測器打開彩色閱讀器
            colorFrameReader = sensor.ColorFrameSource.OpenReader();
      
            // 彩色事件
            colorFrameReader.FrameArrived += ColorFrameReader_FrameArrived;

            frameDescription = sensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Rgba);

            wbData = new WriteableBitmap(frameDescription.Width, frameDescription.Height, 96, 96, PixelFormats.Bgra32, null);

            byteData = new byte[frameDescription.Width * frameDescription.Height * 4];

            //骨架、骨架事件
            bodyFrameReader = sensor.BodyFrameSource.OpenReader();
            bodyFrameReader.FrameArrived += BodyFrameReader_FrameArrived;

        }

        private void InitalizeEvent() {

            //Set DispatcherTimer time
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            //Set Trigger method
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);

            dispatcherTimer.Start();

            // 打開情感辨識的視窗
            api = new Api();
            api.Show();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            //把ColorFrame的wbData的資料從WriteableBitmap格式，轉成Bitmap格式，才能符合QRcode格式
            ZXing.Result result = reader.Decode(BitmapToWriteableBitmap.BitmapFromWriteableBitmap(wbData));

            if (result != null)
            {
                Console.WriteLine(result.Text);
                MessageBox.Show(result.Text);
            }

            result = null;
        }

        private void ColorFrameReader_FrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                if (colorFrame != null)
                {
                    Int32Rect bitmapRect = new Int32Rect(0, 0, frameDescription.Width, frameDescription.Height);
                    colorFrame.CopyConvertedFrameDataToArray(byteData, ColorImageFormat.Bgra);
                    wbData.WritePixels(bitmapRect, byteData, (int)(frameDescription.Width * frameDescription.BytesPerPixel), 0);
                }
            }
        }

        /// <summary>
        /// 骨架事件
        /// </summary>
        private void BodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {

                canvas.Children.Clear();
                if (bodyFrame == null)
                    return;

                
                bodies = new Body[bodyFrame.BodyCount];
                bodyFrame.GetAndRefreshBodyData(bodies);

                Body body = bodies.Where(b => b.IsTracked).FirstOrDefault();

                foreach (var bodys in bodies)
                {
                    if (body != null)
                    {
                        if (body.IsTracked)
                        {
                            // Draw skeleton.
                            
                              canvas.DrawSkeleton(body);
                            
                        }
                    }
                }
                
            }
        }

        private void ScreenshotButton_Click(object sender, RoutedEventArgs e)
        {
            //Call capture method
            Capture_Method.Capture(wbData);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Colorimage.Source = wbData;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            //Close Emotion API Window
            //api.Close();

            dispatcherTimer.Stop();

            //Open mainwindow
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            dispatcherTimer.Stop();

            colorFrameReader.FrameArrived -= ColorFrameReader_FrameArrived;

            if (colorFrameReader != null)
            {
                colorFrameReader.Dispose();
                colorFrameReader = null;
            }      
        }
    }
}
    
