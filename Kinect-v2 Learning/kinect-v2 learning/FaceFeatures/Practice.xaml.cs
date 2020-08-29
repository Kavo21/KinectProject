using System;
using System.Collections.Generic;
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
using Microsoft.Kinect;
using KinectControls.v2;

namespace Kinect_v2_Learning
{
    /// <summary>
    /// Practice.xaml 的互動邏輯
    /// </summary>
    public partial class Practice : Window
    {
#region Construct

        KinectSensor sensor;
        MultiSourceFrameReader multisourceframeReader;
        ColorFrameReader colorFrameReader;
        BodyFrameReader bodyFrameReader;//骨架
        FrameDescription frameDescription;
        Body[] bodies;//骨架陣列，陣列每一個元素代表一個人的骨架
        WriteableBitmap wbData;//影像存在記憶體
        byte[] byteData;
        Poseture poseture = new Poseture(); //建構Poseture.class
        #endregion
        #region Initial
        
        public Practice()
        {
            InitializeComponent();//初始化 介面物件

            sensor = KinectSensor.GetDefault();

            colorFrameReader = sensor.ColorFrameSource.OpenReader(); //從感測器打開彩色閱讀器
            colorFrameReader.FrameArrived += ColorFrameReader_FrameArrived;//彩色事件

            bodyFrameReader = sensor.BodyFrameSource.OpenReader();//骨架
            bodyFrameReader.FrameArrived += BodyFrameReader_FrameArrived;//骨架事件

            frameDescription = sensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Rgba);

            wbData = new WriteableBitmap(frameDescription.Width, frameDescription.Height, 96, 96, PixelFormats.Bgra32, null);

            byteData = new byte[frameDescription.Width * frameDescription.Height * 4];

            sensor.Open();

        }
       
        #endregion
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Colorimage.Source = wbData;
        }

        private void BodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            /*
            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame()) {
                if (bodyFrame == null)
                    return;
                bodies = new Body[bodyFrame.BodyCount];
                bodyFrame.GetAndRefreshBodyData(bodies);

                for (int i = 0; i < bodies.Length; i++) {
                    if (bodies[i].IsTracked)
                    {
                        Joint handRight = bodies[i].Joints[JointType.HandRight];
                        Joint handLeft = bodies[i].Joints[JointType.HandLeft];
                        var activeHand = handRight.Position.Z <= handLeft.Position.Z ? handRight : handLeft;
                        // Get the hand's position relatively to the color image.
                        var position = sensor.CoordinateMapper.MapCameraPointToColorSpace(handLeft.Position);  
                  
                        // Flip the cursor to match the active hand and update its position.
                       
                        
                    };   
                }
            }
            */
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
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            sensor.Close();
            colorFrameReader.FrameArrived -= ColorFrameReader_FrameArrived;
        }
    }
}
