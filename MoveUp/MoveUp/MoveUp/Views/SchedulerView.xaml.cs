using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoveUp.ViewModels;
using Xamarin.Forms;

namespace MoveUp.Views
{
    public partial class SchedulerView 
    {
        private SchedulerViewModel vm;

        public SchedulerView(SchedulerViewModel viewModel)
        {
            BindingContext = viewModel;
            vm = viewModel;
            vm.SchedulerView = this;

            InitializeComponent();
        }

        public async void DisplayErrorPopup()
        {
            await DisplayAlert("Schedule Activity", "The date has to be in the future", "OK");
        }

        public async void DisplayOkPopup()
        {
            await DisplayAlert("Schedule Activity", "Activity successfully scheduled", "OK");
        }
    }
}
