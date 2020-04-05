using System;
using MoveUp.Models;

namespace MoveUp.Services.Interfaces
{
    public interface ICoreMotionController : ISingletonService
    {
        CoreMotionData GetData();
    }
}
