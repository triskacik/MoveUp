using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms.Maps;
using Newtonsoft.Json;

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

        public string positionsSerialized { get; set; } = "";

        [Ignore]
        public SavedPosition[] Positions
        {
            get
            {
                return JsonConvert.DeserializeObject<SavedPosition[]>(positionsSerialized);
            }
            set
            {
                positionsSerialized = JsonConvert.SerializeObject(value);
            }
        }

        public HikingSavedData()
        {
            StartDate = DateTime.Now;
            DurationString = "Duration";
            Distance = 0;
            AvgSpeed = 0;
            ElevationGain = 0;
        }

        public HikingSavedData(CoreActivityData activityData, AltitudeData altitudeData, string duration, List<Position> positions)
        {
            StartDate = activityData.StartDate;
            DurationString = duration;
            Distance = activityData.Distance;
            AvgSpeed = activityData.AvgSpeed;
            ElevationGain = altitudeData.ElevationGain;
            Positions = positions.Select(position => new SavedPosition(position)).ToArray();
        }
    }
}
