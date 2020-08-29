using Microsoft.ProjectOxford.Common.Contract;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kinect_v2_Learning
{
    public class Face_Recogniton : Window
    {
        /// <summary>
        /// 用委派把資料傳過去
        /// </summary>
        /// <param name="face_feature"></param>
        public delegate void FaceData(String face_feature);

        /// <summary>
        /// 用委派把臉部辨識圖片傳過去
        /// </summary>
        public delegate void FaceImage(RenderTargetBitmap bitmapImage);

        /// <summary>
        /// 實體化委派
        /// </summary>
        public FaceData faceData;
        public FaceImage faceImage;

        /// <summary>
        /// Face API Key
        /// </summary>
        private readonly IFaceServiceClient faceServiceClient =
            new FaceServiceClient("573bb4b3c5444fa2b721d4c962878b29", "https://japaneast.api.cognitive.microsoft.com/face/v1.0");

        /// <summary>
        /// The list of detected faces.
        /// </summary>
        Face[] faces;

        /// <summary>
        /// The list of descriptions for the detected faces.
        /// </summary>
        String[] faceDescriptions;

        /// <summary>
        /// The resize factor for the displayed image
        /// </summary>
        double resizeFactor;    

        public async void UploadFaceImage(String path)
        {
            Console.WriteLine("Detecting...................................................................");

            // Display the image file.
            string filePath = path;

            Uri fileUri = new Uri(filePath);
            BitmapImage bitmapSource = new BitmapImage();

            bitmapSource.BeginInit();
            bitmapSource.CacheOption = BitmapCacheOption.None;
            bitmapSource.UriSource = fileUri;
            bitmapSource.EndInit();

            //FacePhoto.Source = bitmapSource;

            // Detect any faces in the image.
            faces = await UploadAndDetectFaces(filePath);

            if (faces.Length > 0)
            {
                // Prepare to draw rectangles around the faces.
                DrawingVisual visual = new DrawingVisual();
                DrawingContext drawingContext = visual.RenderOpen();
                drawingContext.DrawImage(bitmapSource,
                    new Rect(0, 0, bitmapSource.Width, bitmapSource.Height));
                double dpi = bitmapSource.DpiX;
                resizeFactor = 96 / dpi;
                faceDescriptions = new String[faces.Length];

                for (int i = 0; i < faces.Length; ++i)
                {
                    Face face = faces[i];

                    // Draw a rectangle on the face.
                    drawingContext.DrawRectangle(
                        Brushes.Transparent,
                        new Pen(Brushes.Red, 2),
                        new Rect(
                            face.FaceRectangle.Left * resizeFactor,
                            face.FaceRectangle.Top * resizeFactor,
                            face.FaceRectangle.Width * resizeFactor,
                            face.FaceRectangle.Height * resizeFactor
                            )
                    );

                    // Store the face description.
                    faceDescriptions[i] = FaceDescription(face);
                    
                    //委派 把資料傳過去
                    faceData.Invoke(faceDescriptions[i]);
                }

                drawingContext.Close();

                // Display the image with the rectangle around the face.
                RenderTargetBitmap faceWithRectBitmap = new RenderTargetBitmap(
                    (int)(bitmapSource.PixelWidth * resizeFactor),
                    (int)(bitmapSource.PixelHeight * resizeFactor),
                    96,
                    96,
                    PixelFormats.Pbgra32);

                faceWithRectBitmap.Render(visual);
                //委派 把臉部辨識圖片傳過去
                faceImage.Invoke(faceWithRectBitmap);
                //FacePhoto.Source = faceWithRectBitmap;

            }
        }

        private async Task<Face[]> UploadAndDetectFaces(string imageFilePath)
        {
            // The list of Face attributes to return.
            IEnumerable<FaceAttributeType> faceAttributes =
                new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Emotion, FaceAttributeType.Glasses, FaceAttributeType.Hair };

            // Call the Face API.
            try
            {
                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
                    Face[] faces = await faceServiceClient.DetectAsync(imageFileStream, returnFaceId: true, returnFaceLandmarks: false, returnFaceAttributes: faceAttributes);
                    return faces;
                }
            }
            // Catch and display Face API errors.
            catch (FaceAPIException f)
            {
                MessageBox.Show(f.ErrorMessage, f.ErrorCode);
                return new Face[0];
            }
            // Catch and display all other errors.
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return new Face[0];
            }
        }

        // Returns a string that describes the given face.

        private string FaceDescription(Face face)
        {
            StringBuilder sb = new StringBuilder();

            //sb.Append("Face: ");

            // Add the gender, age, and smile.
            //sb.Append(face.FaceAttributes.Gender);
            //sb.Append(Environment.NewLine);
           
            ///sb.Append(", ");
            ///sb.Append(String.Format("smile {0:F1}%, ", face.FaceAttributes.Smile * 100));

            // Add the emotions. Display all emotions over 10%.
            sb.Append("Emotion: ");
            EmotionScores emotionScores = face.FaceAttributes.Emotion;
     
            if (emotionScores.Anger >= 0.1f) sb.Append(String.Format("anger {0:F1}%, ", emotionScores.Anger * 100));
            if (emotionScores.Contempt >= 0.1f) sb.Append(String.Format("contempt {0:F1}%, ", emotionScores.Contempt * 100));
            if (emotionScores.Disgust >= 0.1f) sb.Append(String.Format("disgust {0:F1}%, ", emotionScores.Disgust * 100));
            if (emotionScores.Fear >= 0.1f) sb.Append(String.Format("fear {0:F1}%, ", emotionScores.Fear * 100 ));
            if (emotionScores.Happiness >= 0.1f) sb.Append(String.Format("happiness {0:F1}%, ", emotionScores.Happiness * 100));
            if (emotionScores.Neutral >= 0.1f) sb.Append(String.Format("neutral {0:F1}%, ", emotionScores.Neutral * 100 ));
            if (emotionScores.Sadness >= 0.1f) sb.Append(String.Format("sadness {0:F1}%, ", emotionScores.Sadness * 100 ));
            if (emotionScores.Surprise >= 0.1f) sb.Append(String.Format("surprise {0:F1}%, ", emotionScores.Surprise * 100));

            sb.Append(Environment.NewLine);
            // Add glasses.
            //sb.Append(face.FaceAttributes.Glasses);
            

            // Return the built string.
            return sb.ToString();
        }
    }
}

