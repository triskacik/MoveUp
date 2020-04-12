using System;
using MoveUp.Factories.Interfaces;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;

namespace MoveUp.ViewModels
{
    public class StepsViewModel : ViewModelBase
    {
        private ICommandFactory commandFactory;
        private ICoreMotionController motionController;
        private INavigationService navigationService;

        public CoreMotionData MotionData { get; set; }
        public CoreMotionWeeklyData WeeklyMotionData { get; set; }

        public StepsViewModel(ICommandFactory commandFac, ICoreMotionController motionContr, INavigationService navigation)
        {
            commandFactory = commandFac;
            motionController = motionContr;
            navigationService = navigation;
            InitializeMotion();
        }

        private async void InitializeMotion()
        {
            await motionController.TriggerPedometerAsync();
            MotionData = motionController.GetData();
            WeeklyMotionData = await motionController.GetWeeklyDataAsync();
        }
    }
}
