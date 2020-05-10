using System;
using MoveUp.Factories.Interfaces;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;

namespace MoveUp.ViewModels
{
    public class ActivitiesViewModel : ViewModelBase
    {
        public ActivitiesViewModel(ICommandFactory commandFactory,
                                   INavigationService navigationService) : base(navigationService, commandFactory)
        {

        }
    }
}
