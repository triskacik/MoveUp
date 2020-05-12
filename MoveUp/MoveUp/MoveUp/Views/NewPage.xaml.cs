using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MoveUp.Models;
using MoveUp.ViewModels;
using Xamarin.Forms;

namespace MoveUp.Views
{
    public partial class NewPage : ContentPage
    {
        public ObservableCollection<HikingSavedData> OtherCollection { get; set; } = new ObservableCollection<HikingSavedData>()
        {
            new HikingSavedData(),
            new HikingSavedData()
        };

        public NewPage(NewPageViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
