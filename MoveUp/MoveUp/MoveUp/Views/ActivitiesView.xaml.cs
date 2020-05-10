using System;
using System.Collections.Generic;
using MoveUp.ViewModels;
using Xamarin.Forms;

namespace MoveUp.Views
{
    public partial class ActivitiesView
    {
        public ActivitiesView(ActivitiesViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
