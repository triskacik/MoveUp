﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using MoveUp.Services.Interfaces;
using System.Windows.Input;
using MoveUp.Factories.Interfaces;
using Xamarin.Forms;

namespace MoveUp.ViewModels.Base
{
    public class ViewModelBase : INotifyPropertyChanged, IViewModel
    {
        private INavigationService navigationService;
        private ICommandFactory commandFactory;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ToSummaryViewCom { get; set; }
        public ICommand ToStepsViewCom { get; set; }
        public ICommand ToDistanceViewCom { get; set; }
        public ICommand ToFloorsViewCom { get; set; }
        public ICommand ToCaloriesViewCom { get; set; }

        public ViewModelBase(INavigationService navigation, ICommandFactory commandFac)
        {
            navigationService = navigation;
            commandFactory = commandFac;

            ToSummaryViewCom = commandFactory.CreateCommand(ToSummaryView);
            ToStepsViewCom = commandFactory.CreateCommand(ToStepsView);
            ToDistanceViewCom = commandFactory.CreateCommand(ToDistanceView);
            ToFloorsViewCom = commandFactory.CreateCommand(ToFloorsView);
            ToCaloriesViewCom = commandFactory.CreateCommand(ToCaloriesView);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ToSummaryView()
        {
            navigationService.PushAsync<SummaryViewModel>();
        }

        private void ToStepsView()
        {
            navigationService.PushAsync<StepsViewModel>();
        }

        private void ToDistanceView()
        {
            navigationService.PushAsync<DistanceViewModel>();
        }

        private void ToFloorsView()
        {
            navigationService.PushAsync<FloorsViewModel>();
        }

        private void ToCaloriesView()
        {
            navigationService.PushAsync<CaloriesViewModel>();
        }
    }
}

