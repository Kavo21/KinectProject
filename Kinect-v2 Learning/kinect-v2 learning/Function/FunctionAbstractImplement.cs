using Microsoft.Kinect;
using System;
using System.Windows.Media.Imaging;

namespace Kinect_v2_Learning
{
    public abstract class FunctionAbstractImplement
    {
        public abstract void InitializeKinect();
        public abstract void InitializeEvent();
        public abstract void InitializeMultiSourceFrameReader();
        public abstract void InitializeBodyFrame();
        public abstract void InitializeFace();
        public abstract void InitalizeSpeechRecognition();

        /// <summary>
        /// 判斷成功函數
        /// </summary>
        public abstract void Success_Change_word(object sender, EventArgs e);
        /// <summary>
        /// 委派所註冊的方法，從雲端回傳的資料，傳送到這方法，比對單字跟臉部辨識的表情
        /// </summary>
        public abstract void SetFaceImage(String path);
        /// <summary>
        /// 委派所註冊的方法，從雲端回傳的資料，傳送到這方法，比對單字跟臉部辨識的表情
        /// </summary>
        public abstract void SetFaceFeature(String face_feature);
        /// <summary>
        /// 委派所註冊的方法，從雲端回傳的照片，傳送到這方法，並顯示出來
        /// </summary>
        public abstract void SetFaceImage(RenderTargetBitmap FacebitmapImage);
    }
}
