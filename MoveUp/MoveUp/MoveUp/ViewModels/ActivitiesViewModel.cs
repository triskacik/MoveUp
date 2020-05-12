using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MoveUp.Factories.Interfaces;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;

namespace MoveUp.ViewModels
{
    public class ActivitiesViewModel : ViewModelBase
    {
        public Views.ActivitiesView ActivitiesPage { get; set; }

        public ObservableCollection<HikingSavedData> HikingDataCollection { get; set; } = new ObservableCollection<HikingSavedData>();

        public ICommand ViewHikingDataCom { get; set; }
        public ICommand DeleteHikingDataCom { get; set; }

        private IStorageManager storageManager;
        private IHistoryDataFeeder dataFeeder;
        private INavigationService navigation;
        private ICommandFactory commandFactory;

        public ActivitiesViewModel(ICommandFactory commandFac,
                                   INavigationService navigationService,
                                   IStorageManager storage,
                                   IHistoryDataFeeder feeder) : base(navigationService, commandFac)
        {
            storageManager = storage;
            dataFeeder = feeder;
            navigation = navigationService;
            commandFactory = commandFac;

            HikingDataCollection = new ObservableCollection<HikingSavedData>(storageManager.GetHikingDataTable().ToArray());

            ViewHikingDataCom = commandFactory.CreateCommand<HikingSavedData>(ViewHikingData);
            DeleteHikingDataCom = commandFactory.CreateCommand<HikingSavedData>(DeleteHikingData);
        }

        public void RefreshData()
        {
            HikingDataCollection = new ObservableCollection<HikingSavedData>(storageManager.GetHikingDataTable().ToArray());
        }

        private async void ViewHikingData(HikingSavedData data)
        {
            dataFeeder.SetHikingData(data);

            await navigation.PushAsync<HikingSavedViewModel>();
        }

        private async void DeleteHikingData(HikingSavedData data)
        {
            bool delete = await ActivitiesPage.DisplayPopup();

            if (delete)
            {
                storageManager.DeleteHikingData(data);
                HikingDataCollection.Remove(data);
            }
        }
    }
}
