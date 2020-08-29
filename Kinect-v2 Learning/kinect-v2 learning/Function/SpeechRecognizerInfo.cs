using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using Microsoft.Kinect.Face;
using Microsoft.Speech.AudioFormat;
using System.Speech;
using System.Speech.Synthesis;

namespace Kinect_v2_Learning
{
    public class SpeechRecognizerInfo
    {
        public KinectSensor _sensor;

        /// <summary>
        /// Stream for 32b-16b conversion.  
        /// <summary>
        private KinectAudioStream convertStream = null;

        /// <summary>
        /// Speech recognition engine using audio data from Kinect.
        /// <summary>
        public SpeechRecognitionEngine speechEngine = null;

        public String Phrase = "Phrase";

        /// <summary>
        ///建構單字
        /// <summary>
        public Vocabulary vocabulary= new Vocabulary();

        public SpeechRecognizerInfo() {

            InitalizeSpeechRecognition();
            
        }

        public void InitalizeSpeechRecognition()
        {
            // grab the audio stream
            IReadOnlyList<AudioBeam> audioBeamList = this._sensor.AudioSource.AudioBeams;
            System.IO.Stream audioStream = audioBeamList[0].OpenInputStream();

            // create the convert stream
            this.convertStream = new KinectAudioStream(audioStream);

            //RecognizerInfo
            RecognizerInfo recognizerInfo = TryGetKinectRecognizer();

            if (null != recognizerInfo)
            {
                //Using KinectRecognizer();
                this.speechEngine = new SpeechRecognitionEngine(recognizerInfo.Id);

                var grammarBuilder = new GrammarBuilder { Culture = recognizerInfo.Culture };

                //把語音字典放進去
                grammarBuilder.Append(vocabulary.Speech_Dictionary);

                //Grammar
                var grammar = new Grammar(grammarBuilder);

                //載入文法
                this.speechEngine.LoadGrammar(grammar);

                // let the convertStream know speech is going active
                this.convertStream.SpeechActive = true;

                // For long recognition sessions (a few hours or more), it may be beneficial to turn off adaptation of the acoustic model. 
                // This will prevent recognition accuracy from degrading over time.
                speechEngine.UpdateRecognizerSetting("AdaptationOn", 0);

                this.speechEngine.SetInputToAudioStream(this.convertStream, new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                this.speechEngine.RecognizeAsync(RecognizeMode.Multiple);

                //語音辨識事件
                this.speechEngine.SpeechRecognized += SpeechRecognized;

                //語音辨識拒絕事件
                this.speechEngine.SpeechRecognitionRejected += SpeechRejected;
            }
        }

        //語音辨識拒絕事件
        private void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {

        }

        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // Speech utterance confidence below which we treat speech as if it hadn't been heard
            const double ConfidenceThreshold = 0.85;

            //Show Sppech Confidence number on screen
            String SpeechConfidence = e.Result.Confidence.ToString();
           
            Console.WriteLine(e.Result.Confidence.ToString());

            //設定語音信度門檻值
            if (e.Result.Confidence >= ConfidenceThreshold)
            {
                //語音辨識跟單字比對
                //if (e.Result.Semantics.Value.ToString() == vocabulary.Give_Vocabulary(posture_number))
                {
                    Console.WriteLine(e.Result.Semantics.Value.ToString());

                    
                }
            }
        }

 #region RecognizerInfo
        public static RecognizerInfo TryGetKinectRecognizer()
        {
            IEnumerable<RecognizerInfo> recognizers;

            // This is required to catch the case when an expected recognizer is not installed.
            // By default - the x86 Speech Runtime is always expected. 
            try
            {
                recognizers = SpeechRecognitionEngine.InstalledRecognizers();
            }
            catch (COMException)
            {
                return null;
            }

            foreach (RecognizerInfo recognizer in recognizers)
            {
                string value;
                recognizer.AdditionalInfo.TryGetValue("Kinect", out value);
                if ("True".Equals(value, StringComparison.OrdinalIgnoreCase) &&
                    "en-US".Equals(recognizer.Culture.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return recognizer;
                }
            }
            return null;
        }
#endregion
    }
}
