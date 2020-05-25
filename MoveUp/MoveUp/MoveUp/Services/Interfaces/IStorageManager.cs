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

        List<SavedPosition> GetTodaysPositions();
        void CachePosition(SavedPosition data);
        void SavePositionsCache();

        void InsertNotification(NotificationData data);
        List<NotificationData> GetNotifications();
    }
}
