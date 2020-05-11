using System;
namespace MoveUp.Models
{
    public class TimerData : ModelBase
    {
        public TimeSpan Duration { get; set; } = new TimeSpan(0);
        public string DurationString
        {
            get => String.Format("{0:00}:{1:00}:{2:00}", Duration.Hours, Duration.Minutes, Duration.Seconds);
        }

        public TimerData()
        {
        }
    }
}
