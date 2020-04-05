using System;
namespace MoveUp.Helpers
{
    public static class CaloriesCounter
    {
        private static readonly int AVG_CAL_PER_KM = 50;

        public static int GetCalories(double distance)
        {
            return (int)(distance * AVG_CAL_PER_KM);
        }

    }
}
