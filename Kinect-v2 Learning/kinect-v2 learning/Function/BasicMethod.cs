using KinectBackgroundRemoval;
using Microsoft.Kinect;
using Microsoft.Kinect.Face;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Kinect_v2_Learning
{
    public class BasicMethod : Window
    {
        /// <summary>
        /// Speech utterance confidence below which we treat speech as if it hadn't been heard
        /// </summary>
        public static readonly double ConfidenceThreshold = 0.1;

        /// <summary>
        /// 透過Socket.io 傳送要呼叫那些按鈕的暗號
        /// </summary>
        public enum TriggerButtonCode
        {
            Pronunciation = 100,
            ExamplePosture = 101,
            ShowVocabulary = 102,
            StartPeerMode = 103,
            HideHintView = 104,
            ShowHintView = 105,
            Successful = 106
        }

        public static bool CheckEmotionWord(String Vocabulary,String face_feature)
        {
            switch (Vocabulary)
            {
                case "Grieving":
                    if (face_feature.ToLower().Contains("sadness"))
                    {
                        return true;
                    }
                    break;
                case "Sorrow":
                    if (face_feature.ToLower().Contains("sadness"))
                    {
                        return true;
                    }
                    break;
                case "Startled":
                    if (face_feature.ToLower().Contains("surprise"))
                    {
                        return true;
                    }
                    break;
                case "Anger":
                    if (face_feature.ToLower().Contains("Anger"))
                    {
                        return true;
                    }
                    break;

                case "Despise":
                    if (face_feature.ToLower().Contains("contempt"))
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }


        /// <summary>
        /// 專門判斷句子的情感單字
        /// </summary>
     
        public static ReturnPostureWords CheckEmotionWord_Sentence(String Vocabulary, String face_feature)
        {
            switch (Vocabulary)
            {
                case "Grieving":
                    if (face_feature.ToLower().Contains("sadness"))
                    {
                        return new ReturnPostureWords() {  PostureWords = "Grieving" };
                    }
                    break;
                case "Sorrow":
                    if (face_feature.ToLower().Contains("sadness"))
                    {
                        return new ReturnPostureWords() { PostureWords = "Sorrow" };
                    }
                    break;
                case "Startled":
                    if (face_feature.ToLower().Contains("surprise"))
                    {
                        return new ReturnPostureWords() { PostureWords = "Startled" };
                    }
                    break;
                case "Anger":
                    if (face_feature.ToLower().Contains("Anger"))
                    {
                        return new ReturnPostureWords() { PostureWords = "Anger" };
                    }
                    break;

                case "Despise":
                    if (face_feature.ToLower().Contains("contempt"))
                    {
                        return new ReturnPostureWords() { PostureWords = "Despise" };
                    }
                    break;
            }
            return new ReturnPostureWords() { PostureWords = "" };
        }


        public static int GiveSpeechConfidenceScore(Double SpeechConfidence)
        {
            if (SpeechConfidence >= 0.9)
                return 90;
            else if (SpeechConfidence >= 0.8 && SpeechConfidence < 0.9)
                return 80;
            else if (SpeechConfidence >= 0.7 && SpeechConfidence < 0.8)
                return 70;
            else if (SpeechConfidence >= 0.6 && SpeechConfidence < 0.7)
                return 60;
            else if (SpeechConfidence >= 0.5 && SpeechConfidence < 0.6)
                return 50;
            else if (SpeechConfidence >= 0.4 && SpeechConfidence < 0.5)
                return 40;
            else if (SpeechConfidence >= 0.3 && SpeechConfidence < 0.4)
                return 30;
            else if (SpeechConfidence >= 0.2 && SpeechConfidence < 0.3)
                return 20;
            else
                return 0;
        }

        public static void Init_Mode()
        {
            //個人
            Select_Item.SetVocabularyMode = false;
            Select_Item.SetVocabularyGameMode = false;

            Select_Item.SetSentenceMode = false;
            Select_Item.SetSentenceGameMode = false;

            Select_Item.SetStudentsSentencesMode = false;
            Select_Item.SetStudentsSentencesGameMode = false;

            Select_Item.SetInteractiveMode = false;
            Select_Item.SetInteractiveGameMode = false;

            //雙人
            Select_Item.SetPeerVocabularyPracticeMode = false;
            Select_Item.SetPeerVocabularyGameMode = false;

            Select_Item.SetPeerSentencePracticeMode = false;
            Select_Item.SetPeerSentenceGameMode = false;

            Select_Item.SetPeerStudentsSentencePracticeMode = false;
            Select_Item.SetPeerStudentsSentenceGameMode = false;
        }

        public static void CallMainWindow()
        {
            if (Students.Group == "Individual")
            {
                new MainWindow().Show();
            } else if (Students.Group == "Peer")
            {
                new PeerMainWidnow().Show();
            }
        }
    }
}
