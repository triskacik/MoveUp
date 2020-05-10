using System;
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
        public CoreActivityData ActivityData { get; set; } = new CoreActivityData();
        public AltitudeData AltitudeData { get; set; } = new AltitudeData();

        public ICommand StartActivityCom { get; set; }
        public ICommand StopActivityCom { get; set; }

        public Color HeaderColor { get; set; } = Color.Red;
        public double Opacity { get; set; } = 0.2;
        public double InvertedOpacity { get; set; } = 1;

        public bool IsStopped { get; set; } = true;

        private ICoreMotionController motionController;
        private IAltitudeController altitudeController;

        public HikingViewModel(ICommandFactory commandFactory,
                               INavigationService navigationService,
                               ICoreMotionController coreMotionController,
                               IAltitudeController altitudeContr) : base(navigationService, commandFactory)
        {
            motionController = coreMotionController;
            altitudeController = altitudeContr;

            StartActivityCom = commandFactory.CreateCommand(StartActivity);
            StopActivityCom = commandFactory.CreateCommand(StopActivity);
        }

        private void StartActivity()
        {
            motionController.TriggerActivity();
            ActivityData = motionController.GetActivityData();
            ActivityData.Start();

            altitudeController.TriggerActivity();
            AltitudeData = altitudeController.GetData();

            HeaderColor = Color.Green;
            Opacity = 1;
            InvertedOpacity = 0.2;

            IsStopped = false;
        }

        public void StopActivity()
        {
            motionController.StopActivityUpdates();
            ActivityData.Stop();

            HeaderColor = Color.Red;
            Opacity = 0.2;
            InvertedOpacity = 1;

            IsStopped = true;
        }
    }
}
