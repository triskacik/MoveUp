using System;
using System.Collections.Generic;
using MoveUp.ViewModels;
using Xamarin.Forms;

namespace MoveUp.Views
{
    public partial class HikingSavedView
    {
        public HikingSavedView(HikingSavedViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
