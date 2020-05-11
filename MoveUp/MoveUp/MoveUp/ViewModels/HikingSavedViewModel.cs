using System;
using MoveUp.Factories.Interfaces;
using MoveUp.Models;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;

namespace MoveUp.ViewModels
{
    public class HikingSavedViewModel : ViewModelBase
    {
        public HikingSavedData Data { get; set; } = new HikingSavedData();

        public HikingSavedViewModel(INavigationService navigationService,
                                    ICommandFactory commandFactory,
                                    IHistoryDataFeeder dataFeeder) : base(navigationService, commandFactory)
        {
            Data = dataFeeder.GetHikingData();
        }
    }
}
