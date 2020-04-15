using System;
using System.Collections.Generic;
using MoveUp.ViewModels;
using Xamarin.Forms;

namespace MoveUp.Views
{
    public partial class FloorsView
    {
        public FloorsView(FloorsViewModel floorsViewModel)
        {
            BindingContext = floorsViewModel;
            InitializeComponent();
        }
    }
}
