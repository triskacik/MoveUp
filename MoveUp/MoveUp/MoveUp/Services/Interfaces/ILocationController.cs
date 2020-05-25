using System;
using System.Collections.Generic;
using MoveUp.Models;
using Xamarin.Forms.Maps;

namespace MoveUp.Services.Interfaces
{
    public interface ILocationController : ISingletonService
    {
        void StartUpdates();
        LocationPolyline GetPolyline();
        void SetDelegateMap(Map map);
        void InitializePolyline(List<Position> positions);

        LocationPolyline GetActivityPolyline();
        void StartActivityUpdates(Map map);
        void StopActivityUpdates();
    }
}
