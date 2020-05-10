using System;
using System.Timers;

namespace MoveUp.Models
{
    public class CoreActivityData : ModelBase
    {
        public DateTime StartDate { get; set; }

        public TimeSpan Duration { get; set; } = new TimeSpan(0);
        public string DurationString
        {
            get => String.Format("{0:00}:{1:00}:{2:00}", Duration.Hours, Duration.Minutes, Duration.Seconds);
        }

        public double Distance { get; set; }
        public double AvgSpeed { get; set; }
        public double Pace { get; set; }

        private Timer timer = new Timer(1000);

        public CoreActivityData()
        {
            timer.Elapsed += TimerElapsed;
            StartDate = DateTime.Now;
            Distance = 0;
            AvgSpeed = 0;
            Pace = 0;
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Duration += TimeSpan.FromSeconds(1);
        }
    }
}
