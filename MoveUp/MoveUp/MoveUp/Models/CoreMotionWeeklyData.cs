using System;
using System.Collections.Generic;

namespace MoveUp.Models
{
    public class CoreMotionWeeklyData : ModelBase
    {
        public CoreMotionData[] Data { get; set; } = new CoreMotionData[7];
        public CoreMotionData AverageData { get; set; }

        public CoreMotionWeeklyData(List<CoreMotionData> dataFeed)
        {
            AverageData = new CoreMotionData();
            if (dataFeed.Count == 7)
            {
                int i = 0;
                foreach (CoreMotionData d in dataFeed)
                {
                    Data[i] = d;
                    AverageData.Distance += d.Distance;
                    AverageData.Floors += d.Floors;
                    AverageData.Steps += d.Steps;
                    i++;
                }
            } else
            {
                for (int i = 0; i < Data.Length; i++)
                {
                    Data[i] = new CoreMotionData();
                }
            }

            AverageData.Distance = Math.Round(AverageData.Distance / Data.Length, 3);
            AverageData.Floors = AverageData.Floors / Data.Length;
            AverageData.Steps = AverageData.Steps / Data.Length;
        }
    } 
}
