using System;
using MoveUp.Models;

namespace MoveUp.Services.Interfaces
{
    public interface INotificationsManager : ISingletonService
    {
        void CreateNotification(string title, double timer);
    }
}
