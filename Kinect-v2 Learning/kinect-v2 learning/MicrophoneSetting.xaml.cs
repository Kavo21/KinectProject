using Microsoft.Kinect.Wpf.Controls;
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
    /// MicrophoneSetting.xaml 的互動邏輯
    /// </summary>
    public partial class MicrophoneSetting : Window
    {
        public static bool Microphone_Status = true;

        public MicrophoneSetting()
        {
            InitializeComponent();
            //初始化手勢控制的Image
            KinectRegion.SetKinectRegion(this, kinectRegion);
            App app = ((App)Application.Current);
            app.KinectRegion = kinectRegion;
            btnKinect.Click += new RoutedEventHandler(SelectMicrophone);
            btnHeadset.Click += new RoutedEventHandler(SelectMicrophone);
        }

        private void SelectMicrophone(object sender,RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "btnKinect":
                    Microphone_Status = false;
                    this.Close();
                    break;
                case "btnHeadset":
                    Microphone_Status = true;
                    this.Close();
                    break;
            }
        }
    }
}
