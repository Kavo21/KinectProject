using Microsoft.Kinect;
using Microsoft.Kinect.Face;
using System;

namespace Kinect_v2_Learning
{
    public class Poseture
    {
        /// <summary>
        /// 其他人只要向 Poseture_List 註冊相對應的方法，當Poseture_List(this, new EventArgs());發生，即呼叫當時註冊相對應的方法
        /// </summary>
        public event EventHandler Poseture_List;

        /// <summary>
        /// 鎖住方法，不然會一直回傳成功給伺服器
        /// </summary>
        public bool Lock = false;

        public void Poseture_Detected(Body body, String vocabulary,FaceFrameResult face_result) {

            if (face_result != null)
            {
                //頭部
                var Head = body.Joints[JointType.Head];//頭
                var Neck = body.Joints[JointType.Neck];//脖子
                //手臂兩側
                var RightHandTip = body.Joints[JointType.HandTipRight];//右手頂部
                var LeftHandTip = body.Joints[JointType.HandTipLeft];//左手頂部
                var RightHand = body.Joints[JointType.HandRight];//右手
                var LeftHand = body.Joints[JointType.HandLeft];//左手
                var RightThumb = body.Joints[JointType.ThumbRight];//右手掌
                var LeftThumb = body.Joints[JointType.ThumbLeft];//左手掌
                var RightWrist = body.Joints[JointType.WristRight];//右手腕
                var LeftWrist = body.Joints[JointType.WristRight];//左手腕
                var RightElbow = body.Joints[JointType.ElbowRight];//右手臂
                var LeftElbow = body.Joints[JointType.HandLeft];//左手臂
                var RightShoulder = body.Joints[JointType.ShoulderRight];//右肩膀
                var LeftShoulder = body.Joints[JointType.ShoulderLeft];//左肩膀
                //中間
                var SpineShoulder = body.Joints[JointType.SpineShoulder];//脊柱肩膀
                var SpineMid = body.Joints[JointType.SpineMid];//脊柱中間
                var SpineBase = body.Joints[JointType.SpineBase];//脊柱基礎
                //下半身
                var RightHip = body.Joints[JointType.HipRight];//右髖關節
                var LeftHip = body.Joints[JointType.HipLeft];//左髖關節
                var RightKnee = body.Joints[JointType.KneeRight];//右膝蓋
                var LeftKnee = body.Joints[JointType.KneeLeft];//左膝蓋
                var RightAnkle = body.Joints[JointType.AnkleRight];//右腳踝
                var LeftAnkle = body.Joints[JointType.AnkleLeft];//左腳踝
                var RightFoot = body.Joints[JointType.FootRight];//右腳
                var LeftFoot = body.Joints[JointType.FootLeft];//左腳

                //臉部
                var LeftEye = face_result.FacePointsInColorSpace[FacePointType.EyeLeft];
                var RightEye = face_result.FacePointsInColorSpace[FacePointType.EyeRight];
                var Nose = face_result.FacePointsInColorSpace[FacePointType.Nose];
                var LeftMouth = face_result.FacePointsInColorSpace[FacePointType.MouthCornerLeft];
                var RightMouth = face_result.FacePointsInColorSpace[FacePointType.MouthCornerRight];
                var LeftEyeClosed = face_result.FaceProperties[FaceProperty.LeftEyeClosed];
                var RightEyeClosed = face_result.FaceProperties[FaceProperty.RightEyeClosed];
                var MouthOpen = face_result.FaceProperties[FaceProperty.MouthOpen];
                var Happy = face_result.FaceProperties[FaceProperty.Happy];
                var Glasses = face_result.FaceProperties[FaceProperty.WearingGlasses];

                //手部
                var RightHandState = body.HandLeftState;
                var LeftHandState = body.HandRightState;
    
                switch (vocabulary)
                {
                    #region 方位介係詞
                    case "Behind":
                        if ((RightHand.Position.Y >= RightElbow.Position.Y) && (RightHand.Position.X < RightElbow.Position.X) && (RightElbow.Position.Y > RightShoulder.Position.Y) && (RightHand.Position.Z > RightElbow.Position.Z))
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Behind"
                            };

                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Under":
                        if ((RightHandTip.Position.Y - 0.05 <= RightKnee.Position.Y - 0.05 && RightElbow.Position.Y >= RightHandTip.Position.Y) || (LeftHandTip.Position.Y - 0.05 <= LeftKnee.Position.Y - 0.05 && LeftElbow.Position.Y >= LeftHandTip.Position.Y))
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Under"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Over":
                        if (RightHandTip.Position.Y >= Head.Position.Y || LeftHandTip.Position.Y >= Head.Position.Y)
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Over"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Above":
                        if (RightHand.Position.Y >= Head.Position.Y || LeftHand.Position.Y >= Head.Position.Y)
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Above"
                            };

                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Below":
                        if (RightHandTip.Position.Y - 0.05 <= RightKnee.Position.Y - 0.05 || LeftHandTip.Position.Y - 0.05 <= LeftKnee.Position.Y - 0.05)
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Below"
                            };

                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "In front of":
                        if ((RightHandTip.Position.Z > RightShoulder.Position.Z || RightHandTip.Position.Y >= SpineShoulder.Position.Y) && (LeftHandTip.Position.Z > LeftShoulder.Position.Z || LeftHandTip.Position.Y >= SpineShoulder.Position.Y))
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "In front of"
                            };

                            NotifyEvent(returnPostureWords);
                        }
                        break;
                    case "Down":
                        if ((RightHandTip.Position.Y - 0.05 <= RightKnee.Position.Y - 0.05 && RightElbow.Position.Y >= RightHandTip.Position.Y) || (LeftHandTip.Position.Y - 0.05 <= LeftKnee.Position.Y - 0.05 && LeftElbow.Position.Y >= LeftHandTip.Position.Y))
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Down"
                            };

                            NotifyEvent(returnPostureWords);
                        }
                        break;
                    #endregion

                    #region 動詞
                    case "Relieve":
                        if (LeftHandTip.Position.Y >= SpineShoulder.Position.Y && RightHandTip.Position.Y <= SpineMid.Position.Y &&
                            RightHandTip.Position.Y > SpineBase.Position.Y)
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Relieve"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Scratch":
                        if (LeftHandTip.Position.Y >=SpineShoulder.Position.Y -0.05 && RightHandTip.Position.Y >= SpineShoulder.Position.Y - 0.05  &&
                            LeftHandTip.Position.Y <= Head.Position.Y && //左右手小於頭
                            RightHandTip.Position.Y <= Head.Position.Y && //左右手小於頭
                            LeftHandTip.Position.X > RightHandTip.Position.X && //左右手互相超過
                            RightHandTip.Position.X < LeftHandTip.Position.X) //左右手互相超過
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Scratch"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Comfort":
                        if (LeftHandTip.Position.Y > SpineMid.Position.Y && RightHandTip.Position.Y > SpineMid.Position.Y &&
                            LeftHandTip.Position.Y < Head.Position.Y && RightHandTip.Position.Y < Head.Position.Y &&
                            LeftHandTip.Position.X >= RightShoulder.Position.X &&
                            RightHandTip.Position.X <= LeftShoulder.Position.X)
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Comfort"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Float":
                        if (LeftHandTip.Position.Y > LeftShoulder.Position.Y && RightHandTip.Position.Y > RightShoulder.Position.Y &&
                            LeftHandTip.Position.Y < Head.Position.Y && RightHandTip.Position.Y < Head.Position.Y &&
                            LeftHandTip.Position.X < LeftShoulder.Position.X && RightHandTip.Position.X > RightShoulder.Position.X)
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Float"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Sprinkle":
                        if (LeftHandTip.Position.Y < SpineMid.Position.Y && //左手介於肚子之間
                            LeftHandTip.Position.Y > SpineBase.Position.Y && //左手介於肚子之間
                            LeftHandTip.Position.X > LeftShoulder.Position.X && //左手小於左肩膀
                            RightHandTip.Position.Y > Head.Position.Y && //右手大於頭
                            RightHandTip.Position.X > RightShoulder.Position.X  &&//右手大於右肩膀
                            RightHandTip.Position.Y > RightElbow.Position.Y) //右手大於手肘
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Sprinkle"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Wander":
                        if (LeftHandTip.Position.Y < LeftElbow.Position.Y && //左手小於左肘
                            RightHandTip.Position.Y > RightElbow.Position.Y && //右手大於右肘
                            RightHandTip.Position.Y > RightShoulder.Position.Y && //右手大於肩膀
                            RightKnee.Position.Y > LeftKnee.Position.Y && //右膝蓋大於左膝蓋
                            RightAnkle.Position.Y >= LeftKnee.Position.Y -0.05) //右腳踝大於左膝蓋
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Wander"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Ban":
                        if ((RightHand.Position.X - 0.05 <= LeftElbow.Position.X + 0.05) && (RightHand.Position.Y >= RightElbow.Position.Y) && (LeftHand.Position.Y >= LeftElbow.Position.Y))
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Ban"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Destroy":
                        if (LeftHandTip.Position.Y > LeftElbow.Position.Y && //左手大於左肘
                            LeftHandTip.Position.Y < Head.Position.Y && //左手小於頭
                            LeftHandTip.Position.Y > SpineMid.Position.Y &&
                            RightHandTip.Position.Z >= LeftHandTip.Position.Z && //右手大於左手的 Z
                            RightHandTip.Position.Y < Head.Position.Y && //右手小於頭
                            RightHandTip.Position.Y > LeftHandTip.Position.Y)//右手高於左手
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Destroy"
                            };
                            NotifyEvent(returnPostureWords);

                        }
                        break;

                    case "Merge":
                        if ( LeftHandTip.Position.Y < Head.Position.Y && //左手小於頭
                            RightHandTip.Position.Y < Head.Position.Y && //右手小於頭
                            LeftHandTip.Position.Y > SpineMid.Position.Y && //左手大於中間
                            RightHandTip.Position.Y > SpineMid.Position.Y && //右手大於中間

                            LeftHandTip.Position.X >= RightHandTip.Position.X && //左右手互相碰起來
                            RightHandTip.Position.X <= LeftHandTip.Position.X)
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Merge"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Overcome":
                        if (RightHandTip.Position.Y > RightElbow.Position.Y && //右手大於右肘
                            RightElbow.Position.Y > RightShoulder.Position.Y && //右肘大於右肩
                            RightHandTip.Position.X < RightElbow.Position.X) //右手X小於右肘
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Overcome"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Launch":
                        if (LeftHandTip.Position.Y > Head.Position.Y && //左手大於頭
                            RightHandTip.Position.Y > Head.Position.Y && //右手大於頭
                            LeftHandTip.Position.X < LeftShoulder.Position.X && //左手超過左肩
                            RightHandTip.Position.X > RightShoulder.Position.X)//右手超過右肩
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Launch"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Establish":
                        if (LeftHandTip.Position.Y > Head.Position.Y && //左手大於頭
                            RightHandTip.Position.Y > Head.Position.Y && //右手大於頭
                            LeftHandTip.Position.Y > RightHandTip.Position.Y && //左手大於右手
                            LeftHandTip.Position.X > LeftShoulder.Position.X &&
                            RightHandTip.Position.X < RightShoulder.Position.X)
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Establish"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Attach":
                        if ((RightHandTip.Position.Z > RightShoulder.Position.Z || RightHandTip.Position.Y >= SpineShoulder.Position.Y) && (LeftHandTip.Position.Z > LeftShoulder.Position.Z || LeftHandTip.Position.Y >= SpineShoulder.Position.Y))
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Attach"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Hail":
                        if (RightHandState == HandState.Closed && LeftHandState == HandState.Closed &&
                            LeftHandTip.Position.Y < Head.Position.Y && //左手小於頭
                            LeftHandTip.Position.Y > LeftElbow.Position.Y &&//左手大於左肘
                            LeftHandTip.Position.X > LeftElbow.Position.X &&//左手小於左肘 X
                            RightHandTip.Position.Y > Head.Position.Y &&  //右手大於頭
                            RightHandTip.Position.Y > RightElbow.Position.Y) //右手大於右肘
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Hail"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;
                    #endregion


                    case "Chubby":
                        if (LeftElbow.Position.Y > LeftHandTip.Position.Y && 
                            RightElbow.Position.Y > RightHandTip.Position.Y &&
                            LeftHandTip.Position.X < LeftShoulder.Position.X &&
                            RightHandTip.Position.X > RightShoulder.Position.X &&
                            LeftElbow.Position.X < LeftShoulder.Position.X &&
                             RightElbow.Position.X > RightShoulder.Position.X &&
                             RightHandTip.Position.Y > SpineBase.Position.Y &&
                             LeftHandTip.Position.Y > SpineBase.Position.Y)
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Chubby"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                   
                    case "Smelly":
                        if (LeftHandTip.Position.Y > SpineShoulder.Position.Y &&
                            LeftHandTip.Position.Y < Head.Position.Y &&
                            RightHandTip.Position.Y > SpineShoulder.Position.Y &&
                            RightHandTip.Position.Y < Head.Position.Y &&
                             RightHandTip.Position.X <= LeftHandTip.Position.X &&
                             LeftHandTip.Position.X >= RightHandTip.Position.X)
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Smelly"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Unidentified":
                        if (LeftHandTip.Position.X <  LeftShoulder.Position.X &&
                            RightHandTip.Position.X > RightShoulder.Position.X &&
                            LeftHandTip.Position.Y > LeftShoulder.Position.Y &&
                            RightHandTip.Position.Y > RightShoulder.Position.Y &&
                             LeftHandTip.Position.Y < Head.Position.Y &&
                             RightHandTip.Position.Y < Head.Position.Y)                  
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Unidentified"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Guilty":
                        if (LeftHandTip.Position.Y > SpineShoulder.Position.Y &&
                            LeftHandTip.Position.Y > Head.Position.Y &&
                            RightHandTip.Position.Y > SpineShoulder.Position.Y &&
                            RightHandTip.Position.Y > Head.Position.Y)
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Guilty"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Individual":
                        if (RightHandState == HandState.Closed && LeftHandState == HandState.Closed &&
                            RightHandTip.Position.Y < Neck.Position.Y &&
                            LeftHandTip.Position.Y < Neck.Position.Y &&
                            RightHandTip.Position.Y >= SpineShoulder.Position.Y &&
                            LeftHandTip.Position.Y >= SpineShoulder.Position.Y &&
                            RightHandTip.Position.X < RightShoulder.Position.X &&
                            LeftHandTip.Position.X > LeftShoulder.Position.X)
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Individual"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Depression":
                        if (RightKnee.Position.Y > SpineBase.Position.Y &&
                            LeftKnee.Position.Y > SpineBase.Position.Y &&
                            LeftHandTip.Position.Y > SpineShoulder.Position.Y &&
                            LeftHandTip.Position.Y > Head.Position.Y &&
                            RightHandTip.Position.Y > SpineShoulder.Position.Y &&
                            RightHandTip.Position.Y > Head.Position.Y)
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Depression"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Confidence":
                        if (RightHandTip.Position.Y > RightElbow.Position.Y && //右手大於右肘
                            RightElbow.Position.Y > RightShoulder.Position.Y && //右肘大於右肩
                            RightHandTip.Position.X < RightElbow.Position.X &&
                            LeftHandTip.Position.Y > LeftElbow.Position.Y && //右手大於右肘
                            LeftElbow.Position.Y > LeftShoulder.Position.Y && //右肘大於右肩
                            LeftHandTip.Position.X > LeftElbow.Position.X &&
                            RightHandState == HandState.Closed && LeftHandState == HandState.Closed)
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Confidence"
                            };
                            NotifyEvent(returnPostureWords);
                        }
                        break;

                    case "Happy":
                        if (Happy == DetectionResult.Maybe)
                        {

                        }
                        else if (Happy == DetectionResult.Yes)
                        {
                            ReturnPostureWords returnPostureWords = new ReturnPostureWords()
                            {
                                PostureWords = "Happy"
                            };

                            NotifyEvent(returnPostureWords);
                        }
                        break;

                }
            }
        }

        private void NotifyEvent(ReturnPostureWords returnPostureWords)
        {
            Poseture_List(this, returnPostureWords);
        }

        /// <summary>
        /// 專門回傳ReturnPostureWords 物件，剛好Vocabulary 是需要回傳的值而且，前面Case 已經檢查過
        /// </summary>
        private ReturnPostureWords SetPostureWords(String vocabulary)
        {
            return new ReturnPostureWords { PostureWords = vocabulary };
        }
    }
}

