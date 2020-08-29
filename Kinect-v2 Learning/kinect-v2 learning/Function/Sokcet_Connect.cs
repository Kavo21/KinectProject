using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_v2_Learning
{
    public class Sokcet_Connect
    {
        private Socket socket;

        /// <summary>
        /// 傳送資料委派
        /// </summary>
        public delegate void SendDataToClinet(String data);

        /// <summary>
        /// 實體化委派
        /// </summary>
        public SendDataToClinet sendDataToClinet;

        /// <summary>
        /// 連線成功委派
        /// </summary>
        public delegate void ConnectionSuccessful();

        /// <summary>
        /// 實體化委派
        /// </summary>
        public ConnectionSuccessful connectionSuccessful;

        /// <summary>
        /// 加入房間事件
        /// </summary>
        public delegate void ChatRoomAdded(String data);

        /// <summary>
        /// 實體化委派
        /// </summary>
        public ChatRoomAdded chatRoomAdded;

        public Sokcet_Connect()
        {
            //連接到伺服器網址
            socket = IO.Socket("https://socketio-kinect-server.herokuapp.com/");

            //連接事件
            socket.On("Connection", () =>
            {
                connectionSuccessful.Invoke();
            });

            //加入房間事件
            socket.On("ChatRoomAdded", (data) =>
            {
                chatRoomAdded.Invoke(data.ToString());
            });

            //接受伺服器發送下來的事件與資料
            //創房事件
            socket.On("Chat message Room", (data) =>
            {
                sendDataToClinet.Invoke(data.ToString());
            });
            
            //使用者離開房間事件
            socket.On("disconnect", (data) =>
            {
                chatRoomAdded.Invoke(data.ToString());
            });
        }

        public void JoinRoom(String room)
        {
            //向伺服器端，發送事件，並傳資料
            socket.Emit("CreateRoom", room);
        }

        public void SendDataToServer(int data)
        {

            //向伺服器端，發送事件，並傳資料
            socket.Emit("RoomMessage", data);

        }

        public void SendDataToServerIncludeMyself(int data)
        {
            socket.Emit("RoomMessageIncludeMyself", data);
        }

        public void SocketClose()
        {
            socket.Close();
        }
    }
}
