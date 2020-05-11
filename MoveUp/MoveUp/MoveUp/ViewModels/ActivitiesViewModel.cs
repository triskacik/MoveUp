using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MoveUp.Factories.Interfaces;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;

namespace MoveUp.ViewModels
{
    public class ActivitiesViewModel : ViewModelBase
    {
        public ObservableCollection<HikingSavedData> HikingDataCollection = new ObservableCollection<HikingSavedData>();

        private IStorageManager storageManager;
        private IHistoryDataFeeder dataFeeder;
        private INavigationService navigation;

        public ActivitiesViewModel(ICommandFactory commandFactory,
                                   INavigationService navigationService,
                                   IStorageManager storage,
                                   IHistoryDataFeeder feeder) : base(navigationService, commandFactory)
        {
            storageManager = storage;
            dataFeeder = feeder;
            navigation = navigationService;

            IEnumerable<HikingSavedData> data = storageManager.GetHikingDataTable().ToList();

            foreach (HikingSavedData d in data)
            {
                Console.WriteLine(d.StartDate);
                HikingDataCollection.Add(d);
            }
        }
    }
}
