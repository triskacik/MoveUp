using MoveUp.Commands;
using MoveUp.Models;
using System;
using System.Windows.Input;

namespace MoveUp.ViewModels
{
    public class SummaryViewModel
    {
        public Class1 Test { get; set; } = new Class1();

        public ICommand TestCommand { get; set; }
        public SummaryViewModel()
        {
            TestCommand = new Command(ChangeProperty, () => true);
        }

        public void ChangeProperty()
        {
            Test.MyProperty += 10;
        }
    }
}
