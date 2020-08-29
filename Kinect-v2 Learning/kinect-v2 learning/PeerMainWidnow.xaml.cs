using Microsoft.Kinect;
using Microsoft.Kinect.Wpf.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
namespace Kinect_v2_Learning
{
    /// <summary>
    /// PeerMainWidnow.xaml 的互動邏輯
    /// </summary>
    public partial class PeerMainWidnow : Window
    {
        /// <summary>
        /// 宣告 Kinect
        /// </summary>
        private KinectSensor sensor;

        public PeerMainWidnow()
        {
            InitializeComponent();

            InitializeKinect();

            InitializeHandControl();

            InitializeEventHander();

            txtStudentsName.Content = Students.StudentsName;

            BasicMethod.Init_Mode();
        }

        /// <summary>
        /// 初始化Kinect
        /// </summary>
        private void InitializeKinect()
        {
            sensor = KinectSensor.GetDefault();

            if (sensor != null)
            {
                sensor.Open();
            }
        }

        /// <summary>
        /// 初始化手勢控制的Image
        /// </summary>
        private void InitializeHandControl()
        {

            KinectRegion.SetKinectRegion(this, kinectRegion);
            App app = ((App)Application.Current);
            app.KinectRegion = kinectRegion;
        }


        /// <summary>
        /// 初始化事件
        /// </summary>
        private void InitializeEventHander()
        {
            btnVocabulary.Click += new RoutedEventHandler(Select_Click);
            btnPeerVocabulary.Click += new RoutedEventHandler(Select_Click);
            btnPeerSentence.Click += new RoutedEventHandler(Select_Click);
            btnInteractive.Click += new RoutedEventHandler(Select_Click);
            btnSetting.Click += new RoutedEventHandler(Select_Click);
            btnExit.Click += new RoutedEventHandler(Select_Click);

        }

        /// <summary>
        /// 事件統一呼叫到此函數，並判斷為哪個按鈕觸發
        /// </summary>
        private void Select_Click(object sender, RoutedEventArgs e)
        {

            Button BtnName = (Button)sender;

            switch (BtnName.Name)
            {
                case "btnVocabulary":
                    Vocabulary_Practice vocabulary_Practice = new Vocabulary_Practice();
                    vocabulary_Practice.Show();
                    this.Close();
                    break;

                case "btnPeerVocabulary":
                    PeerVocabularyActivity peerVocabularyActivity = new PeerVocabularyActivity();
                    peerVocabularyActivity.Show();
                    this.Close();
                    break;

                case "btnPeerSentence":
                    PeerStudentsStoryMarking peerStudentsStoryMarking = new PeerStudentsStoryMarking();
                    peerStudentsStoryMarking.Show();
                    this.Close();
                    break;

                case "btnInteractive":
                    Select_Item.SetPeerStudentsSentencePracticeMode = true;
                    Select_Item.SetPeerSentenceGameMode = true;
                    InteractiveActivity interactiveActivity = new InteractiveActivity();
                    interactiveActivity.Show();  
                    this.Close();
                    break;

                case "btnSetting":
                    MicrophoneSetting microphoneSetting = new MicrophoneSetting();
                    microphoneSetting.Show();
                    break;

                case "btnExit":
                    Application.Current.Shutdown();
                    ThrowKinect();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 關閉Kinect
        /// </summary>
        private void ThrowKinect()
        {

            if (sensor != null)
            {
                sensor.Close();
            }
        }
    }
}
