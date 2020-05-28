using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoveUp.ViewModels;
using Xamarin.Forms;

namespace MoveUp.Views
{
    public partial class HikingView
    {
        private HikingViewModel vm;

        public HikingView(HikingViewModel viewModel)
        {
            vm = viewModel;
            BindingContext = viewModel;
            vm.HikingPage = this;
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.StopActivity();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.OnAppearing();
        }

        public async Task<bool> DisplayPopup()
        {
            return await DisplayAlert("Finish", "Do you really want to finish the activity?", "Yes", "No");
        }
    }
}
