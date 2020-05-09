using System;
namespace MoveUp.Services.Interfaces
{
    public interface IPreferencesStorage : ISingletonService
    {
        int GetMaxSteps();
        void SetMaxSteps(int value);

        int GetMaxCalories();
        void SetMaxCalories(int value);

        int GetMaxFloors();
        void SetMaxFloors(int value);

        double GetMaxDistance();
        void SetMaxDistance(double value);

        int GetStepsTarget();
        void SetStepsTarget(int value);

        double GetDistanceTarget();
        void SetDistanceTarget(double value);
    }
}
