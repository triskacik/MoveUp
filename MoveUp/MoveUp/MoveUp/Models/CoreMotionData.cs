using MoveUp.Helpers;

namespace MoveUp.Models
{
    public class CoreMotionData : ModelBase
    {
        public int Steps { get; set; }
        public double Distance { get; set; }
        public int Floors { get; set; }
        public int Calories { get; set; }

        public CoreMotionData(int steps, double distance, int floors)
        {
            Steps = steps;
            Distance = distance;
        }

        public CoreMotionData()
        {
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
