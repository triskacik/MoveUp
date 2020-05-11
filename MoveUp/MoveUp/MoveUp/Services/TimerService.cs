using System;
using System.Timers;
using MoveUp.Models;
using MoveUp.Services.Interfaces;

namespace MoveUp.Services
{
    public class TimerService : ITimerService
    {
        private Timer timer = new Timer(1000);
        private DateTime timeStopped;
        private TimerData timerData = new TimerData();

        public TimerService()
        {
            timer.Elapsed += TimerElapsed;
        }

        public void Reinitialize()
        {
            timer = new Timer(1000);
            timer.Elapsed += TimerElapsed;
            timerData.Duration = new TimeSpan(0);
            timeStopped = new DateTime();
        }

        public void StopTimer()
        {
            timer.Stop();
            timeStopped = DateTime.Now;
        }

        public void StartTimer()
        {
            timer.Start();
        }

        public void ResumeTimer()
        {
            if (timeStopped != null)
            {
                TimeSpan sleepingTime = DateTime.Now - timeStopped;
                timerData.Duration += sleepingTime;
            }
            
            timer.Start();
        }

        public TimerData GetTimerData()
        {
            return timerData;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            timerData.Duration += TimeSpan.FromSeconds(1);
        }
    }
}
