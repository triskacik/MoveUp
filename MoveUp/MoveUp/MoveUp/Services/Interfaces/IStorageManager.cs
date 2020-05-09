using MoveUp.Models;
using SQLite;

namespace MoveUp.Services.Interfaces
{
    public interface IStorageManager : ISingletonService
    {
        TableQuery<CoreMotionData> GetCoreMotionTable();
        void InsertCoreMotionData(CoreMotionData data);
    }
}
