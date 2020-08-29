using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kinect_v2_Learning
{
    /// <summary>
    /// Emotion.xaml 的互動邏輯
    /// </summary>
    public partial class Emotion : Window
    {
        //Declared ImageSource
        public ImageSource image;

        //Build Construct
        public Emotion_Recognition_Method emotion_recognition_method = new Emotion_Recognition_Method();

        //Declared BackgroundWork
        public BackgroundWorker backgroundwork = new BackgroundWorker();

        public Emotion()
        {
            InitializeComponent();
  
            //Declared Dowork method
            backgroundwork.DoWork += Backgroundwork_DoWork;

            //Declared RunWorkerCompleted method
            backgroundwork.RunWorkerCompleted += Backgroundwork_RunWorkerCompleted;

            //Declared ProgressChanged evenet
            backgroundwork.ProgressChanged += Backgroundwork_ProgressChanged;

            //Can get reports progress
            backgroundwork.WorkerReportsProgress = true;

            //If you want to stop the background work .You can set True whatever the backgroundworker is running. 
            backgroundwork.WorkerSupportsCancellation = true;

        }

        private void Backgroundwork_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress.Value = (int)e.ProgressPercentage;
        }

        private void Backgroundwork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Result.Text = (String)e.Result;

            //Return select maximum number from data and show the emotion .
            Finally_Result.Content = emotion_recognition_method.Return_Finally_Result();
        }        

        private void Backgroundwork_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int index = 1; index <= 100; index++)
            {
                Thread.Sleep(5);
                backgroundwork.ReportProgress(index);
            }

            //Use catch Exception ,because if you choose the image without facial. 
            try
            {
                //從RunWorkerAsync方法裡傳過來，需要用e.Argument接收參數，注意要轉型
                e.Result = emotion_recognition_method.Recognition_Method((String)e.Argument);
                //e.Result 是工作完成後會自動傳到Backgroundwork_RunWorkerCompleted方法裡，一樣要注意轉型跟用 e.Result接收參數
            }
            catch (Exception ex){

                //如果發生例外，把錯誤訊息傳到Backgroundwork_RunWorkerCompleted 裡面，因為只有那邊可以更新UI
                e.Result = "Your image without face. Please try again.";

                //Finally_Result.Content = "Your image without face. Please try again.";
                Console.WriteLine(ex);
            }
        }

        private void Open_Image_File_Click(object sender, RoutedEventArgs e)
        {
            //Open the File.
            OpenFileDialog open = new OpenFileDialog();

            //Select File Type .
            open.Filter = "Files | *.jpg; *.jpeg; *.png; ";
            open.ShowDialog();

            if (open.FileName != "" && backgroundwork.IsBusy != true)
            {

                image = new BitmapImage(new Uri(open.FileName));
                Console.WriteLine(open.FileName);

                //Set Image Box Source.
                image_box.Source = image;

                //Clean Text.
                Result.Text = null;
                Finally_Result.Content = null;

                //Call BackgroundWorker (可以傳入參數，傳到Dowork方法裡面，需要用e.Argument接收參數，注意要轉型)
                backgroundwork.RunWorkerAsync(open.FileName);
            }
            else {
                Finally_Result.Content = "執行序忙碌中......";
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
        }
    }
}
