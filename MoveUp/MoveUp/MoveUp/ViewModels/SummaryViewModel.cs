using MoveUp.Commands;
using MoveUp.Factories.Interfaces;
using MoveUp.Models;
using MoveUp.ViewModels.Base;
using System;
using System.Windows.Input;

namespace MoveUp.ViewModels
{
    public class SummaryViewModel : ViewModelBase
    {
        public Class1 Test { get; set; } = new Class1();

        private ICommandFactory commandFactory;
        public ICommand TestCommand { get; set; }

        public SummaryViewModel(ICommandFactory commandFac)
        {
            commandFactory = commandFac;
            TestCommand = commandFactory.CreateCommand(ChangeProperty);
        }

        public void ChangeProperty()
        {
            Test.MyProperty += 10;
        }
    }
}
