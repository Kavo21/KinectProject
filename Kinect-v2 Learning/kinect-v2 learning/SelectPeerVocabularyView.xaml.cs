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
    /// PeerVocabulary.xaml 的互動邏輯
    /// </summary>
    public partial class SelectPeerVocabularyView : Window
    {
        /// <summary>
        /// 通知事件
        /// </summary>
        public event EventHandler SelectEvent;

        public SelectPeerVocabularyView()
        {
            InitializeComponent();
            btnPeerVocabularyPractice.Click += new RoutedEventHandler(Select);
            btnPeerVocabularyGame.Click += new RoutedEventHandler(Select);
  
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
                case "btnPeerVocabularyPractice":
                    PeerVocabularyActivity.ActivityTitle = "PeerVocabulary";
                    Select_Item.SetVocabularyMode = true;
                    Select_Item.SetPeerVocabularyGameMode = true;
                    SelectEvent(this, new EventArgs());
                    this.Close();
                    break;
                case "btnPeerVocabularyGame":
                    PeerVocabularyActivity.ActivityTitle = "PeerVocabulary";
                    Select_Item.SetVocabularyMode = true;
                    Select_Item.SetPeerVocabularyGameMode = true;
                    SelectEvent(this, new EventArgs());
                    this.Close();
                    break;         
                default:
                    break;
            }
        }
    }
}
