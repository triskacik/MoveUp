using System;
using SQLite;

namespace MoveUp.Models
{
    [Table("notifications")]
    public class NotificationData
    {
        [PrimaryKey]
        public DateTime Date { get; set; } = DateTime.Now;
        public string Type { get; set; } = "";

        public NotificationData()
        {
        }

        public NotificationData(DateTime date, string type)
        {
            Date = date;
            Type = type;
        }
    }
}
