﻿using System;
using System.Threading.Tasks;
using MoveUp.Models;

namespace MoveUp.Services.Interfaces
{
    public interface ICoreMotionController : ISingletonService
    {
        CoreMotionData GetData();
        Task<CoreMotionWeeklyData> GetWeeklyDataAsync();
        Task TriggerPedometerAsync();
    }
}
