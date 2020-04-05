using System;
namespace MoveUp.Models
{
    public class CoreMotionData : ModelBase
    {
        public int Steps { get; set; }

        public CoreMotionData(int steps)
        {
            Steps = steps;
        }
    }
}
