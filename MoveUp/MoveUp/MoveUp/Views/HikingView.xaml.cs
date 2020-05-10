using System;
using System.Collections.Generic;
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
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.StopActivity();
        }
    }
}
