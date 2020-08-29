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

namespace Kinect_v2_Learning
{
    /// <summary>
    /// Login.xaml 的互動邏輯
    /// </summary>
    public partial class Login : Window
    {
        private GameEvent gameEvent = new GameEvent(10);

        public Login()
        {
            InitializeComponent();
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));

            //設定事件連結
            gameEvent.GameEventHandler += GameTrigger;
            gameEvent.GameStart();

        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBoxLogin.Text)) {
                Students.Group = "Individual";
                Students.StudentsName = txtBoxLogin.Text.Trim();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }


        private void BtnLoginPeer_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBoxLogin.Text))
            {
                Students.Group = "Peer";
                Students.StudentsName = txtBoxLogin.Text.Trim();
                PeerMainWidnow peerMainWidnow = new PeerMainWidnow();
                peerMainWidnow.Show();
                this.Close();
            }

        }

        private void GameTrigger(object sender, EventArgs e)
        {
            Console.WriteLine("遊戲時間觸發..........");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameEvent.GameStop();
            gameEvent.GameEventHandler -= GameTrigger;
        }
    }
}
