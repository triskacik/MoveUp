using System;
using MoveUp.Factories.Interfaces;
using MoveUp.Services.Interfaces;
using MoveUp.ViewModels.Base;

namespace MoveUp.ViewModels
{
    public class FriendsViewModel : ViewModelBase
    {
        public FriendsViewModel(INavigationService navigation,
                                ICommandFactory commandFac) : base(navigation, commandFac)
        {
        }
    }
}
