using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Kinect_v2_Learning
{
    public class GameEvent
    {
        private int GameTime;
        /// <summary>
        /// 遊戲倒數時間
        /// </summary>
        private DispatcherTimer dispatcherGameTimer = new DispatcherTimer(DispatcherPriority.Normal);

        public event EventHandler GameEventHandler;

        public GameEvent(int gameTime)
        {
            GameTime = gameTime;
            //Set DispatcherTimer Game time
            dispatcherGameTimer.Interval = new TimeSpan(0, 0, 1);

            //Set Trigger method
            dispatcherGameTimer.Tick += new EventHandler(GameTimer_Tick);
        }

        public void GameTimeInit(int time)
        {
            this.GameTime = time;
        }

        public void GameStart()
        {
            dispatcherGameTimer.Start();
        }

        public void GameStop()
        {
            dispatcherGameTimer.Stop();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (GameTime > 0)
            {
                GameTime--;
                GameEventHandler(this, new EventArgs());
            }
            else
            {
                GameStop();
            }    
        }
    }
}
