using MoveUp.Factories.Interfaces;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;

namespace MoveUp.ViewModels
{
    public class SummaryViewModel : ViewModelBase
    {    
        private ICoreMotionController motionController;
        private IPreferencesStorage preferencesStorage;

        public CoreMotionData MotionData { get; set; }

        public int StepsTarget { get; set; }
        public double DistanceTarget { get; set; }


        public SummaryViewModel(ICommandFactory commandFac,
                                ICoreMotionController motionContr,
                                INavigationService navigation,
                                IPreferencesStorage preferences) : base(navigation, commandFac)
        {
            motionController = motionContr;
            preferencesStorage = preferences;

            StepsTarget = preferencesStorage.GetStepsTarget();
            DistanceTarget = preferencesStorage.GetDistanceTarget();

            InitializeMotionAsync();
        }

        public void Refresh()
        {
            StepsTarget = preferencesStorage.GetStepsTarget();
            DistanceTarget = preferencesStorage.GetDistanceTarget();
        }

        private async void InitializeMotionAsync()
        {
            await motionController.TriggerPedometerAsync();
            MotionData = motionController.GetData();
        }
    }
}
