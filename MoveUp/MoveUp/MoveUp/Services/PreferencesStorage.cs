using System;
using MoveUp.Services.Interfaces;
using Xamarin.Essentials;

namespace MoveUp.Services
{
    public class PreferencesStorage : IPreferencesStorage
    {
        private static readonly string STEPS_MAX_KEY = "StepsMax";
        private static readonly string CALORIES_MAX_KEY = "CaloriesMax";
        private static readonly string DISTANCE_MAX_KEY = "DistanceMax";
        private static readonly string FLOORS_MAX_KEY = "FloorsMax";

        private static readonly string STEPS_TARGET_KEY = "StepsTarget";
        private static readonly string DISTANCE_TARGET_KEY = "DistanceTarget";

        public PreferencesStorage()
        {
        }

        private int GetInt(string key, int initial)
        {
            if (Preferences.ContainsKey(key))
            {
                return Preferences.Get(key, initial);
            }
            else
            {
                Preferences.Set(key, initial);
                return initial;
            }
        }

        private double GetDouble(string key, double initial)
        {
            if (Preferences.ContainsKey(key))
            {
                return Preferences.Get(key, initial);
            }
            else
            {
                Preferences.Set(key, initial);
                return initial;
            }
        }

        public int GetMaxSteps()
        {
            return GetInt(STEPS_MAX_KEY, 0);
        }

        public void SetMaxSteps(int value)
        {
            Preferences.Set(STEPS_MAX_KEY, value);
        }

        public int GetMaxCalories()
        {
            return GetInt(CALORIES_MAX_KEY, 0);
        }

        public void SetMaxCalories(int value)
        {
            Preferences.Set(CALORIES_MAX_KEY, value);
        }

        public int GetMaxFloors()
        {
            return GetInt(FLOORS_MAX_KEY, 0);
        }

        public void SetMaxFloors(int value)
        {
            Preferences.Set(FLOORS_MAX_KEY, value);
        }

        public double GetMaxDistance()
        {
            return GetDouble(DISTANCE_MAX_KEY, 0);
        }

        public void SetMaxDistance(double value)
        {
            Preferences.Set(DISTANCE_MAX_KEY, value);
        }

        public int GetStepsTarget()
        {
            return GetInt(STEPS_TARGET_KEY, 10000);
        }

        public void SetStepsTarget(int value)
        {
            Preferences.Set(STEPS_TARGET_KEY, value);
        }

        public double GetDistanceTarget()
        {
            return GetDouble(DISTANCE_TARGET_KEY, 10);
        }

        public void SetDistanceTarget(double value)
        {
            Preferences.Set(DISTANCE_TARGET_KEY, value);
        }
    }
}
