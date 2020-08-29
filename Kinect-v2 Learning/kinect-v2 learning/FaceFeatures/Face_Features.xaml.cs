using Microsoft.Kinect;
using Microsoft.Kinect.Face;
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

namespace Kinect_v2_Learning
{
    /// <summary>
    /// Face_Features.xaml 的互動邏輯
    /// </summary>
    public partial class Face_Features : Window
    {
        public KinectSensor _sensor = null;

        public ColorFrameReader _colorReader = null;

        public BodyFrameReader _bodyReader = null;

        public IList<Body> _bodies = null;

        //宣告顯示表情特徵的頁面，方便呼叫
        public Show_Face_Features show_face_features = new Show_Face_Features();

        // 1) Specify a face frame source and a face frame reader
        public FaceFrameSource _faceSource = null;
        public FaceFrameReader _faceReader = null;

        public Face_Features()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Open Face_Features的頁面
            show_face_features.Show();

            _sensor = KinectSensor.GetDefault();

            if (_sensor != null)
            {
                _sensor.Open();

                _bodies = new Body[_sensor.BodyFrameSource.BodyCount];

                _colorReader = _sensor.ColorFrameSource.OpenReader();
                _colorReader.FrameArrived += ColorReader_FrameArrived;

                _bodyReader = _sensor.BodyFrameSource.OpenReader();
                _bodyReader.FrameArrived += BodyReader_FrameArrived;

                // 2) Initialize the face source with the desired features
                _faceSource = new FaceFrameSource(_sensor, 0, FaceFrameFeatures.BoundingBoxInColorSpace |
                                                              FaceFrameFeatures.FaceEngagement |
                                                              FaceFrameFeatures.Glasses |
                                                              FaceFrameFeatures.Happy |
                                                              FaceFrameFeatures.LeftEyeClosed |
                                                              FaceFrameFeatures.MouthOpen |
                                                              FaceFrameFeatures.PointsInColorSpace |
                                                              FaceFrameFeatures.RightEyeClosed |
                                                              FaceFrameFeatures.LookingAway |
                                                              FaceFrameFeatures.MouthMoved |
                                                              FaceFrameFeatures.RotationOrientation
                                                          );
                _faceReader = _faceSource.OpenReader();
                _faceReader.FrameArrived += FaceReader_FrameArrived;
            }
        }

        void ColorReader_FrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    camera.Source = frame.ToBitmap();
                }
            }
        }

        void BodyReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    frame.GetAndRefreshBodyData(_bodies);

                    Body body = _bodies.Where(b => b.IsTracked).FirstOrDefault();

                    if (!_faceSource.IsTrackingIdValid)
                    {
                        if (body != null)
                        {
                            // 4) Assign a tracking ID to the face source
                            _faceSource.TrackingId = body.TrackingId;
                        }
                    }
                }
            }
        }

        void FaceReader_FrameArrived(object sender, FaceFrameArrivedEventArgs e)
        {
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    // 4) Get the face frame result
                    FaceFrameResult result = frame.FaceFrameResult;

                    if (result != null)
                    {

                        // Get the face points, mapped in the color space.
                        var eyeLeft = result.FacePointsInColorSpace[FacePointType.EyeLeft];
                        var eyeRight = result.FacePointsInColorSpace[FacePointType.EyeRight];
                        var nose = result.FacePointsInColorSpace[FacePointType.Nose];
                        var mouthLeft = result.FacePointsInColorSpace[FacePointType.MouthCornerLeft];
                        var mouthRight = result.FacePointsInColorSpace[FacePointType.MouthCornerRight];

                        var eyeLeftClosed = result.FaceProperties[FaceProperty.LeftEyeClosed];
                        var eyeRightClosed = result.FaceProperties[FaceProperty.RightEyeClosed];
                        var mouthOpen = result.FaceProperties[FaceProperty.MouthOpen];
                        var happy = result.FaceProperties[FaceProperty.Happy];
                        var glass = result.FaceProperties[FaceProperty.WearingGlasses];
                        var looking_away = result.FaceProperties[FaceProperty.LookingAway];
                        var mouth_move = result.FaceProperties[FaceProperty.MouthMoved];

                        //呼叫判斷臉部特徵字串方法，直接把臉部特徵類別傳過去，讓方法自己呼叫類別的屬性來做判斷
                        show_face_features.Change_View(result);

                        // Position the canvas UI elements
                        Canvas.SetLeft(ellipseEyeLeft, eyeLeft.X - ellipseEyeLeft.Width / 2.0);
                        Canvas.SetTop(ellipseEyeLeft, eyeLeft.Y - ellipseEyeLeft.Height / 2.0);

                        Canvas.SetLeft(ellipseEyeRight, eyeRight.X - ellipseEyeRight.Width / 2.0);
                        Canvas.SetTop(ellipseEyeRight, eyeRight.Y - ellipseEyeRight.Height / 2.0);

                        Canvas.SetLeft(ellipseNose, nose.X - ellipseNose.Width / 2.0);
                        Canvas.SetTop(ellipseNose, nose.Y - ellipseNose.Height / 2.0);
                        //Mouth_right
                        Canvas.SetLeft(ellipseMouth_right, mouthRight.X - ellipseMouth.Width / 2.0);
                        Canvas.SetTop(ellipseMouth_right, mouthRight.Y - ellipseMouth.Height / 2.0);

                        Canvas.SetLeft(ellipseMouth, ((mouthRight.X + mouthLeft.X) / 2.0) - ellipseMouth.Width / 2.0);
                        Canvas.SetTop(ellipseMouth, ((mouthRight.Y + mouthLeft.Y) / 2.0) - ellipseMouth.Height / 2.0);
                        ellipseMouth.Width = Math.Abs(mouthRight.X - mouthLeft.X);

                        // Display or hide the ellipses
                        if (eyeLeftClosed == DetectionResult.Yes || eyeLeftClosed == DetectionResult.Maybe)
                        {
                            ellipseEyeLeft.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            ellipseEyeLeft.Visibility = Visibility.Visible;    
                        }

                        if (eyeRightClosed == DetectionResult.Yes || eyeRightClosed == DetectionResult.Maybe)
                        {
                            ellipseEyeRight.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            ellipseEyeRight.Visibility = Visibility.Visible;
                        }

                        if (mouthOpen == DetectionResult.Yes || mouthOpen == DetectionResult.Maybe)
                        {
                            ellipseMouth.Height = 50.0;
                            
                        }
                        else
                        {
                            ellipseMouth.Height = 20.0;
                            
                        }
                    }
                }
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _colorReader.FrameArrived -= ColorReader_FrameArrived;
            _faceReader.FrameArrived -= FaceReader_FrameArrived;

            if (_colorReader != null)
            {
               // _colorReader.Dispose();
                //_colorReader = null;
            }

            if (_bodyReader != null)
            {
                //_bodyReader.Dispose();
               // _bodyReader = null;
            }

            if (_faceReader != null)
            {
               // _faceReader.Dispose();
               // _faceReader = null;
            }

            if (_faceSource != null)
            {
                //_faceSource.Dispose();
                //_faceSource = null;
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }

            //Close show_face_features 頁面
            show_face_features.Close();

            //Open main window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}

