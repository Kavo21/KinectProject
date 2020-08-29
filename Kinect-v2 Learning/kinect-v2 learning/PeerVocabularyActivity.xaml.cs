using KinectBackgroundRemoval;
using Microsoft.Kinect;
using Microsoft.Kinect.Face;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Kinect_v2_Learning
{
    /// <summary>
    /// PeerVocabularyActivity.xaml 的互動邏輯
    /// </summary>
    public partial class PeerVocabularyActivity : Window
    {
        private KinectSensor _sensor;

        /// <summary>
        /// MultiSourceFrameReader
        /// <summary>
        private MultiSourceFrameReader _reader = null;

        /// <summary>
        /// 影像存在記憶體
        /// </summary>
        private WriteableBitmap wbData = null;

        /// <summary>
        /// 骨架陣列，陣列每一個元素代表一個人的骨架
        /// <summary>
        private Body[] bodies = null;

        /// <summary>
        /// 骨架
        /// <summary>
        private BodyFrameReader bodyFrameReader = null;

        /// <summary>
        /// Specify a face frame source and a face frame reader
        ///<summary>
        private FaceFrameSource _faceSource = null;
        private FaceFrameReader _faceReader = null;
        private FaceFrameResult face_result = null;

        /// <summary>
        /// Stream for 32b-16b conversion.  
        /// <summary>
        private KinectAudioStream convertStream = null;

        /// <summary>
        /// Speech recognition engine using audio data from Kinect.
        /// <summary>
        private SpeechRecognitionEngine speechEngine = null;

        /// <summary>
        /// 建構 Poseture.class 為了要判斷骨架
        /// <summary>
        private Poseture poseture = new Poseture();

        /// <summary>
        /// 建構單字
        /// <summary>
        private Vocabulary vocabulary = new Vocabulary();

        /// <summary>
        /// 建構 BackgroundRemovalTool.class
        /// <summary>
        private BackgroundRemovalTool _backgroundRemovalTool;

        /// <summary>
        /// 打開單字類別選單
        /// </summary>
        private SelectPeerVocabularyView selectPeerVocabularyView= new SelectPeerVocabularyView();

        /// <summary>
        /// Text To Speech
        /// <summary>
        private TextToSpeech textToSpeech = new TextToSpeech();

        /// <summary>
        /// Face ApI
        /// <summary>
        private Face_Recogniton face_Recogniton = new Face_Recogniton();

        /// <summary>
        /// BackgroundWorker 更新progress bar
        /// </summary>
        private BackgroundWorker backgroundwork = new BackgroundWorker();

        /// <summary>
        /// 拍照倒數時間，
        /// 設定觸發事件 = dispatcherTimer.Tick = new EventHandler(xxxMethod);，
        /// 設定時間 = dispatcherTimer.Interval = new TimeSpan(0,5,0);，
        /// 開始 = dispatcherTimer.Start();
        /// </summary>
        private DispatcherTimer dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal);

        /// <summary>
        /// 遊戲倒數時間
        /// </summary>
        private DispatcherTimer dispatcherGameTimer = new DispatcherTimer(DispatcherPriority.Normal);

        /// <summary>
        /// QRCode偵測，每秒檢查彩色幀數，而且要Bitmap格式，並傳入到QRCode檢測方法判斷
        /// </summary>
        private DispatcherTimer QRcodeTimer = new DispatcherTimer(DispatcherPriority.Send);

        /// <summary>
        /// QRcode readner
        /// </summary>
        private ZXing.IBarcodeReader reader = new ZXing.BarcodeReader();

        /// <summary>
        /// 專門儲存給QRCode檢測的值
        /// </summary>
        private WriteableBitmap QRCodeBitmap = null;

        /// <summary>
        /// 拍照倒數時間
        /// </summary>
        private int CountdownTimer = 4;

        /// <summary>
        /// 遊戲倒數時間
        /// </summary>
        private int GameTimer = 61;

        /// <summary>
        /// 總共時間
        /// </summary>
        private int TotalTime = 0;

        /// <summary>
        /// 專門儲存遊戲答錯單字
        /// </summary>
        private List<String> WrongVocabularyList = new List<string>();

        /// <summary>
        /// 單字預設index
        /// <summary>
        private int posture_number = 0;

        /// <summary>
        /// 語音辨識結果
        /// <summary>
        private bool Speech_Result = false;

        /// <summary>
        /// 姿勢辨識結果
        /// </summary>
        private bool Posture_Result = false;

        /// <summary>
        /// 發音停止
        /// </summary>
        private bool Speech_Enable = false;

        /// <summary>
        /// 圖片路徑，提供給Face API 拿取
        /// </summary>
        private String ImagePath;

        /// <summary>
        /// 用來限制姿勢正確只發出一次聲音，而且需要用執行序來而外執行此聲音的播放，不然會造成須把正確動作放下才會撥放的問題
        /// </summary>
        private static bool PostureSoundStatus = false;

        /// <summary>
        /// 控制語音辨識偵測以及姿勢辨識
        /// </summary>
        private static bool StopDetect = false;

        /// <summary>
        /// 連接到Socket.io
        /// </summary>
        private Sokcet_Connect sokcet_Connect = new Sokcet_Connect();

        /// <summary>
        /// 因為資料從伺服器發送下來，直接存取會有執行緒問題，所以需要委派
        /// </summary>     
        private delegate void Update_Delegate(int data);

        /// <summary>
        /// 連接成功的委派，因為資料從伺服器發送下來，直接存取會有執行緒問題，所以需要委派
        /// </summary>
        private delegate void Update_Connection_Status();

        /// <summary>
        /// 加入房間委派，因為資料從伺服器發送下來，直接存取會有執行緒問題，所以需要委派
        /// </summary>
        private delegate void Update_ChatRoomAdded(String data);

        /// <summary>
        /// 呼叫合作雙人模式的委派，因為資料從伺服器發送下來，直接存取會有執行緒問題，所以需要委派
        /// </summary>
        private delegate void Update_StartPeerMode();

        /// <summary>
        /// 呼叫發音的委派，因為資料從伺服器發送下來，直接存取會有執行緒問題，所以需要委派
        /// </summary>
        private delegate void Update_CallPronunciation();

        /// <summary>
        /// 呼叫顯示單字的委派，因為資料從伺服器發送下來，直接存取會有執行緒問題，所以需要委派
        /// </summary>
        private delegate void Update_ShowVocabulary();

        /// <summary>
        /// 呼叫顯示範例姿勢的委派，因為資料從伺服器發送下來，直接存取會有執行緒問題，所以需要委派
        /// </summary>
        private delegate void Update_ExamplePosture();

        /// <summary>
        /// 設定活動階段
        /// </summary>
        public static string ActivityTitle;

        /// <summary>
        /// 專門儲存語音辨識資料
        /// </summary>
        private List<int> SpeechConfidenceList = new List<int>();


        public PeerVocabularyActivity()
        {
            InitializeComponent();

            InitializeComponent();

            InitializeKinect();

            InitializeEvent();

            InitializeMultiSourceFrameReader();

            InitializeBodyFrame();

            InitializeFace();

            InitalizeSpeechRecognition();

        }
        /// <summary>
        /// 初始化事件
        /// </summary>
        private void InitializeEvent()
        {
            //打開單字類別選單
            selectPeerVocabularyView.Show();

            //註冊事件觸發後的方法
            selectPeerVocabularyView.SelectEvent += Select_Item_View_SelectEvent;

            //向Eventhandler 註冊方法
            poseture.Poseture_List += Success_Change_word;

            //委派註冊
            face_Recogniton.faceData += SetFaceFeature;
            face_Recogniton.faceImage += SetFaceImage;

            //Declared Dowork method
            backgroundwork.DoWork += Backgroundwork_DoWork;

            //Declared RunWorkerCompleted method
            backgroundwork.RunWorkerCompleted += Backgroundwork_RunWorkerCompleted;

            //Declared ProgressChanged evenet
            backgroundwork.ProgressChanged += Backgroundwork_ProgressChanged;

            //Turn on reports progress
            backgroundwork.WorkerReportsProgress = true;

            //Set DispatcherTimer time
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            //Set Trigger method
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);

            //Set QRcodeTimer 1 second
            QRcodeTimer.Interval = new TimeSpan(0, 0, 1);

            //Set Trigger method
            QRcodeTimer.Tick += new EventHandler(QRCodeTimer_Tick);

            //Start QRcodeTime，每秒偵測QRCode
            QRcodeTimer.Start();

            //Set DispatcherTimer Game time
            dispatcherGameTimer.Interval = new TimeSpan(0, 0, 1);

            //Set Trigger method
            dispatcherGameTimer.Tick += new EventHandler(GameTimer_Tick);

            //連線成功的委派，呼叫
            sokcet_Connect.connectionSuccessful += UpdateConnectionView;

            //加入房間的委派，指定傳資料到 UpdateView 方法
            sokcet_Connect.chatRoomAdded += UpdateChatRoomAddedView;

            //連接委派，指定傳資料到 UpdateView 方法
            sokcet_Connect.sendDataToClinet += UpdateView;

        }

        //更新 UI 需要用委派，連線成功的事件，new Update_Connection_Status(UpdateConnectionView)，委派裡面的方法是呼叫自己 但也是符合委派的規範
        private void UpdateConnectionView()
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new Update_Connection_Status(UpdateConnectionView));
            }
            else
            {
               txtCountWords.Content = "連線成功";
            }
        }

        /// <summary>
        /// 加入房間
        /// </summary>
        private void Room_Click(object sender, RoutedEventArgs e)
        {
            sokcet_Connect.JoinRoom(RoomName.Text.ToString().Trim());
        }

        /// <summary>
        /// 更新 UI 需要用委派，new Update_ChatRoomAdded(UpdateChatRoomAddedView)，符合委派的規範
        /// </summary>
        private void UpdateChatRoomAddedView(String data)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new Update_ChatRoomAdded(UpdateChatRoomAddedView), data.ToString());
            }
            else
            {
               txtCountWords.Content =  data.ToString();
            }
        }

        /// <summary>
        /// 更新 UI 需要用委派，new Update_Delegate(Set_word)，委派裡面的方法是另外的方法 但也是符合委派的規範
        /// </summary>
        private void UpdateView(String data)
        {
            try
            {
                switch (int.Parse(data))
                {
                    //雙人互動開始，另一方關閉語音辨識與姿勢辨識，把自身的開始按鈕隱藏
                    case (int)BasicMethod.TriggerButtonCode.StartPeerMode:
                        if (!Dispatcher.CheckAccess())
                        {    
                            Dispatcher.Invoke(new Update_StartPeerMode(StartPeerMode));
                        }
                        else
                        {
                            btnStart.Visibility = Visibility.Hidden;
                            //關閉偵測姿勢，語音辨識
                            StopDetect = true;
                        }
                        break;

                    //呼叫發音方法
                    case (int)BasicMethod.TriggerButtonCode.Pronunciation:
                        if (!Dispatcher.CheckAccess())
                        {
                            Dispatcher.Invoke(new Update_CallPronunciation(CallPronunciation));
                        }
                        else
                        {
                            textToSpeech.SpeechMethod(txtVocabulary.Text.ToString());
                        }
                        break;

                    //顯示姿勢圖案
                    case (int)BasicMethod.TriggerButtonCode.ExamplePosture:
                        if (!Dispatcher.CheckAccess())
                        {
                            Dispatcher.Invoke(new Update_ExamplePosture(ExamplePosture));
                        }
                        else
                        {
                            exampleImage.Visibility = Visibility.Visible;
                        }
                        break;
                    
                    //顯示單字
                    case (int)BasicMethod.TriggerButtonCode.ShowVocabulary:
                        if (!Dispatcher.CheckAccess())
                        {
                            Dispatcher.Invoke(new Update_ShowVocabulary(ShowVocabulary));
                        }
                        else
                        {
                            txtVocabulary.Visibility = Visibility.Visible;
                        }
                        break;

                    //顯示提示按鈕
                    case (int)BasicMethod.TriggerButtonCode.ShowHintView:
                        if (!Dispatcher.CheckAccess())
                        {
                            Dispatcher.Invoke(new Update_ShowVocabulary(VisibilityAllView));
                        }
                        else
                        {
                            VisibilityAllView();
                        }
                        break;

                    //關閉提示
                    case (int)BasicMethod.TriggerButtonCode.HideHintView:
                        if (!Dispatcher.CheckAccess())
                        {
                            Dispatcher.Invoke(new Update_ShowVocabulary(HideAllView));
                        }
                        else
                        {
                            HideAllView();

                        }
                        break;

                    //顯示句子
                    case (int)BasicMethod.TriggerButtonCode.Successful:
                        if (!Dispatcher.CheckAccess())
                        {
                            Dispatcher.Invoke(new Update_ShowVocabulary(ShowSentence));
                        }
                        else
                        {
                            ShowSentence();
                        }
                        break;

                    //預設是設定單字
                    default:
                        if (!Dispatcher.CheckAccess())
                        {
                            Dispatcher.Invoke(new Update_Delegate(Set_word), int.Parse(data));

                            //為了讓語音辨識的字串與顯示出來的字串一致
                            posture_number = int.Parse(data);
                        }
                        else
                        {
                            Set_word(int.Parse(data));
                            //為了讓語音辨識的字串與顯示出來的字串一致
                            posture_number = int.Parse(data);
                        }
                        break;
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// 合作開始時候的初始化
        /// </summary>
        private void StartPeerMode()
        {
            //關閉偵測姿勢，語音辨識
            //bodyFrameReader.FrameArrived -= BodyFrameReader_FrameArrived;
            //speechEngine.SpeechRecognized -= SpeechRecognized;
            btnStart.Visibility = Visibility.Hidden;
            //關閉偵測
            StopDetect = true;
        }

        /// <summary>
        /// 呼叫發音
        /// </summary>
        private void CallPronunciation()
        {
            textToSpeech.SpeechMethod(txtVocabulary.Text.ToString());
        }

        /// <summary>
        /// 顯示單字
        /// </summary>
        private void ShowVocabulary()
        {
            txtVocabulary.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 顯示姿勢
        /// </summary>
        private void ExamplePosture()
        {
            exampleImage.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 選單觸發事件
        /// </summary>
        private void Select_Item_View_SelectEvent(object sender, EventArgs e)
        {
            //初始化單字資料
            this.posture_number = 0;
            vocabulary = new Vocabulary();
        }

        /// <summary>
        /// 打開選單
        /// </summary>
        private void BtnOpenSelectItem_Click(object sender, RoutedEventArgs e)
        {
            //宣告新的Select 類別，因為前一個被關閉
            Select_Item_View select_Item_View = new Select_Item_View();

            //打開單字類別選單
            select_Item_View.Show();

            //重新註冊類別觸發事件
            select_Item_View.SelectEvent += Select_Item_View_SelectEvent;

            //將音效的狀態設定為初始值
            PostureSoundStatus = false;
        }

        /// <summary>
        /// 從RunWorkerAsync方法裡傳過來，需要用e.Argument接收參數，注意要轉型。
        /// e.Result 是工作完成後會自動傳到Backgroundwork_RunWorkerCompleted方法裡，一樣要注意轉型跟用 e.Result接收參數
        /// </summary>
        private void Backgroundwork_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(5);
                backgroundwork.ReportProgress(i);
            }
        }

        private void Backgroundwork_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.ImageUploadBar.Visibility = Visibility.Visible;
            this.ImageUploadBar.Value = (int)e.ProgressPercentage;
        }

        /// <summary>
        /// 只有 Backgroundwork_RunWorkerCompleted 可以更新UI
        /// </summary>
        private void Backgroundwork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        /// <summary>
        /// 初始化 Kinect
        /// </summary>
        private void InitializeKinect()
        {
            _sensor = KinectSensor.GetDefault();

            if (_sensor != null)
            {
                _sensor.Open();
            }
        }

        /// <summary>
        /// 初始化多重幀數
        /// </summary>
        private void InitializeMultiSourceFrameReader()
        {

            //MultiSourceFrameReader and event
            _reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.BodyIndex);
            _reader.MultiSourceFrameArrived += _reader_MultiSourceFrameArrived;

            //Initialize the background removal tool.
            _backgroundRemovalTool = new BackgroundRemovalTool(_sensor.CoordinateMapper);
        }

        /// <summary>
        /// 多重幀數事件
        /// </summary>
        private void _reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var multi_source_frame = e.FrameReference.AcquireFrame();
            using (var colorFrame = multi_source_frame.ColorFrameReference.AcquireFrame())
            using (var depthFrame = multi_source_frame.DepthFrameReference.AcquireFrame())
            using (var bodyIndexFrame = multi_source_frame.BodyIndexFrameReference.AcquireFrame())
            {
                if (colorFrame != null && depthFrame != null && bodyIndexFrame != null)
                {
                    //Update the image source.
                    camera.Source = _backgroundRemovalTool.GreenScreen(colorFrame, depthFrame, bodyIndexFrame);
                    //colorcamera.Source = colorFrame.ToBitmap();
                    QRCodeBitmap = colorFrame.ToBitmap();
                }
            }
        }

        /// <summary>
        /// 初始化骨架
        /// </summary>
        private void InitializeBodyFrame()
        {
            //骨架、骨架事件
            bodyFrameReader = _sensor.BodyFrameSource.OpenReader();
            bodyFrameReader.FrameArrived += BodyFrameReader_FrameArrived;   
        }

        /// <summary>
        /// 骨架事件
        /// </summary>
        private void BodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e){
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
                        //Assign a tracking ID to the face source
                        _faceSource.TrackingId = body.TrackingId;
                    }
                }

                //判斷骨架，並傳入參數
                for (int i = 0; i < bodies.Length; i++)
                {
                    //如果骨架有追蹤到才能執行 
                    if (bodies[i].IsTracked)
                    {
                        if (StopDetect != true)
                        {
                            //Body class ,posture_number ,face class
                            poseture.Poseture_Detected(bodies[i], vocabulary.Give_Vocabulary(posture_number), face_result);
                        }  
                    }
                }
            }
        }

        /// <summary>
        /// 初始化臉部
        /// </summary>
        private void InitializeFace()
        {
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

        /// <summary>
        /// 臉部事件
        /// </summary>
        private void _faceReader_FrameArrived(object sender, FaceFrameArrivedEventArgs e)
        {
            using (var face_frame = e.FrameReference.AcquireFrame())
            {
                if (face_frame != null)
                {
                    // Get the face frame result. Get the face points, mapped in the color space. in the Posetures.cs   
                    face_result = face_frame.FaceFrameResult;
                }
            }
        }

        /// <summary>
        /// 初始化語音辨識
        /// </summary>
        private void InitalizeSpeechRecognition()
        {
            // grab the audio stream
            IReadOnlyList<AudioBeam> audioBeamList = this._sensor.AudioSource.AudioBeams;
            System.IO.Stream audioStream = audioBeamList[0].OpenInputStream();

            // create the convert stream
            this.convertStream = new KinectAudioStream(audioStream);

            //RecognizerInfo
            RecognizerInfo recognizerInfo = RecognizerSelection.TryGetKinectRecognizer();

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

                try
                {
                    if (MicrophoneSetting.Microphone_Status)
                    {
                        this.speechEngine.SetInputToDefaultAudioDevice();
                    }
                    else
                    {
                        this.speechEngine.SetInputToAudioStream(this.convertStream, new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("輸入設備問題" + e, ToString());
                }

                this.speechEngine.RecognizeAsync(RecognizeMode.Multiple);

                //語音辨識事件，語音辨識拒絕事件
                this.speechEngine.SpeechRecognized += SpeechRecognized;
                this.speechEngine.SpeechRecognitionRejected += SpeechRejected;
            }
        }

        /// <summary>
        /// 語音辨識拒絕事件
        /// </summary>
        private void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            Console.WriteLine(e.Result.Alternates);

            foreach (RecognizedPhrase phrase in e.Result.Alternates)
            {
                Console.WriteLine("  Rejected phrase: " + phrase.Text);
                Console.WriteLine("  Confidence score: " + phrase.Confidence);
            }
        }

        /// <summary>
        /// 語音辨識事件
        /// </summary>
        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //if (StopDetect != true)
            {
                //Show Sppech Confidence number on screen
                Speech_Confidence.Content = "準確度:" + e.Result.Confidence.ToString();

                //設定語音信度門檻值
                if (e.Result.Confidence >= BasicMethod.ConfidenceThreshold)
                {
                    Console.WriteLine(e.Result.Text);

                    //語音辨識跟單字比對
                    if (e.Result.Semantics.Value.ToString().ToLower() == vocabulary.Give_Vocabulary(posture_number).ToLower())
                    {
                        //Firebase上傳資料
                        FirebaseUpload.Upload(ActivityTitle, "正確", new Speech(txtVocabulary.Text.Trim(), e.Result.Confidence));

                        if (Select_Item.SetPeerVocabularyGameMode == true)
                        {
                            //新增語音辨識信心度到List
                            SpeechConfidenceList.Add(BasicMethod.GiveSpeechConfidenceScore(e.Result.Confidence));

                            Console.WriteLine("總分" + TotalTime);

                            Console.WriteLine("語音分數" + BasicMethod.GiveSpeechConfidenceScore(e.Result.Confidence));
                        }

                        //顯示正確圖示
                        Speech_Correct.Visibility = Visibility.Visible;

                        //將語音辨識結果設為True
                        Speech_Result = true;

                        //播放正確音效
                        SoundPlay.PlaySoundCorrect();

                        //通知"事件"
                        if (!Speech_Result == false && !Posture_Result == false)
                        {
                            Success_Change_word(this, new EventArgs());
                        }  
                    }
                    else
                    {
                        //如果答錯就記錄到Firebase 但是也要設定門檻才不容易發生誤判
                        if (txtVocabulary.Text != null || e.Result.Confidence >= 0.35)
                        {
                            //Firebase上傳資料
                            FirebaseUpload.Upload(ActivityTitle, "錯誤", new Speech(txtVocabulary.Text.Trim(), e.Result.Semantics.Value.ToString(), e.Result.Confidence));
                        }
                    }
                }
            }       
        }

        /// <summary>
        /// 設定單字方法
        /// </summary>
        private void Set_word(int posture_number)
        {
            Console.WriteLine("語音辨識 :" + StopDetect);

            //清除房間名稱
            txtCountWords.Content = null;

            //設定圖示
            this.txtSentence.Visibility = Visibility.Collapsed;
            this.Speech_Correct.Visibility = Visibility.Collapsed;
            this.Posture_Correct.Visibility = Visibility.Collapsed;

            //Set Vocabulary
            txtVocabulary.Text = vocabulary.Give_Vocabulary(posture_number);

            //清空SpeechConfidenceList
            SpeechConfidenceList.Clear();

            //初始化遊戲時間
            GameTimer = 61;

            txtVocabulary.Visibility = Visibility.Hidden;

            txtSentence.Text = null;

            //Text To Speech 
            textToSpeech.SpeechMethod(txtVocabulary.Text.ToString());

            //啟動再次句子發音
            Speech_Enable = true;

            //將語音辨識結果設為預設值，以便下一次偵測
            Speech_Result = false;
            Posture_Result = false;

            //判斷是否顯示範例圖片
            //if (Select_Item.SetPeerVocabularyGameMode != true)
            //{
                this.exampleImage.Visibility = Visibility.Hidden;
                //Set Example Image
                this.exampleImage.Source = vocabulary.Give_Image(posture_number);
            //}

            if (Select_Item.SetPeerVocabularyGameMode == true)
            {
                //打開遊戲倒數時間
                dispatcherGameTimer.Start();
            }
        }

        /// <summary>
        /// 判斷成功事件，姿勢正確會呼叫此方法
        /// </summary>
        private void Success_Change_word(object sender, EventArgs e)
        {
            //停止時間
            dispatcherGameTimer.Stop();

            //姿勢正確會呼叫Success_Change_word，並把姿勢正確的圖示顯示出來
            Posture_Correct.Visibility = Visibility.Visible;

            Posture_Result = true;

            // 播放音效，用執行序來呼叫
            this.Dispatcher.Invoke((Action)(() =>
            {
                if (PostureSoundStatus != true)
                {
                    SoundPlay.PlaySoundCorrect();
                    PostureSoundStatus = true;

                    if (txtVocabulary.Text != null)
                    {
                        //Firebase上傳姿勢資料
                        FirebaseUpload.Upload(ActivityTitle, new Posture(txtVocabulary.Text.Trim(), 1));

                    }
                }
            }));

            if (!Speech_Result == false && !Posture_Result == false && !Speech_Enable == false)
            {
                //Hide Example Image
                this.exampleImage.Visibility = Visibility.Collapsed;

                Speech_Enable = false;

                //傳給對方讓它顯示句子
                sokcet_Connect.SendDataToServer((int)BasicMethod.TriggerButtonCode.Successful);

                // 紀錄遊戲時間分數，以時間當作積分，當遊戲模式啟動
                if (Select_Item.SetPeerVocabularyGameMode == true)
                {
                    //把語音辨識信心度當成分數
                    TotalTime += SpeechConfidenceList.Max(x => x);
                    //做出的動作越快分數越高
                    TotalTime += GameTimer;

                    Console.WriteLine("姿勢辨識總時間" + TotalTime + "剩餘時間" + GameTimer);

                    //回存遊戲分數
                    FirebaseUpload.Upload_Rakn_Point(Students.StudentsName, RankData.Rank_PeerVocabulary, TotalTime);

                }

                ShowSentence();
            }
        }


        /// <summary>
        /// 顯示句子
        /// </summary>
        private void ShowSentence()
        {
            //Show Sentence
            this.txtSentence.Visibility = Visibility.Visible;

            //顯示句子 
            txtSentence.Text = vocabulary.Give_Sentence(posture_number);

            //念出句子
            //需要用微軟的TextToSpeech念出句子，因為Azure Text To Speech 只能調整單一語言
            textToSpeech.MicrosoftTextToSpeech(txtSentence.Text.ToString());
            //textToSpeech.SpeechMethod(txtSentence.Text.ToString());

            //顯示下一題按鈕
            btnNext.Visibility = Visibility.Visible;
        }

        private void Next()
        {
            //將音效的狀態設定為初始值
            PostureSoundStatus = false;

            //數字遞增
            posture_number++;

            //改單字
            Set_word(posture_number);

            //隱藏語音辨識正確圖示
            Speech_Correct.Visibility = Visibility.Hidden;
            //隱藏姿勢辨識正確圖示
            Posture_Correct.Visibility = Visibility.Hidden;

            //將語音辨識結果設為預設值，以便下一次偵測
            Speech_Result = false;
            Posture_Result = false;

            //Clean Sppech Confidence number
            Speech_Confidence.Content = null;

            //Clean sentence
            txtSentence.Text = null;

            //Clean Face features
            FaceInfo.Text = null;

            //初始化遊戲時間
            GameTimer = 61;
        }

        /// <summary>
        /// 發音，如果句子是空的，就發音單字，如果否就是發音句子 
        /// </summary>
        private void BtnSpeech_Click(object sender, RoutedEventArgs e)
        {
            sokcet_Connect.SendDataToServerIncludeMyself((int)BasicMethod.TriggerButtonCode.Pronunciation);

            if (String.IsNullOrEmpty(txtSentence.Text))
            {
                textToSpeech.SpeechMethod(txtVocabulary.Text.ToString());
            }
            else
            {
                //需要用微軟的TextToSpeech念出句子，因為Azure Text To Speech 只能調整單一語言
                textToSpeech.MicrosoftTextToSpeech(txtSentence.Text.ToString());
                //textToSpeech.SpeechMethod(txtSentence.Text.ToString());
            }
        }

        /// <summary>
        /// 拍照按鈕按下去執行dispatcherTimer.Start(); 時間倒數
        /// </summary> 
        private void TimerTrigger(object sender, RoutedEventArgs e)
        {
            //拍照前把範例照片關閉
            this.exampleImage.Source = null;

            if (!dispatcherTimer.IsEnabled)
            {
                dispatcherTimer.Start();
            }
            else { return; }
        }

        /// <summary>
        /// 拍照時間觸發方法
        /// </summary>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (CountdownTimer > 1)
            {
                CountdownTimer--;
                this.txtCountdowntime.Content = CountdownTimer.ToString();
            }
            else
            {
                dispatcherTimer.Stop();
                this.txtCountdowntime.Content = null;
                this.CountdownTimer = 4;
                SoundPlay.PlaySoundCamera();
                BtnPicture_Click(this, new RoutedEventArgs());
            }
        }

        /// <summary>
        /// 遊戲時間觸發方法
        /// </summary>
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (GameTimer > 1)
            {
                GameTimer--;
                this.txtGameTime.Content = GameTimer.ToString();
            }
            else
            {

                WrongVocabularyList.Add(txtVocabulary.Text);

                Console.WriteLine("答錯單字 " + txtVocabulary.Text);

                //初始化遊戲倒數
                dispatcherGameTimer.Stop();
                this.txtGameTime.Content = null;
                //this.GameTimer = 31;

            }
        }

        /// <summary>
        /// 拍照，並判斷字串是否包含情感的單字，如果有會進入第一判斷式呼叫 SetFaceImage 顯示所拍攝的照片
        /// </summary>
        private void BtnPicture_Click(object sender, RoutedEventArgs e)
        {
            String IdentifyEmotion_Vocabulary = vocabulary.Give_Vocabulary(posture_number);

            //使用Linq檢查單字是否有情感的單字
            if (Vocabulary.EmotionString.Any(IdentifyEmotion_Vocabulary.Contains))
            {
                SetFaceImage(this.ImagePath = PrintScreenMethod.PrintScreen());
            }
            else
            {
                ImagePath = PrintScreenMethod.PrintScreen();
            }
        }

        /// <summary>
        /// 顯示照片
        /// </summary>
        private void SetFaceImage(String path)
        {
            BitmapSource bitmapSource = new BitmapImage(new Uri(path, UriKind.Absolute));
            this.exampleImage.Source = bitmapSource;
        }

        /// <summary>
        /// 上傳照片到雲端，拿取ImagePath，拍照的方法裡面會回傳路徑到ImagePath
        /// </summary>
        private void BtnUploadFaceImage_Click(object sender, RoutedEventArgs e)
        {
            if (ImagePath == null) { return; }
            face_Recogniton.UploadFaceImage(ImagePath);

            //Show progress bar
            backgroundwork.RunWorkerAsync();
        }

        /// <summary>
        /// 委派所註冊的方法，從雲端回傳的資料，傳送到這方法，比對單字跟臉部辨識的表情
        /// </summary>
        private void SetFaceFeature(String face_feature)
        {
            this.FaceInfo.Text = face_feature;

            //Clear progressbar 
            this.ImageUploadBar.Value = 0;

            this.ImageUploadBar.Visibility = Visibility.Collapsed;

            //Firebase上傳照片結果以及次數
            if (txtVocabulary.Text != null)
            {
                FirebaseUpload.Upload(ActivityTitle, new Photo(txtVocabulary.Text.Trim(), face_feature, 1));
            }

            if (face_feature.ToLower().Contains(vocabulary.Give_Vocabulary(posture_number).ToLower()))
            {
                //呼叫成功的方法
                Success_Change_word(this, new EventArgs());
            }
            Console.WriteLine(face_feature);

            if (BasicMethod.CheckEmotionWord(vocabulary.Give_Vocabulary(posture_number), face_feature))
            {
                //呼叫成功的方法
                Success_Change_word(this, new EventArgs());
            }
        }

        /// <summary>
        /// 委派所註冊的方法，從雲端回傳的照片，傳送到這方法，並顯示出來
        /// </summary>
        private void SetFaceImage(RenderTargetBitmap FacebitmapImage)
        {
            this.exampleImage.Source = FacebitmapImage;
        }

        private void QRCodeTimer_Tick(object sender, EventArgs e)
        {
            ShowScore();

            if (QRCodeBitmap == null) { return; }
            //把ColorFrame的wbData的資料從WriteableBitmap格式，轉成Bitmap格式，才能符合QRcode格式
            ZXing.Result result = reader.Decode(BitmapToWriteableBitmap.BitmapFromWriteableBitmap(QRCodeBitmap));

            if (result != null)
            {
                Console.WriteLine(result.Text);
                if (vocabulary.Give_Vocabulary(posture_number).ToLower().Contains(result.Text.ToLower()))
                {
                    Success_Change_word(this, new EventArgs());
                    Console.WriteLine("成功" + result.Text);
                    FirebaseUpload.Upload(ActivityTitle, "正確", new ORcodeObject(result.Text, txtVocabulary.Text, 1));
                }
                else
                {
                    SoundPlay.PlaySoundFailed();
                    Console.WriteLine("失敗" + result.Text);
                    FirebaseUpload.Upload(ActivityTitle, "錯誤", new ORcodeObject(result.Text, txtVocabulary.Text, 1));
                }
            }
        }

        /// <summary>
        /// 打開排行榜
        /// </summary>
        private void BtnRank_Click(object sender, RoutedEventArgs e)
        {
            RankData rankData = new RankData(RankData.Rank_PeerVocabulary);
            rankData.Show();
        }

        /// <summary>
        /// 關閉頁面
        /// </summary>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //註銷掉事件
            bodyFrameReader.FrameArrived -= BodyFrameReader_FrameArrived;
            _reader.MultiSourceFrameArrived -= _reader_MultiSourceFrameArrived;
            poseture.Poseture_List -= Success_Change_word;
            dispatcherTimer.Tick -= DispatcherTimer_Tick;
            dispatcherGameTimer.Tick -= GameTimer_Tick;
            QRcodeTimer.Tick -= QRCodeTimer_Tick;

            //關閉Socket.io連線
            sokcet_Connect.SocketClose();

            //Stop Text To Speech
            textToSpeech.SpeechPause();

            //Stop SoundPlay
            SoundPlay.PlayStop();

            //停止偵測QRCode
            QRcodeTimer.Stop();

            //暫停拍照倒數時間
            dispatcherTimer.Stop();

            //將音效的狀態設定為初始值
            PostureSoundStatus = false;

            if (_reader != null)
            {
                _reader.Dispose();
                _reader = null;
            }

            if (bodyFrameReader != null)
            {
                bodyFrameReader.Dispose();
                bodyFrameReader = null;
            }

            if (null != this.convertStream)
            {
                this.convertStream.SpeechActive = false;
            }

            if (null != this.speechEngine)
            {
                this.speechEngine.SpeechRecognized -= this.SpeechRecognized;
                this.speechEngine.SpeechRecognitionRejected -= this.SpeechRejected;
                this.speechEngine.RecognizeAsyncStop();
            }

            PeerMainWidnow peerMainWidnow = new PeerMainWidnow();
            peerMainWidnow.Show();
        }

        /// <summary>
        /// 開始按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //關閉提示按鈕
            HideAllView();
            //同步單字
            sokcet_Connect.SendDataToServerIncludeMyself(posture_number);
            //開始雙人模式
            sokcet_Connect.SendDataToServer((int)BasicMethod.TriggerButtonCode.StartPeerMode);
            //通知對方顯示提示按鈕
            sokcet_Connect.SendDataToServer((int)BasicMethod.TriggerButtonCode.ShowHintView);
        }


        /// <summary>
        /// 判斷上述條件是否達成，傳資料過去同步單字
        /// </summary>
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            //顯示所有的按鈕
            VisibilityAllView();

            //通知對方關閉提示按鈕
            sokcet_Connect.SendDataToServer((int)BasicMethod.TriggerButtonCode.HideHintView);

            //傳資料過去同步單字
            sokcet_Connect.SendDataToServerIncludeMyself(posture_number+1);

            //關閉語音辨識與姿勢辨識
            StopDetect = true;
        }

        /// <summary>
        /// 傳給對方姿勢提示
        /// </summary>
        private void BtnPostureHint_Click(object sender, RoutedEventArgs e)
        {
            sokcet_Connect.SendDataToServer((int)BasicMethod.TriggerButtonCode.ExamplePosture);
        }


        /// <summary>
        /// 給對方單字提示
        /// </summary>
        private void BtnVocabularyHint_Click(object sender, RoutedEventArgs e)
        {
            txtVocabulary.Visibility = Visibility.Visible;
            sokcet_Connect.SendDataToServer((int)BasicMethod.TriggerButtonCode.ShowVocabulary);
        }

        private void HideAllView()
        {
            StopDetect = false;
            btnStart.Visibility = Visibility.Hidden;
            txtPronunciation.Visibility = Visibility.Hidden;
            btnSpeech.Visibility = Visibility.Hidden;
            RoomName.Visibility = Visibility.Hidden;
            RoomInput.Visibility = Visibility.Hidden; 
            btnPostureHint.Visibility = Visibility.Hidden;
            txtPostureHint.Visibility = Visibility.Hidden;
            btnVocabularyHint.Visibility = Visibility.Hidden;
            txtVocabularyHint.Visibility = Visibility.Hidden;

            btnPicture.Visibility = Visibility.Visible;
            txtTakePicture.Visibility = Visibility.Visible;
            btnNext.Visibility = Visibility.Visible;
            txtNext.Visibility = Visibility.Visible;

        }

        private void VisibilityAllView()
        {
            txtPronunciation.Visibility = Visibility.Visible;
            txtTakePicture.Visibility = Visibility.Visible;
            txtNext.Visibility = Visibility.Visible;
            btnSpeech.Visibility = Visibility.Visible;
            btnNext.Visibility = Visibility.Visible; 
            btnPostureHint.Visibility = Visibility.Visible;
            txtPostureHint.Visibility = Visibility.Visible;
            btnVocabularyHint.Visibility = Visibility.Visible;
            txtVocabularyHint.Visibility = Visibility.Visible;

            btnNext.Visibility = Visibility.Hidden;
            txtNext.Visibility = Visibility.Hidden;
            btnPicture.Visibility = Visibility.Hidden;
            txtTakePicture.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 顯示分數在畫面上
        /// </summary>
        private void ShowScore()
        {
            txtScore.Content = (String.Format("分數 : {0}", TotalTime.ToString()));
        }
    }
}
   