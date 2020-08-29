namespace Kinect_v2_Learning
{
    /// <summary>
    /// 控制單字與片語的切換值
    /// </summary>
    static class Select_Item
    {
        /// <summary>
        /// Individual Activity
        /// </summary>
        public static bool SetVocabularyMode { get; set; }
        public static bool SetVocabularyGameMode { get; set; }

        public static bool SetSentenceMode { get; set; }
        public static bool SetSentenceGameMode { get; set; }

        public static bool SetStudentsSentencesMode { get; set; }

        public static bool SetStudentsSentencesGameMode { get; set; }

        public static bool SetInteractiveMode { get; set; }
        public static bool SetInteractiveGameMode { get; set; }

        /// <summary>
        /// Peer Activity
        /// </summary>
        public static bool SetPeerVocabularyPracticeMode { get; set; }
        public static bool SetPeerVocabularyGameMode { get; set; }

        public static bool SetPeerSentencePracticeMode { get; set; }
        public static bool SetPeerSentenceGameMode { get; set; }

        public static bool SetPeerStudentsSentencePracticeMode { get; set; }
        public static bool SetPeerStudentsSentenceGameMode { get; set; }

        public static bool SetPeerInteractiveMode { get; set; }



    }
}
