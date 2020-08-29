using System;
using System.Speech.Synthesis;

namespace Kinect_v2_Learning
{
    /// <summary>
    /// 文字轉語音
    /// </summary>
    public class TextToSpeech
    {
        /// <summary>
        /// Init SpeechSynthesizer
        /// </summary>
        public SpeechSynthesizer SpeechSynthesizer;

        /// <summary>
        ///  Get vocabulary
        /// </summary>
        public Vocabulary vocabulary = new Vocabulary();

        /// <summary>
        /// Azure Text To Speech
        /// </summary>
        private Pronunciation pronunciation = new Pronunciation();

        public TextToSpeech() {

            //宣告SpeechSynthesizer
            SpeechSynthesizer = new SpeechSynthesizer();

            //音量大小
            SpeechSynthesizer.Volume = 100;
            //文字轉語音:完成事件
            SpeechSynthesizer.SpeakCompleted += SpeechSynthesizer_SpeakCompleted;

        }

        /// <summary>
        /// 文字轉語音完成後的事件
        /// </summary>
        private void SpeechSynthesizer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            Console.WriteLine("Success Speech");
        }

        public void SpeechMethod(String words) {

            //SpeechSynthesizer.SpeakAsync(words);
            pronunciation.SetText(words.Trim());
        }

        public void MicrosoftTextToSpeech(String words)
        {
            SpeechSynthesizer.SpeakAsync(words);
        }

        public void SpeechMethod(int posture_number) {

            //SpeechSynthesizer.SpeakAsync(vocabulary.Give_Vocabulary(posture_number));         
        }


        public void SpeechPause()
        {
            SpeechSynthesizer.Pause();
        }

        public void SpeechResume() {

            SpeechSynthesizer.Resume();
        }

        public void SpeechDispose() {

            SpeechSynthesizer.Dispose();
        }

    }
}
