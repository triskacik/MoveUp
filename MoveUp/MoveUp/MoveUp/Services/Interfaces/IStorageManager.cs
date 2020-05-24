using System.Collections.Generic;
using MoveUp.Models;
using SQLite;

namespace MoveUp.Services.Interfaces
{
    public interface IStorageManager : ISingletonService
    {
        TableQuery<CoreMotionData> GetCoreMotionTable();
        void InsertCoreMotionData(CoreMotionData data);

        TableQuery<HikingSavedData> GetHikingDataTable();
        void InsertHikingData(HikingSavedData data);
        void DeleteHikingData(HikingSavedData data);

        void InsertTodaysPositions(List<SavedPosition> data);
        List<SavedPosition> GetTodaysPositions();
    }
}
