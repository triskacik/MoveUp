using System;
using System.Timers;
using System.Windows.Input;
using MoveUp.Factories.Interfaces;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;
using Xamarin.Forms;

namespace MoveUp.ViewModels
{
    public class HikingViewModel : ViewModelBase
    {
        public Views.HikingView HikingPage { get; set; }

        public CoreActivityData ActivityData { get; set; } = new CoreActivityData();
        public AltitudeData AltitudeData { get; set; } = new AltitudeData();

        public ICommand StartActivityCom { get; set; }
        public ICommand StopActivityCom { get; set; }
        public ICommand PopUpCom { get; set; }

        public bool IsRunning { get; set; } = false;
        public bool IsStopped { get; set; } = true;

        public Color HeaderColor { get; set; } = Color.Red;
        public double Opacity { get; set; } = 0.2;
        public double InvertedOpacity { get; set; } = 1;

        public TimerData TimerData { get; set; } = new TimerData();
        
        private ICoreMotionController motionController;
        private IAltitudeController altitudeController;
        private ITimerService timer;
        private IStorageManager storage;
        private INavigationService navigation;
        private IHistoryDataFeeder dataFeeder;

        public HikingViewModel(ICommandFactory commandFactory,
                               INavigationService navigationService,
                               ICoreMotionController coreMotionController,
                               IAltitudeController altitudeContr,
                               ITimerService timerService,
                               IStorageManager storageManager,
                               IHistoryDataFeeder feeder) : base(navigationService, commandFactory)
        {
            motionController = coreMotionController;
            altitudeController = altitudeContr;
            timer = timerService;
            storage = storageManager;
            navigation = navigationService;
            dataFeeder = feeder;

            TimerData = timer.GetTimerData();

            StartActivityCom = commandFactory.CreateCommand(StartActivity);
            StopActivityCom = commandFactory.CreateCommand(StopActivity);
            PopUpCom = commandFactory.CreateCommand(FinishActivity);
        }

        private void StartActivity()
        {
            motionController.TriggerActivity();
            ActivityData = motionController.GetActivityData();
            timer.StartTimer();

            altitudeController.TriggerActivity();
            AltitudeData = altitudeController.GetData();

            HeaderColor = Color.Green;
            Opacity = 1;
            InvertedOpacity = 0.2;

            IsRunning = true;
            IsStopped = false;
        }

        public void StopActivity()
        {
            motionController.StopActivityUpdates();
            timer.StopTimer();
            timer.Reinitialize();
            altitudeController.StopActivityUpdates();

            ActivityData = new CoreActivityData();
            AltitudeData = new AltitudeData();

            HeaderColor = Color.Red;
            Opacity = 0.2;
            InvertedOpacity = 1;

            IsRunning = false;
            IsStopped = true;
        }

        public async void FinishActivity()
        {
            bool finish = await HikingPage.DisplayPopup();

            if (finish)
            {
                HikingSavedData hikingData = new HikingSavedData(ActivityData, AltitudeData, TimerData.DurationString);
                StopActivity();
                storage.InsertHikingData(hikingData);

                dataFeeder.SetHikingData(hikingData);

                await navigation.PushAsync<HikingSavedViewModel>();
            }
        }
    }
}
