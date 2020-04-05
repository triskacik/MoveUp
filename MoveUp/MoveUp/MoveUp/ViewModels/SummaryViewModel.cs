using MoveUp.Commands;
using MoveUp.Factories.Interfaces;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;
using System;
using System.Windows.Input;

namespace MoveUp.ViewModels
{
    public class SummaryViewModel : ViewModelBase
    {
        private ICommandFactory commandFactory;
        public ICommand TestCommand { get; set; }

        public CoreMotionData MotionData { get; set; }

        public SummaryViewModel(ICommandFactory commandFac, ICoreMotionController motionController)
        {
            commandFactory = commandFac;
            MotionData = motionController.GetData();
        }
    }
}
