using System;
using System.Windows.Input;
using MoveUp.Commands;
using MoveUp.Factories.Interfaces;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;

namespace MoveUp.ViewModels
{
    public class SummaryViewModel : ViewModelBase
    {
        private ICommandFactory commandFactory;
        private ICoreMotionController motionController;
        private INavigationService navigationService;

        public CoreMotionData MotionData { get; set; }

        public ICommand ToStepsViewCom { get; set; }

        public SummaryViewModel(ICommandFactory commandFac, ICoreMotionController motionContr, INavigationService navigation)
        {
            commandFactory = commandFac;
            motionController = motionContr;
            navigationService = navigation;

            ToStepsViewCom = commandFactory.CreateCommand(ToStepsView);
            InitializeMotion();
        }

        private async void InitializeMotion()
        {
            await motionController.TriggerPedometerAsync();
            MotionData = motionController.GetData();
        }

        private void ToStepsView()
        {
            navigationService.PushAsync<StepsViewModel>();  
        }
    }
}
