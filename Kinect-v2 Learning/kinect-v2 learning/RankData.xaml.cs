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
    /// RankData.xaml 的互動邏輯
    /// </summary>
    public partial class RankData : Window
    {
        public readonly static String Rank_Vocabulary = "Vocabulary";
        public readonly static String Rank_PeerVocabulary = "PeerVocabulary";
        public readonly static String Rank_Sentence = "Sentence";
        public readonly static String Rank_PeerSentence = "PeerSentence";
        public readonly static String Rank_StoryMarking = "StoryMarking";
        public readonly static String Rank_PeerStoryMarking = "PeerStoryMarking";
        public readonly static String Rank_Interactive = "Interactive";
        public readonly static String Rank_PeerInteractive = "PeerInteractive";

        private String Activity;

        public RankData(String Activity)
        {
            InitializeComponent();
            this.Activity = Activity;
            this.Title = Activity;
            FirebaseUpload.QueryData(Activity, RankList_View);
        }

        private void InitializeScore_Click(object sender, RoutedEventArgs e)
        {
            FirebaseUpload.InitializeScore(Activity, 0);
        }
    }
}
