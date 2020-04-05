using Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoveUp.iOS.CoreMotion.Extensions
{
    public static class DateConverter
    {
        public static NSDate DateTimeToNSDate(this DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
                date = DateTime.SpecifyKind(date, DateTimeKind.Local);
            return (NSDate)date;
        }

        public static DateTime NSDateToDateTime(this NSDate date)
        {
            return ((DateTime)date).ToLocalTime();
        }
    }
}
