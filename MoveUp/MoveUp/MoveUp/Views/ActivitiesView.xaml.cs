using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoveUp.ViewModels;
using Xamarin.Forms;

namespace MoveUp.Views
{
    public partial class ActivitiesView
    {
        private ActivitiesViewModel vm;

        public ActivitiesView(ActivitiesViewModel viewModel)
        {
            vm = viewModel;
            vm.ActivitiesPage = this;

            BindingContext = viewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            vm.RefreshData();
            base.OnAppearing();
        }

        public async Task<bool> DisplayPopup()
        {
            return await DisplayAlert("Delete", "Do you really want to delete the item?", "Yes", "No");
        }
    }
}
