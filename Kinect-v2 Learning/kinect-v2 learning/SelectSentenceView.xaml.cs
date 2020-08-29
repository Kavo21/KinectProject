using Microsoft.Kinect.Wpf.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Kinect_v2_Learning
{
    /// <summary>
    /// SelectSentenceView.xaml 的互動邏輯
    /// </summary>
    public partial class SelectSentenceView : Window
    {
        /// <summary>
        /// 通知事件
        /// </summary>
        public event EventHandler SelectEvent;

        public SelectSentenceView()
        {
            InitializeComponent();

            btnSentencePractice.Click += new RoutedEventHandler(Select);
            btnSentenceGame.Click += new RoutedEventHandler(Select);
            btnStoryMarking.Click += new RoutedEventHandler(Select);
            btnStoryMarkingGame.Click += new RoutedEventHandler(Select);

            //初始化手勢控制的Image
            KinectRegion.SetKinectRegion(this, kinectRegion);
            App app = ((App)Application.Current);
            app.KinectRegion = kinectRegion;
        }

        private void Select(object sender, RoutedEventArgs e)
        {
            Button Btn = (Button)sender;
            switch (Btn.Name)
            {
                case "btnSentencePractice":
                    //Select_Item.SetSentenceMode = true;
                    StudentsStoryMarking.ActivityTitle = "SentencePractice";
                    Select_Item.SetStudentsSentencesMode = true;
                    SelectEvent(this, new EventArgs());
                    this.Close();
                    break;
                case "btnSentenceGame":
                    StudentsStoryMarking.ActivityTitle = "SentencePractice";
                    Select_Item.SetStudentsSentencesMode = true;
                    Select_Item.SetSentenceGameMode = true;
                    SelectEvent(this, new EventArgs());
                    this.Close();
                    break;
                case "btnStoryMarking":
                    StudentsStoryMarking.ActivityTitle = "StudentsStoryMarking";
                    Select_Item.SetStudentsSentencesMode = true;
                    Select_Item.SetSentenceGameMode = true;
                    SelectEvent(this, new EventArgs());
                    this.Close();
                    break;
                case "btnStoryMarkingGame":
                    StudentsStoryMarking.ActivityTitle = "InteractiveActivity";
                    Select_Item.SetStudentsSentencesMode = true;
                    Select_Item.SetSentenceGameMode = true;
                    SelectEvent(this, new EventArgs());
                    this.Close();
                    break;
                default:
                    break;
            }
        }
    }
}



//