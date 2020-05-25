using System;
using Foundation;
using MoveUp.Services.Interfaces;
using MoveUp.Models;
using UIKit;

namespace MoveUp.iOS.Notifications
{
    public class NotificationsManager : INotificationsManager
    {
        public NotificationsManager()
        {
        }

        public void CreateNotification(string title, double timer)
        {
            UILocalNotification notification = new UILocalNotification();
            notification.FireDate = NSDate.FromTimeIntervalSinceNow(timer);
            notification.AlertAction = title;
            notification.AlertBody = string.Format("Don't be lazy and start the scheduled {0} activity", title);
            notification.SoundName = UILocalNotification.DefaultSoundName;
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }
    }
}
