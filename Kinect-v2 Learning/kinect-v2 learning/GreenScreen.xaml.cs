using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Kinect;
using KinectBackgroundRemoval;//需引用BackgroundRemovalTool的 namespace using KinectBackgroundRemoval
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;
using System.Threading;
using System.Data;
using Microsoft.Kinect.Face;

namespace Kinect_v2_Learning
{
    /// <summary>
    /// GreenScreen.xaml 的互動邏輯
    /// </summary>
    public partial class GreenScreen : Window
    {
        public KinectSensor _sensor;

        // 1) Specify a face frame source and a face frame reader
        public FaceFrameSource _faceSource = null;
        public FaceFrameReader _faceReader = null;
        public FaceFrameResult face_result = null;

        //多資料元
        public MultiSourceFrameReader _reader;

        //建構 BackgroundRemovalTool.class
        public BackgroundRemovalTool _backgroundRemovalTool;

        //骨架
        public BodyFrameReader bodyFrameReader;

        //骨架陣列，陣列每一個元素代表一個人的骨架
        public Body[] bodies;

        //Socket 連線
        public Socket socket;

        //委派 
        public delegate void ConnectInvoke(string str1);
        public delegate void ServerInovke(int value);

        //委派實體化
        public ConnectInvoke connectInvoke;

        //Server 提供判斷數值
        public int posture_number =0;

        //建構 Poseture.class 為了要判斷骨架
        Poseture poseture = new Poseture();

        //建構單字
        Vocabulary vocabulary = new Vocabulary();

        public GreenScreen()
        {
            InitializeComponent();
            Socket_Connect();
        }

        public void Socket_Connect() {
            //socket = IO.Socket("https://kinectsocket.herokuapp.com");//連接到伺服器
            //socket = IO.Socket("http://localhost:8888/"); //測試用
            socket = IO.Socket("https://kinect-socket-test.herokuapp.com");
            socket.On("start", () => {
                Update_UI();
            });
            socket.On("Posture", (data) =>
            {
                Update_UI(data);
            });
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            String id = "APPLE";
            JObject obj = new JObject(new JProperty("key", id));

            //序列化成字串
            Console.WriteLine(JsonConvert.SerializeObject(obj)); 
            socket.Emit("join", obj);
        }

        private void Return_Successful()
        {
            //回傳伺服器成功-並獲取新資料
            socket.Emit("Return_Successful");
        }
        
        private void Update_UI()//object data
        { 
            if (!Dispatcher.CheckAccess())
            {
                //委派給UpdateForm方法
                connectInvoke = new ConnectInvoke(UpdateForm);

                this.Dispatcher.Invoke(connectInvoke, new Object[] { "已連接" });

            }
            else {
                UpdateForm("已連接");
            }   
        }

        public void UpdateForm(String str1)
        {
            //顯示連線狀態
            connect_status.Content = str1;
        }

        private void Update_UI(object data) {
            //序列化，物件轉字串
            int Convert_data = Convert.ToInt32( JsonConvert.SerializeObject(data));

            if (!Dispatcher.CheckAccess())//判斷是否為同一執行序，否則有跨執行序更新UI問題
            {
                //委派，裡面放要呼叫的方法
                ServerInovke serverInovke = new ServerInovke(Update_Posture_Value);

                //傳入資料
                this.Dispatcher.Invoke(serverInovke, Convert_data);
            }
            else
            {
                //如果在同一執行序，就可直接呼叫-接收資料更改判斷式的值
                Update_Posture_Value(Convert_data);
            }
        }

        private void Update_Posture_Value(int Convert_data) {
            posture_number = Convert.ToInt32(Convert_data);//將字串轉成數值
            show_words.Content = vocabulary.Give_Vocabulary(posture_number);//呼叫方法-從伺服器拿到的值傳入 Vocabulary List方法，顯示單字
        }

        private void Poseture_result(object sender, EventArgs e)//如果判斷動作正確，將顯示..
        {
            //回傳伺服器
            Return_Successful();

            // 呼叫 vocabulary 的 Give_Sentence 方法，並傳入 伺服器判斷值
            Sentence.Text = vocabulary.Give_Sentence(posture_number);
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //獲取感測器資料
            _sensor = KinectSensor.GetDefault();

            if (_sensor != null)
            {
                //_sensor.Open();

                //姿勢事件
                poseture.Poseture_List += Poseture_result;

                // 2) Initialize the background removal tool.
                _backgroundRemovalTool = new BackgroundRemovalTool(_sensor.CoordinateMapper);

                //MultiSourceFrameReader and event
                _reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.BodyIndex);
                _reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;

                //骨架、骨架事件
                bodyFrameReader = _sensor.BodyFrameSource.OpenReader();
                bodyFrameReader.FrameArrived += BodyFrameReader_FrameArrived;

                _faceSource = new FaceFrameSource(_sensor, 0, FaceFrameFeatures.BoundingBoxInColorSpace |
                                                              FaceFrameFeatures.FaceEngagement |
                                                              FaceFrameFeatures.Glasses |
                                                              FaceFrameFeatures.Happy |
                                                              FaceFrameFeatures.LeftEyeClosed |
                                                              FaceFrameFeatures.MouthOpen |
                                                              FaceFrameFeatures.PointsInColorSpace |
                                                              FaceFrameFeatures.RightEyeClosed);
                // Open face Reader and build face event
                _faceReader = _faceSource.OpenReader();
                _faceReader.FrameArrived += _faceReader_FrameArrived;               
            }
        }

        private void _faceReader_FrameArrived(object sender, FaceFrameArrivedEventArgs e)
        {
            using (var face_frame = e.FrameReference.AcquireFrame())
            {
                if (face_frame != null)
                {
                    // 4) Get the face frame result
                    face_result = face_frame.FaceFrameResult;
                    // 5) Get the face points, mapped in the color space. in the Posetures.cs   
                }
            }
        }

        private void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var multi_source_frame = e.FrameReference.AcquireFrame();
            using (var colorFrame = multi_source_frame.ColorFrameReference.AcquireFrame())
            using (var depthFrame = multi_source_frame.DepthFrameReference.AcquireFrame())
            using (var bodyIndexFrame = multi_source_frame.BodyIndexFrameReference.AcquireFrame())
            {
                if (colorFrame != null && depthFrame != null && bodyIndexFrame != null)
                {
                    // 3) Update the image source.
                    camera.Source = _backgroundRemovalTool.GreenScreen(colorFrame, depthFrame, bodyIndexFrame);
                }
            }
        }

        private void BodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame == null)
                    return;
                bodies = new Body[bodyFrame.BodyCount];
                bodyFrame.GetAndRefreshBodyData(bodies);

                Body body = bodies.Where(b => b.IsTracked).FirstOrDefault();
                if (!_faceSource.IsTrackingIdValid)
                {
                    if (body != null)
                    {
                        // 4) Assign a tracking ID to the face source
                        _faceSource.TrackingId = body.TrackingId;
                    }
                }

                //判斷骨架，並傳入參數
                for (int i = 0; i < bodies.Length; i++)
                {
                    if (bodies[i].IsTracked) { //如果骨架有追蹤到才能執行
                        /*
                        poseture.Poseture_Detected(
                            bodies[i].Joints[JointType.Head],
                            bodies[i].Joints[JointType.HandRight], 
                            bodies[i].Joints[JointType.ElbowRight],
                            bodies[i].Joints[JointType.HandLeft],
                            bodies[i].Joints[JointType.ElbowLeft], 
                            bodies[i].Joints[JointType.ShoulderRight], 
                            bodies[i].Joints[JointType.ShoulderLeft],
                            bodies[i].Joints[JointType.SpineMid],
                            posture_number,//posture_number 從伺服器收到的值，傳入判斷式
                            face_result);//Face features 類別直接傳過去，讓他自己呼叫類別的屬性
                            */
                        poseture.Poseture_Detected(bodies[i], vocabulary.Give_Vocabulary(posture_number), face_result);
                    }
                } 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Return_Successful();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            socket.Disconnect();
            
            if (_reader != null)
            {
                _reader.Dispose();
                _reader = null;
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }

            if (bodyFrameReader != null)
            {
                bodyFrameReader.Dispose();
                bodyFrameReader = null;
            }
            

            if (_faceReader != null)
            {
                _faceReader.Dispose();
                _faceReader = null;
            }

            if (_faceSource != null)
            {
                _faceSource.Dispose();
                _faceSource = null;
            }
        }
    }
}
