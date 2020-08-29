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
using Microsoft.Kinect.Face;

namespace Kinect_v2_Learning
{
    /// <summary>
    /// Show_Face_Features.xaml 的互動邏輯
    /// </summary>
    public partial class Show_Face_Features : Window
    {
        public Show_Face_Features()
        {
            InitializeComponent();
        }

        public void Change_View(FaceFrameResult result) {

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

            if (mouthOpen == DetectionResult.Yes)
            {
                Mouth_Open.Visibility = Visibility.Visible;
            }
            else
            {
                Mouth_Open.Visibility = Visibility.Hidden;
            }

            if (eyeRightClosed == DetectionResult.Yes)
            {
                Eye_Right_Closed.Visibility = Visibility.Visible;
            }
            else
            {
                Eye_Right_Closed.Visibility = Visibility.Hidden;
            }

            if (eyeLeftClosed == DetectionResult.Yes)
            {
                Eye_Left_Closed.Visibility = Visibility.Visible;
            }
            else
            {
                Eye_Left_Closed.Visibility = Visibility.Hidden;
            }

            if (happy == DetectionResult.Yes)
            {
                Happy.Visibility = Visibility.Visible;
            }
            else
            {
                Happy.Visibility = Visibility.Hidden;
            }

            if (looking_away == DetectionResult.Yes)
            {
                Looking_Away.Visibility = Visibility.Visible;
            }
            else
            {
                Looking_Away.Visibility = Visibility.Hidden;
            }

            if (mouth_move == DetectionResult.Yes)
            {
                Mouth_Move.Visibility = Visibility.Visible;
            }
            else
            {
                Mouth_Move.Visibility = Visibility.Hidden;
            }
        }
    }
}
