using System;
using MoveUp.Models;

namespace MoveUp.Services.Interfaces
{
    public interface IHistoryDataFeeder : ISingletonService
    {
        HikingSavedData GetHikingData();
        void SetHikingData(HikingSavedData data);
    }
}
