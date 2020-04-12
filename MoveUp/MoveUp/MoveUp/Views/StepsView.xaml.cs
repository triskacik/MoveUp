using System;
using System.Collections.Generic;
using MoveUp.ViewModels;
using Xamarin.Forms;

namespace MoveUp.Views
{
    public partial class StepsView
    {
        public StepsView(StepsViewModel stepsViewModel)
        {
            BindingContext = stepsViewModel;
            InitializeComponent();
        }
    }
}
