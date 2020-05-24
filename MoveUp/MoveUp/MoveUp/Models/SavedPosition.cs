using System;
using SQLite;
using Xamarin.Forms.Maps;

namespace MoveUp.Models
{
    [Table("todays_positions")]
    public class SavedPosition
    {
        [PrimaryKey]
        public DateTime Date { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public SavedPosition()
        {
            Date = DateTime.Now;
        }

        public SavedPosition(Position position)
        {
            Date = DateTime.Now;
            Latitude = position.Latitude;
            Longitude = position.Longitude;
        }
    }
}
