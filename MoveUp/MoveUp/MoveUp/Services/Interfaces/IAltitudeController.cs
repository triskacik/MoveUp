using System;
using MoveUp.Models;

namespace MoveUp.Services.Interfaces
{
    public interface IAltitudeController : ISingletonService
    {
        AltitudeData GetData();

        void TriggerActivity();
        void StopActivityUpdates();
    }
}
