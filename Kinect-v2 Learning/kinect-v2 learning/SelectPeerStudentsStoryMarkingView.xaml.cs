using Microsoft.Kinect.Wpf.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Kinect_v2_Learning
{
    /// <summary>
    /// SelectPeerStudentsStoryMarkingView.xaml 的互動邏輯
    /// </summary>
    public partial class SelectPeerStudentsStoryMarkingView : Window
    {
        /// <summary>
        /// 通知事件
        /// </summary>
        public event EventHandler SelectEvent;

        public SelectPeerStudentsStoryMarkingView()
        {
            InitializeComponent();

            //初始化手勢控制的Image
            KinectRegion.SetKinectRegion(this, kinectRegion);
            App app = ((App)Application.Current);
            app.KinectRegion = kinectRegion;
        }

        private void BtnPeerSentenceGame_Click(object sender, RoutedEventArgs e)
        {
            PeerStudentsStoryMarking.ActivityTitle = "PeerSentence";
            Select_Item.SetPeerSentencePracticeMode = true;
            Select_Item.SetPeerSentenceGameMode = true;
            SelectEvent(this, new EventArgs());
            this.Close();
        }

        private void BtnPeerStoryMarkingGame_Click(object sender, RoutedEventArgs e)
        {
            PeerStudentsStoryMarking.ActivityTitle = "InteractiveActivity";
            Select_Item.SetPeerStudentsSentencePracticeMode = true;
            Select_Item.SetPeerSentenceGameMode = true;
            SelectEvent(this, new EventArgs());
            this.Close();
        }
    }
}
