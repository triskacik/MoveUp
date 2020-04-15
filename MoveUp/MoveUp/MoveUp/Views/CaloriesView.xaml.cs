﻿using System;
using System.Collections.Generic;
using MoveUp.ViewModels;
using Xamarin.Forms;

namespace MoveUp.Views
{
    public partial class CaloriesView 
    {
        public CaloriesView(CaloriesViewModel caloriesViewModel)
        {
            BindingContext = caloriesViewModel;
            InitializeComponent();
        }
    }
}
