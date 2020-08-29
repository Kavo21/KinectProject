using Microsoft.Kinect;
using Microsoft.Kinect.Wpf.Controls;
using System;
using System.Windows;
using System.Windows.Controls;


namespace Kinect_v2_Learning
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 宣告 Kinect
        /// </summary>
        private KinectSensor sensor;

        /// <summary>
        /// 宣告 Kinect_Capture_Picture.xml
        /// </summary>
        private Kinect_Capture_Picture kinect_Capture_Picture;

        /// <summary>
        /// 宣告 StudentsStoryMarking.xml
        /// </summary>
        private StudentsStoryMarking studentsStoryMarking;

        /// <summary>
        /// 宣告 InteractiveActivity.xml
        /// </summary>
        private InteractiveActivity interactiveActivity;

        public MainWindow()
        {  
            InitializeComponent();

            InitializeKinect();

            InitializeHandControl();

            InitializeEventHander();

            BasicMethod.Init_Mode();

            Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            txtStudentsName.Content = Students.StudentsName;
     
        }

        /// <summary>
        /// 初始化Kinect
        /// </summary>
        private void InitializeKinect() {

            sensor = KinectSensor.GetDefault();

            if (sensor != null)
            {
                sensor.Open();
            }
        }

        /// <summary>
        /// 初始化手勢控制的Image
        /// </summary>
        private void InitializeHandControl() {
            
            KinectRegion.SetKinectRegion(this, kinectRegion);
            App app = ((App)Application.Current);
            app.KinectRegion = kinectRegion;
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        private void InitializeEventHander() {
            btnVocabulary.Click += new RoutedEventHandler(Select_Click);
            btnCamera.Click += new RoutedEventHandler(Select_Click);
            btnExit.Click += new RoutedEventHandler(Select_Click);
            btnSpeech.Click += new RoutedEventHandler(Select_Click);
            btnEmotion.Click += new RoutedEventHandler(Select_Click);
            btnSetting.Click += new RoutedEventHandler(Select_Click);
        }

        /// <summary>
        /// 事件統一呼叫到此函數，並判斷為哪個按鈕觸發
        /// </summary>
        private void Select_Click(object sender, RoutedEventArgs e) {

            Button BtnName = (Button)sender;
           
            switch (BtnName.Name) {
                case "btnVocabulary":
                    Vocabulary_Practice vocabulary_Practice = new Vocabulary_Practice();
                    vocabulary_Practice.Show();
                    this.Close();
                    break;

                case "btnCamera":
                    //kinect_Capture_Picture = new Kinect_Capture_Picture();
                    //kinect_Capture_Picture.Show();
                    PeerVocabularyActivity peerVocabularyActivity = new PeerVocabularyActivity();
                    peerVocabularyActivity.Show();
                    this.Close();
                    break;

                case "btnExit":
                    Application.Current.Shutdown();
                    ThrowKinect();
                    break;

                case "btnSpeech":
                    Select_Item.SetInteractiveMode = true;
                    new IndividualInteractiveActivity().Show();
                    this.Close();
                    break;

                case "btnEmotion":
                    //Select_Item.SetStudentsSentencesMode = true;
                    studentsStoryMarking = new StudentsStoryMarking();
                    studentsStoryMarking.Show();
                    this.Close();
                    break;
                case "btnSetting":
                    MicrophoneSetting microphoneSetting = new MicrophoneSetting();
                    microphoneSetting.Show();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 關閉Kinect
        /// </summary>
        private void ThrowKinect() {

            if (sensor != null)
            {
                sensor.Close();
            }
        }
    }
}

