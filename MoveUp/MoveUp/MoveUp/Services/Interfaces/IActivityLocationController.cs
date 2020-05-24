using System;
using MoveUp.Models;
using Xamarin.Forms.Maps;

namespace MoveUp.Services.Interfaces
{
    public interface IActivityLocationController : ISingletonService
    {
        void StartUpdates();
        LocationPolyline GetPolyline();
        void SetDelegateMap(Map map);
    }
}
