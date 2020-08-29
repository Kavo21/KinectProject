using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Kinect_v2_Learning
{
    public class Emotion_Recognition_Method
    {
        double Emotion_Threshold = 0.999999999999 ;
        int Emotion_Max_Value = 5 ;

 #region 委派方式傳送資料(已未使用)
        //建構委派
        public delegate void E_delegate(String Emotion_Names, String Emotion_Values);

        //實體化委派
        public E_delegate e_Delegate;

        public Double Initial_value = 1;

        public String return_finally_result;
#endregion

        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        public String Recognition_Method(String imageFilePath)
        {
            //回傳整個結果，先清空
            String Emotion_Result = null;

            //Clean return_finally_result，因為照片如果沒有偵測到臉部，必須要清空這值，不然上次紀錄會存在
            return_finally_result = null;

            var client = new HttpClient();

            // Request headers - replace this example key with your valid key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "573bb4b3c5444fa2b721d4c962878b29");

            string uri = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0";

            HttpResponseMessage response;

            string responseContent;

            // Request body. Try this sample with a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);
     
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = client.PostAsync(uri, content).Result;
                responseContent = response.Content.ReadAsStringAsync().Result;
            }

            // A peek at the raw JSON response.
            // Console.WriteLine(responseContent);
            // Processing the JSON into manageable objects.
            JToken rootToken = JArray.Parse(responseContent).First;

            // First token is always the faceRectangle identified by the API.
            JToken faceRectangleToken = rootToken.First;

            // Second token is all emotion scores.
            JToken scoresToken = rootToken.Last;

            // Show all face rectangle dimensions
            JEnumerable<JToken> faceRectangleSizeList = faceRectangleToken.First.Children();

            foreach (var size in faceRectangleSizeList)
            {
                //Console.WriteLine(size);
            }

            // Show all scores
            JEnumerable<JToken> scoreList = scoresToken.First.Children();

            //Get Name and Value use JProperty. ex JProperty prop in scoreList.OfType<JProperty>()
            //取得Json的名字、值，需要用 JProperty prop in scoreList.OfType<JProperty>()
            //($"{prop.Value}") 把值拿出來
            foreach (JProperty prop in scoreList.OfType<JProperty>())
            {
                //先取得Json後面的字串值
                //Get String Value
                String Emotion_Value = ($"{prop.Value}");

                //Get String Name
                String Emotion_Name = ($"{prop.Name}");

                //切割字串 取第一個        (從第幾個 ， 取幾位)
                String Cut_String_word = Emotion_Value.Substring(0, 1);

                //將切割後的第一個字，轉為數值，在來進行判斷，如果大於0.99 就等於0，為了去除不必要的數據
                int Compare_Numbers = Convert.ToInt32(Cut_String_word);

                if (Compare_Numbers > Emotion_Threshold)
                {
                    Emotion_Value = "0";
                    //使用委派，呼叫Emotion.cs 把雲端回傳資料傳到，Emotion.cs裡面，所註冊的委派方法
                    //舊方法，主要用委派方式，傳到有註冊委派方法的方法裡
                    //e_Delegate.Invoke(Emotion_Name, Emotion_Value);

                    //新方法，直接創一個字串，將字串一值疊上去，結束後直接回傳字串
                    Emotion_Result += Emotion_Name + ":" + Emotion_Value + Environment.NewLine;
                }
                else
                {   //使用委派，呼叫Emotion.cs 把雲端回傳資料傳到，Emotion.cs裡面，所註冊的委派方法
                    //舊方法，主要用委派方式，傳到有註冊委派方法的方法裡
                    //e_Delegate.Invoke(Emotion_Name, Emotion_Value);

                    //新方法，直接創一個字串，將字串一值疊上去，結束後直接回傳字串
                    Emotion_Result += Emotion_Name + ":" + Emotion_Value + Environment.NewLine;
                                 
                    String Cut_Second_Word = Emotion_Value.Substring(2, 1);//要從第二位開始取，因為前面有小數點
                    int Compare_Second_Number = Convert.ToInt32(Cut_Second_Word);

                    //Select maximum number from data 
                    if (Compare_Second_Number >= Emotion_Max_Value)
                    {
                        Console.WriteLine(Emotion_Name);

                        return_finally_result = Emotion_Name;
                    }                   
                }
            }

            foreach (var score in scoreList)
            {
                Console.WriteLine(score);
            }
            //return responseContent;

            //新方法，直接創一個字串，將字串一值疊上去，結束後直接回傳字串
            return Emotion_Result;
        }

        public String Return_Finally_Result() {  
            return return_finally_result;
        }
    }
}
