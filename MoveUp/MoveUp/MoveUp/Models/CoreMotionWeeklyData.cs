using System;
using System.Collections.Generic;

namespace MoveUp.Models
{
    public class CoreMotionWeeklyData : ModelBase
    {
        public CoreMotionData[] data = new CoreMotionData[7];

        public CoreMotionWeeklyData(List<CoreMotionData> dataFeed)
        {
            if (dataFeed.Count == 7)
            {
                int i = 0;
                foreach (CoreMotionData d in dataFeed)
                {
                    data[i] = d;
                    i++;
                }
            }
        }
    } 
}
