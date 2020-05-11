using System;
using SQLite;

namespace MoveUp.Models
{
    [Table("hiking_data")]
    public class HikingSavedData : ModelBase
    {
        [PrimaryKey]
        public DateTime StartDate { get; set; }

        public string DurationString { get; set; }
        public double Distance { get; set; }
        public double AvgSpeed { get; set; }
        public double ElevationGain { get; set; }
        public string ShortDate
        {
            get => StartDate.ToShortDateString();
        }

        public HikingSavedData()
        {
            StartDate = DateTime.Now;
            DurationString = "";
            Distance = 0;
            AvgSpeed = 0;
            ElevationGain = 0;
        }

        public HikingSavedData(CoreActivityData activityData, AltitudeData altitudeData, string duration)
        {
            StartDate = activityData.StartDate;
            DurationString = duration;
            Distance = activityData.Distance;
            AvgSpeed = activityData.AvgSpeed;
            ElevationGain = altitudeData.ElevationGain;
        }
    }
}
