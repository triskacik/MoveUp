using System;
using MoveUp.Helpers;

namespace MoveUp.Models
{
    public class CoreMotionData : ModelBase
    {
        public DateTime Date { get; set; }
        public int Steps { get; set; }
        public double Distance { get; set; }
        public int Floors { get; set; }
        public int Calories { get; set; }

        public CoreMotionData()
        {
            Date = DateTime.Now;
            Steps = 0;
            Distance = 0;
            Floors = 0;
        }

        public void RefreshCalories()
        {
            Calories = CaloriesCounter.GetCalories(Distance);
        }
    }
}
