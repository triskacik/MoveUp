using System;
using MoveUp.Models;

namespace MoveUp.Services.Interfaces
{
    public interface ITimerService : ISingletonService
    {
        void Reinitialize();
        void StopTimer();
        void StartTimer();
        void ResumeTimer();

        TimerData GetTimerData();
    }
}
