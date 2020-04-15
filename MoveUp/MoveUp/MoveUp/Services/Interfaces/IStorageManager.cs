using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoveUp.Models;

namespace MoveUp.Services.Interfaces
{
    public interface IStorageManager : ISingletonService
    {
        Task<List<CoreMotionMonthlyData>> GetMonthlyDataAsync();
        Task<CoreMotionData> GetAllTimeAverageDataAsync();
    }
}
