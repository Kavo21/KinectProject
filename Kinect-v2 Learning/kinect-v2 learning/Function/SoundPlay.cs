using System;
using System.Windows.Media;

namespace Kinect_v2_Learning
{
    /// <summary>
    /// 播放音效
    /// </summary>
    static class SoundPlay
    {
        /// <summary>
        /// 宣告MediaPlayer類別
        /// </summary>
        private static MediaPlayer Sound1 = new MediaPlayer();
        
        /// <summary>
        /// 音效路徑，音效檔的屬性內，複製到輸出目錄:要改成一律複製
        /// </summary>
        private static Uri SoundCorrectPath = new Uri("Raw/correct_sound.wav", UriKind.Relative);
        private static Uri SoundFailedPath = new Uri("Raw/fault_sound.wav", UriKind.Relative);
        private static Uri SoundCameraPath = new Uri("Raw/camerashutter.wav", UriKind.Relative);
        
        public static void PlaySoundCorrect() {

            Console.WriteLine(SoundCorrectPath);
            Sound1.Open(SoundCorrectPath);
            Sound1.Play();
        }

        public static void PlaySoundFailed()
        {
            Console.WriteLine(SoundFailedPath);
            Sound1.Open(SoundFailedPath);
            Sound1.Play();
        }

        public static void PlaySoundCamera() {

            Console.WriteLine(SoundCameraPath);
            Sound1.Open(SoundCameraPath);
            Sound1.Play();
        }

        public static void PlayStop() {
            Sound1.Stop();
        }
    }
}
