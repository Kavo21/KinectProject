using Microsoft.Kinect.Wpf.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Kinect_v2_Learning
{
    /// <summary>
    /// Select_Item_View.xaml 的互動邏輯
    /// </summary>
    public partial class Select_Item_View : Window
    {
        /// <summary>
        /// 宣告 Vocabulary_Practice類別
        /// </summary>
        private Vocabulary_Practice vocabulary;

        /// <summary>
        /// 宣告事件，當使用者觸發單字類別選單，會通知Vocabulary_Practice頁面 呼叫所註冊的方法
        /// </summary>
        public event EventHandler SelectEvent;

        public Select_Item_View()
        {
            InitializeComponent();

            //把值回到初始化
            Select_Item.SetVocabularyMode = false;
            Select_Item.SetSentenceMode = false;
            Select_Item.SetVocabularyGameMode = false;

            //Click事件統一呼叫Select方法
            btnPhrase.Click += new RoutedEventHandler(Select);
            btnVocabulary.Click+= new RoutedEventHandler(Select);
            btnVocabularyGame.Click += new RoutedEventHandler(Select);
            btnPhraseGame.Click += new RoutedEventHandler(Select);

            //初始化手勢控制的Image
            KinectRegion.SetKinectRegion(this, kinectRegion);
            App app = ((App)Application.Current);
            app.KinectRegion = kinectRegion;
        }

        private void Select(object sender, RoutedEventArgs e) {

            Button Btn = (Button)sender;
            switch (Btn.Name) {
                case "btnVocabulary":
                    Select_Item.SetVocabularyMode = true;
                    Vocabulary_Practice.ActivityTitle = "VocabularyPractice";
                    SelectEvent(this, new EventArgs());
                    this.Close();
                    break;
                case "btnVocabularyGame":
                    Select_Item.SetVocabularyMode = true;
                    Select_Item.SetVocabularyGameMode = true;
                    Vocabulary_Practice.ActivityTitle = "VocabularyGame";
                    SelectEvent(this, new EventArgs());
                    this.Close();
                    break;
                case "btnPhrase":
                    Select_Item.SetSentenceMode = true;
                    SelectEvent(this, new EventArgs());
                    this.Close();
                    break;      
                case "btnPhraseGame":
                    Select_Item.SetSentenceMode = true;
                    Select_Item.SetVocabularyGameMode = true;
                    SelectEvent(this, new EventArgs());
                    this.Close();
                    break;
                default:
                    break;
            }      
        }

    }
}
