using System;
using System.Timers;

namespace MoveUp.Models
{
    public class CoreActivityData : ModelBase
    {
        public DateTime StartDate { get; set; }

        public double Distance { get; set; }
        public double AvgSpeed { get; set; }
        public double Pace { get; set; }

        public CoreActivityData()
        {
            StartDate = DateTime.Now;
            Distance = 0;
            AvgSpeed = 0;
            Pace = 0;
        }
    }
}
