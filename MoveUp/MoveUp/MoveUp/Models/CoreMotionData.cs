using System;
using MoveUp.Helpers;
using SQLite;

namespace MoveUp.Models
{
    [Table("motion_data")]
    public class CoreMotionData : ModelBase
    {
        [PrimaryKey]
        public DateTime Date { get; set; }

        public int Steps { get; set; }
        public double Distance { get; set; }
        public int Floors { get; set; }

        public int Calories
        {
            get => CaloriesCounter.GetCalories(Distance);
        }

        public CoreMotionData()
        {
            Date = DateTime.Now;
            Steps = 0;
            Distance = 0;
            Floors = 0;
        }
    }
}
