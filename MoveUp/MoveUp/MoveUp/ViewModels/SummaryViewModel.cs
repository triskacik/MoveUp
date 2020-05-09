using MoveUp.Factories.Interfaces;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;

namespace MoveUp.ViewModels
{
    public class SummaryViewModel : ViewModelBase
    {
        
        private ICoreMotionController motionController;

        public CoreMotionData MotionData { get; set; }


        public SummaryViewModel(ICommandFactory commandFac, ICoreMotionController motionContr, INavigationService navigation) : base(navigation, commandFac)
        {
            motionController = motionContr;

            InitializeMotionAsync();
        }

        private async void InitializeMotionAsync()
        {
            await motionController.TriggerPedometerAsync();
            MotionData = motionController.GetData();
        }
    }
}
