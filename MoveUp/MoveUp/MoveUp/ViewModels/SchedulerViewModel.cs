using System;
using System.Collections.Generic;
using System.Windows.Input;
using MoveUp.Factories.Interfaces;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;
using MoveUp.Views;

namespace MoveUp.ViewModels
{
    public class SchedulerViewModel : ViewModelBase
    {
        public List<string> Activities { get; set; } = ActivityTypes.GetAll();
        public int SelectedActivity { get; set; } = 0;

        public DateTime Date { get; set; } = DateTime.Today;
        public TimeSpan Time { get; set; } = new TimeSpan();

        public bool Notification { get; set; } = true;

        public ICommand CreateNotificationCom { get; set; }

        private ICommandFactory commandFactory;
        private INotificationsManager notificationsManager;
        private IStorageManager storageManager;

        public SchedulerView SchedulerView { get; set; }

        public SchedulerViewModel(INavigationService navigationService,
                                  ICommandFactory commandFac,
                                  INotificationsManager notifications,
                                  IStorageManager storage) : base(navigationService, commandFac)
        {
            commandFactory = commandFac;
            notificationsManager = notifications;
            storageManager = storage;

            CreateNotificationCom = commandFactory.CreateCommand(CreateNotification);
        }

        private void CreateNotification()
        {
            var scheduledDate = Date.Add(Time);
            if (scheduledDate < DateTime.Now)
            {
                SchedulerView.DisplayErrorPopup();
                return;
            }

            if (Notification)
            {
                double secondsSinceNow = (scheduledDate - DateTime.Now).TotalSeconds;
                notificationsManager.CreateNotification(((ActivitiesEnum)SelectedActivity).ToString(), secondsSinceNow);
            }
            
            storageManager.InsertNotification(new NotificationData(scheduledDate, ((ActivitiesEnum)SelectedActivity).ToString()));
            SchedulerView.DisplayOkPopup();
        }
    }
}
