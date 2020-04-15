using System;
using System.Collections.Generic;
using MoveUp.ViewModels;
using Xamarin.Forms;

namespace MoveUp.Views
{
    public partial class DistanceView 
    {
        public DistanceView(DistanceViewModel distanceViewModel)
        {
            BindingContext = distanceViewModel;
            InitializeComponent();
        }
    }
}
