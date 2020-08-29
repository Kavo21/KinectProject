using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kinect_v2_Learning
{
    class FirebaseUpload
    {
        //Firebase 路徑
        public static FirebaseClient firebase = new FirebaseClient("https://fir-kinect.firebaseio.com/");
        private static ObservableCollection<Rank> ranksList;//會自動更新ListView的值
        private static List<Rank> SortRankList;//為了重新排序
        private static readonly String Rank = "Rank";

        //使用非同步
        public static async Task Upload(String ActivtiyTitle, Posture posture)
        {
            await firebase.Child(Students.Group).Child(Students.StudentsName.Trim()).Child(ActivtiyTitle).Child("Posture").Child(posture.Vocabulary).Child(DateTime.Now.ToString("yyyy-MM-dd")).PostAsync(posture);
        }

        //提供給StoryMarking
        public static async Task Upload(String ActivtiyTitle,String Sentence, Posture posture)
        {
            await firebase.Child(Students.Group).Child(Students.StudentsName.Trim()).Child(ActivtiyTitle).Child("Posture").Child(Sentence).Child(DateTime.Now.ToString("yyyy-MM-dd")).Child(posture.Vocabulary).PostAsync(posture);
        }

        public static async Task Upload(String ActivtiyTitle, String Result, Speech speech)
        {
            await firebase.Child(Students.Group).Child(Students.StudentsName.Trim()).Child(ActivtiyTitle).Child("Speech").Child(Result).Child(speech.Vocabulary).Child(DateTime.Now.ToString("yyyy-MM-dd")).PostAsync(speech);
        }

        public static async Task Upload(String ActivtiyTitle, Photo photo)
        {
            await firebase.Child(Students.Group).Child(Students.StudentsName.Trim()).Child(ActivtiyTitle).Child("Photo").Child(photo.Vocabulary).Child(DateTime.Now.ToString("yyyy-MM-dd")).PostAsync(photo);
        }

        public static async Task Upload(String ActivtiyTitle,String Result ,ORcodeObject oRcodeObject)
        {
            await firebase.Child(Students.Group).Child(Students.StudentsName.Trim()).Child(ActivtiyTitle).Child("ORcodeObject").Child(Result).Child(oRcodeObject.Vocabulary).Child(DateTime.Now.ToString("yyyy-MM-dd")).PostAsync(oRcodeObject);
        }

        /// <summary>
        /// 撈學生的點數並比較大小，如果大於就上傳
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static async Task Upload_Rakn_Point(String Name,String Activity, int Value)
        {
            var data = await firebase
              .Child(Rank)
              .Child(Students.Group)
              .Child(Activity)
              .OrderByKey()
              .StartAt(Name)
              .OnceAsync<int>();

            //資料丟進去List
            foreach (var number in data)
            {
                Console.WriteLine(number.Object);
                if (number.Object < Value)
                {
                    await firebase.Child(Rank).Child(Students.Group).Child(Activity).Child(Name).PutAsync(Value);
                }
                else
                {
                    Console.WriteLine("沒有大於，不寫入");
                }
            }
        }


        /// <summary>
        /// 初始化成績
        /// </summary>
        public static async void InitializeScore(String Activity,int Value)
        {
            //只有節點名稱與值，前面沒有隨機的Key，使用PutAsync
            await firebase.Child(Rank).Child(Students.Group).Child(Activity).Child(Students.StudentsName).PutAsync(Value);
        }

        /// <summary>
        /// 撈取資料
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="Value"></param>
        /// <param name="listView"></param>
        /// <returns></returns>
        public static async Task QueryData(String Activity, ListView listView)
        {
            SortRankList = new List<Rank>();
            ranksList = new ObservableCollection<Rank>();
            listView.ItemsSource = ranksList;

            var data = await firebase
              .Child(Rank)
              .Child(Students.Group)
              .Child(Activity)
              .OrderByPriority()
              .OnceAsync<int>();

            //資料丟進去List
            foreach (var item in data)
            {
                SortRankList.Add(new Rank { Name = item.Key, Score = item.Object, ActivityName = Activity });
            }

            //排序資料，前面加上負號 就會從高排到低，以此類推
            SortRankList.Sort((a, b) => -a.Score.CompareTo(b.Score));

            //再把資料放進去Listview
            foreach (var list in SortRankList)
            {
                ranksList.Add(list);
                Console.WriteLine(list.Score);
            }
        }
    }
}
