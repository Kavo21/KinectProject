using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_v2_Learning
{
    class Students
    {
       public static string Group { get; set; }
       public static string StudentsName { get; set; }
    }

    class Posture
    {
        public String Vocabulary;
        public String Sentence;
        public int PostureTime;
        
        public Posture(string vocabulary, int postureTime)
        {
            Vocabulary = vocabulary;
            PostureTime = postureTime;
        }

        //給StoryMarking的建構值
        public Posture(string sentence, string vocabulary, int postureTime)
        {
            Vocabulary = vocabulary;
            Sentence = sentence;
            PostureTime = postureTime;
        }
    }

    class Speech
    {
        public String Vocabulary;
        public String WrongVocabulary;
        public Double SpeechConfidence;
        public int SpeechTime;

        //給正確的建構值
        public Speech(string vocabulary, double speechConfidence)
        {
            Vocabulary = vocabulary;
            SpeechConfidence = speechConfidence;
        }

        //給錯誤的建構值
        public Speech(string vocabulary, string wrongVocabulary, double speechConfidence)
        {
            Vocabulary = vocabulary;
            WrongVocabulary = wrongVocabulary;
            SpeechConfidence = speechConfidence;
        }

        //紀錄發音所花的時間
        public Speech(string vocabulary, double speechConfidence,int speechTime)
        {
            Vocabulary = vocabulary;
            SpeechConfidence = speechConfidence;
            SpeechTime = speechTime;
        }
    }

    class Photo
    {
        public String Vocabulary, Result;
        public int TakePhotoTime;

        public Photo(string vocabulary, string result, int takePhotoTime)
        {
            Vocabulary = vocabulary;
            Result = result;
            TakePhotoTime = takePhotoTime;
        }
    }

    class ORcodeObject
    {
        public String QRcodeContent;
        public String Vocabulary;
        public int DetectTime;

        public ORcodeObject(string qRcodeContent, string vocabulary, int detectTime)
        {
            QRcodeContent = qRcodeContent;
            Vocabulary = vocabulary;
            DetectTime = detectTime;
        }
    }
}
